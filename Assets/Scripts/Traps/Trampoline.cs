using UnityEngine;

namespace Traps
{
    public class Trampoline : MonoBehaviour
    {
        [Header("Trampoline Settings")]
        public float bounceForce = 10f; // Force applied to the player when they hit the trampoline
        public string playerTag = "Player"; // Tag used to identify the player object
        private Animator _animator; // Animator component for the trampoline
        public GameObject soundEffect; // Sound effect to play when the player hits the trampoline
        private static readonly int Jump = Animator.StringToHash("Jump");

        private void Start()
        {
            // Ensure the animator is assigned
            if (_animator == null)
            {
                _animator = GetComponent<Animator>();
            }

            BoxCollider2D tCollider = GetComponent<BoxCollider2D>();
            if (tCollider == null)
            {
                gameObject.AddComponent<BoxCollider2D>();
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            // Check if the object colliding with the trampoline is the player
            if (collision.collider.CompareTag(playerTag))
            {
                // Apply the bounce force to the player
                Rigidbody2D playerRb = collision.collider.GetComponent<Rigidbody2D>();
                if (playerRb != null)
                {
                    playerRb.velocity = new Vector2(playerRb.velocity.x, bounceForce);
                    TriggerJumpAnimation();
                    Instantiate(soundEffect, transform.position, Quaternion.identity, transform);
                }
            }
        }

        private void TriggerJumpAnimation()
        {
            if (_animator != null)
            {
                _animator.SetTrigger(Jump);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(transform.position, GetComponent<BoxCollider2D>().size);
        }
    }
}