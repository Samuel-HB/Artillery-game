using UnityEngine;

public class TankBody : MonoBehaviour
{
    public bool isGrounded = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<AudioSource>() != null) {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<AudioSource>() != null) {
            isGrounded = false;
        }
    }
}
