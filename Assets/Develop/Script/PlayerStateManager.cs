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

    void Update(){
        if(_state == PlayerState.Dead){
            /*
            if (!_fadeinout.enabled) _fadeinout.enabled = true;
            if (_rigidbody2d.simulated)
            {
                _rigidbody2d.simulated = false;
                _fadeinout.Play("FadeIn");
                _fadeinout.Rebind();
            }*/
        }


    }


    public void SetState(PlayerState newState) => _state = newState;
    public PlayerState GetCurrentState => _state;

}
