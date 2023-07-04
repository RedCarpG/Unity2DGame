using UnityEngine;

using my2DGame.helper;

namespace my2DGame
{
    namespace player
    {
        namespace particle
        {
            public class PlayerParticleController : MonoBehaviour
            {
                [Header("")]
                [SerializeField] private GroundCheck groundCheck;
                [SerializeField] private WallCheck wallCheck;
                [SerializeField] private Rigidbody2D playerRB;

                [Header("Movement Particles")]
                [SerializeField] private ParticleSystem movementParticles;
                [Range(0, 10)]
                [SerializeField] private int vXThreshold;
                [Range(0, 0.2f)]
                [SerializeField] private float dustFormationPeriod;

                [Header("Fall Particles")]
                [SerializeField] private ParticleSystem fallParticles;

                [Header("Wall Hit Particles")]
                [SerializeField] private ParticleSystem wallHitParticles;

                float counter;
                // Start is called before the first frame update
                void Start()
                {
                    groundCheck.SetOnTriggerEnter2DCallback(OnHitGround);
                    wallCheck.SetOnTriggerEnter2DCallback(OnHitWall);
                }

                // Update is called once per frame
                void Update()
                {
                    counter += Time.deltaTime;
                    if ((Mathf.Abs(playerRB.velocity.x) > vXThreshold) && groundCheck.IsGrounded())
                    {
                        if (counter > dustFormationPeriod)
                        {
                            movementParticles.Play();
                            counter = 0;
                        }
                    }
                }

                private void OnHitGround()
                {
                    if (playerRB.velocity.y < 0)
                    {
                        fallParticles.Play();
                    }
                }

                private void OnHitWall()
                {
                    wallHitParticles.Play();
                }
            }
        }
    }
}