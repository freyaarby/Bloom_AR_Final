using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlaceObject : MonoBehaviour
{
    public GameObject objectToPlace;

    public GameObject placePanel;
    public GameObject releasePanel;

    public ARRaycastManager raycastManager;

    public static GameObject spawnedObject;

    static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    void Start()
    {
        // fallback kalau belum di-drag manual
        if (raycastManager == null)
        {
            raycastManager = FindObjectOfType<ARRaycastManager>();
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && raycastManager != null)
        {
            Vector2 touchPosition = Input.mousePosition;

            if (raycastManager.Raycast(
                touchPosition,
                hits,
                TrackableType.PlaneWithinPolygon
            ) && hits.Count > 0)
            {
                Pose hitPose = hits[0].pose;

                if (spawnedObject == null)
                {
                    spawnedObject = Instantiate(
                        objectToPlace,
                        hitPose.position,
                        Quaternion.identity
                    );

                    placePanel.SetActive(false);
                    releasePanel.SetActive(true);
                }
            }
        }
    }
}