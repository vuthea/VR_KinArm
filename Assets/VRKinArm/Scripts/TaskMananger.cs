using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskMananger : MonoBehaviour
{

    [Header("KinArm Handler")]
    [SerializeField]
    private Transform rayOrigin;
    private RaycastHit hit;

    public bool isActivated = false;

    //Layer mask for dots
    [SerializeField]
    private LayerMask layer_mask;

    [Header("Task")]
    public List<TaskData> taskData;
    [SerializeField]
    private GameObject celebrationEffect;
    [SerializeField]
    private GameObject progressPanel;
    [SerializeField]
    private TMPro.TextMeshProUGUI _progressText;
    [Header("Dot Highlight")]
    [SerializeField]
    private Material dotMat;
    [SerializeField]
    private Material hightlightMat;
    [SerializeField]
    private bool isHightlight = true;
    private bool firstTimeHightlight = true;
    [SerializeField]
    private Color defaultColor;
    [SerializeField]
    private Color hightlightColor;
    private int previousIndex = -1;

    private int taskCount = 0;
    private bool isHit = false;
    private int dotId;
    private Vector3 hitPosition;

    [Header("User Study")]
    public Condition selectedCondtion;
    public SelectedTask selectedTask;
    public DirectionalTask directionalTask;
    public enum Condition { VR, VRKinArm}
    public enum SelectedTask { Line, Circle, Diamond };
    public enum DirectionalTask { Clockwise, CounterClockWise };
    private bool checkedTask = false;

    private List<GameObject> lineDots;
    private List<GameObject> circleDots;
    private List<GameObject> diamondDots;

    [Header("Tracking")]
    [SerializeField]
    private Transform _trackerTransform;
    [SerializeField]
    private Transform _headsetTransform;

    //[SerializeField]
    //private DrawHeatmap eyeTracking;

    //private LoggingData _logData;
    [SerializeField]
    private float loggingFrequency = 1.0f;
    private bool _startLog;
    private DateTime _timerStart;
    private double durationInMs;

    // Start is called before the first frame update
    void Start()
    {
        //_logData = new LoggingData();
    }

    // Update is called once per frame
    void Update()
    {
        //if (checkedTask)
        //{
        //    LoadedData();
        //    checkedTask = false;
        //}


        if (isActivated)
        {
            //if (firstTimeHightlight)
            //    HightlightDot();

            if(isHightlight)
                StartCoroutine(ColorBlink(0.5f));


            if (Physics.Raycast(rayOrigin.position, rayOrigin.up, out hit, 10f, layer_mask))
            {
                var hitDot = hit.collider.gameObject.GetComponent<TaskController>();

                isHit = hitDot.SetSelectedDot();
                dotId = hitDot.dotId;
                hitPosition = hit.point;

                if (isHit)
                {
                    durationInMs = Math.Round((DateTime.UtcNow - _timerStart).TotalSeconds, 2);                    
                    CheckProgress();
                    HightlightDot(dotId);

                    //if (_startLog)
                    //    WriteLogData();
                }
                    

            }

            if (!isHit && _startLog) //Time.frameCount % 5 == 0
                StartCoroutine(WriteLogEverySecond());

        }
        
    }

    public void ActivatingTask(int _conId, int _taskId, int _dirId)
    {
        selectedCondtion = (Condition)_conId;
        selectedTask = (SelectedTask)_taskId;
        directionalTask = (DirectionalTask)_dirId;
        checkedTask = true;
    }

    public void LoadedData()
    {
        firstTimeHightlight = true;
        if (selectedTask == SelectedTask.Line)
        {
            lineDots = taskData[0].drawingDots.GetLineDots();
            taskData[0].drawingDots.gameObject.SetActive(true);
            taskData[1].drawingDots.gameObject.SetActive(false);
            taskData[2].drawingDots.gameObject.SetActive(false);
            HightlightDot(0);
        }
        else if (selectedTask == SelectedTask.Circle)
        {
            circleDots = taskData[1].drawingDots.GetCircleDots();
            taskData[0].drawingDots.gameObject.SetActive(false);
            taskData[1].drawingDots.gameObject.SetActive(true);
            taskData[2].drawingDots.gameObject.SetActive(false);
            HightlightDot(1);
        }
        else if (selectedTask == SelectedTask.Diamond)
        {
            diamondDots = taskData[2].drawingDots.GetDiamondDots();
            taskData[0].drawingDots.gameObject.SetActive(false);
            taskData[1].drawingDots.gameObject.SetActive(false);
            taskData[2].drawingDots.gameObject.SetActive(true);
            HightlightDot(2);
        }

        
    }
    private IEnumerator WriteLogEverySecond()
    {
        yield return new WaitForSeconds(0.5f);
        
        //if(!isHit)
        //    WriteLogData();
    }

    public void CreateNewLog()
    {
        _startLog = true;
        _timerStart = DateTime.UtcNow;
    }

    public void StopLogging()
    {
        _startLog = false;
    }


    private void HightlightDot(int dotId = 0)
    {
        int _index = 0;

        if (selectedTask == SelectedTask.Line)
        {
            if (firstTimeHightlight)
            {
                if (directionalTask == DirectionalTask.CounterClockWise)
                    _index = lineDots.Count - 1;
                firstTimeHightlight = false;
            }
            else
            {
                if (directionalTask == DirectionalTask.Clockwise)
                    _index = dotId + 1;
                else
                    _index = dotId - 1;
            }

            if (_index >= 0 && _index < lineDots.Count)
            {
                if (!lineDots[_index].GetComponent<TaskController>().isSelected)
                    lineDots[_index].GetComponent<MeshRenderer>().material = hightlightMat;
            }

        }
        else if (selectedTask == SelectedTask.Circle)
        {
            if (firstTimeHightlight)
            {
                if (directionalTask == DirectionalTask.CounterClockWise)
                    _index = circleDots.Count - 1;
                firstTimeHightlight = false;
            }
            else
            {
                if (directionalTask == DirectionalTask.Clockwise)
                    _index = dotId + 1;
                else
                    _index = dotId - 1;
            }

            if (_index >= 0 && _index < circleDots.Count)
            {
                if (!circleDots[_index].GetComponent<TaskController>().isSelected)
                    circleDots[_index].GetComponent<MeshRenderer>().material = hightlightMat;
            }

        }
        else if (selectedTask == SelectedTask.Diamond)
        {
            if (firstTimeHightlight)
            {
                if (directionalTask == DirectionalTask.CounterClockWise)
                    _index = diamondDots.Count - 1;
                firstTimeHightlight = false;
            }
            else
            {
                if (directionalTask == DirectionalTask.Clockwise)
                    _index = dotId + 1;
                else
                    _index = dotId - 1;
            }

            if (_index >= 0 && _index < diamondDots.Count)
            {
                if (!diamondDots[_index].GetComponent<TaskController>().isSelected)
                    diamondDots[_index].GetComponent<MeshRenderer>().material = hightlightMat;
            }

        }

    }

    private IEnumerator ColorBlink(float TimeToBlink)
    {
        
        hightlightMat.SetColor("_EmissionColor", hightlightColor);
        yield return new WaitForSeconds(TimeToBlink);
        hightlightMat.SetColor("_EmissionColor", defaultColor);
    }

    private void CheckProgress()
    {
        int numPoints = 0;
        if (selectedTask == SelectedTask.Line)
            numPoints = lineDots.Count;
        else if (selectedTask == SelectedTask.Circle)
            numPoints = circleDots.Count;
        else if (selectedTask == SelectedTask.Diamond)
            numPoints = diamondDots.Count;

        if (taskCount >= numPoints - 1)
        {
            Debug.Log("Completed");
            celebrationEffect.SetActive(true);
            progressPanel.SetActive(true);
            _progressText.text = durationInMs.ToString() + " s";
        }

        taskCount++;
        Debug.Log(taskCount);
    }

    //private void WriteLogData()
    //{
    //    if(taskData == null)
    //    {
    //        Debug.LogError("No Tasks Included!");
    //        return;
    //    }


    //    _logData.UserID = LoggingSystem.participantID;
    //    _logData.Condition = selectedCondtion.ToString();
    //    _logData.Scenario = selectedTask.ToString();
    //    _logData.Direction = directionalTask.ToString();
    //    _logData.TimeStamp = DateTime.Now.ToString("yyyy-MM-dd_HH:mm:ss.ff");
    //    _logData.DtInMs = durationInMs;

    //    _logData.ControllerWorldPosition = _trackerTransform.position;
    //    _logData.ControllerPosition = _trackerTransform.localPosition;
    //    _logData.ControllerRotation = _trackerTransform.localEulerAngles;

    //    _logData.IsHit = isHit;
    //    _logData.DotId = dotId;
    //    _logData.HitPosition = hitPosition;

    //    _logData.HeadPosition = _headsetTransform.position;
    //    _logData.HeadRotation = _headsetTransform.rotation.eulerAngles;
    //    _logData.EyeOrigin = eyeTracking.rayOrigin;
    //    _logData.EyeDirection = eyeTracking.rayDirection;
    //    _logData.EyeHitPoint = eyeTracking._eyeHitPos;

    //    LoggingSystem.writeLoggingData(_logData);

    //}

    public void ResetTasks()
    {
        taskCount = 0;        
        celebrationEffect.SetActive(false);
        progressPanel.SetActive(false);
        _progressText.text = 0 + " s";

        if (selectedTask == SelectedTask.Line)
        {
            taskData[0].drawingDots.gameObject.SetActive(false);
        }
        else if (selectedTask == SelectedTask.Circle)
        {
            taskData[1].drawingDots.gameObject.SetActive(false);
        }
        else if (selectedTask == SelectedTask.Diamond)
        {
            taskData[2].drawingDots.gameObject.SetActive(false);
        }


    }


}
