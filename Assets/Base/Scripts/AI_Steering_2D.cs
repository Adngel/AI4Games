using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Utilities
{
    public static class AI_Steering_2D
    {
        //---++ Constants and Enumerations ++---
        /////////////////////////////

        private static readonly AnimationCurve LINEAR = AnimationCurve.Linear(0, 0, 1, 1); //Time start, ? , ? , ?)

        //---++ Variables ++---
        /////////////////////////////




        //---++ Functions ++---
        /////////////////////////////


        //These only gives direction in 3d environment.
        public static Vector3 Seek(Vector3 origin, Vector3 target, float seekForce)
        {
            //This returns a vector direction towards the target increased by the seekForce value.
            return (target - origin).normalized * seekForce;
        }

        public static Vector3 Seek(Vector3 origin, Vector3 target, float seekForce, float Range)
        {
            //Moves to Target
            //Decreases speed till 0 once is inside the range.
            //Using a linear curve information to calculate the speed variation.

            return Seek(origin, target, seekForce, Range, LINEAR);
        }

        public static Vector3 Seek(Vector3 origin, Vector3 target, float seekForce, float Range, AnimationCurve curve)
        {
            //a.k.a. Arrive behaviour
            //Moves to Target
            //Decreases speed till 0 once is inside the range.
            //Using a custom curve information to calculate the speed variation.
            if (Range <= 0f) return Vector3.zero;

            var desiredVelocity = target - origin;
            var sqrDistance = desiredVelocity.sqrMagnitude;
            var factor = curve.Evaluate(Mathf.Min(sqrDistance / Range, 1.0f));

            return desiredVelocity * seekForce * factor;
        }

        public static Vector3 Flee(Vector3 origin, Vector3 target, float fleeForce)
        {
            //Gives a vector that goes from origin to the oposite direction of the target.
            return (origin - target).normalized * fleeForce;
        }

        public static Vector3 Flee(Vector3 origin, Vector3 target, float fleeForce, float Range)
        {
            //Gives a vector that goes from origin to the oposite direction of the target.
            //Using a range, while closer were to the target, stronger will be the fleeForce to run away.
            //Uses a linear curve to calculate the fleeforce variation inside the range.
            return Flee(origin, target, fleeForce, Range, LINEAR);
        }

        public static Vector3 Flee(Vector3 origin, Vector3 target, float fleeForce, float Range, AnimationCurve curve)
        {
            //Gives a vector that goes from origin to the oposite direction of the target.
            //Using a range, while closer were to the target, stronger will be the fleeForce to run away.
            //Uses a custom curve to calculate the fleeforce variation inside the range.


            if (Range <= 0f) return Vector3.zero;

            var desiredVelocity = origin - target;
            var sqrDistance = desiredVelocity.sqrMagnitude;
            var factor = 1.0f - curve.Evaluate(Mathf.Min(sqrDistance / Range, 1.0f));
            return desiredVelocity * fleeForce * factor;
        }




    }
}
