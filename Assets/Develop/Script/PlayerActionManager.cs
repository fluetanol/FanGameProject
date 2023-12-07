using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerActionManager : MonoBehaviour
{
    [SerializeField] private int _moveSpeed = 4;
    [SerializeField] private int _jumpForce = 5;
    [SerializeField] private Rigidbody2D _rigidbody2d;

    private List<ContactPoint2D> _colliders =  new();
    private Vector2 _moveDirection =  new();
    private Vector2 _velocity = new();

    private float _tempDirection;
    private bool _isLand = false;
    private bool _isMove = true;


    void FixedUpdate() {
        int contactcount = _rigidbody2d.GetContacts(_colliders);
        



        _velocity.x = _moveDirection.x * _moveSpeed;        
        _velocity.y = _rigidbody2d.velocity.y;
        _rigidbody2d.velocity = _velocity;
    }

    //Player Input Class Send Messaged
    void OnMove(InputValue value){
        Vector2 input = value.Get<Vector2>();
        if(input!=null && _isMove) {
            _moveDirection.x = input.x;
        }
    }

    void OnJump(InputValue value){
        float input = value.Get<float>();
        if(input!=0 && _isLand){ 
            _rigidbody2d.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
            _isLand = false;
        }
    }


    private void OnCollisionEnter2D(Collision2D other) {

        foreach(var k in other.contacts){
            if(k.normal == Vector2.up){
                _isLand = true;
            }
        }


    }


}
