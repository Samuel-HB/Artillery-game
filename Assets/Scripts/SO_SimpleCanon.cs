using UnityEngine;

[CreateAssetMenu(fileName = "SimpleCanon", menuName = "ScriptableObject/Simple Canon")]

public class SO_SimpleCanon : ScriptableObject
{
    public string weaponName = "gus";
    public int damage = 30;
    public int launchVelocity = 10;
}
