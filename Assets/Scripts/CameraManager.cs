using UnityEngine;
using Unity.Cinemachine;
using System.Collections.Generic;

public class CameraManager : MonoBehaviour
{
    static List<CinemachineCamera> cameras = new List<CinemachineCamera>();
    public static CinemachineCamera activeCamera = null;

    public static bool IsCameraActive(CinemachineCamera cam)
    {
        return cam == activeCamera;
    }

    public static void SwitchCamera(CinemachineCamera newCam)
    {
        newCam.Priority = 10;
        activeCamera = newCam;

        foreach (CinemachineCamera cam in cameras)
        {
            if (cam != newCam) {
                cam.Priority = 0;
            }
        }
    }

    public static void AddCamera(CinemachineCamera cam)
    { 
        cameras.Add(cam);
    }

    public static void RemoveCamera(CinemachineCamera cam)
    {
        cameras.Remove(cam);
    }
}
