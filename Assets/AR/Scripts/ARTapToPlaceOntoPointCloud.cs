using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.ARFoundation;

public class ARTapToPlaceOntoPointCloud : MonoBehaviour
{
    [SerializeField]
    private GameObject refToPrefab;

    [SerializeField]
    private GameObject anchorPrefab;

    [SerializeField]
    private ARRaycastManager raycastManager;

    private static List<ARRaycastHit> hitResults = new List<ARRaycastHit>();

    private GameObject spawnedObject;

    private InputAction touchAction;

    private void Awake()
    {
        touchAction = new InputAction(binding: "<Touchscreen>/primaryTouch/position");
        touchAction.Enable();
    }

    private bool TryGetTouchPosition(out Vector2 touchPos)
    {
        if (touchAction.triggered)
        {
            touchPos = touchAction.ReadValue<Vector2>();
            return true;
        }

        touchPos = default;
        return false;
    }

    private void Update()
    {
        if (!TryGetTouchPosition(out Vector2 touchPos))
        {
            return;
        }

        if (raycastManager.Raycast(touchPos, hitResults, UnityEngine.XR.ARSubsystems.TrackableType.FeaturePoint))
        {
            Pose hitPose = hitResults[0].pose;

            // Variant 1 - Skapar ett specifikt anchorObjekt
            // som vi sedan låter spelplansobjektet/världen
            // placeras under som ett childobjekt. 
            /*
            GameObject anchorObject = Instantiate(anchorPrefab);

            anchorObject.transform.position = hitPose.position;
            anchorObject.transform.rotation = hitPose.rotation;

            ARAnchor anchor = anchorObject.AddComponent<ARAnchor>();

            if (anchor != null)
            {
                spawnedObject = Instantiate(refToPrefab);
                spawnedObject.transform.SetParent(anchorObject.transform);
            }
            */

            // Variant 2 - Placerar anchor direkt på objektet som ska visas
            // dvs spelplanen/världen i detta fall
            spawnedObject = Instantiate(refToPrefab);
            spawnedObject.transform.position = hitPose.position;
            spawnedObject.transform.rotation = hitPose.rotation;

            spawnedObject.AddComponent<ARAnchor>();




        }
    }
}
