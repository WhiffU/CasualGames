using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Turn : MonoBehaviour
{
     public TextMeshProUGUI myText;
     void Start()
    {
        myText = GetComponent<TextMeshProUGUI>();
        myText.text = "RED'S TURN";
        GameManager.instance.message += UpdateMessage;
    }
    void UpdateMessage(Player player)
    {
        myText.text = GameManager.instance.hasGameFinished? "GAME OVER! " : player.ToString() + "'S TURN";
    }
}
