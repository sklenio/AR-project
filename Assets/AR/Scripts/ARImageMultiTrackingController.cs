using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using AugmentedRealityCourse;

public class ARImageMultiTrackingController : MonoBehaviour
{
    [SerializeField]
    private ARTrackedImageManager refToTrackedImageManager;

    [SerializeField]
    private List<GameObject> matchedGamePrefabs = new List<GameObject>();

    [SerializeField]
    private GameObject parentObject;

    [SerializeField]
    private GameObject panelObject;

    [SerializeField]
    private Vector3 placedObjectRelativePosition;

    public Dictionary<string,GameObject> arObjects = new Dictionary<string, GameObject>();


    private void Awake()
    {
        foreach (var mObject in matchedGamePrefabs) { 
            GameObject newMObject = Instantiate(mObject, Vector3.zero, Quaternion.identity);
            newMObject.name = mObject.name; 
            newMObject.transform.parent = parentObject.transform; //
            newMObject.transform.localPosition = placedObjectRelativePosition;
            newMObject.SetActive(false); // Döljer kopian
            arObjects.Add(newMObject.name, newMObject);
           // DebugManager.Instance.AddDebugMessage(newMObject.transform.position.ToString());
        }
    }

    private void OnEnable()
    {
        // Detta sker när vi aktiverar komponenten
        // Subscribe för att lyssna på händelsen när någon bild ändras
        refToTrackedImageManager.trackedImagesChanged += handleTrackedImage;
    }

    private void OnDisable()
    {
        // Detta sker när vi avaktiverar denna komponent
        // UnSubscribe för att sluta lyssna och ta emot när bilder ändras
        refToTrackedImageManager.trackedImagesChanged -= handleTrackedImage;
    }

    private void handleTrackedImage(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (ARTrackedImage trackedImage in eventArgs.added)
        {
            // Här vill jag använda detta att jag har en specifik
            // trackad image som kameran detekterade nyss
            // och matcha den mot ett GameObject 
            if (arObjects.ContainsKey(trackedImage.referenceImage.name))
            {
                // Skapa en referens till gameobjektet i lookuplistan
                GameObject g = arObjects[trackedImage.referenceImage.name];

                g.SetActive(true);
                //g.transform.position = trackedImage.transform.position;
                // g.transform.rotation = trackedImage.transform.rotation;
                //g.transform.position = CameraTransform.position;
                //g.transform.rotation = CameraTransform.rotation;
            }
        }

        foreach (ARTrackedImage trackedImage in eventArgs.updated)
        {
            if (arObjects.ContainsKey(trackedImage.referenceImage.name))
            {
                GameObject g = arObjects[trackedImage.referenceImage.name];
               //g.transform.position = trackedImage.transform.position;
               //g.transform.rotation = trackedImage.transform.rotation;
               //DebugManager.Instance.AddDebugMessage(g.transform.position.ToString());
                //g.transform.position = CameraTransform.position;
                //g.transform.rotation = CameraTransform.rotation;

                //if (trackedImage.trackingState == TrackingState.Limited)
               // {
                //    g.SetActive(false);
               // } else
                //{
                    g.SetActive(true);

                    panelObject.SetActive(true);
               // }

            }
        }

        foreach (ARTrackedImage trackedImage in eventArgs.removed)
        {
            if (arObjects.ContainsKey(trackedImage.referenceImage.name))
            {
                GameObject g = arObjects[trackedImage.referenceImage.name];
                g.SetActive(false);
            }
        }


    }


}
