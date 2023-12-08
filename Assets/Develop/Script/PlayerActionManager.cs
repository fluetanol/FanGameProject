using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerActionManager : MonoBehaviour
{
    [SerializeField] private int _moveSpeed = 4;
    [SerializeField] private float _jumpForce = 5;
    [SerializeField] private float _ySlope = 0.5f;
    [SerializeField] private Rigidbody2D _rigidBody2d;

    private List<ContactPoint2D> _colliders =  new();
    private Vector2 _moveDirection =  new();
    private Vector2 _velocity = new();

    private bool _isLand = false;


    void FixedUpdate() {
        CheckLanding();
        SetMoveVelocity(_moveDirection.x, _rigidBody2d.velocity.y);
    }

    void CheckLanding(){
        int contactcount = _rigidBody2d.GetContacts(_colliders);
        if (contactcount == 0) _isLand = false;
    }

    void SetMoveVelocity(float x, float y){
        _velocity.x = x * _moveSpeed;
        _velocity.y = y;
        _rigidBody2d.velocity = _velocity;
    }

    //Player Input Send Messaged Event
    void OnMove(InputValue value){
        Vector2 input = value.Get<Vector2>();
        if(input!=null) _moveDirection.x = input.x;
    }

    void OnJump(InputValue value){
        float input = value.Get<float>();
        if(input!=0 && _isLand){
            _isLand = false;
            _rigidBody2d.AddForce(transform.up * _jumpForce, ForceMode2D.Impulse);
        }
    }

    //collide Event
    void OnCollisionEnter2D(Collision2D other) {
        foreach(var k in other.contacts){
            if(k.normal.y<=1 && k.normal.y>_ySlope){
                _isLand = true;
                break;
            }
        }
    }


}
