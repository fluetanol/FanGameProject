using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Interactions;


public interface StateInterface
{
    public void State();
}

public class PlayerBasicMove{
    protected Vector2 normalMove(Vector2 moveDirection)
    {
        if (moveDirection.x != 0) moveDirection.x = moveDirection.x / Mathf.Abs(moveDirection.x) * DataFactory.Instance.Speed;
        return moveDirection;
    }
}


public class PlayerLandState : PlayerBasicMove, StateInterface{

    private Rigidbody2D _rigidbody2d;
    private float _speed;

    public PlayerLandState(Rigidbody2D rigidbody2D, float speed){
        this._rigidbody2d = rigidbody2D;
        this._speed = speed;
    }

    public void State()
    {
        Vector2 moveDirection = DataFactory.Instance._moveDirection;
        moveDirection = normalMove(moveDirection);
        moveDirection.y = _rigidbody2d.velocity.y;
        DataFactory.Instance._moveDirection = moveDirection;
        _rigidbody2d.velocity = moveDirection;
    }
}



public class PlayerJumpState : PlayerBasicMove, StateInterface
{
    private Rigidbody2D _rigidbody2d;
    private float _jump;
    private float _speed;


    public PlayerJumpState(Rigidbody2D rigidbody2d, float speed)
    {
        this._rigidbody2d = rigidbody2d;
        this._speed = speed;
        _jump = DataFactory.Instance.Jumpadd;
    }


    public void State(){
        Vector2 moveDirection = DataFactory.Instance._moveDirection;
        float _Jumpadd = DataFactory.Instance.Jumpadd;
        moveDirection = normalMove(moveDirection);
        _rigidbody2d.gravityScale = 2;
        moveDirection.y += _Jumpadd * Time.deltaTime;

        DataFactory.Instance.Jumpadd -= 1;
        DataFactory.Instance._moveDirection = moveDirection;
        if (_Jumpadd <= 0) {
            PlayerStateManager.Instance.SetState(PlayerState.HoldAir);
            DataFactory.Instance.Jumpadd = _jump;
        }
        _rigidbody2d.velocity = moveDirection;
    }
}


public class PlayerHoldAirState : PlayerBasicMove, StateInterface
{
    private Rigidbody2D _rigidbody2d;

    public PlayerHoldAirState(Rigidbody2D rigidbody2D){
        _rigidbody2d = rigidbody2D;

    }

    public void State(){
        Vector2 moveDirection = DataFactory.Instance._moveDirection;
        moveDirection = normalMove(moveDirection);
        _rigidbody2d.gravityScale = 5;
        moveDirection.y = _rigidbody2d.velocity.y;
        DataFactory.Instance._moveDirection =  moveDirection;
        _rigidbody2d.velocity = moveDirection;
    
    }
}




public class PlayerDeadState : StateInterface
{
    private Rigidbody2D _rigidbody2d;
    private Animator _animator;

    public PlayerDeadState(Rigidbody2D rigidbody2d, Animator animator)
    {
        this._rigidbody2d = rigidbody2d;
        this._animator = animator;
    }

    public void State()
    {
        if (!_animator.enabled) _animator.enabled = true;
        if (_rigidbody2d.simulated)
        {
            _rigidbody2d.simulated = false;
            _animator.Play("FadeIn");
            _animator.Rebind();
        }
    }
}