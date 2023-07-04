using System.Collections.Generic;
using UnityEngine;
using my2DGame.tool;

namespace my2DGame
{
    namespace helper
    {
        public class WallCheck : MonoBehaviour
        {
            [ReadOnly] private bool _isWalled = false;

            public delegate void OnTrigger2DCallback(); // This defines what type of method you're going to call.
            private readonly List<OnTrigger2DCallback> m_onTriggerEnter2D = new();
            private readonly List<OnTrigger2DCallback> m_onTriggerExit2D = new();

            public bool IsWalled()
            { return _isWalled; }

            private void OnTriggerEnter2D(Collider2D collision)
            {
                if (collision.gameObject.CompareTag("Ground"))
                {
                    _isWalled = true;
                    foreach (var m in m_onTriggerEnter2D)
                    {
                        m();
                    }
                }
            }

            private void OnTriggerExit2D(Collider2D collision)
            {
                if (collision.gameObject.CompareTag("Ground"))
                {
                    _isWalled = false;
                    foreach (var m in m_onTriggerExit2D)
                    {
                        m();
                    }
                }
            }

            public void SetOnTriggerEnter2DCallback(OnTrigger2DCallback iCallBack)
            {
                Debug.Log("<WallCheck> SetOnTriggerEnter2DCallback: " + iCallBack);
                m_onTriggerEnter2D.Add(iCallBack);
            }
            public void SetOnTriggerExit2DCallback(OnTrigger2DCallback iCallBack)
            {
                Debug.Log("<WallCheck> SetOnTriggerExit2DCallback: " + iCallBack);
                m_onTriggerExit2D.Add(iCallBack);
            }
        }
    }
}