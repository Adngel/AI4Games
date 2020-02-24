using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;


[RequireComponent (requiredComponent: typeof (SphereCollider), requiredComponent2: typeof (Rigidbody))]
public class AI_FleeAndSeek_w2 : MonoBehaviour
{
    [SerializeField] Rigidbody RB;
    [SerializeField] SphereCollider Collider;
    [SerializeField] Transform Target;
    Vector3 targetPos;

    [SerializeField] bool Following = true;
    [SerializeField] bool Fleeing = true;

    [SerializeField] float SeekForce = 1.0f;
    [SerializeField] float FleeForce = 1.0f;
    [SerializeField] const float MAXSPEED = 100.0f;
    [SerializeField] float Range = 1.0f;
    [SerializeField] Vector3 DirectionMov;
    [SerializeField] AnimationCurve curve;

    [SerializeField] Transform avatar;

    private void Reset()
    {
        RB = GetComponent<Rigidbody>();
        Collider = GetComponent<SphereCollider>();
        SeekForce = 1.0f;
        FleeForce = 1.0f;

        avatar = transform.GetChild(0);

        Following = true;
        Fleeing = false;

        if (RB)
        {
            RB.useGravity = false;
            RB.collisionDetectionMode = CollisionDetectionMode.Continuous;
            RB.freezeRotation = true;
        }
    }

    void Update()
    {
        DirectionMov = Vector3.zero;
       
        if (Target)
        {
            targetPos = Target.position;
        }else{
            Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1);
            var _ray = Camera.main.ScreenPointToRay(mousePos);

            RaycastHit hitpoint;
            if (Physics.Raycast(_ray, out hitpoint))
            {
                targetPos = hitpoint.point;
            }
        }
        
        
        if (Following) DirectionMov = SteeringForce.Arrive(transform.position, targetPos, SeekForce, Range, true, curve);
        if (Fleeing) DirectionMov = SteeringForce.StarFlee(transform.position, targetPos, FleeForce, Range);

        //Rotation
        if (DirectionMov.sqrMagnitude > 0f) avatar.forward = DirectionMov;
    }

    private void FixedUpdate()
    {
        RB.AddForce(DirectionMov); //Not Time.deltatime needed in FixedUpdate
        RB.velocity = Vector3.ClampMagnitude(RB.velocity, MAXSPEED);
    }
}
