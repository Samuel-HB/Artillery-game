using UnityEngine;

public class WheelRotation : MonoBehaviour
{
    private TankBehavior ref_TankBehavior;
    private WheelMovement ref_WheelMovement;
    private Vector3 lastPosition;

    void Start()
    {
        ref_TankBehavior = GetComponentInParent<TankBehavior>();
        ref_WheelMovement = GetComponentInChildren<WheelMovement>();
    }

    void Update()
    {
        if (ref_WheelMovement.isWheelGrounded == true)
        {
            if (transform.position.x > lastPosition.x + 0.001f) {
                transform.Rotate(0, 0, -ref_TankBehavior.speed * 150 * Time.deltaTime);
            }
            if (transform.position.x < lastPosition.x - 0.001f) {
                transform.Rotate(0, 0, ref_TankBehavior.speed * 150 * Time.deltaTime);
            }
        }
    }

    void LateUpdate()
    {
        lastPosition.x = transform.position.x;
    }
}
