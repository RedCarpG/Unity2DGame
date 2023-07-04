using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using my2DGame.tool;

namespace my2DGame
{
    namespace helper
    {
        public class GroundEdgeCheck : MonoBehaviour
        {
            [ReadOnly] private bool _isReachingEdge = false;
            [SerializeField] private LayerMask GROUND_LAYER;

            // Start is called before the first frame update
            void Start()
            {

            }

            // Update is called once per frame
            void Update()
            {
                if (Physics2D.OverlapPoint(this.transform.position, GROUND_LAYER))
                {
                    _isReachingEdge = false;
                }  
                else
                {
                    _isReachingEdge = true;
                }
            }

            public bool IsReachingEdge()
            {
                return _isReachingEdge;
            }
        }

    }
}