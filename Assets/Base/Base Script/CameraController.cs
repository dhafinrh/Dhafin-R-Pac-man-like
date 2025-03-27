using System;
using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("State Settings")] [SerializeField]
    private CameraStateValue closePosition;

    [SerializeField] private CameraStateValue middlePosition;
    [SerializeField] private CameraStateValue farPosition;
    [SerializeField] private float transitionSpeed = 5f;
    private CameraState currentState;
    private CinemachineFreeLook freeLookCam;

    private void Start()
    {
        freeLookCam = GetComponent<CinemachineFreeLook>();
        currentState = CameraState.Middle;
        SmoothTransition();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V)) SwitchCamState();

        SmoothTransition();
    }

    private void SwitchCamState()
    {
        switch (currentState)
        {
            case CameraState.Close:
                currentState = CameraState.Middle;
                break;
            case CameraState.Middle:
                currentState = CameraState.Far;
                break;
            case CameraState.Far:
                currentState = CameraState.Close;
                break;
        }

        Debug.Log("Camera state changed to: " + currentState);
    }

    private void SmoothTransition()
    {
        var targetCamValue = GetTargetPosition();

        // Top Rig
        freeLookCam.m_Orbits[0].m_Height = Mathf.Lerp(
            freeLookCam.m_Orbits[0].m_Height,
            targetCamValue.topRig.x,
            Time.deltaTime * transitionSpeed
        );
        freeLookCam.m_Orbits[0].m_Radius = Mathf.Lerp(
            freeLookCam.m_Orbits[0].m_Radius,
            targetCamValue.topRig.y,
            Time.deltaTime * transitionSpeed
        );

        // Middle Rig
        freeLookCam.m_Orbits[1].m_Height = Mathf.Lerp(
            freeLookCam.m_Orbits[1].m_Height,
            targetCamValue.midRig.x,
            Time.deltaTime * transitionSpeed
        );
        freeLookCam.m_Orbits[1].m_Radius = Mathf.Lerp(
            freeLookCam.m_Orbits[1].m_Radius,
            targetCamValue.midRig.y,
            Time.deltaTime * transitionSpeed
        );

        // Bottom Rig
        freeLookCam.m_Orbits[2].m_Height = Mathf.Lerp(
            freeLookCam.m_Orbits[2].m_Height,
            targetCamValue.botRig.x,
            Time.deltaTime * transitionSpeed
        );
        freeLookCam.m_Orbits[2].m_Radius = Mathf.Lerp(
            freeLookCam.m_Orbits[2].m_Radius,
            targetCamValue.botRig.y,
            Time.deltaTime * transitionSpeed
        );
    }

    private CameraStateValue GetTargetPosition()
    {
        switch (currentState)
        {
            case CameraState.Close:
                return closePosition;
            case CameraState.Middle:
                return middlePosition;
            case CameraState.Far:
                return farPosition;
            default:
                return middlePosition;
        }
    }

    private enum CameraState
    {
        Close,
        Middle,
        Far
    }

    [Serializable]
    public class CameraStateValue
    {
        public Vector2 topRig;
        public Vector2 midRig;
        public Vector2 botRig;
    }
}