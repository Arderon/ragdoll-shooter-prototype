using UnityEngine;

namespace EnemyAI
{
    public class EnemyVision : MonoBehaviour
    {
        [Header("Vision Settings")]
        [SerializeField] private float viewDistance = 15f;
        [SerializeField, Range(0, 360)] private float viewAngle = 90f;
        [SerializeField] private LayerMask obstacleLayer;
        private LayerMask playerLayer;

        [Header("View Point (Enemy Eyes)")]
        [SerializeField] private Transform eyesPoint;

        [Header("Debug")]
        [SerializeField] private bool drawGizmos = true;

        private Transform player;
        private bool canSeePlayer;

        private void Awake()
        {
            if (eyesPoint == null)
                eyesPoint = transform;

            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
                player = playerObj.transform;
        }

        /// <summary>
        /// Checks if the enemy can see the player based on distance, angle, and obstacles.
        /// </summary>
        public Transform GetPlayerInSight()
        {
            if (player == null) return null;

            Vector3 originPos = eyesPoint.position;
            Vector3 directionToPlayer = player.position - originPos;
            float distanceToPlayer = directionToPlayer.magnitude;

            // 1. Distance check
            if (distanceToPlayer > viewDistance)
            {
                canSeePlayer = false;
                return null;
            }

            // 2. Angle check
            float angleToPlayer = Vector3.Angle(eyesPoint.forward, directionToPlayer);
            if (angleToPlayer > viewAngle / 2f)
            {
                canSeePlayer = false;
                return null;
            }

            // 3. Raycast check for obstacles
            if (Physics.Raycast(originPos, directionToPlayer.normalized, out RaycastHit hit, viewDistance, obstacleLayer | playerLayer))
            {
                if (((1 << hit.collider.gameObject.layer) & playerLayer) != 0)
                {
                    canSeePlayer = true;
                    return player;
                }
            }

            canSeePlayer = false;
            return null;
        }

        public void SetPlayerLayer(LayerMask layer)
        {
            playerLayer = layer;
        }

        private void OnDrawGizmosSelected()
        {
            if (!drawGizmos) return;

            Vector3 originPos = eyesPoint != null ? eyesPoint.position : transform.position;

            // Vision radius
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(originPos, viewDistance);

            // Vision cone
            Vector3 forward = eyesPoint != null ? eyesPoint.forward : transform.forward;
            Quaternion leftRayRotation = Quaternion.AngleAxis(-viewAngle / 2f, Vector3.up);
            Quaternion rightRayRotation = Quaternion.AngleAxis(viewAngle / 2f, Vector3.up);

            Vector3 leftRayDirection = leftRayRotation * forward;
            Vector3 rightRayDirection = rightRayRotation * forward;

            Gizmos.color = canSeePlayer ? Color.red : Color.cyan;
            Gizmos.DrawRay(originPos, leftRayDirection * viewDistance);
            Gizmos.DrawRay(originPos, rightRayDirection * viewDistance);

            // Vision ray to player (in Play mode)
            if (player != null)
            {
                Gizmos.color = canSeePlayer ? Color.green : Color.red;
                Gizmos.DrawLine(originPos, player.position);
            }
        }
    }
}
