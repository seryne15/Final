using UnityEngine;
using System.Collections;
using System;

public class GestureListener : MonoBehaviour, KinectGestures.GestureListenerInterface
{
    // GUI Text to display the gesture messages.
    // public GUIText GestureInfo;

    private bool swipeLeft;
    private bool swipeRight;
    private bool SwipeUp;
    private bool SwipeDown;
    private bool Click;
    private bool Push;

    public bool IsPush()
    {
        if (Push)
        {
            Push = false;
            return true;
        }

        return false;
    }

    public bool IsClick()
    {
        if (Click)
        {
            Click = false;
            return true;
        }

        return false;
    }

    public bool IsSwipeDown()
    {
        if (SwipeDown)
        {
            SwipeDown = false;
            return true;
        }

        return false;
    }

    public bool IsSwipeUp()
    {
        if (SwipeUp)
        {
            SwipeUp = false;
            return true;
        }

        return false;
    }

    public bool IsSwipeLeft()
    {
        if (swipeLeft)
        {
            swipeLeft = false;
            return true;
        }

        return false;
    }

    public bool IsSwipeRight()
    {
        if (swipeRight)
        {
            swipeRight = false;
            return true;
        }

        return false;
    }



    public void UserDetected(uint userId, int userIndex)
    {
        // detect these user specific gestures
        KinectManager manager = KinectManager.Instance;
        // KinectManager manager = Camera.main.GetComponent<KinectManager>();

        manager.DetectGesture(userId, KinectGestures.Gestures.SwipeLeft);
        manager.DetectGesture(userId, KinectGestures.Gestures.SwipeRight);
        manager.DetectGesture(userId, KinectGestures.Gestures.SwipeUp);
        manager.DetectGesture(userId, KinectGestures.Gestures.SwipeDown);
        manager.DetectGesture(userId, KinectGestures.Gestures.Click);
        manager.DetectGesture(userId, KinectGestures.Gestures.Push);

    }

    public void UserLost(uint userId, int userIndex)
    {
        //if (GestureInfo != null)
        //{
        //    GestureInfo.GetComponent<GUIText>().text = string.Empty;
        //}
    }

    public void GestureInProgress(uint userId, int userIndex, KinectGestures.Gestures gesture,
                                  float progress, KinectWrapper.NuiSkeletonPositionIndex joint, Vector3 screenPos)
    {
        // don't do anything here
    }

    public bool GestureCompleted(uint userId, int userIndex, KinectGestures.Gestures gesture,
                                  KinectWrapper.NuiSkeletonPositionIndex joint, Vector3 screenPos)
    {


        if (gesture == KinectGestures.Gestures.SwipeLeft)
            swipeLeft = true;
        else if (gesture == KinectGestures.Gestures.SwipeRight)
            swipeRight = true;
        else if (gesture == KinectGestures.Gestures.SwipeUp)
            SwipeUp = true;
        else if (gesture == KinectGestures.Gestures.SwipeDown)
            SwipeDown = true;
        else if (gesture == KinectGestures.Gestures.Click)
            Click = true;
        else if (gesture == KinectGestures.Gestures.Push)
            Push = true;


        return true;
    }

    public bool GestureCancelled(uint userId, int userIndex, KinectGestures.Gestures gesture,
                                  KinectWrapper.NuiSkeletonPositionIndex joint)
    {
        // don't do anything here, just reset the gesture state
        return true;
    }

}
