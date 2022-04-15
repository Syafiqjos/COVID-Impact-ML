using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayManager : MonoBehaviour
{
    public static GameplayManager Instance { get; set; }
    [SerializeField] private bool gameFinished = false;

    [SerializeField] private int gameScore;
    private string latestScoreMessage = "";

    [SerializeField] private float gameTime = 60.0f;
    private float timer;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        timer = gameTime;
    }

    private void Update()
    {
        if (timer < 0)
        {
            gameFinished = true;
            PlayerInstance.Instance.controller.enableController = false;

            timer = 0;
        } else
        {
            timer -= Time.deltaTime;
        }
    }

    public bool IsGameFinished()
    {
        return gameFinished;
    }

    public void AddScore(int score) {
        if (!gameFinished)
        {
            gameScore += score;
        }
    }

    public int GetScore() {
        return gameScore;
    }

    public float GetTimeLeft()
    {
        return timer;
    }

    public void ShowScoreMessage(string message)
    {
        latestScoreMessage = message;
        Debug.Log(message);
    }

    public string GetScoreMessage()
    {
        string message = latestScoreMessage;
        latestScoreMessage = "";
        return message;
    }

    public void RetryLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
