using UnityEngine;
using Unity.Cinemachine;
using System.Collections.Generic;

public class CameraManager : MonoBehaviour
{
    static List<CinemachineCamera> cameras = new List<CinemachineCamera>();
    public static CinemachineCamera activeCamera = null;
    [SerializeField] private CameraContainer ref_CameraContainer;

    public static bool isCameraTimerOver = true;
    private int timer = 0;
    public bool isTimerStarted = false;
    private int i = 0;
    public List<TankBehavior> tanksBehavior;

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

    public void FindBullet() // these lines for checking if bullet isn't already destroyed before finded
    {
        Bullet ref_Bullet = GameObject.FindFirstObjectByType<Bullet>();
        if (ref_Bullet != null)
        {
            GameObject go_bullet = ref_Bullet.GetComponent<Bullet>().gameObject;
            Transform transformBullet = go_bullet.GetComponent<Bullet>().transform;
            ref_CameraContainer.camShoot.Target.TrackingTarget = transformBullet;
            SwitchCamera(ref_CameraContainer.camShoot);
        }
    }

    public void ChangeCamera()
    {
        if (BattleManager.playerDefeat == true)
        {
            BattleManager.playerDefeat = false;
            isCameraTimerOver = false;
        }

        if (isCameraTimerOver == false) {
            SwitchCamera(ref_CameraContainer.camGlobal);
        }
        //else if (BattleManager.tanks[BattleManager.playerPlays].GetComponent<TankBehavior>().isDefeated == true) {
        //    SwitchCamera(ref_CameraContainer.camGlobal);
        //}
        else
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

    private void Start()
    {
        tanksBehavior = new List<TankBehavior>();
    }

    private void FixedUpdate()
    {
        if (isCameraTimerOver == false)
        {
            timer++;
            if (timer > 180)
            {
                isCameraTimerOver = true;
                timer = 0;
                ChangeCamera();
            }
        }

        if (isTimerStarted == true)
        {
            i++;
            if (i > 120)
            {
                foreach (TankBehavior refTank in tanksBehavior) {
                    refTank.transform.position = new Vector3(1000, 0);
                }
                tanksBehavior.Clear();
                i = 0;
                isTimerStarted = false;
            }
        }
    }
}
