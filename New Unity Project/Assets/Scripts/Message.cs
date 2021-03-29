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
        message.resizeTextForBestFit = true;
        message.enabled = true;
        snake = FindObjectOfType<Snake>();
    }

    // Update is called once per frame
    void Update()
    {
        message.text = $"You got {snake.SnakeChains.Count} snake points.";
    }

    public void SetMessage(string newMessage)
    {
        message.text = newMessage;
    }
}
