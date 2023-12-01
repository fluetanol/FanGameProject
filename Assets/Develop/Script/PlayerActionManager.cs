using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerActionManager : MonoBehaviour
{
    Vector3 moveDirection;
    int moveSpeed = 2;


    void Update()
    {
        bool hasControl = (moveDirection != Vector3.zero);
        if (hasControl)
        {
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
        }
    }
    
    void OnMove(InputValue value){

        Vector2 input = value.Get<Vector2>();
        if(input!=null){
            
            moveDirection = new Vector2(input.x, input.y);
            Debug.Log($"Send: {input.magnitude}, input : {input.x}, {input.y}");
        }
    }

}
