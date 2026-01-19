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

    int timer = 0;
    private void Update()
    {
        timer++;

        if (timer == 1)
        {
            actualCamera = GetComponent<CinemachineCamera>();

            player = GameObject.Find(playerName);

            if (player != null)
            {
                transformPlayer = player.GetComponent<Transform>();
                actualCamera.Target.TrackingTarget = transformPlayer;
            }
        }
    }

    private void OnEnable()
    {
        CameraManager.AddCamera(GetComponent<CinemachineCamera>());
    }

    private void OnDisable()
    {
        CameraManager.RemoveCamera(GetComponent<CinemachineCamera>());
    }
}
