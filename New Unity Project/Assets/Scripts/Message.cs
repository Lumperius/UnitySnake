using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Message : MonoBehaviour
{
    private Snake snake;
    public Text message;

    // Start is called before the first frame update
    void Start()
    {
        snake = GameObject.FindObjectOfType<Snake>();
        Instantiate(message);
    }

    // Update is called once per frame
    void Update()
    {
        Instantiate(message);
        print(snake.SnakeChains.Count);
        message.text = $"You got {snake.SnakeChains.Count} snake points.";
    }
}
