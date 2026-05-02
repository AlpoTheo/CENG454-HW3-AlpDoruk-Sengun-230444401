using UnityEngine;
using UnityEngine.UI;
using TMPro;

// Subscribes to Observer events from EnergyCore and WaveManager.
// Hides the HUD elements when the run ends so the win/lose panel stays clean.
public class HUDController : MonoBehaviour
{
    [SerializeField] private Slider          coreHealthBar;
    [SerializeField] private TextMeshProUGUI waveLabel;

    private void OnEnable()
    {
        EnergyCore.OnHealthChanged    += RefreshHealth;
        WaveManager.OnWaveStarted     += RefreshWave;
        EnergyCore.OnCoreDestroyed    += HideHud;
        WaveManager.OnAllWavesCleared += HideHud;
    }

    private void OnDisable()
    {
        EnergyCore.OnHealthChanged    -= RefreshHealth;
        WaveManager.OnWaveStarted     -= RefreshWave;
        EnergyCore.OnCoreDestroyed    -= HideHud;
        WaveManager.OnAllWavesCleared -= HideHud;
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

    private void HideHud()
    {
        if (coreHealthBar != null) coreHealthBar.gameObject.SetActive(false);
        if (waveLabel     != null) waveLabel.gameObject.SetActive(false);
    }
}
