using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;

public enum PlayerState{
    Idle,
    Land,
    Jump,
    Dead,
    HoldAir
}

public class PlayerStateManager : Singleton<PlayerStateManager>
{
    private PlayerStateFactory _statefactory;
    private StateInterface _stateinterface;
    private StateInterface _fixedStateinterface;
    public PlayerState _playerState;

    void Start(){
        _statefactory = new PlayerStateFactory();
        _playerState = PlayerState.HoldAir;
    }

    void Update(){
        if(_stateinterface!=null) _stateinterface.State();
    }

    void FixedUpdate(){
        if (_fixedStateinterface != null) _fixedStateinterface.State();
    }

    public void SetState(PlayerState newState) {
        _playerState = newState;

        if(newState == PlayerState.Idle || newState == PlayerState.Dead){
            _fixedStateinterface = null;
            _stateinterface = _statefactory.Create(newState);
        }
        else{
            _stateinterface = null;
            _fixedStateinterface = _statefactory.Create(newState);
        }
    }

    public PlayerState GetCurrentState => _playerState;

}

public class PlayerStateFactory{
    private PlayerDeadState _playerDeadState;
    private PlayerJumpState _playerJumpState;
    private PlayerHoldAirState _playerHoldAirState;
    private PlayerLandState _playerLandState;

    public PlayerStateFactory(){}

    public StateInterface Create(PlayerState state){
        if (state==PlayerState.Dead){
            Animator animator = (Animator) DataFactory.Instance.Create(PlayerDataType.dieAnim);
            Rigidbody2D rigidbody = (Rigidbody2D) DataFactory.Instance.Create(PlayerDataType.playerRigidbody);
            if(_playerDeadState == null) _playerDeadState = new PlayerDeadState(rigidbody, animator);
            return _playerDeadState;
        }

        else if(state == PlayerState.Land){
            Rigidbody2D rigidbody = (Rigidbody2D)DataFactory.Instance.Create(PlayerDataType.playerRigidbody);
            float speed = DataFactory.Instance.Speed;
            if (_playerLandState == null) _playerLandState = new PlayerLandState(rigidbody,speed);
            return _playerLandState;
        }


        else if(state == PlayerState.Jump){
            Rigidbody2D rigidbody = (Rigidbody2D)DataFactory.Instance.Create(PlayerDataType.playerRigidbody);
            float Speed = DataFactory.Instance.Speed;
            if(_playerJumpState == null) _playerJumpState = new PlayerJumpState(rigidbody,Speed);
            return _playerJumpState;
        }


        else if (state == PlayerState.HoldAir)
        {
            Rigidbody2D rigidbody = (Rigidbody2D)DataFactory.Instance.Create(PlayerDataType.playerRigidbody);
            if (_playerHoldAirState == null) _playerHoldAirState = new PlayerHoldAirState(rigidbody);
            return _playerHoldAirState;
        }

        else
            return null;
    }

}
