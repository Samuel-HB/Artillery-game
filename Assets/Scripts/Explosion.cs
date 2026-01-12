using UnityEngine;

public class Explosion : MonoBehaviour
{
    private CircleCollider2D coll;
    private float maxHitDistance;
    public int explosionDamage = 0;
    [SerializeField] private float pushForce = 3f;

    private void Start()
    {
        coll = GetComponent<CircleCollider2D>();
        maxHitDistance = coll.radius;
    }

    private void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponentInParent<TankBehavior>() != null &&
            collision.gameObject.GetComponentInParent<TankBehavior>().hasBeenHit == false)
            // because each tank has multiple collider the function execute for each of them,
            // so changed variable of each tank when hit and check it, to only hit once
        {
            TankBehavior ref_TankBehavior = collision.GetComponentInParent<TankBehavior>();
            // find the parent gameObject desired by the unic component TankBehavior,
            // wich he is the only one to have,
            // and getComponnent from this GameObject and not the others

            Rigidbody2D rbTank = ref_TankBehavior.GetComponent<Rigidbody2D>();
            Transform tankTransform = ref_TankBehavior.GetComponent<Transform>();
            float distance = Vector3.Distance(transform.position, tankTransform.position);
            Vector3 pushDirection = (transform.position - tankTransform.position).normalized;

            if (distance < 2f ) {
                distance = 2f;
            }
            //if (distance > 6f)
            //{
            //    distance = 6f;
            //}

            pushForce /= distance / maxHitDistance;
            ref_TankBehavior.health -= (int)(explosionDamage / distance);
            rbTank.AddForce(-pushDirection * pushForce);

            Debug.Log(ref_TankBehavior.health + "   distance: " + distance);
            Debug.Log("pushForce: " + pushForce);

            Destroy(gameObject);
            ref_TankBehavior.hasBeenHit = true;
        }
        else if (collision.gameObject.tag == "ground")
        {
            Destroy(gameObject);
        }
    }
}
