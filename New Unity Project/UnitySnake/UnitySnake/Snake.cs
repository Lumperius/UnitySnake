using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    public GameObject SnakeHead;
    public List<SnakeChain> SnakeChains;

    private SnakeHead _snakeHeadScript;
    private SnakeChain _snakeChainScript;

    // Start is called before the first frame update
    void Start()
    {
        _snakeHeadScript = SnakeHead.GetComponent<SnakeHead>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
