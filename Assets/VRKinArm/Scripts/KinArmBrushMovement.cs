using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinArmBrushMovement : MonoBehaviour
{

    [SerializeField]
    private Transform brushMoudule;

    [SerializeField]
    private bool isFollowed = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {

        if (isFollowed)
        {
            if (brushMoudule)
            {
                Vector3 _followPosition = new Vector3(brushMoudule.position.x, transform.position.y, brushMoudule.position.z);

                transform.position = _followPosition;

            }
        }      

    }

    public void setFollowBrushMovement(bool flag)
    {
        isFollowed = flag;
    }
}
