using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ARPlaneController : MonoBehaviour
{
    [SerializeField]
    private ARPlaneManager refToPlaneManager;

    public void TogglePlaneVisibility()
    {
        if (refToPlaneManager != null)
        {
            refToPlaneManager.enabled = !refToPlaneManager.enabled;

            foreach (var plane in refToPlaneManager.trackables)
            {
                plane.gameObject.SetActive(refToPlaneManager.enabled);
            }
        }
    }

    public void StopPlaneTracking()
    {
        if (refToPlaneManager != null)
        {
            refToPlaneManager.enabled = false;
        }
    }

    public void ResumePlaneTracking()
    {
        // H�r �r er �vning se till att ni �terst�ller trackingen igen
        // dvs b�rjar tracka igen
    }


}
