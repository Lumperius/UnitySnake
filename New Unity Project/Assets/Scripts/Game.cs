using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    private Field field;
    private Snake snake;

    // Start is called before the first frame update
    void Start()
    {
        field = FindObjectOfType<Field>();
        snake = FindObjectOfType<Snake>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetNewFieldSize(int x, int y)
    {
        if(x < 5 || y < 5)
            throw new Exception("This game is harcore, choose larger field!");

        field.Width = x;
        field.Height = y;

        StartNewGame();
    }

    public void SetSnakeSpeed(float newSpeed)
    {
        snake.SnakeSpeed = newSpeed;
    }

    public void StartNewGame()
    {
        snake.ResetSnake();
    }
}
