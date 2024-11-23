using System.Collections;
using Player;
using UnityEngine;

namespace Enemies.Generic
{
    public class EnemyMovement : MonoBehaviour
    {
        [SerializeField] protected internal float followRange = 10f;
        [SerializeField] private float moveSpeed = 3f;
        [SerializeField] private GameObject moveSoundGameObject;
        private AudioSource _moveSoundAudioSource;

        private BoxCollider2D _collider2D;
        private GenericEnemy _enemy;

        private bool _isMoveSoundPlaying;
        private static readonly int IsMoving = Animator.StringToHash("isMoving");

        private void Start()
        {
            _enemy = GetComponent<GenericEnemy>();
            _collider2D = GetComponent<BoxCollider2D>();
            if (moveSoundGameObject)
                _moveSoundAudioSource =
                    Instantiate(moveSoundGameObject, transform.position, Quaternion.identity, transform)
                        .GetComponent<AudioSource>();
        }

        public void FollowPlayer()
        {
            var enemy = GetComponent<GenericEnemy>();
            Vector2 direction = enemy.Target.transform.position - transform.position;
            float horizontalDirection = Mathf.Sign(direction.x); // -1 for left, 1 for right

            float moveDistance = moveSpeed * Time.fixedDeltaTime;
            float colliderEdgeOffset = _collider2D.size.x * 0.5f * Mathf.Abs(transform.localScale.x);
            Vector2 raycastStart = enemy.Rigidbody.position + new Vector2(colliderEdgeOffset * horizontalDirection, 0);

            // Reduce the ray length by a small amount to allow the enemy to get closer
            float stoppingDistanceAdjustment = 0.5f;
            float rayLength = Mathf.Abs(moveDistance + colliderEdgeOffset) - stoppingDistanceAdjustment;

            // Cast a ray to check if the player is in front of the enemy
            RaycastHit2D playerHit = Physics2D.Raycast(raycastStart, Vector2.right * horizontalDirection, rayLength,
                LayerMask.GetMask("Player"));
            // Cast a ray to check if there is a block in front of the enemy
            RaycastHit2D blockHit = Physics2D.Raycast(raycastStart, Vector2.right * horizontalDirection, rayLength,
                LayerMask.GetMask("Ground"));

            if (playerHit.collider != null)
            {
                // Stop moving if the player is directly in front of the enemy
                enemy.Animator.SetBool(IsMoving, false);
                StopMoveSound();
            }
            else if (blockHit.collider == null)
            {
                Vector2 newPosition = enemy.Rigidbody.position + new Vector2(horizontalDirection * moveDistance, 0);
                enemy.Rigidbody.MovePosition(newPosition);
                enemy.Animator.SetBool(IsMoving, true);
                PlayMoveSound();
            }
            else
            {
                StopMoveSound();
                enemy.Animator.SetBool(IsMoving, false);
            }

            // Call the new method to check and flip the direction if needed
            CheckAndFlipDirection();
        }


        public void CheckAndFlipDirection()
        {
            var enemy = GetComponent<GenericEnemy>();
            Vector2 direction = enemy.Target.transform.position - transform.position;
            float horizontalDirection = Mathf.Sign(direction.x); // -1 for left, 1 for right

            if ((horizontalDirection > 0 && !enemy.IsFacingRight) || (horizontalDirection < 0 && enemy.IsFacingRight))
            {
                Flip();
            }
        }

        private void PlayMoveSound()
        {
            if (_moveSoundAudioSource != null && !_isMoveSoundPlaying)
            {
                _moveSoundAudioSource.Play();
                _isMoveSoundPlaying = true;
                StartCoroutine(ResetMoveSound(_moveSoundAudioSource.clip.length));
            }
        }

        private void StopMoveSound()
        {
            if (_moveSoundAudioSource != null && _isMoveSoundPlaying)
            {
                _moveSoundAudioSource.Stop();
                _isMoveSoundPlaying = false;
            }
        }

        private IEnumerator ResetMoveSound(float duration)
        {
            yield return new WaitForSeconds(duration);
            _isMoveSoundPlaying = false;
        }

        public void Flip()
        {
            _enemy.IsFacingRight = !_enemy.IsFacingRight;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.CompareTag("Player"))
            {
                
                    Debug.Log("Slip coroutine started");
                    ApplySlipForce(collision.collider.GetComponent<Rigidbody2D>());
                
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.collider.CompareTag("Player"))
            {
                StopSlipForce();
            }
        }

        public void ApplySlipForce(Rigidbody2D playerRb)
        {
            if (playerRb == null)
                return;

            // Determine the initial direction of the slipping force based on the player's current velocity
            float initialSlipDirection = Mathf.Sign(playerRb.linearVelocity.x);

            // If the player is not moving horizontally, choose a default direction
            if (initialSlipDirection == 0)
            {
                initialSlipDirection = Random.value < 0.5f ? -1f : 1f; // Randomly choose left or right
            }

            if (GetComponent<GenericEnemy>() == null) return;

            Vector2 slipForce = new Vector2(initialSlipDirection * 5f, 0f);
            PlayerController.Instance.ApplyExternalForce(slipForce);
        }

        public void StopSlipForce()
        {
            PlayerController.Instance.ResetExternalForce();
        }
    }
}