using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public List<int> ranking;

    public UnityEvent OverEvent, ReadyEvent, StartEvent;

    public GameObject pause_window, rank_window, upArrow;

    public TextMeshProUGUI scoreText, rankingText;

    private Player player;

    public enum GameState
    {
        Ready, // (게임 시작 전) 대기
        Start, // 게임 시작
        Pause,   // 일시정지
        Resume,    // 게임 진행
        Over    // 종료
    }

    public GameState gameState;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();

        pause_window.SetActive(false);
        rank_window.SetActive(false);
        upArrow.SetActive(true);

        scoreText.text = "0";
        Time.timeScale = 0;

        for (int i = 0; i < 10; i++) ranking.Add(player.score);

        gameState = GameState.Ready;
    }

    void Update()
    {
        scoreText.text = player.score.ToString();

        switch (gameState)
        {
            case GameState.Ready:
                ReadyEvent.Invoke();
                if (Input.GetKeyDown(KeyCode.Space)) gameState = GameState.Start;
                break;
            case GameState.Start:
                if (player.health == 0) gameState = GameState.Over;

                StartEvent.Invoke();
                break;
            case GameState.Over:
                OverEvent.Invoke();
                break;
        }
    }

    public void Pause_Button()
    {
        pause_window.SetActive(true);
        Time.timeScale = 0;

        gameState = GameState.Pause;
    }

    public void Resume_Button()
    {
        pause_window.SetActive(false);
        Time.timeScale = 1;

        gameState = GameState.Resume;
    }

    public void Restart_Button()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void Over()
    {
        rank_window.SetActive(true);
        Time.timeScale = 0;
    }

    public void Ready()
    {
        upArrow.SetActive(true);
        Time.timeScale = 0;
    }

    public void gameStart()
    {
        upArrow.SetActive(false);

        if (Time.timeScale == 0) Time.timeScale = 1;
        Time.timeScale += player.score / 200 * Time.deltaTime;
    }
}
