using UnityEngine;

using my2DGame.player.movement;
using my2DGame.helper;
using my2DGame.tool;

namespace my2DGame
{
    namespace debug
    {
        namespace player
        {
            public class PlayerDebug : MonoBehaviour
            {
                [Header("")]
                [SerializeField] private PlayerMovement playerMovement;
                [SerializeField] private WallCheck wallCheck;
                [SerializeField] private GroundCheck groundCheck;
                [SerializeField] private Rigidbody2D RB;
                [SerializeField] private BoxCollider2D BOX_COLLIDER_2D;
                [SerializeField] private CircleCollider2D CIRCLE_COLLIDER_2D;

                [Header("Read Only")]
                [ReadOnly] public bool isWalled;
                [ReadOnly] public bool isGrounded;

                [Header("Enable Debug")]
                [SerializeField] private bool enableDebug = true;

                void Start()
                {
                    RB = this.GetComponent<Rigidbody2D>();
                    BOX_COLLIDER_2D = this.GetComponent<BoxCollider2D>();
                    CIRCLE_COLLIDER_2D = this.GetComponent<CircleCollider2D>();
                }

                private void OnDrawGizmos()
                {
                    if (!enableDebug)
                    {
                        return;
                    }
                    Gizmos.color = Color.green;
                    // Collider Boxs
                    Gizmos.DrawWireSphere(CIRCLE_COLLIDER_2D.bounds.center, CIRCLE_COLLIDER_2D.radius);
                    Gizmos.DrawWireCube(BOX_COLLIDER_2D.bounds.center, BOX_COLLIDER_2D.size);
                    Gizmos.color = Color.red;
                    // Move Vector
                    Gizmos.DrawRay(RB.position, RB.velocity * 0.2f);
                    Gizmos.color = Color.yellow;
                    if (wallCheck.IsWalled())
                    {
                        // Wall Direction
                        Gizmos.DrawRay(RB.position, new Vector2(playerMovement.facingDirection, 0));
                    }
                    if (groundCheck.IsGrounded())
                    {
                        // Ground Direction
                        Gizmos.DrawRay(RB.position, new Vector2(0, -1));
                    }
                }

                private void Update()
                {
                    if (enableDebug)
                    {
                        isWalled = wallCheck.IsWalled();
                        isGrounded = groundCheck.IsGrounded();
                    }
                }
            }

        }
    }
}