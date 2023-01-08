using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimStateManager : MonoBehaviour
{
    #region CamConfing
    private float xAxis, yAxis;
    [SerializeField] float mouseSense;
    [SerializeField] Transform camFollowPos;
    #endregion
    void Start()
    {
        
    }

    void Update()
    {
        xAxis += (Input.GetAxisRaw("Mouse X") * mouseSense);
        //subtract y input otherwise cam movement reversed
        yAxis -= (Input.GetAxisRaw("Mouse Y") * mouseSense);
        //prevent looking upwards or downwards from 80 degrees
        yAxis = Mathf.Clamp(yAxis, -80, 80);
    }
    private void LateUpdate()
    {
        //This way we rotated the CamFollowPos to look up and down, and the player to look right and left
        camFollowPos.localEulerAngles = new Vector3(yAxis, camFollowPos.localEulerAngles.y, camFollowPos.localEulerAngles.z);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, xAxis, transform.eulerAngles.z);
    }
}
