using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UISystem : MonoBehaviour
{
    
    private TMP_Text _scoreText;
    private int _score;

    public void AddScore(int addscore){
        _score+=addscore;
        _scoreText.text = "Score : "+_score.ToString();
    }
}
