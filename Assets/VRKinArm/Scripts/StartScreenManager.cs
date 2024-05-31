using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StartScreenManager : MonoBehaviour
{

    [Header("UI Controller")]
    [SerializeField]
    private TMP_Dropdown setupDropDown;
    private int setupId = 0;

    [SerializeField]
    private GameObject scenariosPanel;

    [SerializeField]
    private TMP_Dropdown deviceDropdown;

    [Header("LapSim FrameGrabber")]
    [SerializeField]
    //private FrameGrabber frameGrabber;
    WebCamDevice[] devices;



    private void Start()
    {

        devices = WebCamTexture.devices;
        List<TMP_Dropdown.OptionData> optionsDropdown = new List<TMP_Dropdown.OptionData>();

        
        TMP_Dropdown.OptionData newItem = new TMP_Dropdown.OptionData("Choose Device");
        optionsDropdown.Add(newItem);

        for (int i = 0; i < devices.Length; i++)
        {
            TMP_Dropdown.OptionData newItem1 = new TMP_Dropdown.OptionData(devices[i].name);
            optionsDropdown.Add(newItem1);
        }

        deviceDropdown.AddOptions(optionsDropdown);
    }


    public void PlayScenarioVideos()
    {
        //if(devices.Length > 0 && deviceDropdown.value > 0)
        //{
        //    frameGrabber.gameObject.SetActive(true);
        //    frameGrabber.StartCapturing(deviceDropdown.value - 1);
        //}

        //scenariosPanel.SetActive(false);
    }



}
