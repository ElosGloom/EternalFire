using TMPro;
using UnityEngine;

namespace Game.Scripts.GUI
{
    public class HealthBarWithText : HealthBar
    {
        [SerializeField] private TextMeshProUGUI hpText;

        protected override void OnHealthChanged()
        {
            hpText.text = $"{HealthComponent.CurrentHealth:0}/{HealthComponent.MaxHealth}";
            base.OnHealthChanged();
        }
    }
}