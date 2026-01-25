using UnityEngine;

public class WheelMovement : MonoBehaviour
{
    public bool isWheelGrounded = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<AudioSource>() != null) {
            isWheelGrounded = true;
        }
        if (collision.gameObject.GetComponentInParent<TankBehavior>() != null) {
            isWheelGrounded = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<AudioSource>() != null) {
            isWheelGrounded = false;
        }
        if (collision.gameObject.GetComponentInParent<TankBehavior>() != null) {
            isWheelGrounded = true;
        }
    }
}
