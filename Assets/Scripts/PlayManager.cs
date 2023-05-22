using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayManager : MonoBehaviour
{
    [SerializeField] TMP_Text timerText;
    [SerializeField] GameObject[] stars;
    [SerializeField] GameObject finishedCanvas;
    [SerializeField] TMP_Text finishedText;

    [SerializeField] float timer;

    public bool gameOn;

    public void SetText(string text)
    {
        int Minutes = Mathf.FloorToInt(timer / 60);
        int Seconds = Mathf.FloorToInt(timer % 60);
        timerText.text = Minutes.ToString("00") + ":" + Seconds.ToString("00");
    }

    public void GameStart()
    {
        gameOn = true;
    }

    private void Update() {
        if(gameOn)
        {
            timer -= Time.deltaTime;
            SetText(timer.ToString());
            if(timer <= 0)
            {
                gameOn = false;
                GameOver();
            }
        }
    }
    
    public void GameOver()
    {
        finishedText.text = "Game Over\nPlease Try Again!";
        finishedCanvas.SetActive(true);
    }

    public void PlayerWin()
    {
        finishedText.text = "You Win!";
        finishedCanvas.SetActive(true);

        if(timer > 90)
        {
            stars[0].SetActive(true);
            stars[1].SetActive(true);
            stars[2].SetActive(true);
        }
        else if(timer > 45)
        {
            stars[0].SetActive(true);
            stars[1].SetActive(true);
        }
        else
        {
            stars[0].SetActive(true);
        }
    }
}
