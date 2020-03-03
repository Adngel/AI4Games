using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

public class AIB_Evader : SteeringBehaviour
{
    [SerializeField] float _fleeForce = 1.0f;
    [SerializeField] float _lookAhead = 1.0f;
    [SerializeField] AnimationCurve curve;
    
    private Transform _target; //You must initialize it in Humanoid component.

    [SerializeField] LayerMask _mask;
    [SerializeField] private RaycastHit[] _obstacles = new RaycastHit[10];

    private void Reset()
    {
        _fleeForce = 1.0f;
        _lookAhead = 1.0f;
        curve = AnimationCurve.Linear(0, 0, 1, 1);
        _mask = 1 << 9;
    }

    private void Start()
    {
        Humanoid hm = GetComponent<Humanoid>();
        isFlying = hm.IsFlying;  //isFlying is a protected var inside of SteeringBehaviour
        _target = hm.Target;
    }

    public override Vector3 SteeringForce
    {
        get
        {
            var position = transform.position;
            var target = _target ? _target.position : IO_Mouse.MouseWorldPosition(transform.position, _mask);

            var direction = target - position;

            
            _obstacles = Physics.RaycastAll(position + Vector3.up, direction, _lookAhead, _mask);

            if (_obstacles.Length <= 0)
            {
                //if there are not articles, avoid force is 0.
                return Vector3.zero;
            }

            target = Utilities.UT_Lists.GetClosestObstacle(_obstacles, position).transform.position ;

            var LeftV = Vector3.Cross(direction, Vector3.up);
            
            if (!isFlying)
            {
                return AI_Steering.Evade(position, target, _fleeForce, LeftV, _lookAhead, curve);
            }else{
                return AI_Steering.EvadeFlying(position, target, _fleeForce, LeftV, _lookAhead, curve);
            }
        }
    }

}
