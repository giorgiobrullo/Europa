using UnityEngine;

namespace Cameras
{
    public class CameraController : MonoBehaviour
    {
        // Camera Settings
        [Header("Camera Settings")]
        [SerializeField] private GameObject target;
        [SerializeField] private float smoothRate = 0.3f;
        private Vector3 _cameraVelocity = Vector3.zero;

        // Map Boundaries
        [Header("Map Boundaries")]
        [SerializeField] private float leftMapBoundary;
        [SerializeField] private float rightMapBoundary;
        [SerializeField] private float upperMapBoundary;
        [SerializeField] private float lowerMapBoundary;

        // Dead Zone Settings
        [Header("Dead Zone Settings")]
        [SerializeField] private float verticalDeadZone = 1.5f; // Height of the vertical dead zone
        [SerializeField] private float downwardAdjustment = 10.0f; // Additional downward offset when the player moves down

        private Camera _camera;
        private float _originalZPosition;

        private void Start()
        {
            _camera = GetComponent<Camera>();
            _originalZPosition = transform.position.z;
        }

        private void LateUpdate()
        {
            if (target is null || _camera is null)
            {
                return;
            }

            float cameraHalfWidth = _camera.orthographicSize * _camera.aspect;
            float cameraHalfHeight = _camera.orthographicSize;
            float leftBoundary = leftMapBoundary + cameraHalfWidth;
            float rightBoundary = rightMapBoundary - cameraHalfWidth;
            float upperBoundary = upperMapBoundary - cameraHalfHeight;
            float lowerBoundary = lowerMapBoundary + cameraHalfHeight;

            // Calculate the target position, only modify y if out of dead zone
            Vector3 targetPosition = new Vector3(target.transform.position.x, transform.position.y, _originalZPosition);

            // Check if the target is out of the vertical dead zone
            float verticalDistance = target.transform.position.y - transform.position.y;
            if (Mathf.Abs(verticalDistance) > verticalDeadZone)
            {
                targetPosition.y = target.transform.position.y - Mathf.Sign(verticalDistance) * verticalDeadZone;
                // Apply additional downward adjustment when the player is moving down
                if (verticalDistance < 0)
                {
                    targetPosition.y -= downwardAdjustment;
                }
            }

            // Smoothly move the camera towards the target position
            Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, targetPosition, ref _cameraVelocity, smoothRate);
            smoothedPosition.x = Mathf.Clamp(smoothedPosition.x, leftBoundary, rightBoundary);
            smoothedPosition.y = Mathf.Clamp(smoothedPosition.y, lowerBoundary, upperBoundary);

            transform.position = smoothedPosition;
        }

        private void OnDrawGizmos()
        {
            if (_camera == null)
            {
                _camera = GetComponent<Camera>();
            }

            float cameraHalfWidth = _camera.orthographicSize * _camera.aspect;
            float cameraHalfHeight = _camera.orthographicSize;

            Gizmos.color = Color.red;

            // Calculate boundary positions
            Vector3 topLeft = new Vector3(leftMapBoundary, upperMapBoundary, _originalZPosition);
            Vector3 topRight = new Vector3(rightMapBoundary, upperMapBoundary, _originalZPosition);
            Vector3 bottomLeft = new Vector3(leftMapBoundary, lowerMapBoundary, _originalZPosition);
            Vector3 bottomRight = new Vector3(rightMapBoundary, lowerMapBoundary, _originalZPosition);

            // Draw outer boundaries
            Gizmos.DrawLine(topLeft, topRight);
            Gizmos.DrawLine(topRight, bottomRight);
            Gizmos.DrawLine(bottomRight, bottomLeft);
            Gizmos.DrawLine(bottomLeft, topLeft);

            // Draw camera boundaries
            Vector3 cameraTopLeft = new Vector3(leftMapBoundary + cameraHalfWidth, upperMapBoundary - cameraHalfHeight, _originalZPosition);
            Vector3 cameraTopRight = new Vector3(rightMapBoundary - cameraHalfWidth, upperMapBoundary - cameraHalfHeight, _originalZPosition);
            Vector3 cameraBottomLeft = new Vector3(leftMapBoundary + cameraHalfWidth, lowerMapBoundary + cameraHalfHeight, _originalZPosition);
            Vector3 cameraBottomRight = new Vector3(rightMapBoundary - cameraHalfWidth, lowerMapBoundary + cameraHalfHeight, _originalZPosition);

            Gizmos.color = Color.blue;
            Gizmos.DrawLine(cameraTopLeft, cameraTopRight);
            Gizmos.DrawLine(cameraTopRight, cameraBottomRight);
            Gizmos.DrawLine(cameraBottomRight, cameraBottomLeft);
            Gizmos.DrawLine(cameraBottomLeft, cameraTopLeft);
        }
    }
}
