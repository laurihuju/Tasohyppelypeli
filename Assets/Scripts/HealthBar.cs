using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image filler;
    [SerializeField] private Color colorFull;
    [SerializeField] private Color colorHalf;
    [SerializeField] private Color colorEmpty;

    private Slider slider;

    private float maxHealth;

    public void Initialize(float maxHealth)
    {
        slider = GetComponent<Slider>();

        filler.color = colorFull;

        slider.maxValue = maxHealth;
        slider.value = maxHealth;

        this.maxHealth = maxHealth;
    }

    public void SetHealth(float health)
    {
        slider.value = health;

        filler.color = GetHealthBarColor(health);
    }

    private Color GetHealthBarColor(float health)
    {
        if (health / maxHealth > 2f / 3)
        {
            return colorFull;
        }
        else if (health / maxHealth > 1f / 3)
        {
            return colorHalf;
        }
        else
        {
            return colorEmpty;
        }
    }
}
