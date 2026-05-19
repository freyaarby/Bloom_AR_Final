using UnityEngine;
using UnityEngine.UI;

public class DestroyObject : MonoBehaviour
{
    public GameObject flowerObject;

    public Slider progressBar;

    public GameObject releasePanel;
    public GameObject resultPanel;

    private int tapCount = 0;
    private int maxTap = 5;

    void Update()
    {
        if (PlaceObject.spawnedObject == null)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject == PlaceObject.spawnedObject)
                {
                    tapCount++;

                    if (progressBar != null)
                    {
                        progressBar.value = (float)tapCount / maxTap;
                    }

                    if (tapCount >= maxTap)
                    {
                        ReleaseHealing();
                    }
                }
            }
        }
    }

    public void ReleaseHealing()
    {
        Vector3 pos = PlaceObject.spawnedObject.transform.position;

        Destroy(PlaceObject.spawnedObject);

        if (flowerObject != null)
        {
            flowerObject.transform.position = pos;
            flowerObject.SetActive(true);
        }

        releasePanel.SetActive(false);

        resultPanel.SetActive(true);
    }
}