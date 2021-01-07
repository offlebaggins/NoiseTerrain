using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CamYMovement : MonoBehaviour
{
    CinemachineOrbitalTransposer vCam;

    public float sensitivity = 1;

    public float minZoom, maxZoom;

    // Start is called before the first frame update
    void Start()
    {
        vCam = GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineOrbitalTransposer>();
    }

    // Update is called once per frame
    void Update()
    {
        float z = vCam.m_FollowOffset.z;
        z += Input.GetAxis("Mouse Y") * sensitivity;
        z = Mathf.Clamp(z, minZoom, maxZoom);
        vCam.m_FollowOffset.z = z;
    }
}
