using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float timeBeforeDestroy = 3f;
    Rigidbody2D rb;

    //void Awake()
    //{
    //    Destroy(gameObject, timeBeforeDestroy);
    //}

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float angle = Mathf.Atan2(rb.linearVelocity.y, rb.linearVelocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        BattleManager.hasExplode = true;
        Destroy(gameObject);

        if (collision.gameObject.GetComponent<TankBehavior>() != null)
        {
            // changement de refTankMovement en refTankBehavior
            TankBehavior refTankBehavior = collision.gameObject.GetComponent<TankBehavior>();
            //TankMovement refTankMovement = this;
            refTankBehavior.health -= 50;
            Debug.Log(refTankBehavior.health);
        }
    }
}
