using System.Collections;
using Other;
using UnityEngine;

namespace Enemies.Generic
{
    public class EnemyHealth : MonoBehaviour
    {
        [SerializeField] public int health = 500;
        [SerializeField] private int scoreValue = 100;
        public GameObject deathSound;
        private GenericEnemy _enemy;
        private EnemyItemDrop _enemyItemDrop;
        private EnemyMovement _enemyMovement;
        private Collider2D _collider2D;
        private static readonly int IsHurt = Animator.StringToHash("isHurt");
        private static readonly int IsDead = Animator.StringToHash("isDead");

        private void Start()
        {
            _collider2D = GetComponent<Collider2D>();
            _enemyItemDrop = GetComponent<EnemyItemDrop>();
            _enemy = GetComponent<GenericEnemy>();
            _enemyMovement = GetComponent<EnemyMovement>();
        }

        public virtual void TakeDamage(int damage)
        {
            if (_enemy.isDead) return;

            health -= damage;
            _enemy.Animator.SetTrigger(IsHurt);

            if (health <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            _enemy.isDead = true;
            //_enemy.Rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
            _enemy.Animator.SetTrigger(IsDead);
            _enemyMovement.StopSlipForce();
            _collider2D.isTrigger = true;
            if (deathSound) Instantiate(deathSound, transform.position, Quaternion.identity);
            StartCoroutine(WaitForDeathAnimation());
        }

        private IEnumerator WaitForDeathAnimation()
        {
            while (!_enemy.Animator.GetCurrentAnimatorStateInfo(1).IsName("dead"))
            {
                yield return null;
            }
            
            float normalizedTime = _enemy.Animator.GetCurrentAnimatorStateInfo(1).normalizedTime;
            float waitTime = (1 - (normalizedTime % 1)) * _enemy.Animator.GetCurrentAnimatorStateInfo(1).length;
            
            yield return new WaitForSeconds(waitTime);
            // Disable enemy (self gameobject)
            gameObject.SetActive(false);
            
            ScoreManager.Instance.AddEnemyScore(scoreValue);
            _enemyItemDrop.DropItems();

        }

    }
}
