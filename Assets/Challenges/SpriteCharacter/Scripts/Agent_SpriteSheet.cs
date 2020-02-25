using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

[RequireComponent(requiredComponent: typeof(SphereCollider), requiredComponent2: typeof(Rigidbody))]
public class Agent_SpriteSheet : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Rigidbody RB;
    [SerializeField] SphereCollider Collider;
    [SerializeField] Animator anim;

    [Header("Constants")]
    [SerializeField] const float MAXSPEED = 100.0f;
    enum CardinalPoints { NW, N, NE, E, SE, S, SW, W };

    [Header("Movement Physics")]
    [SerializeField] bool IsFlying = false; //¿Or swimming?
    [SerializeField] LayerMask mask;
    
    [SerializeField] float SeekForce = 1.0f;
    [SerializeField] float Range = 1.0f;
    [SerializeField] AnimationCurve curve;

    [Header("Visuals")]
    [SerializeField] CardinalPoints RotationgFacing;
    [SerializeField] float VelocityStop = 0.5f;

    Vector3 targetPos;
    Vector3 DirectionMov;

    void Reset ()
    {
        RB = GetComponent<Rigidbody>();
        Collider = GetComponent<SphereCollider>();
        anim = transform.GetChild(0).GetComponent<Animator>();
        RotationgFacing = CardinalPoints.E;

        SeekForce = 1.0f;
        if (RB)
        {
            RB.useGravity = false;
            RB.drag = 1.0f;
            RB.collisionDetectionMode = CollisionDetectionMode.Continuous;
            RB.freezeRotation = true;
        }

        mask = 1 << 8;
        curve = AnimationCurve.Linear(0, 0, 1, 1);
    }


    void Update()
    {
        DirectionMov = Vector3.zero;
        if (Input.GetMouseButton (0))   targetPos = IO_Mouse.MouseWorldPosition(transform.position, mask);

        if (!IsFlying)
        {
            DirectionMov = AI_Steering.Seek(transform.position, targetPos, SeekForce, Range, curve);
        }else{
            DirectionMov = AI_Steering.SeekFlying(transform.position, targetPos, SeekForce, Range, curve);
        }

        //Avatar visuals
        if (DirectionMov.sqrMagnitude > VelocityStop)
        {
            anim.SetFloat("IsMoving", 1.0f);
            CalculateRotation();
        }else{
            anim.SetFloat("IsMoving", 0.0f);
        }
    }

    private void FixedUpdate()
    {
        RB.AddForce(DirectionMov); //Not Time.deltatime needed in FixedUpdate
        RB.velocity = Vector3.ClampMagnitude(RB.velocity, MAXSPEED);
    }


    void CalculateRotation ()
    {
        //We can use the dot product to get a float number that says us in what direction is facing in relation to "forward vector"
        //Because we are not rotating the character model, the forward always will point to the same place (the depht of the screen, our "north").

        float productNS = Vector3.Dot(DirectionMov.normalized, transform.forward);
        float productWE = Vector3.Dot(DirectionMov.normalized, transform.right);
        
        if ((productNS > 0.5f) && (productWE < 0.5f) && (productWE > -0.5f))   //Is North?
        {
            RotationgFacing = CardinalPoints.N;
        }
        if ((productNS > 0.5f) && (productWE > 0.5f))   //Is North East?
        {
            RotationgFacing = CardinalPoints.NE;
        }
        if ((productNS <= 0.5f) && (productNS >= -0.5f) && (productWE > 0.5f))   //Is East?
        {
            RotationgFacing = CardinalPoints.E;
        }
        if ((productNS < -0.5f) && (productWE > 0.5f))   //Is South East?
        {
            RotationgFacing = CardinalPoints.SE;
        }
        if ((productNS < -0.5f) && (productWE < 0.5f) && (productWE > -0.5f))   //Is South?
        {
            RotationgFacing = CardinalPoints.S;
        }
        if ((productNS < -0.5f) && (productWE < 0.25f) && (productWE < -0.5f))   //Is South West?
        {
            RotationgFacing = CardinalPoints.SW;
        }
        if ((productNS <= 0.5f) && (productNS >= -0.5f) && (productWE < -0.5f))   //Is West?
        {
            RotationgFacing = CardinalPoints.W;
        }
        if ((productNS > 0.5f) && (productWE < -0.5f))   //Is North West?
        {
            RotationgFacing = CardinalPoints.NW;
        }
        
        anim.SetFloat("CardinalFacing", (float)RotationgFacing);
    }

}
