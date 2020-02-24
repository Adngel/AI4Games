using UnityEngine;
using Utilities;


[RequireComponent (requiredComponent: typeof (SphereCollider), requiredComponent2: typeof (Rigidbody))]
public class Agent_FleeAndSeek_w1 : MonoBehaviour
{
    [SerializeField] Rigidbody RB;
    [SerializeField] SphereCollider Collider;
    [SerializeField] Transform Target;
    
    [SerializeField] float SeekForce = 1.0f;
    [SerializeField] float FleeForce = 1.0f;
    [SerializeField] const float MAXSPEED = 100.0f;
    [SerializeField] bool IsFlying = true;

    Vector3 DirectionMov;

    private void Reset()
    {
        RB = GetComponent<Rigidbody>();
        Collider = GetComponent<SphereCollider>();
        SeekForce = 1.0f;
        FleeForce = 1.0f;

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

        if (!IsFlying)
        {
            DirectionMov = AI_Steering.Seek(transform.position, Target.position, SeekForce);
            DirectionMov += AI_Steering.Flee(transform.position, Target.position, FleeForce);
        }else{
            DirectionMov = AI_Steering.SeekFlying(transform.position, Target.position, SeekForce);
            DirectionMov += AI_Steering.FleeFlying(transform.position, Target.position, FleeForce);
        }
    }

    private void FixedUpdate()
    {
        //Translation
        RB.AddForce(DirectionMov); //Not Time.deltatime needed in FixedUpdate
        RB.velocity = Vector3.ClampMagnitude(RB.velocity, MAXSPEED);

        //Rotation
        if (RB.velocity.sqrMagnitude > 0.1f)    transform.forward = DirectionMov;
    }
}
