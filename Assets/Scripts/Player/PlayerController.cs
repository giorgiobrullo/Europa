using Other;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        public static PlayerController Instance { get; private set; }
        
        [SerializeField] private float moveSpeed;
        [SerializeField] private float jumpHeight;

        [SerializeField] private GameObject attack;
        [SerializeField] private GameObject row;
        [SerializeField] private GameObject menu;
        [SerializeField] private GameObject stats;
        [SerializeField] private GameObject mobileController;
        [SerializeField] private int damageFromPatrols = 25;
        [SerializeField] private ParticleSystem dust;
        [SerializeField] private GameObject jumpSound;

        private bool _canJump;
        private bool _canDoubleJump;
        private bool _wasMovingLeft;
        private bool _isDead;
        private bool _isLoading;

        public bool IsLoading => _isLoading;

        private const float AttackRate = 0.6f;
        private float _nextAttackTime = 0.5f;
        
        private Rigidbody2D _rb;
        private Animator _animator;
        private PlayerControllerUp _playerControllerUp;
        private static readonly int Moving = Animator.StringToHash("moving");

        private float _horizontalInput; // For input caching
        private const float JumpInputBufferTime = 0.1f; // 100 ms buffer window
        private float _lastJumpInputTime = -1f;

        private Vector2 _externalForce = Vector2.zero; // Field for storing external force

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
            _rb = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            _externalForce = Vector2.zero;
            _animator.SetBool(Moving, false);
            
            // if scene is Game_1, start split
            string currentScene = SceneManager.GetActiveScene().name;
            if(LiveSplitIntegration.Instance != null)
                if (currentScene == "Game_1")
                    LiveSplitIntegration.Instance.SendStartGameEvent();
                else LiveSplitIntegration.Instance.SendSceneChangeEvent();
        }

        private void Start()
        {
            _playerControllerUp = PlayerControllerUp.Instance;
            if(PlayerPrefs.GetInt("LoadFromAutoSave", 0) == 1)
            {
                ES3AutoSaveMgr.Current.Load();
                PlayerPrefs.SetInt("LoadFromAutoSave", 0);
                
                Time.timeScale = 1f;
            }
            else
            {
                string currentSceneName = SceneManager.GetActiveScene().name;
                PlayerPrefs.SetString("LastSceneName", currentSceneName);
            
                ES3AutoSaveMgr.Current.Save(); 
            }
        }

        void Update()
        {
            // Cache input in Update to ensure all inputs are registered.
            if(!_isDead && !_isLoading && !mobileController.activeSelf)
            {                    
                int controls = PlayerPrefs.GetInt("InputSystem", 0);
                float smoothedInput;
                if(controls == 0)
                     smoothedInput = Input.GetAxis("wasdH");
                else
                    smoothedInput = Input.GetAxis("arrowsH");

                _horizontalInput = smoothedInput;
                
                if(Input.GetKeyDown(KeyCode.Space))
                {
                    _lastJumpInputTime = Time.time;
                }
            }
        }

        private void FixedUpdate()
        {
            // Perform physics-based movement and jumping in FixedUpdate
            if(!_isDead && !_isLoading && !mobileController.activeSelf)
            {
                MovementInput();
                JumpInput();
            }
        }   
        
        public bool IsDead()
        {
            return _isDead;
        }
        
        public void Respawn()
        {
            transform.position = GameObject.FindWithTag("Respawn").transform.position;
        }
        
        private void JumpInput()
        {
            if (Time.time - _lastJumpInputTime <= JumpInputBufferTime)
            {
                _canJump = _playerControllerUp.AttemptJump();
                // Consider this as a valid jump input within the buffer window
                if (_canJump)
                {
                    PerformJump();
                    _lastJumpInputTime = -1f; // Reset
                }
            }
        }

        private void MovementInput()
        {
            // Process horizontal movement based on cached input
            if (_horizontalInput != 0)
            {
                Move(_horizontalInput * moveSpeed);
            }
            else
            {
                _animator.SetBool(Moving, false);
                _rb.linearVelocity = new Vector2(_externalForce.x, _rb.linearVelocity.y);
            }
        }

        private void PerformJump()
        {
            CreateDust();
            Instantiate(jumpSound, transform.position, Quaternion.identity, transform);
            _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, 0); 
            _rb.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
        }

        private void Move(float speed)
        {
            // Add external forces to the player movement
            _rb.linearVelocity = new Vector2(speed + _externalForce.x, _rb.linearVelocity.y);
            _animator.SetBool(Moving, true);
    
            // Determine if character is facing left or right based on speed.
            bool isMovingLeft = speed < 0;
            GetComponent<SpriteRenderer>().flipX = isMovingLeft;

            // Previously flipped state stored to detect direction change.
            // Assuming you have a boolean member variable named _wasMovingLeft initialized outside this method.
            if (_wasMovingLeft != isMovingLeft)
            {
                Rotate(row);
                Rotate(attack);
                // Update the previously flipped state.
                _wasMovingLeft = isMovingLeft;
            }
        }

        private void Rotate(GameObject gobj)
        {
            Debug.Log("Rotating row");
            gobj.transform.localPosition = new Vector3(-gobj.transform.localPosition.x, gobj.transform.localPosition.y, gobj.transform.localPosition.z);
        }

        private void TakeDamageFromPatrol()
        {
            if (Time.time >= _nextAttackTime)
            {
                Stats.Instance.TakeDamage(damageFromPatrols);
                _nextAttackTime = Time.time + AttackRate;
                _rb.AddForce(new Vector2(5, 5) * 2, ForceMode2D.Impulse);
            }
        }

        void CreateDust()
        {
            dust.Play();
        }

        public void SetDead(bool dead)
        {
            _isDead = dead;
            _rb.linearVelocity = new Vector2(0, _rb.linearVelocity.y);
            _animator.SetBool(Moving, false);
        }
        
        public void SetLoading(bool loading)
        {
            _isLoading = loading;
        }

        // New method to get the facing direction
        public Vector3 GetFacingDirection()
        {
            // Determine the direction based on the last known movement direction
            if (_horizontalInput < 0)
            {
                return Vector3.left;
            }
            else if (_horizontalInput > 0)
            {
                return Vector3.right;
            }
            else
            {
                // Return the current direction the sprite is facing
                return _wasMovingLeft ? Vector3.left : Vector3.right;
            }
        }

        // Method to apply external force
        public void ApplyExternalForce(Vector2 force)
        {
            _externalForce = force;
        }

        // Method to reset external force
        public void ResetExternalForce()
        {
            _externalForce = Vector2.zero;
        }
    }
}
