
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARObjectPlacement : MonoBehaviour
{
    // The StackModel prefab
    public GameObject stackPrefab;

    // AR Raycast Manager from XR Origin
    public ARRaycastManager raycastManager;

    // Stores the placed StackModel
    private GameObject placedStack;

    // Stores AR raycast results
    private static readonly List<ARRaycastHit> hits =
        new List<ARRaycastHit>();

    void Update()
    {
        Vector2 touchPosition;
        bool isPointerOverUI = false;

#if UNITY_EDITOR

        // Detect mouse clicks in Unity XR Simulation
        if (Mouse.current == null ||
            !Mouse.current.leftButton.wasPressedThisFrame)
        {
            return;
        }

        touchPosition = Mouse.current.position.ReadValue();

        if (EventSystem.current != null)
        {
            isPointerOverUI =
                EventSystem.current.IsPointerOverGameObject();
        }

        Debug.Log("Mouse click detected at: " + touchPosition);

#else

        // Detect finger taps on Android
        if (Touchscreen.current == null)
        {
            return;
        }

        var touch = Touchscreen.current.primaryTouch;

        if (!touch.press.wasPressedThisFrame)
        {
            return;
        }

        touchPosition = touch.position.ReadValue();

        if (EventSystem.current != null)
        {
            isPointerOverUI =
                EventSystem.current.IsPointerOverGameObject(
                    touch.touchId.ReadValue());
        }

#endif

        // Do not place objects when tapping UI buttons
        if (isPointerOverUI)
        {
            Debug.Log("Pointer is over UI. Placement skipped.");
            return;
        }

        // Check that the references are assigned
        if (stackPrefab == null || raycastManager == null)
        {
            Debug.LogWarning(
                "Assign Stack Prefab and Raycast Manager in Inspector!"
            );
            return;
        }

        // Only allow one StackModel
        if (placedStack != null)
        {
            return;
        }

        // Check whether the tap hits a detected AR plane
        if (raycastManager.Raycast(
            touchPosition,
            hits,
            TrackableType.PlaneWithinPolygon))
        {
            Pose hitPose = hits[0].pose;

            // Create the StackModel on the detected surface
            placedStack = Instantiate(
                stackPrefab,
                hitPose.position,
                Quaternion.identity
            );

            Debug.Log("Stack successfully placed!");
        }
        else
        {
            Debug.Log(
                "AR raycast did not hit a detected plane."
            );
        }
    }
}
