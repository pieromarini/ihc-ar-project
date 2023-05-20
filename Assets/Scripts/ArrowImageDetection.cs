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

    private void updateTrackedImage(string name, bool state, Transform t)
    { 
		switch (name)
		{
		    case "Right":
				t.GetChild(0).gameObject.SetActive(state);
				break;

		    case "Left":
				t.GetChild(1).gameObject.SetActive(state);
				break;

		    case "Forward":
				t.GetChild(2).gameObject.SetActive(state);
				break;

		    case "Backward":
				t.GetChild(3).gameObject.SetActive(state);
				break;
		}
    }

    private void OnImageChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach(var trackedImage in eventArgs.added)
        {
            GameObject parent = trackedImage.gameObject;
            if (trackedImage.trackingState != TrackingState.None)
            {
                parent.SetActive(true);
                updateTrackedImage(trackedImage.referenceImage.name, true, parent.transform);
            }
            else
            {
                parent.SetActive(false);
            }
        }

        foreach(var trackedImage in eventArgs.updated)
        {
            GameObject parent = trackedImage.gameObject;
			parent.SetActive(true);
            updateTrackedImage(trackedImage.referenceImage.name, trackedImage.trackingState == TrackingState.Tracking, parent.transform);
        }

        foreach(var trackedImage in eventArgs.removed)
        {
            GameObject parent = trackedImage.gameObject;
			parent.SetActive(true);
            updateTrackedImage(trackedImage.referenceImage.name, false, parent.transform);
        }
    }
}
