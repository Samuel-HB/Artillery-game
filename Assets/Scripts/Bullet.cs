using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Transform explosion;
    public int damage = 0;
    public float timeBeforeDestroy = 3f;
    Rigidbody2D rb;
    // in projects settings layer 6 and 7 can't interract with each other

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
        BlackBoardTank blackBoardTank = collision.gameObject.GetComponentInParent<BlackBoardTank>();

        if (blackBoardTank == null)
        {
            //BattleManager.hasExplode = true;
            Destroy(gameObject);
            explosion = Instantiate(explosion, transform.position, Quaternion.identity);
            explosion.GetComponent<Explosion>().maxExplosionDamage = damage;
        }
        else if (blackBoardTank.pivotCanon.tankID == BattleManager.playerPlays) {
            //nothing
        }
        else if (blackBoardTank.pivotCanon.tankID != BattleManager.playerPlays)
        {
            //BattleManager.hasExplode = true;
            Destroy(gameObject);
            explosion = Instantiate(explosion, transform.position, Quaternion.identity);
            explosion.GetComponent<Explosion>().maxExplosionDamage = damage;
        }
    }
}
