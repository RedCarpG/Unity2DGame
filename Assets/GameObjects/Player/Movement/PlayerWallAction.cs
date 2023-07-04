using UnityEngine;

using my2DGame.helper;
using my2DGame.tool;

namespace my2DGame
{
    namespace player
    {
        namespace movement
        {
            public class PlayerWallAction : MonoBehaviour
            {
                [Header("")]
                // Rigid Body
                [SerializeField] private Rigidbody2D RB;
                // Animation
                [SerializeField] private Animator ANIMATOR;
                [SerializeField] private PlayerMovement playerMovement;
                [SerializeField] private GroundCheck groundCheck;
                [SerializeField] private WallCheck wallCheck;

                [Header("Wall Slide")]
                // Wall Slide
                [SerializeField] private LayerMask WALL_SLIDE_LAYER;
                [SerializeField] private float wallSlidingSpeed = 0.5f;

                [Header("Read Only")]
                [ReadOnly] public bool isWallSliding = false;
                [ReadOnly] public bool isFastSliding = false;

                void Start()
                {
                    RB = this.GetComponent<Rigidbody2D>();
                    wallCheck.SetOnTriggerEnter2DCallback(OnWallCheckTrigger);
                    wallCheck.SetOnTriggerExit2DCallback(OnWallCheckExit);
                    groundCheck.SetOnTriggerEnter2DCallback(OnGroundCheckTrigger);
                }


                void Update()
                {
                    // Fast sliding
                    if (Input.GetKeyDown(KeyCode.S))
                    {
                        isFastSliding = true;
                    }
                    else if (Input.GetKeyUp(KeyCode.S))
                    {
                        isFastSliding = false;
                    }
                }

                void FixedUpdate()
                {
                    if (wallCheck.IsWalled())
                    {
                        // - Sticking to the Wall
                        WallSlide();
                    }
                }
                private void WallSlide()
                {
                    if (playerMovement.horizontalMovement != 0f)
                    {
                        isWallSliding = true;
                        if (!isFastSliding)
                        {
                            RB.velocity = new Vector2(RB.velocity.x, Mathf.Clamp(RB.velocity.y, -wallSlidingSpeed, float.MaxValue));
                        }
                    }
                    else
                    {
                        isWallSliding = false;
                    }
                }

                // Ground Check
                private void OnGroundCheckTrigger()
                {
                    isFastSliding = false;
                    isWallSliding = false;
                }

                // Wall Check
                private void OnWallCheckTrigger()
                {
                    ANIMATOR.SetBool("isWalled", true);
                }

                private void OnWallCheckExit()
                {
                    ANIMATOR.SetBool("isWalled", false);
                    isFastSliding = false;
                    isWallSliding = false;
                }

            }
        }
    }
}