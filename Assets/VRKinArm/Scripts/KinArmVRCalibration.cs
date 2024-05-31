using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class KinArmVRCalibration : MonoBehaviour
{
    [Header("Controllers")]
    [SerializeField]
    private GameObject leftController;
    [SerializeField]
    private GameObject rightController;
    [SerializeField]
    SteamVR_TrackedObject viveTracker;

    [Header("Reset Position")]
    public Transform resetPosition;

    //Assign these variables in the inspector, or find them some other way (eg. in Start() )
    public Transform vrCamera;
    public Transform cameraRig;


    [Header("Tracker Follower")]
    [SerializeField]
    private FollowTracker followTracker;

    [SerializeField]
    private KinArmBrushMovement kinArmBrushMovement;


    [Header("Task Manager")]
    [SerializeField]
    private TaskMananger taskManager;

    //[Header("Heatmap")]
    //[SerializeField]
    //private DrawHeatmap heatmap;

    //[Header("Userstudy")]
    //[SerializeField]
    //private UserStudySetup userStudy;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
            CheckController();

        //if (Input.GetKeyDown(KeyCode.Q))
        //    userStudy.EnableScenarioPanel(true);

        if (Input.GetKeyDown(KeyCode.F1))
            viveTracker.index = SteamVR_TrackedObject.EIndex.Device1;

        if (Input.GetKeyDown(KeyCode.F2))
            viveTracker.index = SteamVR_TrackedObject.EIndex.Device2;

        if (Input.GetKeyDown(KeyCode.F3))
            viveTracker.index = SteamVR_TrackedObject.EIndex.Device3;

        if (Input.GetKeyDown(KeyCode.F4))
            viveTracker.index = SteamVR_TrackedObject.EIndex.Device4;

        if (Input.GetKeyDown(KeyCode.F5))
            viveTracker.index = SteamVR_TrackedObject.EIndex.Device5;

        if (Input.GetKeyDown(KeyCode.F6))
            viveTracker.index = SteamVR_TrackedObject.EIndex.Device6;

        if (Input.GetKeyDown(KeyCode.F7))
            viveTracker.index = SteamVR_TrackedObject.EIndex.Device7;

        if (Input.GetKeyDown(KeyCode.F8))
            viveTracker.index = SteamVR_TrackedObject.EIndex.Device8;
        
        if (Input.GetKeyDown(KeyCode.F9))
            viveTracker.index = SteamVR_TrackedObject.EIndex.Device9;
        
        if (Input.GetKeyDown(KeyCode.F10))
            viveTracker.index = SteamVR_TrackedObject.EIndex.Device10;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Calibrate VR Pose");
            CalibratePosition();
            

            if (followTracker)
                followTracker.setFollowedPosition(true);

            if (kinArmBrushMovement)
                kinArmBrushMovement.setFollowBrushMovement(true);

        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (taskManager)
            {
                taskManager.LoadedData();
                taskManager.isActivated = true;
                taskManager.CreateNewLog();

                //heatmap._useEyeForHeatmap = true;
            }
                
        }


    }

    public void CalibratePosition()
    {
        CalibratePositon(resetPosition);
    }

    public void CalibratePositon(Transform desiredHeadPos)
    {
        if (vrCamera && cameraRig)
        {
            //ROTATION
            // Get current head heading in scene (y-only, to avoid tilting the floor)            
            float offsetAngle = desiredHeadPos.eulerAngles.y - vrCamera.rotation.eulerAngles.y;
            // Now rotate CameraRig in opposite direction to compensate
            //cameraRig.Rotate(0f, -offsetAngle, 0f);

            cameraRig.Rotate(0f, offsetAngle, 0f);

            //POSITION
            // Calculate postional offset between CameraRig and Camera
            Vector3 offsetPos = vrCamera.position - cameraRig.position;
            // Reposition CameraRig to desired position minus offset
            //cameraRig.position = (desiredHeadPos.position - offsetPos);
            cameraRig.position = new Vector3(desiredHeadPos.position.x - offsetPos.x, 0, desiredHeadPos.position.z - offsetPos.z); //Y should depending on tracking

            Debug.Log("Seat recentered!");
        }
        else
        {
            Debug.Log("Error: VR camera & cameraRig objects not found!");
        }
    }

    void CheckController()
    {
        if (!leftController.activeSelf && !rightController.activeSelf)
        {
            viveTracker.index = SteamVR_TrackedObject.EIndex.Device1;
        }else if (!leftController.activeSelf && rightController.activeSelf)
        {
            viveTracker.index = SteamVR_TrackedObject.EIndex.Device2;
        }else if (leftController.activeSelf && rightController.activeSelf)
        {
            viveTracker.index = SteamVR_TrackedObject.EIndex.Device3;
        }

    }

}
