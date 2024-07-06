using System;
using System.Collections;
using Player;
using UnityEngine;

public class RockHead : MonoBehaviour
{
    public bool detectBottom = true;
    public bool detectLeft = true;
    public bool detectRight = true;
    public bool detectTop = true;

    public float detectionDistance = 1f; // Distance to detect collisions
    public float idleBlinkInterval = 3f; // Interval for blinking while idle
    public GameObject dustPrefab; // Prefab for dust particles
    public float dustDuration = 1f; // Duration for dust particles to exist
    public float initialSpeed = 1f; // Initial speed when moving towards ground
    public float acceleration = 0.5f; // Acceleration factor
    public float returnSpeed = 1f; // Speed for returning to the original position

    public LayerMask detectionLayerMask; // Layer mask for player detection
    public LayerMask groundLayerMask; // Layer mask for ground detection

    public GameObject groundHitSound; // Audio source for ground hit sound

    private Animator animator;
    private float blinkTimer;
    private Vector2 originalPosition;
    private Vector2 moveDirection;
    private bool isMoving = false;
    private bool isReturning = false;
    private bool hasHitGround = false; // Flag to ensure hit animation triggers once
    private float currentSpeed;
    [SerializeField] private int damage = 25;
    [SerializeField] private float hurtCooldown = 1.0f; // Duration of the hurt animation

    private bool _isPlayerHurt;

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

    void Start()
    {
        animator = GetComponent<Animator>();
        blinkTimer = idleBlinkInterval;
        originalPosition = transform.position;
        currentSpeed = initialSpeed;
    }

    void Update()
    {
        HandleBlinking();

        if (!isMoving && !isReturning)
        {
            DetectHits();
        }
        else if (isMoving)
        {
            MoveTowardsGround();
        }
        else if (isReturning)
        {
            ReturnToOriginalPosition();
        }
    }

    void HandleBlinking()
    {
        blinkTimer -= Time.deltaTime;
        if (blinkTimer <= 0)
        {
            animator.SetTrigger("Blink");
            blinkTimer = idleBlinkInterval;
        }
    }

    void DetectHits()
    {
        if (detectBottom && DetectPlayer(Vector2.down))
        {
            TriggerMovement(Vector2.down);
        }
        else if (detectLeft && DetectPlayer(Vector2.left))
        {
            TriggerMovement(Vector2.left);
        }
        else if (detectRight && DetectPlayer(Vector2.right))
        {
            TriggerMovement(Vector2.right);
        }
        else if (detectTop && DetectPlayer(Vector2.up))
        {
            TriggerMovement(Vector2.up);
        }
    }

    bool DetectPlayer(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, detectionDistance, detectionLayerMask);
        Debug.DrawRay(transform.position, direction * detectionDistance, Color.red); // Visualize the ray
        return hit.collider != null && hit.collider.CompareTag("Player");
    }

    void TriggerMovement(Vector2 direction)
    {
        moveDirection = direction;
        isMoving = true;
        isReturning = false;
        currentSpeed = initialSpeed; // Reset speed at the start of movement
        Debug.Log("Triggering movement in direction: " + direction);

    }

    void MoveTowardsGround()
    {
        transform.Translate(moveDirection * currentSpeed * Time.deltaTime);
        currentSpeed += acceleration * Time.deltaTime;

        // Calculate the ray distance based on the sprite's size
        float rayDistance = (GetComponent<SpriteRenderer>().bounds.size.y / 2) - 0.3f;

        // Check if the rock head has hit the ground
        RaycastHit2D groundHit = Physics2D.Raycast(transform.position, moveDirection, rayDistance, groundLayerMask);
        if (groundHit.collider != null && !hasHitGround)
        {
            Debug.Log("Hit ground");
            isMoving = false;
            isReturning = true;
            hasHitGround = true; // Ensure the hit animation triggers only once
            SpawnDust(transform.position);
            TriggerGroundHitAnimation();

            if (groundHitSound != null)
            {
                Instantiate(groundHitSound, transform.position, Quaternion.identity, transform);
            }
        }
    }

    void ReturnToOriginalPosition()
    {
        transform.position = Vector2.MoveTowards(transform.position, originalPosition, returnSpeed * Time.deltaTime);
        if (Vector2.Distance(transform.position, originalPosition) < 0.1f)
        {
            isReturning = false;
            ResetThwomp();
        }
    }

    void TriggerGroundHitAnimation()
    {
        if (moveDirection == Vector2.down)
        {
            animator.SetTrigger("HitBottom");
        }
        else if (moveDirection == Vector2.left)
        {
            animator.SetTrigger("HitLeft");
        }
        else if (moveDirection == Vector2.right)
        {
            animator.SetTrigger("HitRight");
        }
        else if (moveDirection == Vector2.up)
        {
            animator.SetTrigger("HitTop");
        }
    }

    void ResetThwomp()
    {
        // Reset to initial conditions
        currentSpeed = initialSpeed;
        moveDirection = Vector2.zero;
        isMoving = false;
        isReturning = false;
        hasHitGround = false; 
    }

    void SpawnDust(Vector2 position)
    {
        GameObject dust = Instantiate(dustPrefab, position, Quaternion.identity);
        var particleSystem = dust.GetComponent<ParticleSystem>();
        if (particleSystem != null)
        {
            particleSystem.Play();
        }
        Destroy(dust, dustDuration);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        if (detectBottom)
        {
            Gizmos.DrawRay(transform.position, Vector2.down * detectionDistance);
        }

        if (detectLeft)
        {
            Gizmos.DrawRay(transform.position, Vector2.left * detectionDistance);
        }

        if (detectRight)
        {
            Gizmos.DrawRay(transform.position, Vector2.right * detectionDistance);
        }

        if (detectTop)
        {
            Gizmos.DrawRay(transform.position, Vector2.up * detectionDistance);
        }
    }
}
