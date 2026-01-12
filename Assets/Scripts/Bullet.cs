using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Transform explosion;
    //private Canon ref_Canon;
    //TankBehavior ref_TankBehavior;

    public int damage = 0;

    public float timeBeforeDestroy = 3f;
    Rigidbody2D rb;

    //void Awake()
    //{
    //    Destroy(gameObject, timeBeforeDestroy);
    //}

    private void Start()
    {
        //ref_Canon = GetComponentInParent<Canon>();
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
        explosion = Instantiate(explosion, transform.position, Quaternion.identity);
        explosion.GetComponent<Explosion>().explosionDamage = damage;

        //if (collision.gameObject.GetComponent<TankBehavior>() != null)
        //{
        //    ref_TankBehavior = collision.gameObject.GetComponent<TankBehavior>();
        //    ref_TankBehavior.health -= damage;
        //    Debug.Log(ref_TankBehavior.health);
        //}
    }
}
