using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Dice : MonoBehaviour
{
    int roll; //value of the dice

    [SerializeField]
    List<Sprite> dices;//image of dice (1-6)

    //Generate random dice image

    public void RandomImage()
    {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        renderer.sprite = dices[Random.Range(0, dices.Count)];
    }


    //Set value for the last dice image
    public void SetImage()
    {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        renderer.sprite = dices[roll-1];
        GameManager.instance.MovePiece();
    }


    // Temperary roll value
    public void Roll(int temp)
    {
        roll = temp;
        Animator animator = GetComponent<Animator>();
        animator.Play("Roll", -1, 0f);
    }
}
