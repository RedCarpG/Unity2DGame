using UnityEngine;

using my2DGame.helper;
using my2DGame.tool;

namespace my2DGame
{
    namespace enemy
    {
        namespace patrol
        {
            public class EnemyPatrol : MonoBehaviour
            {
                private Vector3 VELOCITY0 = Vector3.zero;

                [Header("")]
                [SerializeField] private GroundEdgeCheck groundEdgeCheck;
                [SerializeField] private GroundCheck groundCheck;
                [SerializeField] private WallCheck wallCheck;
                // Animation
                [SerializeField] private Animator ANIMATOR;
                // Rigid Body
                [SerializeField] private Rigidbody2D RB;

                [Header("Movement")]
                [SerializeField] private float maxMoveSpeed = 5;

                [Header("Info")]
                [ReadOnly] public int facingDirection = 1;
                [ReadOnly][SerializeField] private float currentMoveSpeed;
                public float CurrentMoveSpeed
                {
                    set { currentMoveSpeed = value; }
                    get { return currentMoveSpeed; }
                }

                void Start()
                {
                    facingDirection = (int) this.transform.localScale.x;
                    currentMoveSpeed = maxMoveSpeed;
                    RB = this.GetComponent<Rigidbody2D>();
                }

                // Update is called once per frame
                void Update()
                {
                    MoveX();
                }

                private void FixedUpdate()
                {
                    if ((groundEdgeCheck.IsReachingEdge() || wallCheck.IsWalled()) && groundCheck.IsGrounded())
                    {
                        FlipImage();
                    }
                }

                /** Movements */
                private void MoveX()
                {
                    // Smoothly 
                    Vector3 aTargetVelocity = new Vector2(facingDirection * currentMoveSpeed, RB.velocity.y);
                    RB.velocity = Vector3.SmoothDamp(RB.velocity, aTargetVelocity, ref VELOCITY0, 0.1f);
                }

                /** Animations */
                private void FlipImage()
                {
                    facingDirection = -facingDirection;
                    Vector3 aScale = transform.localScale;
                    aScale.x *= -1;
                    transform.localScale = aScale;
                }
            }
        }
    }
}
