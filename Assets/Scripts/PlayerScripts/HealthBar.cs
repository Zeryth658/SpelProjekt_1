using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    [SerializeField] private TMP_Text hpIndicator;

    public void SetValue(float health, float maxHealth)
    {
        slider.maxValue = maxHealth;
        slider.value = health;

        fill.color = gradient.Evaluate(1f);

        hpIndicator.SetText($"{health}/{maxHealth}");
    }

    public void SetNewValue(float health, float maxHealth)
    {
        slider.value = health;

        fill.color = gradient.Evaluate(slider.normalizedValue);

        hpIndicator.SetText($"{health}/{maxHealth}");
    }
}
