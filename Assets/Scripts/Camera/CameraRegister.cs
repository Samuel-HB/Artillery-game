using UnityEngine;
using Unity.Cinemachine;

public class CameraRegister : MonoBehaviour
{
    private CinemachineCamera actualCamera;
    private GameObject player;
    private Transform transformPlayer;
    [SerializeField] private string playerName;
    private int timer = 0;

    private void Start()
    {
        actualCamera = GetComponent<CinemachineCamera>();
    }

    private void Update()
    {
        timer++;

        if (timer == 1)
        {
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
