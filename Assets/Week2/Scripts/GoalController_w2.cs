using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(requiredComponent: typeof(SphereCollider), requiredComponent2: typeof(Rigidbody))]
public class GoalController_w2 : MonoBehaviour
{


    Rigidbody RB;
    Vector3 directionMove;

    [SerializeField] float movX = 0.0f;
    [SerializeField] float movY = 0.0f;
    [SerializeField] float Speed = 1.0f;

    void Start()
    {
        RB = GetComponent<Rigidbody>();
    }

    void Update()
    {
        directionMove = Vector3.zero;

        movX = Input.GetAxis("Horizontal");
        movY = Input.GetAxis("Vertical");

        directionMove = new Vector3(movX, 0, movY) * Speed * Time.deltaTime;
        RB.velocity = directionMove;
    }

}
