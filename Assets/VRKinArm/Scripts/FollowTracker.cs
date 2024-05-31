using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTracker : MonoBehaviour
{
    [SerializeField]
    private Transform viveTracker;
    private Vector3 cachedPositon;

    [SerializeField]
    private bool followedPosition;

    //[SerializeField]
    //private bool followedRotation;

    // Start is called before the first frame update
    void Start()
    {
        if (viveTracker)
        {
            cachedPositon = viveTracker.position;
        }
    }

    // Update is called once per frame
    void Update()
    {

        //if (Input.GetKeyDown(KeyCode.Q))
        //{
        //    followedPosition = true;
        //}


        if (followedPosition)
        {
            if (viveTracker && cachedPositon != viveTracker.position)
            {
                transform.position = viveTracker.position;
            }
        }
            
    }

    public void setFollowedPosition(bool flag)
    {
        followedPosition = flag;
    }
}
