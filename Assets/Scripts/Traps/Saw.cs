using System;
using System.Collections;
using Player;
using UnityEngine;

public class SawMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public Vector2 direction = Vector2.right; // Direction of movement
    public float duration = 2f; // Duration of the movement in seconds
    public float startDelay = 0f; // Delay before the saw starts moving
    public float initialOffset = 0f; // Initial offset distance along the direction vector
    public float speedMultiplier = 1f; // Speed multiplier for the movement

    [Header("Chain Settings")]
    public GameObject chainPrefab; // Prefab for the chain (dot) object
    public float chainSpacing = 0.5f; // Distance between each chain object

    [Header("Damage Settings")]
    [SerializeField] private int damage = 25;
    [SerializeField] private float hurtCooldown = 1.0f; // Duration of the hurt animation

    private Vector2 startPosition;
    private Vector2 initialStartPosition;
    private float elapsedTime = 0f;
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
        startPosition = transform.position;
        initialStartPosition = startPosition + direction.normalized * initialOffset;
        SpawnChainObjects();
        StartCoroutine(StartMovementAfterDelay());
    }

    void Update()
    {
        if (elapsedTime > 0)
        {
            MoveSaw();
        }
    }

    private IEnumerator StartMovementAfterDelay()
    {
        yield return new WaitForSeconds(startDelay);
        elapsedTime = initialOffset / direction.magnitude * duration; // Adjust elapsed time based on offset
        transform.position = initialStartPosition;
        if (initialOffset == 0)
        {
            elapsedTime = 0.001f; // Reset elapsed time for zero offset
        }
    }

    void MoveSaw()
    {
        elapsedTime += Time.deltaTime * speedMultiplier;
        float t = Mathf.PingPong(elapsedTime / duration, 1f);
        t = Mathf.SmoothStep(0f, 1f, t); // Smooth movement at the ends

        // Move between startPosition and startPosition + direction
        transform.position = Vector2.Lerp(startPosition, startPosition + direction, t);
    }

    void SpawnChainObjects()
    {
        float distance = direction.magnitude;
        int chainCount = Mathf.CeilToInt(distance / chainSpacing);

        for (int i = 0; i <= chainCount; i++)
        {
            Vector2 position = Vector2.Lerp(startPosition, startPosition + direction, (float)i / chainCount);
            Instantiate(chainPrefab, position, Quaternion.identity);
        }
    }

    void OnDrawGizmos()
    {
        if (!Application.isPlaying)
        {
            startPosition = transform.position;
            initialStartPosition = startPosition + direction.normalized * initialOffset;
        }

        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(startPosition, 0.1f); // Initial position of the saw
        Gizmos.DrawSphere(initialStartPosition, 0.2f); // Offset start position of the saw (larger to represent start delay)

        Gizmos.color = Color.red;
        Gizmos.DrawRay(startPosition, direction);

        float distance = direction.magnitude;
        int chainCount = Mathf.CeilToInt(distance / chainSpacing);

        for (int i = 0; i <= chainCount; i++)
        {
            Vector2 position = Vector2.Lerp(startPosition, startPosition + direction, (float)i / chainCount);
            Gizmos.DrawSphere(position, 0.1f); // Visualize chain objects with spheres
        }
    }
}
