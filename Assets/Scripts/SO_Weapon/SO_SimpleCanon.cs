using UnityEngine;

[CreateAssetMenu(fileName = "SimpleCanon", menuName = "ScriptableObject/Simple Canon")]

public class SO_SimpleCanon : ScriptableObject
{
    public string weaponName = "gus";
    public GameObject bullet;
    public int damage = 30;
    public int launchVelocity = 10;
    public int pointsNumber = 15;
    public float pointSpace = 0.8f;
}
