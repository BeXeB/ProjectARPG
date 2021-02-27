using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] TMP_Text text;

    private void Start()
    {
        PlayerStats.onHelathChangedCallback += OnHealthChanged;
        PlayerStats stats = PlayerManager.instance.player.GetComponent<PlayerStats>();
        float maxHealth = stats.GetMaxHealth().GetValue();
        slider.maxValue = maxHealth;
        slider.value = maxHealth;
        text.text = maxHealth.ToString();
        stats.GetMaxHealth().onStatChangeCallback += OnMaxHealthChanged;
    }

    private void OnHealthChanged(GameObject go, float currentHealt, float maxHealth)
    {
        if (go == PlayerManager.instance.player)
        {
            slider.value = currentHealt;
            text.text = Mathf.FloorToInt(currentHealt).ToString();
        }
    }

    private void OnMaxHealthChanged(Stat maxHealth)
    {
        slider.maxValue = maxHealth.GetValue();
    }
}
