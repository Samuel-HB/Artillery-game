using UnityEngine;

[CreateAssetMenu(fileName = "TankData", menuName = "ScriptableObject/Tank Data")]

public class SO_Tank : ScriptableObject
{
    //public string tankName = "gus";
    public int health = 100;
    public float movementSpeed = 0;
    //public float airForce = 0;

    public bool hasHeavyCanon = true;
    public bool hasLightCanon = true;
    public bool hasProjectionCanon = true;

    public float fuelCapacity = 0f;
    public float fuelForRecovery = 0f;
}


