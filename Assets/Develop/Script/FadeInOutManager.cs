using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInOutManager : MonoBehaviour
{
    public PlayerActionManager Player;

    public void OnReStart(){
        Player.Restart();
    }


}
