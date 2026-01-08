using UnityEngine;

[CreateAssetMenu(fileName = "TankData", menuName = "ScriptableObject/Tank Data")]

public class SO_Tank : ScriptableObject
{
    public string tankName = "gus";
    public int health = 100;
    public float movementSpeed = 0;
    public float airForce = 0;
}
