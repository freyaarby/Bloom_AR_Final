using UnityEngine;

public class PlaceObject : MonoBehaviour
{
    public GameObject objectToPlace;

    public GameObject placePanel;
    public GameObject releasePanel;

    public static GameObject spawnedObject;

    private bool hasSpawned = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !hasSpawned)
        {
            Vector3 spawnPos =
                Camera.main.transform.position +
                Camera.main.transform.forward * 1.5f;

            spawnedObject = Instantiate(
                objectToPlace,
                spawnPos,
                Quaternion.identity
            );

            hasSpawned = true;

            placePanel.SetActive(false);
            releasePanel.SetActive(true);
        }
    }
}