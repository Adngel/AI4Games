using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

public class AIB_Pursuer : SteeringBehaviour
{
    [SerializeField] float Range = 1.0f;
    [SerializeField] AnimationCurve curve;
    [SerializeField] float _seekForce = 1.0f;
    [SerializeField] float _lookAhead = 1.0f;
    [SerializeField] private Transform _target;
    [SerializeField] LayerMask _mask;


    private void Reset()
    {
        Range = 1.0f;
        curve = AnimationCurve.Linear(0, 0, 1, 1);
        _seekForce = 1.0f;
        _lookAhead = 1.0f;
        _mask = 1 << 8;
    }

    public override Vector3 SteeringForce
    {
        get
        {
            var position = transform.position;
            var target = _target ? _target.position : IO_Mouse.MouseWorldPosition(transform.position, _mask);
            Rigidbody RB = _target.GetComponent<Rigidbody>();
            var speed = RB ? RB.velocity : Vector3.one;
            return AI_Steering.Pursue(position, target, Vector3.one, _lookAhead, _seekForce);
        }
    }


}
