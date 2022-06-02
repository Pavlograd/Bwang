using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "PlayerData/PlayerData")]
public class PlayerData : ScriptableObject
{
    public float speed = 0.1f;
    public float force;
    public float density;
    public float startMass = 0.01f;
}
