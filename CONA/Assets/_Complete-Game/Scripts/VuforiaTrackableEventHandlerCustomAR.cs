using UnityEngine;
using UnityEngine.UI;
using Vuforia;

using System.Collections;

public class VuforiaTrackableEventHandlerCustomAR : MonoBehaviour, ITrackableEventHandler
{
    #region PROTECTED_MEMBER_VARIABLES

    protected TrackableBehaviour mTrackableBehaviour;

    #endregion // PROTECTED_MEMBER_VARIABLES

    #region UNITY_MONOBEHAVIOUR_METHODS

    public Transform ARTargetObjects;
    public GameObject indicatorOnTrackingLost;

    protected virtual void Start()
    {
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();

        if (mTrackableBehaviour)
        {
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
        }
    }

    protected virtual void OnDestroy()
    {
        if (mTrackableBehaviour)
        {
            mTrackableBehaviour.UnregisterTrackableEventHandler(this);
        }
    }

    #endregion // UNITY_MONOBEHAVIOUR_METHODS

    #region PUBLIC_METHODS

    /// <summary>
    ///     Implementation of the ITrackableEventHandler function called when the
    ///     tracking state changes.
    /// </summary>
    public void OnTrackableStateChanged(
        TrackableBehaviour.Status previousStatus,
        TrackableBehaviour.Status newStatus)
    {
        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            //Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " found");
            OnTrackingFound();
        }
        else if (previousStatus == TrackableBehaviour.Status.TRACKED &&
                 newStatus == TrackableBehaviour.Status.NO_POSE)
        {
            //Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " lost");
            OnTrackingLost();
        }
        else
        {
            // For combo of previousStatus=UNKNOWN + newStatus=UNKNOWN|NOT_FOUND
            // Vuforia is starting, but tracking has not been lost or found yet
            // Call OnTrackingLost() to hide the augmentations
            OnTrackingLost();
        }
    }

    #endregion // PUBLIC_METHODS

    #region PROTECTED_METHODS

    protected virtual void OnTrackingFound()
    {

        SetARTargetObjectsEnabled(true);
    }

    protected virtual void OnTrackingLost()
    {
        SetARTargetObjectsEnabled(false);
    }

    private void SetARTargetObjectsEnabled(bool enabled)
    {
        if (indicatorOnTrackingLost)
        {
            indicatorOnTrackingLost.SetActive(!enabled);
        }

        if (ARTargetObjects)
        {
            var rendererComponents = ARTargetObjects.GetComponentsInChildren<Renderer>(true);

            if (rendererComponents != null)
            {
                for (int i = 0; i < rendererComponents.Length; i++)
                {
                    rendererComponents[i].enabled = enabled;
                }
            }

            var colliderComponents = ARTargetObjects.GetComponentsInChildren<Collider>(true);

            if (colliderComponents != null)
            {
                for (int i = 0; i < colliderComponents.Length; i++)
                {
                    colliderComponents[i].enabled = enabled;
                }
            }
        }

        // if (isPauseAndPlayOn)
        // {
        //     if (enabled)
        //     {
        //         Time.timeScale = 1f;
        //     }
        //     else
        //     {
        //         Time.timeScale = 0f;
        //     }
        // }
    }

    #endregion // PROTECTED_METHODS
}