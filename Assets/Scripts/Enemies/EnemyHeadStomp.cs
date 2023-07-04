using UnityEngine;

using my2DGame.helper;
using my2DGame.tool;
using System.Collections;

namespace my2DGame
{
    namespace enemy
    {
        public class EnemyHeadStomp : MonoBehaviour
        {
            [SerializeField] private EnemyDeath enemyDeath;
            [SerializeField] private float reverseForce = 1000;

            private void OnTriggerEnter2D(Collider2D collision)
            {
                if (collision.gameObject.CompareTag("Player"))
                {
                    collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, reverseForce));
                    this.GetComponent<Collider2D>().enabled = false;
                    enemyDeath.FallDeath();
                }
            }
        }
    }
}
