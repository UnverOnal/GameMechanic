using UnityEngine;

namespace Util
{
    public static class GameObjectUtil
    {
        //Returns correct center point.
        //After slicing, the center point of the new platform is not correct.
        public static Vector3 GetCenter(this GameObject gameObject)
        {
            var center = gameObject.GetComponent<BoxCollider>().center;
            center = gameObject.transform.TransformPoint(center);
            return center;
        }
    }
}
