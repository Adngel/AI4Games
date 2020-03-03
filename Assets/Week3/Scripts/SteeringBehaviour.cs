using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SteeringBehaviour : MonoBehaviour
{
    public abstract Vector3 SteeringForce { get; }

    protected   bool isFlying = false;
}

