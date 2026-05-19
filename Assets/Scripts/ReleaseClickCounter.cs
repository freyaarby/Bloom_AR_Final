using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ReleaseClickCounter : MonoBehaviour
{
    [Header("Settings")]
    public int requiredClicks = 5;

    [Header("UI")]
    public TMP_Text instructionText;
    public Slider progressBar;

    [Header("Flow Manager")]
    public UIFlowManager uiFlowManager;

    [Header("Fallback Panels")]
    public GameObject releasePanel;
    public GameObject resultPanel;

    private int currentClicks = 0;

    public void OnReleaseButtonClicked()
    {
        currentClicks++;

        Debug.Log("Release click: " + currentClicks);

        if (progressBar != null)
        {
            progressBar.value = currentClicks;
        }

        if (instructionText != null)
        {
            int remainingClicks = requiredClicks - currentClicks;

            if (remainingClicks > 0)
            {
                instructionText.text = "Keep releasing...\n" + remainingClicks + " more taps";
            }
            else
            {
                instructionText.text = "Released.";
            }
        }

        if (currentClicks >= requiredClicks)
        {
            // Ini bagian terpenting:
            // hapus awan dulu sebelum pindah ke halaman result.
            PlaceObject.ClearSpawnedObject();

            currentClicks = 0;

            if (uiFlowManager != null)
            {
                uiFlowManager.OpenResultPanel();
            }
            else
            {
                if (releasePanel != null)
                    releasePanel.SetActive(false);

                if (resultPanel != null)
                    resultPanel.SetActive(true);

                Debug.LogWarning("UIFlowManager belum di-drag. Pakai fallback panel.");
            }
        }
    }

    public void ResetCounter()
    {
        currentClicks = 0;

        if (progressBar != null)
        {
            progressBar.value = 0;
        }

        if (instructionText != null)
        {
            instructionText.text = "Swipe or hold\nto dissolve the object.";
        }
    }
}