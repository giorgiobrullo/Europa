using Player;
using UnityEngine;
using UnityEngine.Serialization;

namespace Enemies.Generic
{
    public class GenericEnemy : MonoBehaviour
    {

        [SerializeField] private float gravityScale = 1.0f;

        [FormerlySerializedAs("hitSound")] public GameObject hurtSound;
        public GameObject idleSound; 
        
        public Animator Animator { get; private set; }
        public Rigidbody2D Rigidbody { get; private set; }
        public GameObject Target { get; private set; }
        public bool IsFacingRight { get; set; }
        public bool isDead { get; set; }

        private BoxCollider2D _collider2D;
        private Vector2 _velocity;
        private EnemyAttack _enemyAttack;
        private EnemyMovement _enemyMovement;
        private EnemyHealth _enemyHealth;
        private GameObject _idleSound;
        private PlayerController _playerController;
        private static readonly int IsMoving = Animator.StringToHash("isMoving");

        protected void Start()
        {
            _enemyHealth = GetComponent<EnemyHealth>();
            _enemyMovement = GetComponent<EnemyMovement>();
            _enemyAttack = GetComponent<EnemyAttack>();
            Target = GameObject.FindWithTag("Player");
            _playerController = PlayerController.Instance;
            Rigidbody = GetComponent<Rigidbody2D>();
            Rigidbody.isKinematic = true;
            _velocity = Vector2.zero;
            _collider2D = GetComponent<BoxCollider2D>();
            Animator = GetComponentInChildren<Animator>();
            
            if (idleSound) _idleSound = Instantiate(idleSound, transform.position, Quaternion.identity, transform); 
        }
        

        protected virtual void FixedUpdate()
        {
            ApplyGravity();
            if(!isDead) HandleMovement();
        }

        private void ApplyGravity()
        {
            if (!IsGrounded())
            {
                _velocity.y += Physics2D.gravity.y * gravityScale * Time.fixedDeltaTime;
                float newPositionY = transform.position.y + _velocity.y * Time.fixedDeltaTime;
                RaycastHit2D hit = Physics2D.Raycast(Rigidbody.position, Vector2.down,
                    Mathf.Abs(_velocity.y * Time.fixedDeltaTime), LayerMask.GetMask("Ground"));

                if (hit.collider != null)
                {
                    newPositionY = hit.point.y + _collider2D.bounds.extents.y;
                    _velocity.y = 0;
                }

                transform.position += new Vector3(0, newPositionY - transform.position.y, 0);
            }
            else
            {
                _velocity.y = 0;
            }
        }

        private bool IsGrounded()
        {
            float extraHeightTest = 0.1f;
            Vector2 boxCenter = (Vector2)_collider2D.bounds.center +
                                Vector2.down * (extraHeightTest + _collider2D.bounds.extents.y);
            Vector2 boxSize = new Vector2(_collider2D.bounds.size.x, extraHeightTest);
            Collider2D hit = Physics2D.OverlapBox(boxCenter, boxSize, 0f, LayerMask.GetMask("Ground"));
            return hit != null;
        }

        private void HandleMovement()
        {
            if (_playerController.IsDead()) return;

            float horizontalDistance = Mathf.Abs(Target.transform.position.x - transform.position.x);
            float verticalDistance = Mathf.Abs(Target.transform.position.y - transform.position.y);
            float verticalThreshold = _enemyAttack.hitRange * 0.5f; // Half the height of the attack box
            float horizontalThreshold = _enemyAttack.hitRange * 0.5f * 1.45f; // Half the width of the attack box
            
            // Log all info in one line only if distance is less than the follow range
            //if (horizontalDistance <= _enemyMovement.followRange)
            //    Debug.Log($"Horizontal Distance: {horizontalDistance}, Vertical Distance: {verticalDistance}, Horizontal Threshold: {horizontalThreshold}, Vertical Threshold: {verticalThreshold}");
            

            if (horizontalDistance <= horizontalThreshold && verticalDistance <= verticalThreshold)
            {
                _idleSound.SetActive(false);
                _enemyAttack.TryAttack();
            }
            else if (horizontalDistance <= _enemyMovement.followRange && horizontalDistance > horizontalThreshold)
            {
                _idleSound.SetActive(false);
                _enemyMovement.FollowPlayer();
            }
            else
            {
                _idleSound.SetActive(true);
                Animator.SetBool(IsMoving, false);
            }
        }



        public void Hit(int damage)
        {
            if (hurtSound && !isDead) Instantiate(hurtSound, transform.position, Quaternion.identity);
            _enemyHealth.TakeDamage(damage);
        }

        protected virtual void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, GetComponent<EnemyMovement>().followRange);
        }
    }
}
