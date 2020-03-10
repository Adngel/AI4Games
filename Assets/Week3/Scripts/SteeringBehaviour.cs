using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SteeringBehaviour : MonoBehaviour
{
    public abstract Vector3 SteeringForce { get; }

    [SerializeField] protected bool is2D = true;
    protected bool isFlying = false;
}

