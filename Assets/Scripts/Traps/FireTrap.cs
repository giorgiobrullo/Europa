using System.Collections;
using Player;
using UnityEngine;

namespace Traps
{
    public class FireTrap : MonoBehaviour
    {
        [Header("Trap Settings")]
        [SerializeField] private int damage = 10; // Damage dealt by the fire trap
        [SerializeField] private float activationInterval = 2f; // Time interval between activations
        [SerializeField] private float activationLength = 1f; // Duration of the trap's activation
        [SerializeField] private float activationOffset; // Time to wait before the first activation
        [SerializeField] private bool isActive; // Is the trap active

        private Animator _animator;
        private bool _isPlayerHurt;
        private static readonly int On = Animator.StringToHash("on");
        private static readonly int Off = Animator.StringToHash("off");

        private void Start()
        {
            _animator = GetComponent<Animator>();
            StartCoroutine(StartWithDelay(activationOffset));
        }

        private IEnumerator StartWithDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            StartCoroutine(FireTrapCycle());
        }

        private IEnumerator FireTrapCycle()
        {
            while (true)
            {
                isActive = true;
                _animator.SetTrigger(On);
                yield return new WaitForSeconds(activationLength);

                isActive = false;
                _animator.SetTrigger(Off);
                yield return new WaitForSeconds(activationInterval);
            }
            // ReSharper disable once IteratorNeverReturns
            // Supposed to never return, always on
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.gameObject.name != "Player" || _isPlayerHurt) return;

            if (isActive)
            {
                var playerStats = Stats.Instance;

                if (playerStats != null)
                {
                    playerStats.TakeDamage(damage, true);
                    StartCoroutine(HandleHurtCooldown());
                }
            }
        }

        private IEnumerator HandleHurtCooldown()
        {
            _isPlayerHurt = true;
            yield return new WaitForSeconds(activationInterval); // Adjust if you need a separate cooldown
            _isPlayerHurt = false;
        }
    }
}
