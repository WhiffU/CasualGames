using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;
using System.Linq;

public class HangmanController : MonoBehaviour
{
     
    [SerializeField] GameObject wordContainer;
    [SerializeField] GameObject category;

    [SerializeField] GameObject keyboardContainer;
    [SerializeField] GameObject letterContainer;
    [SerializeField] GameObject[] hangmanStages;
    [SerializeField] GameObject letterButton;
    [SerializeField] TextAsset possibleWord;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject losePanel;
    [SerializeField] private Button btnContinue;
    [SerializeField] private Button btnRestart;
    [SerializeField] private Button btnHome;

    private string word;
    private int incorrectGuess, correctGuess;
    private void Start()
    {
        btnContinue.onClick.AddListener(ContinueGame);
        btnRestart.onClick.AddListener(RestartGame);
        btnHome.onClick.AddListener(GoToMainMenu);
        InitialiseButtons();
        InitialiseGame();
    }
    private void GoToMainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }

    private void RestartGame()
    {
        losePanel.SetActive(false);
        InitialiseGame();
        Time.timeScale = 1f;
    }

    private void ContinueGame()
    {
        winPanel.SetActive(false);
        InitialiseGame();
        Time.timeScale = 1f;
    }

    private void InitialiseButtons()
    {
        for (int i = 65; i <= 90; i++) // ASCII 
        {
            CreateButton(i);
        }
    }

    private void InitialiseGame()
    {
        //Reset data back to original state
        incorrectGuess = 0;
        correctGuess = 0;
        foreach (Button child in keyboardContainer.GetComponentsInChildren<Button>())
        {
            child.interactable = true;
        }
        foreach (Transform child in wordContainer.GetComponentInChildren<Transform>())
        {
            Destroy(child.gameObject);
        }
        foreach (GameObject stage in hangmanStages)
        {
            stage.SetActive(false);
        }

        //Generate new word
        word = generateWord().ToUpper();
        foreach (char letter in word)
        {
            var temp = Instantiate(letterContainer, wordContainer.transform);
        }

    }

    private void CreateButton(int i)
    {
        GameObject temp = Instantiate(letterButton, keyboardContainer.transform);
        temp.GetComponentInChildren<TextMeshProUGUI>().text = ((char)i).ToString();
        temp.GetComponent<Button>().onClick.AddListener(delegate { CheckLetter(((char)i).ToString()); });
    }

    private string generateWord()
    {
        string[] wordList = possibleWord.text.Split();
        string line = wordList[Random.Range(0,wordList.Length)];
        Debug.Log(line);
        return line.Substring(0, line.Length );
    }

    private void CheckLetter(string inputLetter)
    {
        bool letterInWord = false;
        for (int i = 0; i < word.Length; i++)
        {
            if (inputLetter == word[i].ToString())
            {
                letterInWord = true;
                correctGuess++;
                wordContainer.GetComponentsInChildren<TextMeshProUGUI>()[i].text = inputLetter;

            }
        }
        if (letterInWord == false)
        {
            incorrectGuess++;
            hangmanStages[incorrectGuess - 1].gameObject.SetActive(true);
        }
        CheckOutcome();
    }
    private void CheckOutcome()
    {
        if (correctGuess == word.Length) //Win
        {
            for (int i = 0; i < word.Length; i++)
            {
                wordContainer.GetComponentsInChildren<TextMeshProUGUI>()[i].color = Color.green;
            }
            Time.timeScale = 0f;
            winPanel.SetActive(true);
            //Invoke("InitialiseGame", 3f);
        }

        if (incorrectGuess == hangmanStages.Length) // Lose
        {
            for (int i = 0; i < word.Length; i++)
            {
                wordContainer.GetComponentsInChildren<TextMeshProUGUI>()[i].color = Color.red;
                wordContainer.GetComponentsInChildren<TextMeshProUGUI>()[i].text = word[i].ToString();
            }
            //Invoke("InitialiseGame", 3f);
            Time.timeScale = 0f;
            losePanel.SetActive(true);
        }
    }



}
