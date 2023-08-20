using UnityEngine;

namespace Player
{
    public static class Movement
    {
        public static void MoveForward(this Transform transform, Vector3 basePosition, float speed)
        {
            var position = transform.position;
            var targetPosition = position + Vector3.forward * speed;
            targetPosition.x = basePosition.x;
            
            var smoothedPosition = Vector3.Lerp(position, targetPosition, Time.deltaTime);
            transform.position = smoothedPosition;
        }

        public static void Look(this Transform transform, Vector3 basePosition, float lookFactor)
        {
            var position = transform.position;
            var targetPosition = position + Vector3.forward;
            targetPosition.x = basePosition.x;
            var rotation = transform.rotation;
            
            var direction = (targetPosition - position).normalized;
            
            var targetRotation = Quaternion.LookRotation(direction);
            var smoothedRotation = Quaternion.Slerp(rotation, targetRotation, Time.deltaTime * lookFactor);
            transform.rotation = smoothedRotation;
        }
    }
}
