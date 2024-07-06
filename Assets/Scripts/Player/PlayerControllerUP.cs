using UnityEngine;

namespace Player
{
    public class PlayerControllerUp : MonoBehaviour
    {
        public static PlayerControllerUp Instance { get; private set; }

        
        [SerializeField] private Vector2 groundCheckSize;
        [SerializeField] private Transform groundCheck;
        [SerializeField] private LayerMask whatIsGround;

        private bool _isGrounded;
        private int _jumpCount; // Track the number of jumps made
        private const int MaxJump = 2; // Maximum number of jumps (1 for initial jump and 1 for double jump)
        private float _jumpCooldown = 0.2f; // Cooldown period in seconds
        private float _lastJumpTime;

        private bool _cooldownElapsed;
        private Animator _parentAnimator;
        private static readonly int Jumping = Animator.StringToHash("jumping");

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
            
            _parentAnimator = GetComponentInParent<Animator>();
            ResetJump();
        }

        private void FixedUpdate()
        {
            CheckGroundStatus();
        }

        private void CheckGroundStatus()
        {
            _isGrounded = Physics2D.OverlapBox(groundCheck.position, groundCheckSize, 0f, whatIsGround);
            if(Time.time < _lastJumpTime) // Time is in the past, reset last jump time
                _lastJumpTime = Time.time;
            
            _cooldownElapsed = (Time.time - _lastJumpTime) >= _jumpCooldown;

            // Debug all variables in one line, incuding the cooldown calculus
            //Debug.Log($"Is Grounded: {_isGrounded}, Cooldown Elapsed: {cooldownElapsed}, _Last Jump Time: {_lastJumpTime}, Time: {Time.time}, Cooldown: {_jumpCooldown}, GetBool: {_parentAnimator.GetBool(Jumping)}");
            if (_isGrounded && _cooldownElapsed && _parentAnimator.GetBool(Jumping))
            {
                ResetJump();
            }
            else if (!_isGrounded)
            {
                _parentAnimator.SetBool(Jumping, true);
            }
        }

        public bool AttemptJump()
        {
            if (_jumpCount < MaxJump)
            {
                _jumpCount++;
                _lastJumpTime = Time.time;
                _parentAnimator.SetBool(Jumping, true);
                return true;
            }
            return false;
        }

        private void ResetJump()
        {
            _jumpCount = 0;
            _parentAnimator.SetBool(Jumping, false);
        }

        public bool CanJump()
        {
            return _jumpCount < MaxJump;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(groundCheck.position, groundCheckSize);
        }
    }
}