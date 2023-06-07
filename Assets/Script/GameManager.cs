using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject pause_window;
    public GameObject rank_window;

    public TextMeshProUGUI scoreText;
    public TextMesh boardScoreText;

    private Player player;
         
    public enum GameState
    {
        Pause,   // 일시정지
        Run,    // 게임 진행
        Over    // 종료
    }

    public GameState gameState;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();

        pause_window.SetActive(false);
        rank_window.SetActive(false);

        scoreText.text = "0";

        gameState = GameState.Run;
    }

    void Update()
    {
        if (player.health == 0) gameState = GameState.Over;
        scoreText.text = player.score.ToString();

        switch (gameState)
        {
            case GameState.Pause:
                pause_window.SetActive(true);
                Time.timeScale = 0;
                break;
            case GameState.Run:
                pause_window.SetActive(false);
                Time.timeScale = 1;
                break;
            case GameState.Over:
                Time.timeScale = 0;
                rank_window.SetActive(true);
                break;
        }
    }

    public void Pause()
    {
        gameState = GameState.Pause;
    }

    public void Resume()
    { 
        gameState = GameState.Run;
    }
    public void Restart()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
