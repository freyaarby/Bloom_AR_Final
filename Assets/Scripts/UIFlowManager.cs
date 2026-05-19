using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIFlowManager : MonoBehaviour
{
    public ReleaseClickCounter releaseClickCounter;

    [Header("Result Objects")]
    public GameObject flowerBloom;

    [Header("AR Objects")]
    public GameObject arSession;
    public GameObject xrOrigin;
    public GameObject arManager;

    [Header("Panels")]
    public GameObject splashScreenPanel;
    public GameObject disclaimerPanel;
    public GameObject stressInputPanel;
    public GameObject chooseManifestationPanel;
    public GameObject scanPanel;
    public GameObject placePanel;
    public GameObject releasePanel;
    public GameObject resultPanel;
    public GameObject affirmationResultPanel;

    [Header("Stress Input")]
    public TMP_InputField stressInputField;

    [Header("Manifestation Cards")]
    public Image darkCloudCardImage;
    public Image heavyRockCardImage;
    public Image crumpledPaperCardImage;

    [Header("Manifestation Toggles")]
    public Toggle darkCloudToggle;
    public Toggle heavyRockToggle;
    public Toggle crumpledPaperToggle;

    private string selectedManifestation = "Dark Cloud";

    private Color selectedCardColor;
    private Color normalCardColor;

    void Awake()
    {
        ColorUtility.TryParseHtmlString("#E6E6FA", out selectedCardColor);
        ColorUtility.TryParseHtmlString("#FFFFFF", out normalCardColor);
    }

    void Start()
    {
        if (arSession != null) arSession.SetActive(false);
        if (xrOrigin != null) xrOrigin.SetActive(false);
        if (arManager != null) arManager.SetActive(false);

        OpenSplashScreenPanel();
        SelectDarkCloud();
        StartCoroutine(AutoMoveFromSplash());
    }

    private void HideAllPanels()
    {
        if (splashScreenPanel != null) splashScreenPanel.SetActive(false);
        if (disclaimerPanel != null) disclaimerPanel.SetActive(false);
        if (stressInputPanel != null) stressInputPanel.SetActive(false);
        if (chooseManifestationPanel != null) chooseManifestationPanel.SetActive(false);
        if (scanPanel != null) scanPanel.SetActive(false);
        if (placePanel != null) placePanel.SetActive(false);
        if (releasePanel != null) releasePanel.SetActive(false);
        if (resultPanel != null) resultPanel.SetActive(false);
        if (affirmationResultPanel != null) affirmationResultPanel.SetActive(false);
    }

    public void OpenSplashScreenPanel()
    {
        HideAllPanels();
        if (splashScreenPanel != null) splashScreenPanel.SetActive(true);
    }

    public void OpenDisclaimerPanel()
    {
        HideAllPanels();
        if (disclaimerPanel != null) disclaimerPanel.SetActive(true);
    }

    public void OpenStressInputPanel()
    {
        HideAllPanels();
        if (stressInputPanel != null) stressInputPanel.SetActive(true);
    }

    public void OpenManifestationPanel()
    {
        SaveStressInput();

        HideAllPanels();
        if (chooseManifestationPanel != null) chooseManifestationPanel.SetActive(true);
    }

    public void OpenScanPanel()
    {
        SaveManifestation();
    
        if (arSession != null) arSession.SetActive(true);
        if (xrOrigin != null) xrOrigin.SetActive(true);
        if (arManager != null) arManager.SetActive(true);
    
        HideAllPanels();
    
        if (scanPanel != null)
        {
            scanPanel.SetActive(true);
        }
    }

    public void OpenPlacePanel()
    {
        HideAllPanels();
        if (placePanel != null) placePanel.SetActive(true);
    }

    public void OpenReleasePanel()
    {
        HideAllPanels();

        if (releaseClickCounter != null)
        {
            releaseClickCounter.ResetCounter();
        }

        if (releasePanel != null)
        {
            releasePanel.SetActive(true);
        }
    }

    public void OpenResultPanel()
{
    HideAllPanels();

    if (resultPanel != null)
        resultPanel.SetActive(true);

    if (flowerBloom != null)
    {
        flowerBloom.SetActive(true);
        StartCoroutine(GrowFlower());
    }

        StartCoroutine(AutoMoveToAffirmationResult());
    }

    private IEnumerator GrowFlower()
    {
        flowerBloom.transform.localScale = Vector3.zero;

        float duration = 2f;
        float timer = 0f;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            float progress = timer / duration;

            flowerBloom.transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, progress);

            yield return null;
        }

        flowerBloom.transform.localScale = Vector3.one;
    }

    public void OpenAffirmationResultPanel()
    {
        HideAllPanels();
        if (affirmationResultPanel != null) affirmationResultPanel.SetActive(true);
    }

    public void StartAnotherBloom()
    {
        if (stressInputField != null)
        {
            stressInputField.text = "";
        }

        SelectDarkCloud();

        HideAllPanels();

        if (stressInputPanel != null)
        {
            stressInputPanel.SetActive(true);
        }
    }

    public void SaveStressInput()
    {
        if (stressInputField == null)
        {
            Debug.LogWarning("Stress input field is not assigned.");
            return;
        }

        StressData.stressText = stressInputField.text;
        Debug.Log("Stress Saved: " + StressData.stressText);
    }

    public void SelectDarkCloud()
    {
        SelectManifestation("Dark Cloud");
    }

    public void SelectHeavyRock()
    {
        SelectManifestation("Heavy Rock");
    }

    public void SelectCrumpledPaper()
    {
        SelectManifestation("Crumpled Paper");
    }

    private IEnumerator AutoMoveFromSplash()
    {
        yield return new WaitForSeconds(2f);
        OpenDisclaimerPanel();
    }

    private IEnumerator AutoMoveToAffirmationResult()
    {
        yield return new WaitForSeconds(5f);
        OpenAffirmationResultPanel();
    }

    private void SelectManifestation(string manifestation)
    {
        selectedManifestation = manifestation;
        StressData.manifestationType = selectedManifestation;

        if (darkCloudToggle != null)
            darkCloudToggle.SetIsOnWithoutNotify(manifestation == "Dark Cloud");

        if (heavyRockToggle != null)
            heavyRockToggle.SetIsOnWithoutNotify(manifestation == "Heavy Rock");

        if (crumpledPaperToggle != null)
            crumpledPaperToggle.SetIsOnWithoutNotify(manifestation == "Crumpled Paper");

        if (darkCloudCardImage != null)
            darkCloudCardImage.color = manifestation == "Dark Cloud" ? selectedCardColor : normalCardColor;

        if (heavyRockCardImage != null)
            heavyRockCardImage.color = manifestation == "Heavy Rock" ? selectedCardColor : normalCardColor;

        if (crumpledPaperCardImage != null)
            crumpledPaperCardImage.color = manifestation == "Crumpled Paper" ? selectedCardColor : normalCardColor;

        Debug.Log("Manifestation Selected: " + StressData.manifestationType);
    }

    private void SaveManifestation()
    {
        StressData.manifestationType = selectedManifestation;
        Debug.Log("Manifestation Saved: " + StressData.manifestationType);
    }
}