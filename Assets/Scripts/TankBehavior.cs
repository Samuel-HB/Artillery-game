using UnityEngine;

public class TankBehavior : MonoBehaviour
{
    //[SerializeField] private SO_Tank so_tank;
    private Canon refrenceToCanonScript;
    [SerializeField] private SO_Tank so_tank;
    public int health;
    public float speed;

    void Start()
    {
        refrenceToCanonScript = GetComponentInChildren<Canon>();
        health = so_tank.health;
        speed = so_tank.movementSpeed;
    }
    
    void Update()
    {
        if (BattleManager.playerPlays == refrenceToCanonScript.tankID && BattleManager.state == State.WaitingForInput)
        {
            if (Input.GetKey(KeyCode.RightArrow)) {
                transform.position += new Vector3(1, 0, 0) * speed * Time.deltaTime;
            }
            else if (Input.GetKey(KeyCode.LeftArrow)) {
                transform.position += new Vector3(-1, 0, 0) * speed * Time.deltaTime;
            }
        }
    }
}
