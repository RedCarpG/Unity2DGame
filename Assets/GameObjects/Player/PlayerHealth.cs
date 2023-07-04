using my2DGame.ui;
using UnityEngine;
using my2DGame.events;

namespace my2DGame
{
    namespace player
    {
        public class PlayerHealth : MonoBehaviour
        {
            [SerializeField] private int  maxHealth = 100;
            [SerializeField] private int currentHealth;
            public int CurrentHealth
            { 
                get { return currentHealth; } 
                set {
                    if (value > maxHealth)
                    {
                        currentHealth = maxHealth;
                    } else if (value < 0) {
                        currentHealth = 0;
                    } else
                    {
                        currentHealth = value;
                    }
                   } 
            }

            [SerializeField] private HealthBar healthBar;

            private void Start()
            {
                currentHealth = maxHealth;
                healthBar.SetMaxHealth(currentHealth);
                EventManager.PlayerHurtEvent += TakeDamage;
                EventManager.PlayerHealEvent += TakeHeal;
            }
            private void OnDisable()
            {
                EventManager.PlayerHurtEvent -= TakeDamage;
                EventManager.PlayerHealEvent -= TakeHeal;
            }

            private void ResetHealth()
            {
                CurrentHealth = maxHealth;
                healthBar.SetHealth(currentHealth);
            }

            private void TakeDamage(int damage)
            {
                CurrentHealth -= damage;
                healthBar.SetHealth(currentHealth);
            }
            private void TakeHeal(int healing)
            {
                CurrentHealth += healing;
                healthBar.SetHealth(currentHealth);
            }

        }

    }
}
