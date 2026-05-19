using UnityEngine;
using TMPro;

public class UIFlowManager : MonoBehaviour
{
    public GameObject stressInputPanel;
    public GameObject chooseManifestationPanel;
    public GameObject scanPanel;
    public GameObject placePanel;
    public GameObject releasePanel;
    public GameObject resultPanel;

    public TMP_InputField stressInputField;

    void Start()
    {
        stressInputPanel.SetActive(true);

        chooseManifestationPanel.SetActive(false);
        scanPanel.SetActive(false);
        placePanel.SetActive(false);
        releasePanel.SetActive(false);
        resultPanel.SetActive(false);
    }

    public void OpenManifestationPanel()
    {
        stressInputPanel.SetActive(false);
        chooseManifestationPanel.SetActive(true);
    }

    public void OpenScanPanel()
    {
        chooseManifestationPanel.SetActive(false);
        scanPanel.SetActive(true);
    }

    public void OpenPlacePanel()
    {
        scanPanel.SetActive(false);
        placePanel.SetActive(true);
    }

    public void OpenReleasePanel()
    {
        placePanel.SetActive(false);
        releasePanel.SetActive(true);
    }

    public void OpenResultPanel()
    {
        releasePanel.SetActive(false);
        resultPanel.SetActive(true);
    }

    public void SaveStressInput()
    {
        StressData.stressText = stressInputField.text;

        Debug.Log("Stress Saved: " + StressData.stressText);
    }
}