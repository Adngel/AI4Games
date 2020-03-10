using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

public class AIB_Seek : SteeringBehaviour
{
    [SerializeField] float _seekForce = 1.0f;
    private Transform _target; //You must initialize it in Humanoid component.
    [SerializeField] float _range = 1.0f;
    [SerializeField] AnimationCurve _curve;
    [SerializeField] LayerMask _mask;

    private void Reset()
    {
        _range = 1.0f;
        _curve = AnimationCurve.Linear(0, 0, 1, 1);
        _seekForce = 1.0f;
        _mask = 1 << 8;
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

            if (is2D)
            {
                return AI_Steering_2D.Seek(position, target, _seekForce, _range, _curve);
            }else{//Is 3D
                if (!isFlying)
                {
                    return AI_Steering.Seek(position, target, _seekForce, _range, _curve);
                }else{
                    return AI_Steering.SeekFlying(position, target, _seekForce, _range, _curve);
                }
            }
        }
    }

    public Transform Target
    {
        get => _target;
        set => _target = value;
    }
}
