using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

public class AIB_Seek : SteeringBehaviour
{
    [SerializeField] float _seekForce = 1.0f;
    [SerializeField] private Transform _target;
    [SerializeField] LayerMask _mask;

    private void Reset()
    {
        _seekForce = 1.0f;
        _mask = 1 << 8;
    }

    public override Vector3 SteeringForce
    {
        get
        {
            var position = transform.position;
            var target = _target ? _target.position : IO_Mouse.MouseWorldPosition(transform.position, _mask);

            return AI_Steering.Seek(position, target, _seekForce);
        }
    }
}
