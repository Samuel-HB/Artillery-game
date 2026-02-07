//using UnityEngine;

//public class ActualWheelMovement : MonoBehaviour
//{
//    public bool isActualWheelGrounded = false;

//    private void OnCollisionEnter2D(Collision2D collision)
//    {
//        if (collision.gameObject.GetComponent<AudioSource>() != null ||
//            collision.gameObject.GetComponentInParent<TankBehavior>() != null)
//        {
//            isActualWheelGrounded = true;
//        }
//    }

//    private void OnCollisionExit2D(Collision2D collision)
//    {
//        if (collision.gameObject.GetComponent<AudioSource>() != null) {
//            isActualWheelGrounded = false;
//        }
//        if (collision.gameObject.GetComponentInParent<TankBehavior>() != null) {
//            isActualWheelGrounded = false;
//        }
//    }
//}
