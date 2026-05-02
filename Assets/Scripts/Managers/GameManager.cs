using UnityEngine;
using UnityEngine.SceneManagement;

// Subscribes to Observer events from EnergyCore and WaveManager.
// Triggers win/lose screens without polling or direct references to those systems.
public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject losePanel;

    private bool gameEnded;

    private void OnEnable()
    {
        EnergyCore.OnCoreDestroyed    += HandleLose;
        WaveManager.OnAllWavesCleared += HandleWin;
    }

    private void OnDisable()
    {
        EnergyCore.OnCoreDestroyed    -= HandleLose;
        WaveManager.OnAllWavesCleared -= HandleWin;
    }

    private void HandleWin()
    {
        if (gameEnded) return;
        gameEnded = true;
        Time.timeScale = 0f;
        winPanel?.SetActive(true);
    }

    private void HandleLose()
    {
        if (gameEnded) return;
        gameEnded = true;
        Time.timeScale = 0f;
        losePanel?.SetActive(true);
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
