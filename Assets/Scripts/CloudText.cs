using UnityEngine;
using TMPro;

public class CloudText : MonoBehaviour
{
    public TextMeshPro textDisplay;

    void Start()
    {
        textDisplay.text = StressData.stressText;
    }
}