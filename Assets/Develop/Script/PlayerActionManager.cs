using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Entities.UniversalDelegates;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerActionManager : MonoBehaviour
{
    public float Speed;
    public float JumpV0;
    public float Jumpadd;
    private float _jump;
    
    [SerializeField] private Transform _deadLine;
    [SerializeField] private Transform _startPoint;
    [SerializeField] private Animator _fadeinout;
    private Vector2 _moveDirection;
    private Rigidbody2D _rigidbody2d;

    private bool _isLand;
    private bool _isJump;
    private bool _isJumpHold;

    void Start(){
        _moveDirection = new();
        _rigidbody2d = GetComponent<Rigidbody2D>();
        _jump = Jumpadd;
        _isJump = false;
        _isLand = false;
        _isJumpHold = false;
    }

    void Update(){
        if(transform.position.y < _deadLine.position.y) PlayerStateManager.Instance.SetState(PlayerState.Dead);
        
    }

    void FixedUpdate() {
        normalMove();
        jumpMove();
        _rigidbody2d.velocity = _moveDirection;
        //PlayerStateManager.Instance;
    }
    
    public void Restart(){
        transform.position = _startPoint.position;
        _rigidbody2d.simulated = true;
        _rigidbody2d.velocity = Vector2.zero;
    }

    void normalMove(){
        if (_moveDirection.x != 0) _moveDirection.x = _moveDirection.x / Mathf.Abs(_moveDirection.x) * Speed;
    }

    void jumpMove(){
        if (_isJumpHold) {
            _rigidbody2d.gravityScale = 2;
            _moveDirection.y += Jumpadd * Time.deltaTime;
            Jumpadd-=1;
            if(Jumpadd<=0) _isJumpHold = false;
        }
        else{
            _rigidbody2d.gravityScale = 5;
             _moveDirection.y = _rigidbody2d.velocity.y;
        }
    }

    //Player Input Send Messaged Event
    void OnMove(InputValue value){
        Vector2 input = value.Get<Vector2>();
        if(input!=null)  _moveDirection.x = input.x;
    }

    //press == 1
    //unpress = 0
    void OnJump(InputValue value){
        print(_isJump+" "+_isLand);
        if(value.isPressed && !_isJump && _isLand){
            print("press");
            _moveDirection.y = JumpV0;
            _isLand = false;
            _isJump = true;
            _isJumpHold = true;
        }
        else{
            print("depress");
            _isJumpHold = false;
            Jumpadd = _jump;
        }

    }

    void OnCollisionEnter2D(Collision2D other) {
        foreach(var hit in other.contacts){
            if(hit.normal == Vector2.up){
                _isJump = false;
                _isLand = true;
                break;
            }
        }

    }
    


}
