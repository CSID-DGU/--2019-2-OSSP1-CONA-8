/*
============================
Unity Assets by MAKAKA GAMES
============================

Online Docs: https://makaka.org/category/docs/
Offline Docs: You have a PDF file in the package folder.

=======
SUPPORT
=======

First of all, read the docs. If it didn’t help, get the support.

Web: https://makaka.org/support/
Email: info@makaka.org

If you find a bug or you can’t use the asset as you need, 
please first send email to info@makaka.org (in English or in Russian) 
before leaving a review to the asset store.

I am here to help you and to improve my products for the best.
*/

using UnityEngine;
using UnityEngine.UI;
using Vuforia;

using System.Collections;

/// <summary>
/// A custom handler that implements the ITrackableEventHandler interface.
/// 
/// Changes made to this file could be overwritten when upgrading the Vuforia version. 
/// When implementing custom event handler behavior, consider inheriting from this class instead.
/// </summary>
[HelpURL("https://library.vuforia.com/articles/Training/getting-started-with-vuforia-in-unity.html")]
[AddComponentMenu("AR/Vuforia/Trackable Event Handler Custom")]
public class VuforiaTrackableEventHandlerCustomAR : MonoBehaviour, ITrackableEventHandler
{
    #region PROTECTED_MEMBER_VARIABLES

    protected TrackableBehaviour mTrackableBehaviour;

    #endregion // PROTECTED_MEMBER_VARIABLES

    #region UNITY_MONOBEHAVIOUR_METHODS

    public Transform ARTargetObjects;
    public GameObject indicatorOnTrackingLost;

    // [Header("Pause & Play on Trackable State Changed")]
    // public bool isPauseAndPlayOn = false;

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