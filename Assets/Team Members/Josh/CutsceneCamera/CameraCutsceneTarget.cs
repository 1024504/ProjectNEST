    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCutsceneTarget : MonoBehaviour
{
    [SerializeField] CameraTracker cameraTracker;
    [SerializeField] [Range(0.0f, 1.0f)] public float targetSpeed = 0.3f;

    private void Start()
    {
        cameraTracker = FindObjectOfType<CameraTracker>();
    }

    public void CameraCutsceneStart()
    {
        cameraTracker.CutsceneModeEnable(transform.position);
        cameraTracker.CutsceneSpeedUpdate(targetSpeed);
    }

    public void CameraCutsceneEnd()
    {
        cameraTracker.CutsceneModeDisable();
    }    
    
    public void CameraCutsceneUpdate()
    {
        print("i'm updating the tracker pos");
        cameraTracker.CutscenePositionUpdate(transform.position);
        cameraTracker.CutsceneSpeedUpdate(targetSpeed);
    }

    public void CameraCutsceneSpeedUpdate()
    {
        cameraTracker.CutsceneSpeedUpdate(targetSpeed);
    }
}
