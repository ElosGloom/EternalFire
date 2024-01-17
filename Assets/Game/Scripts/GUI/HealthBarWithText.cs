using TMPro;
using UnityEngine;

namespace Game.Scripts.GUI
{
    public class HealthBarWithText : HealthBar
    {
        [SerializeField] private TextMeshProUGUI hpText;

        protected override void OnHealthChanged(float currentHealth, float maxHealth)
        {
            hpText.text = $"{currentHealth:0}/{maxHealth}";
            base.OnHealthChanged(currentHealth, maxHealth);
        }
    }
}