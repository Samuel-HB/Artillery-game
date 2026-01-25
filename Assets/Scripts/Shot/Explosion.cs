using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Explosion : MonoBehaviour
{
    private CircleCollider2D coll;
    private float maxHitDistance;
    private float minHitDistance = 2f;
    public int maxExplosionDamage = 0;
    private int minExplosionDamage = 1;
    [SerializeField] private int maxPushForce = 400;
    private int minPushForce = 1;

    private CameraManager ref_CameraManager;
    //private int i = 0;
    //private bool isTimerStarted = false;
    //private List<TankBehavior> tanksBehavior;

    private void Start()
    {
        ref_CameraManager = GameObject.FindFirstObjectByType<CameraManager>();

        BattleManager.explosionJustOver = false;

        coll = GetComponent<CircleCollider2D>();
        maxHitDistance = coll.radius;
        minExplosionDamage = maxExplosionDamage / 3;

        //tanksBehavior = new List<TankBehavior>();
    }

    private void MapCollision() //destroy map in a circlar zone
    {
        for (int i = 0; i < 3; i++)
        {
            TileMapInterraction.tilesPositions.Add(new Vector3Int((int)(transform.position.x) - 1 + i,
                                                                    (int)(transform.position.y) - 2));
        }
        for (int i = 0; i < 5; i++)
        {
            TileMapInterraction.tilesPositions.Add(new Vector3Int((int)(transform.position.x) - 2 + i,
                                                                    (int)(transform.position.y) - 1));
        }
        for (int i = 0; i < 5; i++)
        {
            TileMapInterraction.tilesPositions.Add(new Vector3Int((int)(transform.position.x) - 2 + i,
                                                                    (int)(transform.position.y)));
        }
        for (int i = 0; i < 5; i++)
        {
            TileMapInterraction.tilesPositions.Add(new Vector3Int((int)(transform.position.x) - 2 + i,
                                                                    (int)(transform.position.y) + 1));
        }
        for (int i = 0; i < 3; i++)
        {
            TileMapInterraction.tilesPositions.Add(new Vector3Int((int)(transform.position.x) - 1 + i,
                                                                    (int)(transform.position.y) + 2));
        }
        TileMapInterraction.isStartedTileExplosion = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // because each tank has multiple collider the function execute for each of them,
        // so changed variable of each tank when hit and check it, to only hit once
        if (collision.gameObject.GetComponentInParent<TankBehavior>() != null &&
            collision.gameObject.GetComponentInParent<TankBehavior>().hasBeenHit == false)
        {
            TankBehavior ref_TankBehavior = collision.GetComponentInParent<TankBehavior>();

            Rigidbody2D rbTank = ref_TankBehavior.GetComponent<Rigidbody2D>();
            Transform tankTransform = ref_TankBehavior.GetComponent<Transform>();
            float distance = Vector3.Distance(transform.position, tankTransform.position);
            Vector3 pushDirection = (transform.position - tankTransform.position).normalized;

            if (distance < minHitDistance) {
                distance = minHitDistance;
            }
            //    n = (actualDistance - minDistance) / (maxDistance - minDistance)
            //                   n * minDistance + (1 - n) * maxDistance
            float n = (distance - minHitDistance) / (maxHitDistance - minHitDistance);
            float damageResult = n * minExplosionDamage + (1 - n) * maxExplosionDamage;
            float pushForceResult = n * minPushForce + (1 - n) * maxPushForce;

            ref_TankBehavior.health -= (int)damageResult;
            ref_TankBehavior.blackBoardTank.healthBar.UpdateHealthBar(ref_TankBehavior.so_tank.health, ref_TankBehavior.health);
            rbTank.AddForce(-pushDirection * pushForceResult);


            if (ref_TankBehavior.health <= 0)
            {
                ref_TankBehavior.isDefeated = true;
                ref_CameraManager.ChangeCamera();

                ref_CameraManager.tanksBehavior.Add(ref_TankBehavior);
                ref_CameraManager.isTimerStarted = true;
                ref_CameraManager.ChangeCamera();
                BattleManager.playerDefeat = true;
            }

            Destroy(gameObject);
            ref_TankBehavior.hasBeenHit = true;
            BattleManager.explosionJustOver = true;
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
