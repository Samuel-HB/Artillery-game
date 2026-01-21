using UnityEngine;

public class Recovery : MonoBehaviour
{
    private TankBehavior ref_TankBehavior;
    private BlackBoardTank ref_BlackBoardTank;
    private Rigidbody2D rbTank;
    private int fuelNeeded = 2;

    private bool isTimerStarted = false;
    private int timer = 0;
    private float zero = 0f;

    private void Start()
    {
        ref_TankBehavior = GetComponent<TankBehavior>();
        ref_BlackBoardTank = GetComponent<BlackBoardTank>();
        rbTank = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (BattleManager.playerPlays == ref_BlackBoardTank.ref_Canon.tankID && ref_TankBehavior.fuel > fuelNeeded &&
            (ref_TankBehavior.isGrounded == true || ref_BlackBoardTank.ref_TankBody.isGrounded == true) &&
            (BattleManager.state == State.WaitingForInput || BattleManager.state == State.WaitingForInputAfterAttack))
        {
            if (Input.GetKeyDown(KeyCode.S) && isTimerStarted == false)
            {
                rbTank.AddForce(Vector3.up * 500);
                rbTank.AddForce(new Vector3(0, 0, transform.rotation.z) * 20);
                isTimerStarted = true;


                ref_TankBehavior.fuel -= fuelNeeded;
                ref_TankBehavior.blackBoardTank.fuelBar.UpdateFuelBar(ref_TankBehavior.so_tank.fuelCapacity, ref_TankBehavior.fuel);
            }
        }
    }

    private void FixedUpdate()
    {
        if (isTimerStarted == true)
        {
            timer++;

            if (timer < 60)
            {  
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.z, 0, ref zero, 0.3f);
                transform.rotation = Quaternion.Euler(0, 0, angle);
            }
            else
            {
                isTimerStarted = false;
                timer = 0;
            }
        }
    }
}
