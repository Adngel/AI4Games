using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities
{
    public static class UT_Lists
    {
        public static RaycastHit GetClosestObstacle(RaycastHit[] _obstacles, Vector3 origin)
        {
            var ClosestHit = _obstacles[0];
            var ClosestDistance = Vector3.Distance(origin, ClosestHit.point);

            var CurrentDistance = 0.0f;

            for (int i = 1; i < _obstacles.Length; i++)
            {
                CurrentDistance = Vector3.Distance(origin, _obstacles[i].point);
                if (CurrentDistance < ClosestDistance)
                {
                    ClosestDistance = CurrentDistance;
                    ClosestHit = _obstacles[i];
                }
            }

            return ClosestHit;
        }
    }
}

