using Enemies.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerCombat : MonoBehaviour
    {
        public static PlayerCombat Instance { get; private set; }

        
        private PlayerController _playerController;
        private Animator _animator;
        public Transform attackPoint;
        public LayerMask enemyLayers;
        public GameObject attackSoundEffect;

        public float attackCoolDown = 0.35f;
        public float attackRange = 1f;
        private float _nextAttackTime;
        private static int _attack1;

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
            
            _playerController = PlayerController.Instance;
            _animator = GetComponent<Animator>();
            _attack1 = Animator.StringToHash("attack");
        }

        void Update() // Changed from FixedUpdate to Update
        {
            if (Time.timeScale == 0f || _playerController.IsDead() || _playerController.IsLoading) return; // Don't do anything if game is paused or char is dead
            UpdateAttackPoint();

            if (Time.time >= _nextAttackTime)
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    Debug.Log("Attacking, current time " + Time.time + " more than min required " + _nextAttackTime + ", next will be " + (Time.time + attackCoolDown));
                    _nextAttackTime = Time.time + attackCoolDown; // For a cooldown, simply use attackRate without inverse if attackRate represents the cooldown in seconds.
                    Attack();
                }
            }
        }
        
        private void UpdateAttackPoint()
        {
            // Assuming the player has a direction or facing property that indicates where they are facing
            Vector3 direction = _playerController.GetFacingDirection(); // Implement this method in PlayerController

            // Set the attack point's position in front of the player
            attackPoint.localPosition = direction * attackRange;

            // Optionally, you can rotate the attack point to face the direction
            attackPoint.localRotation = Quaternion.LookRotation(direction);
        }

        void Attack()
        {
            _animator.SetTrigger(_attack1);
            Instantiate(attackSoundEffect, transform.position, Quaternion.identity, transform);

            Collider2D[] results = new Collider2D[10];
            Physics2D.OverlapCircleNonAlloc(attackPoint.position, attackRange, results, enemyLayers);

            foreach (Collider2D enemy in results)
            {
                if(enemy == null) continue; // Skip if the collider is null (no enemy found)
                Debug.Log("hit " + enemy.name);
                int dif = PlayerPrefs.GetInt("Difficulty");
                int value = Stats.Instance.AttackDamage + (Stats.Instance.Power * 2);
                switch (dif)
                {
                    case 1:
                        value *= 2;
                        break;
                    case 2:
                        break;
                    case 3:
                        value *= 2; // Enemies are already tough, so make it 2x for hardcore
                        break;
                }
                
                var enemyComponent = enemy.GetComponent<GenericEnemy>();
                if (enemyComponent != null)
                {
                    enemyComponent.Hit(value);

                    break; // Consider removing this if you want the attack to affect all enemies within range.
                }
            }
        }

        // Visual aid to show the attack range in the editor.
        private void OnDrawGizmosSelected()
        {
            if (attackPoint == null) return;
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }
    }
}
