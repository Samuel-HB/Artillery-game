using UnityEngine;

public class WheelRotation : MonoBehaviour
{
    private TankBehavior refTankBehavior;

    private Vector3 lastPosition;

    void Start()
    {
        refTankBehavior = GetComponentInParent<TankBehavior>();
    }


    void Update()
    {
        if (transform.position.x > lastPosition.x + 0.001f) {
            transform.Rotate(0, 0, -refTankBehavior.speed * 20 * Time.deltaTime);
        }
        if (transform.position.x < lastPosition.x - 0.001f) {
            transform.Rotate(0, 0, refTankBehavior.speed * 20 * Time.deltaTime);
        }
    }
    // pour que ce ne soit que lorsqu'on se déplace avec les boutons, faire référence aux roues
    // dans tankBehavior et mettre dans la condition ce script de rotation

    void LateUpdate()
    {
        lastPosition.x = transform.position.x;
    }
}
