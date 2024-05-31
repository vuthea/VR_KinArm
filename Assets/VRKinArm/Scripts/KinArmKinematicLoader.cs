using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinArmKinematicLoader : MonoBehaviour
{
    [SerializeField]
    private GameObject disableObjectsRuntime;

    [SerializeField]
    private GameObject enableObjectsRuntime;


    // Start is called before the first frame update
    void Start()
    {
        if (disableObjectsRuntime)
            disableObjectsRuntime.SetActive(false);

        if (enableObjectsRuntime)
            enableObjectsRuntime.SetActive(true);
    }
}
