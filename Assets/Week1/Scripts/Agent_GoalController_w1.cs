using UnityEngine;
using Utilities;

[RequireComponent(requiredComponent: typeof(SphereCollider), requiredComponent2: typeof(Rigidbody))]
public class Agent_GoalController_w1 : MonoBehaviour
{
    [SerializeField] Rigidbody RB;
    [SerializeField] float Speed = 1.0f;
    [SerializeField] const float MAXSPEED = 100.0f;
    [SerializeField] LayerMask mask;

    Vector3 directionMove;
    float movX = 0.0f;
    float movY = 0.0f;

    private void Reset()
    {
        RB = GetComponent<Rigidbody>();
        if (RB)
        {
            RB.useGravity = false;
            RB.drag = 1.0f;
            RB.collisionDetectionMode = CollisionDetectionMode.Continuous;
            RB.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        }
        mask = 1 << 8;
    }

    void Update()
    {
        directionMove = Vector3.zero;
        if (Input.GetMouseButton(0))
        {
            Vector3 targetPos = IO_Mouse.MouseWorldPosition(transform.position, mask);
            directionMove = AI_Steering.SeekFlying (transform.position, targetPos, Speed);
        }
    }

    private void FixedUpdate()
    {
        RB.AddForce(directionMove); //Not Time.deltatime needed in FixedUpdate
        RB.velocity = Vector3.ClampMagnitude(RB.velocity, MAXSPEED);
    }

}
