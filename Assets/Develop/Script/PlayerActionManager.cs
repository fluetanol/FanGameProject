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
    [SerializeField] private Transform _deadLine;
    [SerializeField] private Transform _startPoint;
    private float _tempJumpadd;
    private Rigidbody2D _rigidbody2d;


    void Start(){
        _rigidbody2d = GetComponent<Rigidbody2D>();
        _tempJumpadd = DataFactory.Instance.Jumpadd;
    }

    void Update(){
        if(transform.position.y < _deadLine.position.y) PlayerStateManager.Instance.SetState(PlayerState.Dead);
    }

    public void Restart(){
        transform.position = _startPoint.position;
        _rigidbody2d.simulated = true;
        _rigidbody2d.velocity = Vector2.zero;
    }


    //Player Input Send Messaged Event
    void OnMove(InputValue value){
        Vector2 input = value.Get<Vector2>();
        if(input!=null)  DataFactory.Instance._moveDirection.x = input.x;
    }

    //press == 1
    //unpress = 0
    void OnJump(InputValue value){
        if(value.isPressed && PlayerStateManager.Instance.GetCurrentState == PlayerState.Land){
            PlayerStateManager.Instance.SetState(PlayerState.Jump);
            DataFactory.Instance._moveDirection.y = DataFactory.Instance.JumpV0;
        }
        else{
            if(PlayerStateManager.Instance.GetCurrentState != PlayerState.Land)
                PlayerStateManager.Instance.SetState(PlayerState.HoldAir);
            DataFactory.Instance.Jumpadd = _tempJumpadd;
        }

    }

    void OnCollisionEnter2D(Collision2D other) {
        foreach(var hit in other.contacts){
            if(hit.normal == Vector2.up){
                PlayerStateManager.Instance.SetState(PlayerState.Land);
                break;
            }
        }
    }
    


}
