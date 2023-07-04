using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using my2DGame.helper;

namespace my2DGame
{
    namespace player
    {
        namespace movement
        {
            public class PlayerOneWayPlatform : MonoBehaviour
            {
                [Header("")]
                [SerializeField] private GroundCheck groundCheck;
                [SerializeField] private LayerMask platformLayer;

                private Collider2D[] playerColliders;
                private readonly List<Collider2D> currentPlatformColliders = new();
                private ContactFilter2D contactFilter;

                private void Start()
                {
                    playerColliders = this.GetComponents<Collider2D>();
                    contactFilter.SetLayerMask(platformLayer);
                }

                void Update()
                {
                    if (Input.GetKeyDown(KeyCode.S) && groundCheck.IsGrounded())
                    {
                        groundCheck.GetComponent<Collider2D>().OverlapCollider(contactFilter, currentPlatformColliders);
                        if (currentPlatformColliders.Count > 0)
                        {
                            StartCoroutine(DisableCollision());
                        }
                    }
                }

                private IEnumerator DisableCollision()
                {
                    foreach (var aPlatformColl in currentPlatformColliders)
                    {
                        foreach (var aPlayerColl in playerColliders)
                        {
                            Physics2D.IgnoreCollision(aPlatformColl, aPlayerColl);
                        }
                    }
                    yield return new WaitForSeconds(0.25f);
                    foreach (var aPlatformColl in currentPlatformColliders)
                    {
                        foreach (var aPlayerColl in playerColliders)
                        {
                            Physics2D.IgnoreCollision(aPlatformColl, aPlayerColl, false);
                        }
                    }
                }
            }
        }
    }
}