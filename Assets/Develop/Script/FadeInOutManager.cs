using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInOutManager : MonoBehaviour
{
    public PlayerActionManager Player;
    public Event some;

    public void OnReStart(){
        Player.Restart();
        PlayerStateManager.Instance.SetState(PlayerState.Idle);
    }


}
