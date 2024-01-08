using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum PlayerState{
    Land,
    Jump,
    Dead,
    HoldAir
}


public class PlayerStateManager : Singleton<PlayerStateManager>
{
    [SerializeField] private GameObject player;
    private PlayerState _state;
    private Rigidbody2D _rigidbody2d;
    private Animator _animator;

    void Start(){
        _rigidbody2d = player.GetComponent<Rigidbody2D>();
        _animator = player.GetComponent<Animator>();
    }

    void Update(){
        if(_state == PlayerState.Dead){
            DeadState();
        }


    }

    private void DeadState(){
        if (!_animator.enabled)_animator.enabled = true;
        if (_rigidbody2d.simulated)
        {
            _rigidbody2d.simulated = false;
            _animator.Play("FadeIn");
            _animator.Rebind();
        }
    }


    public void SetState(PlayerState newState) => _state = newState;
    public PlayerState GetCurrentState => _state;

}
