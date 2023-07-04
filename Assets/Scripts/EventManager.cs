using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace my2DGame
{
    namespace events
    {
        public class EventManager : MonoBehaviour
        {
            public static event Action<int> PlayerHurtEvent;
            public static event Action<int> PlayerHealEvent;

            // Update is called once per frame
            private void Update()
            {
                if (Input.GetKeyUp(KeyCode.G))
                {
                    PlayerHurt(20);
                }
                if (Input.GetKeyUp(KeyCode.H))
                {
                    PlayerHeal(20);
                }
            }

            public static void PlayerHurt(int value)
            {
                PlayerHurtEvent?.Invoke(value);
            }
            public static void PlayerHeal(int value)
            {
                PlayerHealEvent?.Invoke(value);
            }
        }
    }
}
