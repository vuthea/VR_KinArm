using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;


public class ViveTrackerDetector : MonoBehaviour
{
    [SerializeField]
    private VRTK_ControllerEvents leftController;
    [SerializeField]
    private VRTK_ControllerEvents rightController;
    [SerializeField]
    SteamVR_TrackedObject viveTracker;

    private bool isChanged = false;


    void CheckController()
    {
        if(!leftController.isActiveAndEnabled && !rightController.isActiveAndEnabled)
        {
            viveTracker.index = SteamVR_TrackedObject.EIndex.Device1;
            isChanged = true;
        }

        if (!leftController.isActiveAndEnabled && rightController.isActiveAndEnabled)
        {
            viveTracker.index = SteamVR_TrackedObject.EIndex.Device2;
            isChanged = true;
        }

        if (leftController.isActiveAndEnabled && rightController.isActiveAndEnabled)
        {
            viveTracker.index = SteamVR_TrackedObject.EIndex.Device3;
            isChanged = true;
        }

    }

    private void Update()
    {
        if (!isChanged)
        {
            CheckController();
        }
    }


}
