using UnityEngine;

public class BlackBoardTank : MonoBehaviour
{
    public Canon ref_Canon;
    public GameObject tankBody;
    public TankBody ref_TankBody;

    public GameObject wheelLeft;
    public GameObject wheelRight;
    //public CircleCollider2D triggerLeft;
    //public CircleCollider2D triggerRight;
    //public CircleCollider2D colliderLeft;
    //public CircleCollider2D colliderRight;
    public WheelMovement wheelMovementLeft;
    public WheelMovement wheelMovementRight;
    //public ActualWheelMovement actualWheelMovementLeft;
    //public ActualWheelMovement actualWheelMovementRight;

    public HealthBar healthBar;
    public FuelBar fuelBar;
    public Canvas canvasHealth;
    public Canvas canvasFuel;
}
