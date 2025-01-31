using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCustomScript : MonoBehaviour
{
    private CinemachineFreeLook freeLookCamera;
    private void Start()
    {
        freeLookCamera = GetComponent<CinemachineFreeLook>();
        ChangeAxisControl(true);
    }
    public void ChangeAxisControl(bool enable)
    {
        if (enable)
        {
            freeLookCamera.m_YAxis.m_InputAxisName = "Mouse Y";
            freeLookCamera.m_XAxis.m_InputAxisName = "Mouse X";
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            freeLookCamera.m_YAxis.m_InputAxisName = "";
            freeLookCamera.m_XAxis.m_InputAxisName = "";
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
