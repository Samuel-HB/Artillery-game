using UnityEngine;

public class BlackBoardTank : MonoBehaviour
{
    public Canon ref_Canon;
    public GameObject tankBody;
    public GameObject wheelLeft;
    public GameObject wheelRight;
    public CircleCollider2D triggerLeft;
    public CircleCollider2D triggerRight;
    public TankBody ref_TankBody;
    public HealthBar healthBar;
    public FuelBar fuelBar;
    public Canvas canvasHealth;
    public Canvas canvasFuel;
    public WheelMovement wheelMovementLeft;
    public WheelMovement wheelMovementRight;
}
