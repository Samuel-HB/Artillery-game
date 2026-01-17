using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Tilemaps;

public class Explosion : MonoBehaviour
{
    private CircleCollider2D coll;
    private float maxHitDistance;
    private float minHitDistance = 2f;

    public int maxExplosionDamage = 0;
    private int minExplosionDamage = 1;

    [SerializeField] private int maxPushForce = 400;
    private int minPushForce = 1;



    private void Start()
    {
        BattleManager.explosionJustOver = false;

        coll = GetComponent<CircleCollider2D>();
        maxHitDistance = coll.radius;

        minExplosionDamage = maxExplosionDamage / 3;
    }

    private void Update()
    {
    }

    private void MapCollision()
    {
        for (int i = 0; i < 3; i++)
        {
            TileMapInterraction.tilesPositions.Add(new Vector3Int((int)(transform.position.x) - 1 + i,
                                                                    (int)(transform.position.y) - 1));
        }
        for (int i = 0; i < 3; i++)
        {
            TileMapInterraction.tilesPositions.Add(new Vector3Int((int)(transform.position.x) - 1 + i,
                                                                    (int)(transform.position.y)));
        }
        for (int i = 0; i < 3; i++)
        {
            TileMapInterraction.tilesPositions.Add(new Vector3Int((int)(transform.position.x),
                                                                    (int)(transform.position.y) + 1));
        }
        TileMapInterraction.isStartedTileExplosion = true;
        Destroy(gameObject);
        BattleManager.explosionJustOver = true;
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

            if (distance < minHitDistance) {
                distance = minHitDistance;
            }
            //if (distance > 6f)
            //{
            //    distance = 6f;
            //}

            //    n = (actualDistance - minDistance) / (maxDistance - minDistance)
            //                   n * minDistance + (1 - n) * maxDistance
            float n = (distance - minHitDistance) / (maxHitDistance - minHitDistance);
            float damageResult = n * minExplosionDamage + (1 - n) * maxExplosionDamage;
            float pushForceResult = n * minPushForce + (1 - n) * maxPushForce;

            ref_TankBehavior.health -= (int)damageResult;
            ref_TankBehavior.healthBar.UpdateHealthBar(ref_TankBehavior.so_tank.health, ref_TankBehavior.health);
            rbTank.AddForce(-pushDirection * pushForceResult);


            if (ref_TankBehavior.health <= 0) {
                ref_TankBehavior.isDefeated = true;
            }


            Debug.Log("health: " + ref_TankBehavior.health + " - distance: " + distance);
            // parfois valeurs négatives dans la console, peut etre parce que le pushforce
            // est quand même calculé même quand il est en dessous de la distance minimale

            Destroy(gameObject);
            ref_TankBehavior.hasBeenHit = true;
            BattleManager.explosionJustOver = true; // au lieu de mettre ça en true direct
            // mettre un timer qui s'enclenche à ce moment là, le temps que la caméra se repositionne et s'arrête lentement
            // avant de mettre en false

            // de toute manière changer cette variable pour une qui ne s'enclenche que lorsque le tour est fini
            // ce qui peut arriver après un tir, ou même avant

            MapCollision();

        }
        else
        {
            Destroy(gameObject);
            BattleManager.explosionJustOver = true;

            MapCollision();
        }
    }
}
