using UnityEngine;

namespace Game.Scripts
{
    public static class DistanceMeter
    {
        public static float DistanceToTarget(Vector3 selfPosition, Vector3 targetPosition)
        {
          
            Vector2 selfPos2D = new Vector2(selfPosition.x, selfPosition.z);
            Vector2 targetPos2D = new Vector2(targetPosition.x, targetPosition.z);

            float distanceToTarget = Vector2.Distance(selfPos2D, targetPos2D);

            return distanceToTarget;

            

        }
    }
}