using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interface : MonoBehaviour
{
    [SerializeField]
    private Transform ButtonPrefab;

    public Transform StartButton;
    public Transform StopButton;
    public Transform RestartButton;

    public Transform ResizeButton;

    // Start is called before the first frame update
    void Start()
    {
        StartButton = Instantiate(ButtonPrefab, transform);
        StartButton.position = new Vector2(0, 5);

        StopButton = Instantiate(ButtonPrefab, transform);
        StopButton.position = new Vector2(2, 5);

        RestartButton = Instantiate(ButtonPrefab, transform);
        RestartButton.position = new Vector2(4, 5);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
