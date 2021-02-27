using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShieldBarUI : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] TMP_Text text;
    [SerializeField] GameObject UI;

    PlayerStats stats;

    private void Start()
    {
        stats = PlayerManager.instance.player.GetComponent<PlayerStats>();
        stats.onShieldChangedCallback += OnShieldChanged;
        float maxShield = stats.GetShield().GetValue(); ;
        slider.maxValue = maxShield;
        text.text = maxShield.ToString();
        stats.GetShield().onStatChangeCallback += OnMaxShieldChanged;
        if (stats.GetCurrentCore() == null)
        {
            UI.SetActive(false);
        }
    }

    private void OnShieldChanged(float currentShield)
    {
        text.text = Mathf.FloorToInt(currentShield).ToString();
        if(slider.maxValue < currentShield)
        {
            slider.maxValue = currentShield;
        }
        slider.value = currentShield;
    }

    private void OnMaxShieldChanged(Stat maxShield)
    {
        slider.maxValue = maxShield.GetValue();
        if (stats.GetCurrentCore())
        {
            UI.SetActive(true);
        }
        else
        {
            UI.SetActive(false);
        }
    }
}
