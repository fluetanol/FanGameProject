using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Threading;


public class UISystem : MonoBehaviour
{
    
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private TMP_Text _fpsText;
    private int _score;


    private void Update() {
        float fps = 1.0f/Time.deltaTime;
        float ms = Time.deltaTime * 1000.0f;
        _fpsText.text = "Fps : "+fps;
    }



    public void AddScore(int addscore){
        _score+=addscore;
        _scoreText.text = "Score : "+_score.ToString();
    }
}
