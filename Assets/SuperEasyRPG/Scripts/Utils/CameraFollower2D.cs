using UnityEngine;

namespace SuperEasyRPG
{
    public class CameraFollower2D : MonoBehaviour
    {
        public Transform target;
        public float followSpeed = 3.0f;
        public Vector2 deadzone = new(1.5f, 1.5f);

        [Header("Boundary")]
        [Min(0)]
        public float left = 10;
        [Min(0)]
        public float right = 10;
        [Min(0)]
        public float top = 10;
        [Min(0)]
        public float bottom = 10;

        private Vector3 initialPos;
        private Vector2 boundaryMin;
        private Vector2 boundaryMax;

        // Start is called before the first frame update
        void Start()
        {
            initialPos = new(transform.position.x, transform.position.y, -10);
            boundaryMin = new(initialPos.x - left, initialPos.y - bottom);
            boundaryMax = new(initialPos.x + right, initialPos.y + top);
            transform.position = target.position;
        }

        // Update is called once per frame
        void Update()
        {
            FollowSlowly();
        }

        private void FollowSlowly()
        {
            float deltaX = target.position.x - transform.position.x;
            float deltaY = target.position.y - transform.position.y;
            if (deltaX < 0)
            {
                deltaX = Mathf.Min(deltaX + deadzone.x, 0);
            }
            else
            {
                deltaX = Mathf.Max(deltaX - deadzone.x, 0);
            }

            if (deltaY < 0)
            {
                deltaY = Mathf.Min(deltaY + deadzone.y, 0);
            }
            else
            {
                deltaY = Mathf.Max(deltaY - deadzone.x, 0);
            }

            Vector3 newPos = new Vector3(transform.position.x + deltaX, transform.position.y + deltaY, initialPos.z);
            Vector2 viewportSize = GetViewportSize();

            newPos.x = Mathf.Clamp(newPos.x, boundaryMin.x + viewportSize.x / 2, boundaryMax.x - viewportSize.x / 2);
            newPos.y = Mathf.Clamp(newPos.y, boundaryMin.y + viewportSize.y / 2, boundaryMax.y - viewportSize.y / 2);

            transform.position = Vector3.Slerp(transform.position, newPos, followSpeed * Time.deltaTime);
        }

        private void OnDrawGizmosSelected()
        {
            DrawBoundary();
            DrawDeadzone();
        }

        private void DrawBoundary()
        {
            Vector2 viewportSize = GetViewportSize();

            float boundaryWidth = right + left;
            float boundaryHeight = top + bottom;

            Vector3 center = initialPos != null ? initialPos : transform.position; 
            Vector3 boundaryCenter = new(center.x + right - boundaryWidth / 2, center.y + top - boundaryHeight / 2, 0);

            if (boundaryWidth < viewportSize.x || boundaryHeight < viewportSize.y)
            {
                Gizmos.color = Color.red;
            } else
            {
                Gizmos.color = Color.green;
            }
            Gizmos.DrawWireCube(boundaryCenter, new Vector3(boundaryWidth, boundaryHeight, 1));
        }

        private void DrawDeadzone()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(transform.position, new Vector3(deadzone.x * 2, deadzone.y * 2, 1));
        }

        private Vector2 GetViewportSize()
        {
            float viewportHeight = Camera.main.orthographicSize * 2f;
            float viewportWidth = viewportHeight * Camera.main.aspect;
            return new Vector2(viewportWidth, viewportHeight);
        }
    }
}