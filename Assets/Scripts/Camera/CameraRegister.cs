using UnityEngine;
using Unity.Cinemachine;

public class CameraRegister : MonoBehaviour
{
    private CinemachineCamera actualCamera;

    private GameObject player;
    private Transform transformPlayer;
    [SerializeField] private string playerName;

    //private void Start()
    //{
    //    actualCamera = GetComponent<CinemachineCamera>();

    //    player = GameObject.Find(playerName);

    //    if (player != null)
    //    {
    //        transformPlayer = player.GetComponent<Transform>();
    //        actualCamera.Target.TrackingTarget = transformPlayer;
    //    }
    //}

    private void Start() // new (start)
    {
        actualCamera = GetComponent<CinemachineCamera>();
    }

    int timer = 0;
    private void Update()
    {
        timer++;

        if (timer == 1)
        {
            //actualCamera = GetComponent<CinemachineCamera>();

            player = GameObject.Find(playerName);

            if (player != null)
            {
                transformPlayer = player.GetComponent<Transform>();
                actualCamera.Target.TrackingTarget = transformPlayer;
            }
        }
    }

    //private void FindBullet()
    //{
    //    Transform transformBullet = GameObject.FindFirstObjectByType<Bullet>().transform;
    //    actualCamera.Target.TrackingTarget = transformBullet;
    //    //CameraContainer.camShoot
    //    //CameraManager.SwitchCamera(ref_CameraContainer.cam4);
    //}

    private void OnEnable()
    {
        CameraManager.AddCamera(GetComponent<CinemachineCamera>());
    }

    private void OnDisable()
    {
        CameraManager.RemoveCamera(GetComponent<CinemachineCamera>());
    }
}
