using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities
{
    public static class Vector2Extension
    {
        public static float angleRadians (this Vector2 vector)
        {
            return Mathf.Atan2(vector.y, vector.x);
        }

        public static float angleDegrees(this Vector2 vector)
        {
            return Mathf.Rad2Deg * Mathf.Atan2(vector.y, vector.x);
        }

        public static Vector2 randomVector(this Vector2 vector, float angleRangeDegrees, float maxMagnitudeRange)
        {
            var angle = vector.angleDegrees();
            angle += Random.Range(angle - angleRangeDegrees, angle + angleRangeDegrees);
            var magnitude = Random.Range(0.0f, maxMagnitudeRange);

            return new Vector2(Mathf.Sin(angle) * magnitude, Mathf.Cos(angle) * magnitude);
        }
    }
}//rider

