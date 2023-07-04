using UnityEngine;
using UnityEngine.UI;
using my2DGame.events;
using my2DGame.tool;

namespace my2DGame
{
    namespace ui
    {
        public class HealthBar : MonoBehaviour
        {
            [SerializeField] private Slider sliderHealthBarFillFront;
            [SerializeField] private Slider sliderHealthBarFillBack;
            [ReadOnly] private int currentHealth;
            private int previousHealth;
            [SerializeField] private int healthUpdateUnit;
            [SerializeField] private Gradient gradientFrontColor;
            [SerializeField] private Image healthBarFillFront;
            [SerializeField] private Image healthBarFillBack;

            private void FixedUpdate()
            {
                if (currentHealth < previousHealth)
                {
                    // Case decrease Health
                    sliderHealthBarFillBack.value -= healthUpdateUnit;
                    previousHealth -= healthUpdateUnit; 
                    if (currentHealth > previousHealth)
                    {
                        sliderHealthBarFillBack.value = currentHealth;
                        previousHealth = currentHealth;
                    }
                } else if (currentHealth > previousHealth)
                {
                    // Case increase Health
                    sliderHealthBarFillFront.value += healthUpdateUnit;
                    previousHealth += healthUpdateUnit;
                    if (currentHealth < previousHealth)
                    {
                        sliderHealthBarFillBack.value = currentHealth;
                        previousHealth = currentHealth;
                    }
                }
            }

            public void SetMaxHealth(int maxHealth)
            {
                sliderHealthBarFillFront.maxValue = maxHealth;
                sliderHealthBarFillFront.value = maxHealth;
                sliderHealthBarFillBack.maxValue = maxHealth;
                sliderHealthBarFillBack.value = maxHealth;
                currentHealth = maxHealth;
                previousHealth = maxHealth;
                healthBarFillFront.color = gradientFrontColor.Evaluate(1f);
            }

            public void SetHealth(int health)
            {
                previousHealth = currentHealth;
                currentHealth = health;
                if (currentHealth < previousHealth)
                {
                    // Case decrease Health
                    sliderHealthBarFillFront.value = currentHealth;
                    sliderHealthBarFillBack.value = previousHealth;
                    healthBarFillFront.color = gradientFrontColor.Evaluate(sliderHealthBarFillFront.normalizedValue);
                    healthBarFillBack.color = Color.red;
                }
                else
                {
                    // Case increase Health
                    sliderHealthBarFillBack.value = currentHealth;
                    sliderHealthBarFillFront.value = previousHealth;
                    healthBarFillFront.color = gradientFrontColor.Evaluate(sliderHealthBarFillBack.normalizedValue);
                    healthBarFillBack.color = Color.green;
                }
            }
        }
    }
}
