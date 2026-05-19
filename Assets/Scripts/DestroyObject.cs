using UnityEngine;
using UnityEngine.UI;

public class DestroyObject : MonoBehaviour
{
    public GameObject cloudObject;
    public GameObject flowerObject;

    public Slider progressBar;

    public GameObject releasePanel;
    public GameObject resultPanel;

    private int tapCount = 0;
    private int maxTap = 5;

    void Update()
    {
        if (cloudObject == null)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            tapCount++;

            progressBar.value = (float)tapCount / maxTap;

            if (tapCount >= maxTap)
            {
                ReleaseHealing();
            }
        }
    }

    public void ReleaseHealing()
    {
        Vector3 pos = cloudObject.transform.position;

        Destroy(cloudObject);

        flowerObject.transform.position = pos;
        flowerObject.SetActive(true);

        releasePanel.SetActive(false);

        resultPanel.SetActive(true);
    }
}