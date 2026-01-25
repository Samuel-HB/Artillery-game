using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Transform explosion;
    private Rigidbody2D rb;
    public int damage = 0;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float angle = Mathf.Atan2(rb.linearVelocity.y, rb.linearVelocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }


    // in projects settings layer 6 (player) and 7 (tank) can't interract with each other
    // same thing for destructible and undestructible tilemaps (8 and 9)
    private void OnCollisionEnter2D(Collision2D collision)
    {
        BlackBoardTank blackBoardTank = collision.gameObject.GetComponentInParent<BlackBoardTank>();

        if (blackBoardTank == null) {
            BulletExplode();
        }
        else if (blackBoardTank.ref_Canon.tankID != BattleManager.playerPlays) {
            BulletExplode();
        }
        // nothing happend to the tank who launch fire
    }

    private void BulletExplode()
    {
        Destroy(gameObject);
        explosion = Instantiate(explosion, transform.position, Quaternion.identity);
        explosion.GetComponent<Explosion>().maxExplosionDamage = damage;
    }
}
