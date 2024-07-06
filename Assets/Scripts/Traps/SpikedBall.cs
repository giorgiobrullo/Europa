using System.Collections.Generic;
using UnityEngine;

namespace Traps
{
    public class SpikedBallPendulum : MonoBehaviour
    {
        [Header("Pendulum Settings")]
        public GameObject spikedBallPrefab; // Prefab for the spiked ball
        public GameObject chainPrefab; // Prefab for the chain link
        public float length = 5f; // Length of the pendulum
        public float speed = 1f; // Speed of the pendulum swing
        public float chainInterval = 0.5f; // Distance between each chain link
        public float maxAngle = 45f; // Maximum angle (in degrees) the pendulum swings to either side

        [Header("Sound Settings")]
        public GameObject soundAtBottomPrefab; // Prefab for the sound object to instantiate at the bottom

        private GameObject _spikedBall; // Instance of the spiked ball
        private readonly List<GameObject> _chainLinks = new(); // List to hold chain link instances
        private float _timeCounter; // Time counter for the swing
        private bool _soundPlayed; // To ensure sound is played only once per crossing

        void Start()
        {
            InitializePendulum();
        }

        void Update()
        {
            SwingPendulum();
        }

        private void InitializePendulum()
        {
            // Instantiate the spiked ball
            _spikedBall = Instantiate(spikedBallPrefab, CalculateSpikedBallPosition(), Quaternion.identity, transform);

            // Generate the chain links
            GenerateChain();
        }

        private Vector3 CalculateSpikedBallPosition()
        {
            float initialX = transform.position.x;
            float initialY = transform.position.y - length;
            return new Vector3(initialX, initialY, transform.position.z);
        }

        private void GenerateChain()
        {
            float distanceFromCenter = length;
            int numberOfLinks = Mathf.CeilToInt(distanceFromCenter / chainInterval);
            Vector2 direction = (_spikedBall.transform.position - transform.position).normalized;

            for (int i = 1; i <= numberOfLinks; i++)
            {
                Vector2 position = (Vector2)transform.position + direction * (chainInterval * i);
                GameObject chainLink = Instantiate(chainPrefab, position, Quaternion.identity, transform);
                _chainLinks.Add(chainLink);
            }
        }

        private void SwingPendulum()
        {
            _timeCounter += Time.deltaTime * speed;
            float angle = Mathf.Sin(_timeCounter) * Mathf.Deg2Rad * maxAngle;

            float x = transform.position.x + Mathf.Sin(angle) * length;
            float y = transform.position.y - Mathf.Cos(angle) * length;

            _spikedBall.transform.position = new Vector3(x, y, _spikedBall.transform.position.z);

            // Play sound when pendulum is at the bottom (swing angle is zero)
            if (Mathf.Abs(Mathf.Sin(_timeCounter)) < 0.02f && !_soundPlayed)
            {
                Instantiate(soundAtBottomPrefab, _spikedBall.transform.position, Quaternion.identity);
                _soundPlayed = true;
            }
            else if (Mathf.Abs(Mathf.Sin(_timeCounter)) >= 0.02f)
            {
                _soundPlayed = false;
            }

            // Update chain links position
            UpdateChain();
        }

        private void UpdateChain()
        {
            int numberOfLinks = _chainLinks.Count;
            Vector2 direction = (_spikedBall.transform.position - transform.position).normalized;

            for (int i = 0; i < numberOfLinks; i++)
            {
                Vector2 position = (Vector2)transform.position + direction * (chainInterval * (i + 1));
                _chainLinks[i].transform.position = new Vector3(position.x, position.y, _chainLinks[i].transform.position.z);
            }
        }

        private void OnDrawGizmos()
        {
            if (spikedBallPrefab != null)
            {
                // Draw the line from the center to the spiked ball
                Gizmos.color = Color.yellow;
                Vector3 spikedBallPosition = CalculateSpikedBallPosition();
                Gizmos.DrawLine(transform.position, spikedBallPosition);

                // Draw chain links
                float distanceFromCenter = length;
                int numberOfLinks = Mathf.CeilToInt(distanceFromCenter / chainInterval);
                Vector2 direction = ((Vector2)spikedBallPosition - (Vector2)transform.position).normalized;

                for (int i = 1; i <= numberOfLinks; i++)
                {
                    Vector2 position = (Vector2)transform.position + direction * (chainInterval * i);
                    Gizmos.DrawSphere(position, 0.1f); // Draw spheres to represent chain links
                }

                // Draw the arc of the pendulum swing
                Gizmos.color = Color.red;
                for (float t = -Mathf.Deg2Rad * maxAngle; t <= Mathf.Deg2Rad * maxAngle; t += 0.1f)
                {
                    float x = transform.position.x + Mathf.Sin(t) * length;
                    float y = transform.position.y - Mathf.Cos(t) * length;
                    Gizmos.DrawSphere(new Vector3(x, y, 0), 0.05f);
                }
            }
        }
    }
}
