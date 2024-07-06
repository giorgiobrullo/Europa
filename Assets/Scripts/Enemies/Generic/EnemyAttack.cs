using Player;
using UnityEngine;

namespace Enemies.Generic
{
    public class EnemyAttack : MonoBehaviour
    {
        [SerializeField] protected internal float hitRange = 1f;
        [SerializeField] private float attackCooldown = 1f;
        [SerializeField] private int attackDamage = 20;
        [SerializeField] private GameObject attackSound;
        private float _lastAttackTime;

        private GenericEnemy _enemy;
        private static readonly int IsMoving = Animator.StringToHash("isMoving");
        private static readonly int IsAttacking = Animator.StringToHash("isAttacking");

        private void Start()
        {
            _enemy = GetComponent<GenericEnemy>();
            _lastAttackTime = 0;
        }

        public void TryAttack()
        {
            _enemy.Animator.SetBool(IsMoving, false);
            _enemy.Animator.SetTrigger(IsAttacking);
            if (Time.time >= _lastAttackTime + attackCooldown)
            {
                _lastAttackTime = Time.time;
                if (attackSound) Instantiate(attackSound, transform.position, Quaternion.identity);
                Attack();
            }
        }

        private void Attack()
        {
            // Define the size of the box (hit area)
            Vector2 boxSize = new Vector2(hitRange, hitRange * 0.5f); // Make the vertical range smaller

            GetComponent<EnemyMovement>().CheckAndFlipDirection();
            
            // Check if the player is within the hit range
            Collider2D[] results = new Collider2D[1];
            Physics2D.OverlapBoxNonAlloc(transform.position, boxSize, 0, results, LayerMask.GetMask("Player"));

            if (results[0] != null && results[0].gameObject.CompareTag("Player"))
            {
                // Apply damage to the player
                Stats.Instance.TakeDamage(attackDamage);
            }
        }

        // Optionally, visualize the attack range in the editor
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Vector2 boxSize = new Vector2(hitRange, hitRange * 0.5f); // Make the vertical range smaller
            Gizmos.DrawWireCube(transform.position, boxSize);
        }
    }
}
