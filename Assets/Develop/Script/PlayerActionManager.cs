using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerActionManager : MonoBehaviour
{
    [SerializeField] private int _moveSpeed = 4;
    [SerializeField] private int _maxSpeed = 10;
    [SerializeField] private int _jumpForce = 5;
    [SerializeField] private Rigidbody2D rigidbody2d;
    private Vector3 _moveDirection;
    private bool _isLand = true;

    void Start(){
    }

    void FixedUpdate() {
        if (rigidbody2d.GetPointVelocity(transform.position).magnitude < _maxSpeed)
            rigidbody2d.AddForce(_moveDirection * _moveSpeed, ForceMode2D.Force);
    }
    
    //Player Input Class Send Message
    void OnMove(InputValue value){
        Vector2 input = value.Get<Vector2>();
        if(input!=null){
            _moveDirection = new Vector2(input.x, input.y);
            Debug.Log($"Send: {input.magnitude}, input : {input.x}, {input.y}");
        }
    }

    void OnJump(InputValue value){
        float input = value.Get<float>();
        if(input==1 && _isLand){
            rigidbody2d.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
            //_isLand = false;
        }
    }

    private void OnCollisionEnter(Collision other) {
        print(other.contacts[0].normal);
    }

}
