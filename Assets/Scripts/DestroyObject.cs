using UnityEngine;
using TMPro;

public class DestroyObject : MonoBehaviour
{
    [Header("Optional AR Flower")]
    public GameObject flowerObject;

    [Header("UI")]
    public TMP_Text instructionText;

    [Header("Panels")]
    public GameObject releasePanel;
    public GameObject resultPanel;

    private int tapCount = 0;
    private int maxTap = 5;

    void Start()
    {
        ResetRelease();
    }

    public void OnReleaseButtonClicked()
    {
        if (PlaceObject.spawnedObject == null)
        {
            Debug.LogWarning("Belum ada object AR yang di-place.");
            return;
        }

        tapCount++;

        Debug.Log("Release tap: " + tapCount);

        if (instructionText != null)
        {
            int remainingTap = maxTap - tapCount;

            if (remainingTap > 0)
            {
                instructionText.text = "Keep releasing...\n" + remainingTap + " more taps";
            }
            else
            {
                instructionText.text = "Released.";
            }
        }

        if (tapCount >= maxTap)
        {
            ReleaseHealing();
        }
    }

    public void ReleaseHealing()
    {
        Vector3 pos = PlaceObject.spawnedObject.transform.position;

        Destroy(PlaceObject.spawnedObject);
        PlaceObject.spawnedObject = null;

        // Kalau bunga kamu ada di ResultPanel UI, flowerObject boleh dikosongkan.
        if (flowerObject != null)
        {
            flowerObject.transform.position = pos;
            flowerObject.SetActive(true);
        }

        if (releasePanel != null)
            releasePanel.SetActive(false);

        if (resultPanel != null)
            resultPanel.SetActive(true);

        ResetRelease();
    }

    public void ResetRelease()
    {
        tapCount = 0;

        if (instructionText != null)
        {
            instructionText.text = "Swipe or hold\nto dissolve the object.";
        }

        if (flowerObject != null)
        {
            flowerObject.SetActive(false);
        }
    }
}