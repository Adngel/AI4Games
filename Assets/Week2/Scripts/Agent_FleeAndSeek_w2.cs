using UnityEngine;
using Utilities;


[RequireComponent (requiredComponent: typeof (SphereCollider), requiredComponent2: typeof (Rigidbody))]
public class Agent_FleeAndSeek_w2 : MonoBehaviour
{
    [SerializeField] Rigidbody RB;
    [SerializeField] SphereCollider Collider;
    [SerializeField] Transform Target;

    [SerializeField] bool IsFlying = false;
    [SerializeField] LayerMask mask;

    [SerializeField] float SeekForce = 1.0f;
    [SerializeField] float FleeForce = 1.0f;

    [SerializeField] const float MAXSPEED = 100.0f;

    [SerializeField] float Range = 1.0f;
    [SerializeField] AnimationCurve curve;
    
    [SerializeField] Transform avatar;

    Vector3 targetPos;
    Vector3 DirectionMov;

    private void Reset()
    {
        RB = GetComponent<Rigidbody>();
        Collider = GetComponent<SphereCollider>();
        SeekForce = 1.0f;
        FleeForce = 1.0f;

        avatar = transform.GetChild(0);

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
       
        if (Target)
        {
            targetPos = Target.position;
        }else{
            targetPos = IO_Mouse.MouseWorldPosition(transform.position, mask);
        }

        if (!IsFlying)
        {
            DirectionMov = AI_Steering.Seek(transform.position, targetPos, SeekForce, Range, curve);
            DirectionMov += AI_Steering.Flee(transform.position, targetPos, FleeForce, Range, curve);
        }else{
            DirectionMov = AI_Steering.SeekFlying(transform.position, targetPos, SeekForce, Range, curve);
            DirectionMov += AI_Steering.FleeFlying(transform.position, targetPos, FleeForce, Range, curve);
        }

        //Rotation
        if (DirectionMov.sqrMagnitude > 0f) avatar.forward = DirectionMov;
    }

    private void FixedUpdate()
    {
        RB.AddForce(DirectionMov); //Not Time.deltatime needed in FixedUpdate
        RB.velocity = Vector3.ClampMagnitude(RB.velocity, MAXSPEED);
    }
}
