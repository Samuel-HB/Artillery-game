using UnityEngine;

public class TankBehavior : MonoBehaviour
{
    //[SerializeField] 
    public SO_Tank so_tank;
    public BlackBoardTank blackBoardTank;
    public Rigidbody2D rb;
    public int health;
    public float speed;
    public float fuel = 0f;
    public bool isGrounded = false;
    public bool hasBeenHit = false;
    public bool hasFall = false;
    public bool isDefeated = false;
    private Vector3 canvasRelativePos;

    private void Awake()
    {
        blackBoardTank = GetComponentInParent<BlackBoardTank>();
        fuel = so_tank.fuelCapacity;
        speed = so_tank.movementSpeed;
        health = so_tank.health;
        canvasRelativePos = blackBoardTank.canvasHealth.transform.position;

        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (blackBoardTank.wheelMovementLeft.isWheelGrounded == true || blackBoardTank.wheelMovementRight.isWheelGrounded == true) {
            isGrounded = true;
        }
        else if (blackBoardTank.wheelMovementLeft.isWheelGrounded == false && blackBoardTank.wheelMovementRight.isWheelGrounded == false) {
            isGrounded = false;
        }

        //if (blackBoardTank.wheelMovementLeft.isWheelGrounded == true && blackBoardTank.wheelMovementRight.isWheelGrounded == true) {
        //    isGrounded = true;
        //    //speed = so_tank.movementSpeed;
        //}
        //else if (blackBoardTank.wheelMovementLeft.isWheelGrounded == false && blackBoardTank.wheelMovementRight.isWheelGrounded == false
        //      && blackBoardTank.actualWheelMovementLeft.isActualWheelGrounded == false && blackBoardTank.actualWheelMovementRight.isActualWheelGrounded == false)
        //{
        //    isGrounded = false;
        //}
        //else if (blackBoardTank.wheelMovementLeft.isWheelGrounded == true || blackBoardTank.wheelMovementRight.isWheelGrounded == false) {
        //    isGrounded = true;
        //    //speed = so_tank.movementSpeed / 2;
        //}
        //else if (blackBoardTank.wheelMovementLeft.isWheelGrounded == false || blackBoardTank.wheelMovementRight.isWheelGrounded == true) {
        //    isGrounded = true;
        //    //speed = so_tank.movementSpeed / 2;
        //}
        //else if (blackBoardTank.actualWheelMovementLeft.isActualWheelGrounded == true && blackBoardTank.actualWheelMovementRight.isActualWheelGrounded == false)
        //{
        //    isGrounded = true;
        //    //speed = so_tank.movementSpeed / 2;
        //}
        //else if (blackBoardTank.actualWheelMovementLeft.isActualWheelGrounded == false && blackBoardTank.actualWheelMovementRight.isActualWheelGrounded == true)
        //{
        //    isGrounded = true;
        //    //speed = so_tank.movementSpeed / 2;
        //}


        //if (blackBoardTank.actualWheelMovementLeft.isActualWheelGrounded == true && blackBoardTank.actualWheelMovementRight.isActualWheelGrounded == true) {
        //    isGrounded = true;
        //    //speed = so_tank.movementSpeed;
        //}

        //    //if (blackBoardTank.actualWheelMovementLeft.isActualWheelGrounded == false && blackBoardTank.actualWheelMovementRight.isActualWheelGrounded == false) {
        //    //    isGrounded = false;
        //    //}

        //else if (blackBoardTank.actualWheelMovementLeft.isActualWheelGrounded == true && blackBoardTank.actualWheelMovementRight.isActualWheelGrounded == false) {
        //    isGrounded = true;
        //    //speed = so_tank.movementSpeed / 2;
        //}
        //else if (blackBoardTank.actualWheelMovementLeft.isActualWheelGrounded == false && blackBoardTank.actualWheelMovementRight.isActualWheelGrounded == true) {
        //    isGrounded = true;
        //    //speed = so_tank.movementSpeed / 2;
        //}
    }

    private void FixedUpdate()
    {
        if (BattleManager.playerPlays == blackBoardTank.ref_Canon.tankID && fuel > 0 && isGrounded == true &&
            (BattleManager.state == State.WaitingForInput || BattleManager.state == State.WaitingForInputAfterAttack))
        {
            if (Input.GetKey(KeyCode.D))
            {
                transform.position += new Vector3(1, 0, 0) * speed * Time.deltaTime;
                //rb.MovePosition(transform.position + new Vector3(1, 0, 0) * speed  * Time.deltaTime);
                fuel -= 0.04f;
                blackBoardTank.fuelBar.UpdateFuelBar(so_tank.fuelCapacity, fuel);
            }
            else if (Input.GetKey(KeyCode.Q))
            {
                transform.position += new Vector3(-1, 0, 0) * speed * Time.deltaTime;
                //rb.MovePosition(transform.position + new Vector3(-1, 0, 0) * speed * Time.deltaTime);
                fuel -= 0.04f;
                blackBoardTank.fuelBar.UpdateFuelBar(so_tank.fuelCapacity, fuel);
            }
        }
        blackBoardTank.canvasHealth.transform.position = new Vector3(transform.position.x, (transform.position.y + canvasRelativePos.y));
        blackBoardTank.canvasHealth.transform.rotation = Quaternion.identity;
        blackBoardTank.canvasFuel.transform.position = new Vector3(transform.position.x, (transform.position.y + canvasRelativePos.y + 0.6f));
        blackBoardTank.canvasFuel.transform.rotation = Quaternion.identity;
    }
}
