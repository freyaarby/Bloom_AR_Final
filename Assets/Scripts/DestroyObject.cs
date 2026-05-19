using UnityEngine;
using UnityEngine.UI;

public class DestroyObject : MonoBehaviour
{
    // fallback manual jika diperlukan
    public GameObject cloudObject;

    public GameObject flowerObject;

    public Slider progressBar;

    public GameObject releasePanel;
    public GameObject resultPanel;

    private int tapCount = 0;
    private int maxTap = 5;

    void Update()
    {
        // Prioritas object hasil spawn AR
        GameObject targetObject = PlaceObject.spawnedObject;

        // fallback kalau belum ada
        if (targetObject == null)
        {
            targetObject = cloudObject;
        }

        // kalau tetap null, stop
        if (targetObject == null)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            tapCount++;

            // update progress bar
            if (progressBar != null)
            {
                progressBar.value = (float)tapCount / maxTap;
            }

            // release setelah 5 tap
            if (tapCount >= maxTap)
            {
                ReleaseHealing(targetObject);
            }
        }
    }

    public void ReleaseHealing(GameObject targetObject)
    {
        // ambil posisi awan
        Vector3 pos = targetObject.transform.position;

        // destroy awan
        Destroy(targetObject);

        // reset static object
        if (PlaceObject.spawnedObject == targetObject)
        {
            PlaceObject.spawnedObject = null;
        }

        // munculkan bunga
        if (flowerObject != null)
        {
            flowerObject.transform.position = pos;
            flowerObject.SetActive(true);
        }

        // ganti panel
        if (releasePanel != null)
        {
            releasePanel.SetActive(false);
        }

        if (resultPanel != null)
        {
            resultPanel.SetActive(true);
        }
    }
}