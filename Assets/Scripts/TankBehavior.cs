using UnityEngine;

public class TankBehavior : MonoBehaviour
{
    public bool hasBeenHit = false;

    private Canon refrenceToCanonScript;
    [SerializeField] public SO_Tank so_tank;
    public int health;
    public HealthBar healthBar;
    public float speed;
    private bool isGrounded = false;

    [SerializeField] private WheelRotation ref_WheelRotationLeft;
    [SerializeField] private WheelRotation ref_WheelRotationRight;

    private Canvas canvas;
    private Vector3 canvasRelativePos;

    void Start()
    {
        refrenceToCanonScript = GetComponentInChildren<Canon>();
        speed = so_tank.movementSpeed;

        health = so_tank.health;
        healthBar = GetComponentInChildren<HealthBar>();
        canvasRelativePos = healthBar.transform.position;

        canvas = healthBar.GetComponentInParent<Canvas>();
        canvasRelativePos = canvas.transform.position;
    }
    
    void Update()
    {
        if (ref_WheelRotationLeft.isWheelGrounded == true && ref_WheelRotationRight.isWheelGrounded == true) {
            isGrounded = true;
            speed = so_tank.movementSpeed;
        }
        else if (ref_WheelRotationLeft.isWheelGrounded == false && ref_WheelRotationRight.isWheelGrounded == false) {
            isGrounded = false;
        }
        else if (ref_WheelRotationLeft.isWheelGrounded == true || ref_WheelRotationRight.isWheelGrounded == false) {
            isGrounded = true;
            speed = so_tank.movementSpeed / 2;
        }
        else if (ref_WheelRotationLeft.isWheelGrounded == false || ref_WheelRotationRight.isWheelGrounded == true) {
            isGrounded = true;
            speed = so_tank.movementSpeed / 2;
        }

        if (BattleManager.playerPlays == refrenceToCanonScript.tankID &&
            BattleManager.state == State.WaitingForInput && isGrounded == true)
        {
            if (Input.GetKey(KeyCode.RightArrow)) {
                transform.position += new Vector3(1, 0, 0) * speed * Time.deltaTime;
            }
            else if (Input.GetKey(KeyCode.LeftArrow)) {
                transform.position += new Vector3(-1, 0, 0) * speed * Time.deltaTime;
            }
        }

        canvas.transform.position = new Vector3(transform.position.x, (transform.position.y + canvasRelativePos.y));
        canvas.transform.rotation = Quaternion.identity;
    }
}
