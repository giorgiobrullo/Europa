using System.Collections;
using UnityEngine;

namespace Traps
{
    public class FallingPlatform : MonoBehaviour
    {
        [Header("Falling Settings")]
        public float fallDelay = 0.5f; // Delay before the platform starts falling
        public float initialFallSpeed = 0.5f; // Initial speed of falling
        public float acceleration = 0.1f; // Acceleration of the fall
        public LayerMask playerLayer; // Layer mask to identify the player

        private Rigidbody2D _rb;
        private bool _isFalling;
        private float _currentFallSpeed;

        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
            GetComponent<Collider2D>();
            _rb.isKinematic = true; // Initially make the platform kinematic
            _currentFallSpeed = initialFallSpeed;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (IsPlayer(collision))
            {
                StartCoroutine(FallAfterDelay());
            }
        }

        private bool IsPlayer(Collision2D collision)
        {
            return (playerLayer.value & 1 << collision.gameObject.layer) != 0;
        }

        private IEnumerator FallAfterDelay()
        {
            yield return new WaitForSeconds(fallDelay);
            _isFalling = true;
            _rb.isKinematic = false; // Make the platform non-kinematic to enable physics
        }

        private void FixedUpdate()
        {
            if (_isFalling)
            {
                _currentFallSpeed += acceleration * Time.fixedDeltaTime;
                _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, -_currentFallSpeed);
            }
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            if (_isFalling && collision.gameObject.CompareTag("Ground"))
            {
                _rb.isKinematic = true; // Stop the platform
                _rb.linearVelocity = Vector2.zero;
                _isFalling = false;
            }
        }
    }
}