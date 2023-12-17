using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Entities.UniversalDelegates;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerActionManager : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 4;
    [SerializeField] private float _moveMaxSpeed = 10;
    [SerializeField] private float _jumpForce = 5;
    [SerializeField] private float _ySlope = 0.5f;
    [SerializeField] private Rigidbody2D _rigidBody2d;
    [SerializeField] private Transform Raypoint;

    private Vector2 _moveDirection =  new();
    private Vector2 _velocity = new();

    private float _jumpKeep = 0;
    private bool _isLand = false;

    private RaycastHit2D _specialLandHit;
    private Vector2 _specialLandPoint = new();

    void Update(){
        CheckSpecialLand(Raypoint.position, 3, LayerMask.GetMask("SpecialTerrain"));

    }
    void FixedUpdate() {
        SetMoveAccerlate(_moveSpeed, _moveMaxSpeed, _moveDirection);
        _isLand = CheckLanding(_rigidBody2d.velocity.y) && CheckKeepJump(_jumpKeep, _isLand);
    }

    bool CheckLanding(float velocity_y){
        if (velocity_y!=0) return false;
        else return true;
    }

    bool CheckKeepJump(float keepkey, bool isLand){
        if (keepkey != 0 && isLand){
            _rigidBody2d.velocity = _rigidBody2d.velocity * 0.75f;
            _rigidBody2d.AddForce(transform.up * _jumpForce, ForceMode2D.Impulse);
            return false;
        }
        else return true;
    }


    void SetMoveAccerlate(float speed, float maxSpeed, Vector2 direction){
        if (_rigidBody2d.velocity.magnitude<1) _rigidBody2d.AddForce(speed*direction*Time.deltaTime * 2, ForceMode2D.Impulse);
        else _rigidBody2d.AddForce(speed * direction * Time.deltaTime, ForceMode2D.Impulse);
        
        if(Mathf.Abs(_rigidBody2d.velocity.x) >= maxSpeed){
            _velocity.y = _rigidBody2d.velocity.y;
            _velocity.x = Mathf.Abs(_rigidBody2d.velocity.x)/ _rigidBody2d.velocity.x * maxSpeed;
            _rigidBody2d.velocity = _velocity;
        }
    }

    void CheckSpecialLand(Vector3 position, float checkDistance, int layerMask){
        RaycastHit2D downHit = Physics2D.Raycast(position, -Vector2.up, checkDistance, layerMask);
        if(_specialLandHit == false)
            print("!");

        if(downHit.transform != null)
        {
            _specialLandHit = downHit;
            _specialLandPoint = downHit.point;

            float d = Vector2.Distance(position, _specialLandPoint);
            if (d < 1) downHit.collider.isTrigger = false;
            else downHit.collider.isTrigger = true;
        }
    }


    //Player Input Send Messaged Event
    void OnMove(InputValue value){
        Vector2 input = value.Get<Vector2>();
        if(input!=null) _moveDirection.x = input.x;
    }

    void OnJump(InputValue value){
        print(value.Get<float>());
        _jumpKeep = value.Get<float>();
        //_isLand = CheckKeepJump(_jumpKeep,_isLand);
    }

    //collide Event
    void OnCollisionEnter2D(Collision2D other) {
        foreach(var k in other.contacts){
            print(k.collider.name);
            if(k.normal.y<=1 && k.normal.y>_ySlope){
                _isLand = true;
                break;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Coin"){
            Destroy(other.gameObject);
        }
        
    }



}
