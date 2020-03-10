using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

[RequireComponent(requiredComponent: typeof(SphereCollider), requiredComponent2: typeof(Rigidbody))]
public class Humanoid : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Rigidbody RB;
    [SerializeField] SphereCollider Collider;
    [SerializeField] public Transform Target;

    [Header("Constants")]
    [SerializeField] float MAXSPEED = 10.0f;

    [Header("Movement Physics")]
    [SerializeField] public bool IsFlying = false;
    

    [Header("Movement Logics")]
    [SerializeField] List<SteeringBehaviour> SteeringBehaviours;

    [Header("Visuals")]
    [SerializeField] Transform avatar;
        
    Vector3 _steeringForce;

    private void Reset()
    {
        RB = GetComponent<Rigidbody>();
        Collider = GetComponent<SphereCollider>();

        avatar = transform.GetChild(0);

        if (RB)
        {
            RB.useGravity = false;
            RB.drag = 1.0f;
            RB.collisionDetectionMode = CollisionDetectionMode.Continuous;
            RB.freezeRotation = true;
        }
    }

    private void Update()
    {
        _steeringForce = Vector3.zero;

        foreach (var steeringBehaviour in SteeringBehaviours)
        {
            _steeringForce += steeringBehaviour.SteeringForce;
        }

        //Rotation
        //if (_steeringForce.sqrMagnitude > 0f) avatar.forward = _steeringForce;
        if (_steeringForce.sqrMagnitude > 0f) avatar.up = _steeringForce.normalized;
    }

    private void FixedUpdate()
    {
        RB.AddForce(_steeringForce);
        RB.velocity = Vector3.ClampMagnitude(RB.velocity, MAXSPEED);
    }
}
