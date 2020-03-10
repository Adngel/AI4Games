using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

public class FSM_test : SteeringBehaviour
{
    private IEnumerator _currState;
    private IEnumerator _nextState;
    private Vector3 _steeringForce;


    private IEnumerator FSM()
    {
        while (_currState != null)
        {
            yield return StartCoroutine(_currState);
            _currState = _nextState;
            _nextState = null;
        }
    }
    private void Start()
    {
        _currState = Idle();
        StartCoroutine(FSM());
    }
    
    private IEnumerator Idle()
    {
        //Mathf.Atan2(y, x);
        Vector2

        // ENTRY
        //setIdleAnimation();
        print(" I entered in Idle State");
        _steeringForce = Vector3.zero;

        // UPDATE
        while (_nextState == null)
        {
            //wander();
            if (Input.GetKeyDown(KeyCode.A))
            {
                _nextState = SeekState(Camera.main.ScreenToWorldPoint(Input.mousePosition), 50.0f);
            }

            if (Input.GetKey (KeyCode.Escape))
            {
                _nextState = Bye();
            }

            yield return null;
        }
        // EXIT
        print(" I am exiting the Idle State");
    }
    

    private IEnumerator Bye()
    {
        // ENTRY
        //setIdleAnimation();
        print(" I entered in Bye State");
        _steeringForce = Vector3.zero;

        // UPDATE
        while (_nextState == null)
        {
            //wander();
            

            yield return null;
        }
        // EXIT
        print(" I am exiting the Bye State");
    }

    private IEnumerator SeekState(Vector3 targetPos, float Force)
    {
        // ENTRY
        //setIdleAnimation();
        print(" I entered in Seek State");
        _steeringForce = AI_Steering_2D.Seek(transform.position, targetPos, Force);

        // UPDATE
        while (_nextState == null)
        {
            _steeringForce = AI_Steering_2D.Seek(transform.position, targetPos, Force);

            if (Input.GetKey(KeyCode.Escape))
            {
                _nextState = Bye();
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                _nextState = Idle();
            }

            yield return null;
        }

        // EXIT
        print(" I am exiting the Seek State");
    }

    public override Vector3 SteeringForce
    {
        get
        {
            return _steeringForce; 
        }
    }
}
