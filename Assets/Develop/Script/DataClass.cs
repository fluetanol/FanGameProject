using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public enum PlayerDataType{
    dieAnim,
    endLine,
    startpoint,
    playerRigidbody,
    moveDirection

}


public class DataFactory : Singleton<DataFactory>
{
    [SerializeField] private Animator animator;
    [SerializeField] private Transform startpoint;
    [SerializeField] private Transform endline;
    [SerializeField] private Rigidbody2D playerRigidbody;
    public Vector2 _moveDirection;

    public float Speed;
    public float JumpV0;
    public float Jumpadd;
    private float _jump;

    public object Create(PlayerDataType type){
        if(type == PlayerDataType.endLine)
            return endline;
        else if(type == PlayerDataType.dieAnim)
            return animator;
        else if (type == PlayerDataType.startpoint)
            return startpoint;
        else if(type == PlayerDataType.playerRigidbody)
            return playerRigidbody;
        else
            return null;
    }
}
