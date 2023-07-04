using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace my2DGame
{
    namespace effect
    {
        public class OneTimeEffect : MonoBehaviour
        {
            public void ToDestroy() 
            { 
                Destroy(gameObject); 
            }
        }
    }
}
