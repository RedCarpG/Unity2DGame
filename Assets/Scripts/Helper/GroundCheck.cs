using System.Collections.Generic;
using UnityEngine;
using my2DGame.tool;

namespace my2DGame
{
    namespace helper
    {
        public class GroundCheck : MonoBehaviour
        {
            [ReadOnly] private bool _isGrounded = false;
            [SerializeField] private LayerMask JUMPABLE_GROUND_LAYER;

            public delegate void OnTrigger2DCallback(); // This defines what type of method you're going to call.
            private readonly List<OnTrigger2DCallback> m_onTriggerEnter2D = new();
            private readonly List<OnTrigger2DCallback> m_onTriggerExit2D = new();

            public bool IsGrounded()
            { return _isGrounded; }

            private bool isJumpableGroundLayer(int iLayer)
            {
                return JUMPABLE_GROUND_LAYER == (JUMPABLE_GROUND_LAYER | (1 << iLayer));
            }

            private void OnTriggerEnter2D(Collider2D collision)
            {
                if (isJumpableGroundLayer(collision.gameObject.layer))
                {
                    _isGrounded = true;
                    foreach (var m in m_onTriggerEnter2D)
                    {
                        m();
                    }
                }
            }

            private void OnTriggerExit2D(Collider2D collision)
            {
                if (isJumpableGroundLayer(collision.gameObject.layer))
                {
                    _isGrounded = false;
                    foreach (var m in m_onTriggerExit2D)
                    {
                        m();
                    }
                }
            }

            public void SetOnTriggerEnter2DCallback(OnTrigger2DCallback iCallBack)
            {
                Debug.Log("<GroundCheck> SetOnTriggerEnter2DCallback: " + iCallBack);
                m_onTriggerEnter2D.Add(iCallBack);
            }
            public void SetOnTriggerExit2DCallback(OnTrigger2DCallback iCallBack)
            {
                Debug.Log("<GroundCheck> SetOnTriggerExit2DCallback: " + iCallBack);
                m_onTriggerExit2D.Add(iCallBack);
            }
        }
    }
}
