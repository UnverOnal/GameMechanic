using UnityEngine;

namespace ScriptableObject
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData", order = 1)]
    public class PlayerData : UnityEngine.ScriptableObject
    {
        public float playerSpeed = 1f;
        public float lookRotationFactor = 3f;
    }
}
