using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private Button btnHangMan;
    [SerializeField] private Button btnSnake;
    [SerializeField] private Button btnSnakeLadder;

    private void Start()
    {
        btnHangMan.onClick.AddListener(playHangman);
        btnSnake.onClick.AddListener(playSnake);
        btnSnakeLadder.onClick.AddListener(playSnakeLadder);

    }
    private void playHangman()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    private void playSnake()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
    }
    private void playSnakeLadder()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(3);
    }


}
