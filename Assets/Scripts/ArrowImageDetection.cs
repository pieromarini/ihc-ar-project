using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARTrackedImageManager))]
public class ArrowImageDetection : MonoBehaviour
{
    private ARTrackedImageManager _arTrackedImageManager;

    void Awake()
    {
        _arTrackedImageManager = FindObjectOfType<ARTrackedImageManager>();
    }

    void OnEnable()
    {
        _arTrackedImageManager.trackedImagesChanged += OnImageChanged;
    }

    void OnDisable()
    {
        _arTrackedImageManager.trackedImagesChanged -= OnImageChanged;
    }

    private void OnImageChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach(var trackedImage in eventArgs.added)
        {
            GameObject parent = trackedImage.gameObject;
            if (trackedImage.trackingState != TrackingState.None)
            {
                parent.SetActive(true);
                switch (trackedImage.referenceImage.name)
                {
                    case "Right":
                        parent.transform.GetChild(0).gameObject.SetActive(true);
                        break;
                    case "Left":
                        parent.transform.GetChild(1).gameObject.SetActive(true);
                        break;
                    case "Forward":
                        parent.transform.GetChild(2).gameObject.SetActive(true);
                        break;
                    case "Backward":
                        parent.transform.GetChild(3).gameObject.SetActive(true);
                        break;
                }
            }
            else
            {
                parent.SetActive(false);
            }
        }
    }
}
