using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities
{
    public static class AI_Steering
    {
        //---++ Constants and Enumerations ++---
        /////////////////////////////

        private static readonly AnimationCurve LINEAR = AnimationCurve.Linear(0, 0, 1, 1); //Time start, ? , ? , ?)
        private static readonly bool IS3D = true;

        //---++ Variables ++---
        /////////////////////////////




        //---++ Functions ++---
        /////////////////////////////

        //Utility function
        /////////////////////////////

        static void NullifyPlane(ref Vector3 origin, ref Vector3 target)
        {//Nullify the vertical axis.

            if (IS3D) 
            {
                //Horizontal Movement is in XZ
                origin.y = 0;
                target.y = 0;
            }else{ 
                //Horizontal Movement is in XY
                origin.z = 0;
                target.z = 0;
            }
        }

        //Towards Target Behaviours
        /////////////////////////////

        //These only gives direction in 3d environment.
        public static Vector3 SeekFlying(Vector3 origin, Vector3 target, float seekForce)
        {
            //This returns a vector direction towards the target increased by the seekForce value.

            return (target - origin) * seekForce;
        }

        public static Vector3 SeekFlying(Vector3 origin, Vector3 target, float seekForce, float Range)
        {
            //Moves to Target
            //Decreases speed till 0 once is inside the range.
            //Using a linear curve information to calculate the speed variation.

            return SeekFlying(origin, target, seekForce, Range, LINEAR);
        }

        public static Vector3 SeekFlying(Vector3 origin, Vector3 target, float seekForce, float Range, AnimationCurve curve)
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


        //These only gives direction in 2d environment. (reusing the previous SeekFlying ones).
        public static Vector3 Seek(Vector3 origin, Vector3 target, float seekForce)
        {
            NullifyPlane(ref origin, ref target);
            return SeekFlying(origin, target, seekForce);
        }

        public static Vector3 Seek(Vector3 origin, Vector3 target, float seekForce, float Range)
        {
            NullifyPlane(ref origin, ref target);
            return SeekFlying(origin, target, seekForce, Range, LINEAR);
        }

        public static Vector3 Seek(Vector3 origin, Vector3 target, float seekForce, float Range, AnimationCurve curve)
        {
            NullifyPlane(ref origin, ref target);
            return SeekFlying(origin, target, seekForce, Range, curve);
        }

        public static Vector3 Pursue (Vector3 origin, Vector3 targetPos, Vector3 targetVel, float lookAhead, float PursueForce)
        {
            NullifyPlane(ref origin, ref targetPos);

            Vector3 lookAheadVector = targetVel.normalized * lookAhead;
            Vector3 seekPosition = targetPos + lookAheadVector;
            return Seek(origin, seekPosition, PursueForce);
        }

        public static Vector3 Pursue(Vector3 origin, Vector3 targetPos, Vector3 targetVel, float lookAhead, float PursueForce, float Range)
        {
            NullifyPlane(ref origin, ref targetPos);

            return Pursue(origin, targetPos, targetVel, lookAhead, PursueForce, Range, LINEAR);
        }

        public static Vector3 Pursue(Vector3 origin, Vector3 targetPos, Vector3 targetVel, float lookAhead, float PursueForce, float Range, AnimationCurve curve)
        {
            NullifyPlane(ref origin, ref targetPos);

            if (Range <= 0f) return Vector3.zero;

            var desiredVelocity = targetPos - origin;
            var sqrDistance = desiredVelocity.sqrMagnitude;
            var factor = curve.Evaluate(Mathf.Min(sqrDistance / Range, 1.0f));

            var lookAheadVector = targetVel.normalized * lookAhead * factor;
            var seekPosition = targetPos + lookAheadVector;
            desiredVelocity = seekPosition - origin;

            return desiredVelocity * PursueForce * factor;

        }

        //Away from Target Behaviours
        /////////////////////////////

        //These only gives direction in 3d environment.
        public static Vector3 FleeFlying(Vector3 origin, Vector3 target, float fleeForce)
        {
            //Gives a vector that goes from origin to the oposite direction of the target.
            return (origin - target) * fleeForce;
        }

        public static Vector3 FleeFlying(Vector3 origin, Vector3 target, float fleeForce, float Range)
        {
            //Gives a vector that goes from origin to the oposite direction of the target.
            //Using a range, while closer were to the target, stronger will be the fleeForce to run away.
            //Uses a linear curve to calculate the fleeforce variation inside the range.
            return FleeFlying(origin, target, fleeForce, Range, LINEAR);
        }

        public static Vector3 FleeFlying(Vector3 origin, Vector3 target, float fleeForce, float Range, AnimationCurve curve)
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


        //These only gives direction in 2d environment. (reusing the previous FleeFlying ones).
        public static Vector3 Flee(Vector3 origin, Vector3 target, float fleeForce)
        {
            NullifyPlane(ref origin, ref target);
            return FleeFlying (origin, target, fleeForce);
        }

        public static Vector3 Flee(Vector3 origin, Vector3 target, float fleeForce, float Range)
        {
            NullifyPlane(ref origin, ref target);
            return FleeFlying(origin, target, fleeForce, Range, LINEAR);
        }

        public static Vector3 Flee(Vector3 origin, Vector3 target, float fleeForce, float Range, AnimationCurve curve)
        {
            NullifyPlane(ref origin, ref target);
            return FleeFlying(origin, target, fleeForce, Range, curve);
        }

    }
}
