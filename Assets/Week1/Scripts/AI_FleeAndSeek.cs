using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;


[RequireComponent (requiredComponent: typeof (SphereCollider), requiredComponent2: typeof (Rigidbody))]
public class AI_FleeAndSeek : MonoBehaviour
{
    [SerializeField] Rigidbody RB;
    [SerializeField] SphereCollider Collider;
    [SerializeField] Transform Target;
    
    [SerializeField] float Reach = 1.0f;
    [SerializeField] bool Following = true;
    [SerializeField] bool Fleeing = true;

    [SerializeField] float SeekForce = 1.0f;
    [SerializeField] float FleeForce = 1.0f;
    [SerializeField] const float MAXSPEED = 100.0f;

    [SerializeField] Vector3 DirectionMov;

    private void Reset()
    {
        RB = GetComponent<Rigidbody>();
        Collider = GetComponent<SphereCollider>();
        SeekForce = 1.0f;
        FleeForce = 1.0f;
        Reach = Collider.radius;

        if (RB)
        {
            RB.useGravity = false;
            RB.collisionDetectionMode = CollisionDetectionMode.Continuous;
        }
    }

    private void Start()
    {
        if (!Target)
        {
            this.enabled = false;
            print("Goal not assigned on " + gameObject.name);
        }
    }

    void Update()
    {
        DirectionMov = Vector3.zero;

        if (Vector3.Distance(transform.position, Target.position) > Reach)
        {
            if (Following) DirectionMov = SteeringForce.Seek(transform.position, Target.position, SeekForce);
            if (Fleeing) DirectionMov += SteeringForce.Flee(transform.position, Target.position, FleeForce);
        }
    }

    private void FixedUpdate()
    {
        RB.AddForce(DirectionMov); //Not Time.deltatime needed in FixedUpdate
        RB.velocity = Vector3.ClampMagnitude(RB.velocity, MAXSPEED);

        if (RB.velocity.sqrMagnitude > 0.1f)    transform.forward = DirectionMov;
    }
}
