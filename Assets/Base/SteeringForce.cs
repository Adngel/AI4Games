using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities
{
    public static class SteeringForce
    {
        private static readonly AnimationCurve Linear = AnimationCurve.Linear(0, 0, 1, 1); //Time start, ? , ? , ?)
        public static Vector3 Seek(Vector3 origin, Vector3 target, float seekForce, bool isflat = true)
        {
            if (!isflat)
            {
                origin.y = 0;
                target.y = 0;
            }
            return (target - origin) * seekForce;
        }

        public static Vector3 Flee(Vector3 origin, Vector3 target, float fleeForce, bool isflat = true)
        {
            if (!isflat)
            {
                origin.y = 0;
                target.y = 0;
            }
            return (origin - target) * fleeForce;
        }

        public static Vector3 Arrive(Vector3 origin, Vector3 target, float seekForce, float Range, bool isflat = true)
        {
            if (!isflat)
            {
                origin.y = 0;
                target.y = 0;
            }
            return Arrive (origin, target, seekForce, Range, isflat, Linear);
        }

        public static Vector3 Arrive(Vector3 origin, Vector3 target, float seekForce, float Range, bool isflat, AnimationCurve curve)
        {
            if (Range <= 0f) return Vector3.zero;

            var desiredVelocity = target - origin;
            var sqrDistance = desiredVelocity.sqrMagnitude;
            var factor = curve.Evaluate(Mathf.Min(sqrDistance / Range, 1.0f));

            return desiredVelocity * seekForce * factor;
        }

        public static Vector3 StarFlee(Vector3 origin, Vector3 target, float fleeForce, float Range, bool isflat = true)
        {

            /*if (!isflat)
            {
                origin.y = 0;
                target.y = 0;
            }

            if (Range <= 0f)
            {
                return desiredVelocity * fleeForce;
            }

            var desiredVelocity = origin - target;
            var sqrDistance = desiredVelocity.sqrMagnitude;
            var factor = () 1.0f - Mathf.Min(sqrDistance / Range, 1.0f);

            var result =  desiredVelocity * fleeForce * factor;
            
             */

            return Vector3.zero;
        }

    }
}
