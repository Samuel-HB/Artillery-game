using UnityEngine;

public class FallCollision : MonoBehaviour
{
    [SerializeField] private SpawnPoints ref_SpawnPoints;
    private int fallingDamage = 35;
    private Transform respawnPosition;
    [SerializeField] private CameraManager ref_CameraManager;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponentInParent<TankBehavior>() != null &&
            collision.gameObject.GetComponentInParent<TankBehavior>().hasFall == false)
        {
            TankBehavior ref_TankBehavior = collision.gameObject.GetComponentInParent<TankBehavior>();

            ref_TankBehavior.health -= fallingDamage;
            ref_TankBehavior.blackBoardTank.healthBar.UpdateHealthBar(ref_TankBehavior.so_tank.health, ref_TankBehavior.health);

            if (ref_TankBehavior.health <= 0)
            {
                ref_TankBehavior.isDefeated = true;
                ref_CameraManager.tanksBehavior.Add(ref_TankBehavior);
                ref_CameraManager.isTimerStarted = true;
                BattleManager.playerDefeat = true;
                ref_CameraManager.ChangeCamera();
            }
            ref_TankBehavior.hasFall = true; // to avoid repeteting this collision function more than one time a frame

            int random = Random.Range(0, 2);
            if (random == 0) {
                respawnPosition = ref_SpawnPoints.spawmPoint2P_1;
            } else {
                respawnPosition = ref_SpawnPoints.spawmPoint2P_2;
            }
            ref_TankBehavior.transform.position = new Vector3(respawnPosition.transform.position.x,
                                                              respawnPosition.transform.position.y);
        }
    }
}
