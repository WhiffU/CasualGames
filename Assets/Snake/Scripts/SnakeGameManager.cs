using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SnakeGameManager : MonoBehaviour
{
    [SerializeField] private Button btnHome;
    [SerializeField] private Button btnHome2;
    [SerializeField] private Button btnRestart;
    [SerializeField] private Snake snake;
    [SerializeField] private GameObject losePanel;


    private void Start()
    {
        btnHome.onClick.AddListener(GoToMainMenu);
        btnHome2.onClick.AddListener(GoToMainMenu);
        btnRestart.onClick.AddListener(RestartGame);
    }
    private void GoToMainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }
    private void RestartGame()
    {
        losePanel.SetActive(false);
        snake.ResetState();
        Time.timeScale = 1f;
    }    
}
