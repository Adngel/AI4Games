﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

public class AIB_Seek : SteeringBehaviour
{
    [SerializeField] float _seekForce = 1.0f;
    private Transform _target; //You must initialize it in Humanoid component.
    [SerializeField] float Range = 1.0f;
    [SerializeField] AnimationCurve curve;
    [SerializeField] LayerMask _mask;

    private void Reset()
    {
        Range = 1.0f;
        curve = AnimationCurve.Linear(0, 0, 1, 1);
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

            if (!isFlying)
            {
                return AI_Steering.Seek(position, target, _seekForce, Range,curve);
            }else{
                return AI_Steering.SeekFlying(position, target, _seekForce, Range, curve);
            }
        }
    }
}
