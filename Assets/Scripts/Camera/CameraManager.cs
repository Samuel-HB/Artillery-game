using UnityEngine;
using Unity.Cinemachine;
using System.Collections.Generic;

public class CameraManager : MonoBehaviour
{
    static List<CinemachineCamera> cameras = new List<CinemachineCamera>();
    public static CinemachineCamera activeCamera = null;

    //new line
    [SerializeField] CameraContainer ref_CameraContainer;


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

    // tous ces moyens détournés pour éviter d'avoir une référence nulle
    //(d'abord enreguister l'objet touvé grâce à son script par le script, puis ensuite accéder au transform)
    // peut etre pas besoin de passer avant cela par le gameObject
    public void FindBullet() // peut etre pas le temps de trouver la bullet qu'elle est déjà détruite
    {
        Bullet ref_Bullet = GameObject.FindFirstObjectByType<Bullet>();
        //GameObject gameObjectBullet = GameObject.FindFirstObjectByType<Bullet>().gameObject;
        //Transform transformBullet = GameObject.FindFirstObjectByType<Bullet>().transform;
        //if (transformBullet != null)
        //if (gameObjectBullet != null)
        if (ref_Bullet != null)
        {
            GameObject go_bullet = ref_Bullet.GetComponent<Bullet>().gameObject;
            Transform transformBullet = go_bullet.GetComponent<Bullet>().transform;
            //Transform transformBullet = GameObject.FindFirstObjectByType<Bullet>().transform;
            ref_CameraContainer.camShoot.Target.TrackingTarget = transformBullet;
            SwitchCamera(ref_CameraContainer.camShoot);
        }
    }

    public void ChangeCamera()
    {
        if (BattleManager.playerPlays == 0) {
            SwitchCamera(ref_CameraContainer.cam1);
        }
        if (BattleManager.playerPlays == 1) {
            SwitchCamera(ref_CameraContainer.cam2);
        }
        if (BattleManager.playerPlays == 2) {
            SwitchCamera(ref_CameraContainer.cam3);
        }
        if (BattleManager.playerPlays == 3) {
            SwitchCamera(ref_CameraContainer.cam4);
        }
    }
}
