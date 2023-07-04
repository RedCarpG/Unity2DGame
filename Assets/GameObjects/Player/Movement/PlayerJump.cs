using UnityEngine;

using my2DGame.helper;
using my2DGame.tool;

namespace my2DGame
{
    namespace player
    {
        namespace movement
        {
            public class PlayerJump : MonoBehaviour
            {
                [Header("")]
                [SerializeField] private PlayerMovement playerMovement;
                [SerializeField] private GroundCheck groundCheck;
                [SerializeField] private WallCheck wallCheck;
                // Rigid Body
                [SerializeField] private Rigidbody2D RB;
                // Animation
                [SerializeField] private Animator ANIMATOR;

                [Header("Jump")]
                // Jump
                [SerializeField] private float jumpForce = 1000;
                [SerializeField] private float gravityScale = 5;
                [SerializeField] private float wallJumpXForce = 1200;
                [SerializeField] private float wallJumpYForce = 600;

                private float _jumpCounter = 0;
                private readonly float JUMP_INTERVAL = 0.2f;

                // Jump Hang
                private readonly float jumpHangThreshold = 1;
                private readonly float JUMP_HANG_GRAVITY_MULT = 0.5f;
                // Jump Interruption
                private bool _releaseJumpButton = false;
                private readonly float RELEASE_JUMPBUTTON_VELOCITY_OFFSET = 0.5f;
                private readonly float fallingMaxVelocityThreashold = -15;

                [Header("ReadOnly")]
                [ReadOnly] public bool isJumpping = false;
                [ReadOnly] public bool isWallJumpping = false;
                [ReadOnly] public bool isFalling = false;


                // Other
                private Vector3 VELOCITY0 = Vector3.zero;

                void Start()
                {
                    RB = this.GetComponent<Rigidbody2D>();
                    wallCheck.SetOnTriggerEnter2DCallback(OnWallCheckTrigger);
                    groundCheck.SetOnTriggerEnter2DCallback(OnGroundCheckTrigger);
                }

                void Update()
                {
                    // Jump
                    if (Input.GetKeyDown(KeyCode.W))
                    {
                        Jump();
                    }
                    // For jump interruption
                    else if (Input.GetKeyUp(KeyCode.W) && !groundCheck.IsGrounded() && !wallCheck.IsWalled())
                    {
                        _releaseJumpButton = true;
                    }
                    _jumpCounter -= Time.deltaTime;
                }

                void FixedUpdate()
                {
                    if (isJumpping || isWallJumpping)
                    {
                        // - Jumping up
                        UpdateJumpHangTime();
                        if (isJumpping)
                        {
                            UpdateJumpInterrupt();
                        }
                    }
                    if (FallingCheck())
                    {
                        // - Falling down
                        UpdateFallingVelocityLock();
                    }
                }

                private void Jump()
                {
                    if (_jumpCounter > 0f)
                    {
                        // Avoid spaming jump button
                        return;
                    }
                    if (groundCheck.IsGrounded())
                    {
                        // Ground jumpping
                        isJumpping = true;
                        RB.AddForce(new Vector2(0f, jumpForce));
                    }
                    else if (wallCheck.IsWalled())
                    {
                        // Wall jumpping
                        isWallJumpping = true;
                        RB.AddForce(new Vector2(-playerMovement.facingDirection * wallJumpXForce, wallJumpYForce));
                    }
                    _jumpCounter = JUMP_INTERVAL;
                    ANIMATOR.SetBool("isJumpping", isJumpping);
                }

                private bool FallingCheck()
                {
                    isFalling = RB.velocity.y < -1;
                    if (isFalling)
                    {
                        isJumpping = false;
                        isWallJumpping = false;
                        ANIMATOR.SetBool("isJumpping", false);
                    }
                    return isFalling;
                }

                private void UpdateJumpHangTime()
                {
                    if (Mathf.Abs(RB.velocity.y) < jumpHangThreshold)
                    {
                        RB.gravityScale = gravityScale * JUMP_HANG_GRAVITY_MULT;
                    }
                    else
                    {
                        RB.gravityScale = gravityScale;
                    }
                }

                private void UpdateJumpInterrupt()
                {
                    if (_releaseJumpButton)
                    {
                        if (RB.velocity.y > jumpHangThreshold)
                        {
                            RB.gravityScale = gravityScale * Mathf.Abs(RB.velocity.y) * RELEASE_JUMPBUTTON_VELOCITY_OFFSET;
                        }
                        else
                        {
                            RB.gravityScale = gravityScale;
                            _releaseJumpButton = false;
                        }
                    }
                }

                private void UpdateFallingVelocityLock()
                {
                    if (RB.velocity.y < fallingMaxVelocityThreashold)
                    {
                        Vector3 aTargetVelocity = new Vector2(RB.velocity.x, fallingMaxVelocityThreashold);
                        RB.velocity = Vector3.SmoothDamp(RB.velocity, aTargetVelocity, ref VELOCITY0, 0.01f);
                    }
                }


                // Ground Check
                private void OnGroundCheckTrigger()
                {
                    ANIMATOR.SetBool("isJumpping", false);
                    isFalling = false;
                    isJumpping = false;
                    isWallJumpping = false;
                    RB.gravityScale = gravityScale;
                }

                // Wall Check
                private void OnWallCheckTrigger()
                {
                    ANIMATOR.SetBool("isJumpping", false);
                    isFalling = false;
                    isJumpping = false;
                    isWallJumpping = false;
                    RB.gravityScale = gravityScale;
                }
            }
        }
    }
}