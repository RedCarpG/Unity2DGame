using UnityEngine;

using my2DGame.helper;
using my2DGame.tool;

namespace my2DGame
{
    namespace player
    {
        namespace movement
        {
            public class PlayerMovement : MonoBehaviour
            {
                private Vector3 VELOCITY0 = Vector3.zero;

                [Header("")]
                // Rigid Body
                [SerializeField] private Rigidbody2D RB;
                // Animation
                [SerializeField] private Animator ANIMATOR;
                // For Ground check
                [SerializeField] private GroundCheck groundCheck;

                [Header("Movement")]
                // Movement
                [SerializeField] private float moveSpeed = 400;
                private readonly float MOVE_VY_CTL_MULT = 0.1f;
                private readonly float MOVE_VY_CTL_THREASHOLD = 5;

                [Header("Info")]
                [ReadOnly] public float horizontalMovement = 0;
                [ReadOnly] public int facingDirection = 1;

                void Start()
                {
                    groundCheck.SetOnTriggerEnter2DCallback(OnGroundCheckTrigger);
                    groundCheck.SetOnTriggerExit2DCallback(OnGroundCheckExit);
                }


                void FixedUpdate()
                {
                    // ----- Movement
                    horizontalMovement = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
                    MoveX(horizontalMovement);
                    // ----- Animation
                    UpdateAnimation();
                }

                /** Movements */
                private void MoveX(float iHorizontalMovment)
                {
                    float aVx = iHorizontalMovment;
                    if (Mathf.Abs(RB.velocity.y) > MOVE_VY_CTL_THREASHOLD)
                    {
                        // Reduce Velocity X control when Y Velocity is high
                        aVx = iHorizontalMovment / (1 + Mathf.Abs(RB.velocity.y) * MOVE_VY_CTL_MULT);
                    }
                    // Smoothly 
                    Vector3 aTargetVelocity = new Vector2(aVx, RB.velocity.y);
                    RB.velocity = Vector3.SmoothDamp(RB.velocity, aTargetVelocity, ref VELOCITY0, 0.1f);
                }


                // Ground Check
                private void OnGroundCheckTrigger()
                {
                    ANIMATOR.SetBool("isGrounded", true);
                }

                private void OnGroundCheckExit()
                {
                    ANIMATOR.SetBool("isGrounded", false);
                }

                /** Animations */
                private void FlipImage()
                {
                    facingDirection = -facingDirection;
                    Vector3 aScale = transform.localScale;
                    aScale.x *= -1;
                    transform.localScale = aScale;
                }

                private void UpdateAnimation()
                {
                    ANIMATOR.SetFloat("xVelocity", Mathf.Abs(RB.velocity.x));
                    ANIMATOR.SetFloat("yVelocity", RB.velocity.y);
                    if (horizontalMovement > 0 && (facingDirection != 1))
                    {
                        FlipImage();
                    }
                    else if (horizontalMovement < 0 && (facingDirection == 1))
                    {
                        FlipImage();
                    }
                }

            }
        }
    }
}