using UnityEngine;

public class WheelRotation : MonoBehaviour
{
    private TankBehavior ref_TankBehavior;
    private Vector3 lastPosition;
    public bool isWheelGrounded = false;

    void Start()
    {
        ref_TankBehavior = GetComponentInParent<TankBehavior>();
    }

    void Update()
    {
        if (isWheelGrounded == true)
        {
            if (transform.position.x > lastPosition.x + 0.001f) {
                transform.Rotate(0, 0, -ref_TankBehavior.speed * 65 * Time.deltaTime);
            }
            if (transform.position.x < lastPosition.x - 0.001f) {
                transform.Rotate(0, 0, ref_TankBehavior.speed * 65 * Time.deltaTime);
            }
        }
    }

    void LateUpdate()
    {
        lastPosition.x = transform.position.x;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<AudioSource>() != null) {
            isWheelGrounded = true;
        }
        //isWheelGrounded = true;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<AudioSource>() != null) {
            isWheelGrounded = false;
        }
        //isWheelGrounded = true;
    }
}
