using System.Collections;
using Player;
using UnityEngine;

namespace Traps
{
    public class SpikeDamage : MonoBehaviour
    {
        [SerializeField] private int damage = 25;
        [SerializeField] private float hurtCooldown = 1.0f; // Duration of the hurt animation

        private bool _isPlayerHurt;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.name != "Player" || _isPlayerHurt) return;

            var playerStats = Stats.Instance;

            if (playerStats != null)
            {
                playerStats.TakeDamage(damage, true);
                StartCoroutine(HandleHurtCooldown());
            }
        }
        
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.name != "Player" || _isPlayerHurt) return;

            var playerStats = Stats.Instance;

            if (playerStats != null)
            {
                playerStats.TakeDamage(damage, true);
                StartCoroutine(HandleHurtCooldown());
            }
        }
        private IEnumerator HandleHurtCooldown()
        {
            _isPlayerHurt = true;
            yield return new WaitForSeconds(hurtCooldown);
            _isPlayerHurt = false;
        }
    }
}
