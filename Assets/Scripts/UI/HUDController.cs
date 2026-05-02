using UnityEngine;
using UnityEngine.UI;
using TMPro;

// Subscribes to Observer events — updates UI without polling or direct references.
public class HUDController : MonoBehaviour
{
    [SerializeField] private Slider        coreHealthBar;
    [SerializeField] private TextMeshProUGUI waveLabel;

    private void OnEnable()
    {
        EnergyCore.OnHealthChanged  += RefreshHealth;
        WaveManager.OnWaveStarted   += RefreshWave;
    }

    private void OnDisable()
    {
        EnergyCore.OnHealthChanged  -= RefreshHealth;
        WaveManager.OnWaveStarted   -= RefreshWave;
    }

    private void RefreshHealth(float current, float max)
    {
        if (coreHealthBar != null)
            coreHealthBar.value = current / max;
    }

    private void RefreshWave(int wave)
    {
        if (waveLabel != null)
            waveLabel.text = $"Wave {wave}";
    }
}
