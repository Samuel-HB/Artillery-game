using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Transform explosion;
    private Rigidbody2D rb;
    public int damage = 0;
    //public float timeBeforeDestroy = 3f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float angle = Mathf.Atan2(rb.linearVelocity.y, rb.linearVelocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }


    // in projects settings layer 6 and 7 can't interract with each other
    private void OnCollisionEnter2D(Collision2D collision)
    {
        BlackBoardTank blackBoardTank = collision.gameObject.GetComponentInParent<BlackBoardTank>();

        if (blackBoardTank == null)
        {
            Destroy(gameObject);
            explosion = Instantiate(explosion, transform.position, Quaternion.identity);
            explosion.GetComponent<Explosion>().maxExplosionDamage = damage;
        }
        else if (blackBoardTank.ref_Canon.tankID == BattleManager.playerPlays) {
            //nothing
        }
        else if (blackBoardTank.ref_Canon.tankID != BattleManager.playerPlays)
        {
            Destroy(gameObject);
            explosion = Instantiate(explosion, transform.position, Quaternion.identity);
            explosion.GetComponent<Explosion>().maxExplosionDamage = damage;
        }
    }
}
