using UnityEngine;

namespace ScriptableObject
{
    [CreateAssetMenu(fileName = "PlatformData", menuName = "ScriptableObjects/PlatformData", order = 2)]
    public class PlatformData : UnityEngine.ScriptableObject
    {
        public float platformMovementDuration = 10f;
        public float startingPlatformDistance = 10f;
        public float delayForDestroyExtraParts = 5f;
        public float perfectTapTolerance = 0.25f;
        public int maximumTrashSize = 10;
    }
}
