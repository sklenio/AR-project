using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.InputSystem;
using AugmentedRealityCourse;

public class ARTapToPlaceWithPlaneControl : MonoBehaviour
{
    [SerializeField]
    private GameObject refToPrefab;

    [SerializeField]
    private ARRaycastManager raycastManager;

    // NEW ### NEW ### NEW ###
    [SerializeField]
    private ARPlaneController refToPlaneController;


    private static List<ARRaycastHit> hitResults = new List<ARRaycastHit>();

    private GameObject spawnedObject;

    private Camera mainCamera;

    private InputAction touchAction;

    private Vector2 touchPosition;

    private void Awake()
    {
        mainCamera = Camera.main;

        touchAction = new InputAction(binding: "<Touchscreen>/primaryTouch/position");
        touchAction.Enable();

        if (refToPlaneController == null)
        {
            DebugManager.Instance.AddDebugMessage("You missing a reference to planecontroller object");
        }

        // Väg 1: En väg att gå!!
        //touchAction.started += TouchAction_started;
    }

    // Tillhör väg nr 1
    /*
    private void TouchAction_started(InputAction.CallbackContext obj)
    {
        // ..... skriv resten av koden här
    }
    */

    private void OnDestroy()
    {
        // Väg 1: Tillhör väg nr 1
        //touchAction.started -= TouchAction_started;

        touchAction.Disable();

        if (touchAction != null)
        {
            touchAction = null;
        }
    }

    private bool TryGetTouchPosition(out Vector2 touchPos)
    {
        // Här använder vi nu inputAction äntligen för att läsa av
        // vart på mobilskärmen vi har tryckt
        // Triggered håller reda på om detta stämmer eller ej och retunerar sant/falskt
        if (touchAction.triggered)
        {
            touchPos = touchAction.ReadValue<Vector2>();
            return true;
        }

        touchPos = default;
        return false;
    }

    // Väg 2: 
    private void Update()
    {
        // Här anropar vi TryGetTouchPosition metoden
        // och förhoppningsvis får vi tillbaka vart vi har tryckt på skärmen

        if (!TryGetTouchPosition(out Vector2 touchPos))
        {
            return;
        }

        // Detta omvandlar två punkten på skärmen till bokstavligen en
        // raycast stråle som träffar eventuella planes i AR
        if (raycastManager.Raycast(touchPos, hitResults, TrackableType.Planes))
        {
            // Ok vi har verkligen träffat en punkt på ett plan
            // här så kan vi göra vad vi vill nu med prefabobjektet
            // och ta vara på positionen vi tryckte på
            Pose hitPose = hitResults[0].pose;

            Instantiate(refToPrefab, hitPose.position, hitPose.rotation);

            refToPlaneController.TogglePlaneVisibility();
        }
    }


}
