using UnityEngine;

public class PlaceObject : MonoBehaviour
{
    [Header("Object")]
    public GameObject objectToPlace;

    [Header("Panels")]
    public GameObject placePanel;
    public GameObject releasePanel;

    public static GameObject spawnedObject;

    private bool hasSpawned = false;

    void Update()
    {
        if (placePanel != null && !placePanel.activeInHierarchy)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0) && !hasSpawned)
        {
            if (objectToPlace == null)
            {
                Debug.LogWarning("Object To Place belum diisi di Inspector ARManager.");
                return;
            }

            if (Camera.main == null)
            {
                Debug.LogWarning("Main Camera tidak ditemukan.");
                return;
            }

            Vector3 spawnPos =
                Camera.main.transform.position +
                Camera.main.transform.forward * 1.5f;

            spawnedObject = Instantiate(
                objectToPlace,
                spawnPos,
                Quaternion.identity
            );

            spawnedObject.name = "SpawnedManifestation";
            spawnedObject.SetActive(true);

            hasSpawned = true;

            if (placePanel != null)
                placePanel.SetActive(false);

            if (releasePanel != null)
                releasePanel.SetActive(true);

            Debug.Log("Object spawned: " + spawnedObject.name);
        }
    }

    public void ResetPlacement()
    {
        hasSpawned = false;
        ClearSpawnedObject();
    }

    public static void ClearSpawnedObject()
    {
        if (spawnedObject != null)
        {
            spawnedObject.SetActive(false);
            Object.Destroy(spawnedObject);
            spawnedObject = null;

            Debug.Log("Main spawned manifestation destroyed.");
        }

        GameObject[] allObjects =
            Object.FindObjectsByType<GameObject>(FindObjectsSortMode.None);

        foreach (GameObject obj in allObjects)
        {
            if (obj.name.StartsWith("SpawnedManifestation") ||
                obj.name.StartsWith("CloudObject"))
            {
                obj.SetActive(false);
                Object.Destroy(obj);

                Debug.Log("Destroyed leftover manifestation: " + obj.name);
            }
        }
    }
}