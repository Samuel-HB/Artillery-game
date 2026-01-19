using UnityEngine;

public class TankBehavior : MonoBehaviour
{
    private Canon refrenceToCanonScript;
    [SerializeField] public SO_Tank so_tank;
    public int health;
    public HealthBar healthBar;
    public FuelBar fuelBar;
    public float speed;
    public float fuel = 0f;
    private bool isGrounded = false;

    public bool hasBeenHit = false;
    public bool isDefeated = false;

    [SerializeField] private WheelRotation ref_WheelRotationLeft;
    [SerializeField] private WheelRotation ref_WheelRotationRight;

    private Canvas canvasHealth;
    private Canvas canvasFuel;
    private Vector3 canvasRelativePos;


    void Start()
    {
        refrenceToCanonScript = GetComponentInChildren<Canon>();

        fuel = so_tank.fuelCapacity;
        speed = so_tank.movementSpeed; // la ligne était en dessous de ref Canon script
        health = so_tank.health;

        healthBar = GetComponentInChildren<HealthBar>();
        fuelBar = GetComponentInChildren<FuelBar>();

        canvasHealth = healthBar.GetComponentInParent<Canvas>();
        canvasFuel = fuelBar.GetComponentInParent<Canvas>();
        canvasRelativePos = canvasHealth.transform.position;
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

        //if (BattleManager.playerPlays == refrenceToCanonScript.tankID && fuel > 0 &&
        //BattleManager.state == State.WaitingForInput && isGrounded == true)

        if (BattleManager.playerPlays == refrenceToCanonScript.tankID && fuel > 0 && isGrounded == true &&
            (BattleManager.state == State.WaitingForInput || BattleManager.state == State.WaitingForInputAfterAttack))
        {
            if (Input.GetKey(KeyCode.RightArrow)) {
                transform.position += new Vector3(1, 0, 0) * speed * Time.deltaTime;
                //fuel -= 0.005f - Time.deltaTime; // ajout de time
                fuel -= 0.005f;
                fuelBar.UpdateFuelBar(so_tank.fuelCapacity, fuel);
            }
            else if (Input.GetKey(KeyCode.LeftArrow)) {
                transform.position += new Vector3(-1, 0, 0) * speed * Time.deltaTime;
                //fuel -= 0.005f - Time.deltaTime; // ajout de time
                fuel -= 0.005f;
                fuelBar.UpdateFuelBar(so_tank.fuelCapacity, fuel);
            }
        }

        canvasHealth.transform.position = new Vector3(transform.position.x, (transform.position.y + canvasRelativePos.y));
        canvasHealth.transform.rotation = Quaternion.identity;
        canvasFuel.transform.position = new Vector3(transform.position.x, (transform.position.y + canvasRelativePos.y + 0.6f));
        canvasFuel.transform.rotation = Quaternion.identity;
    }
}
