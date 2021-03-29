using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Convert = System.Convert;
using UnityEngine;

public enum Direction
{
    Up = 1,
    Down = 2,
    Left = 3,
    Right = 4
}

public class Snake : MonoBehaviour
{
    [SerializeField]
    Transform SnakeHeadPrefab, SnakeChainPrefab;

    private Game game;
    private Food food;
    private Field field;

    public List<Transform> SnakeChains = new List<Transform>();
    public float SnakeSpeed { get; set; } = 0.2f;

    private Direction Direction = Direction.Right;
    private Direction PrevDirection = Direction.Left;

    // Start is called before the first frame update
    void Start()
    {
        game = FindObjectOfType<Game>();
        food = FindObjectOfType<Food>();
        field = FindObjectOfType<Field>();

        transform.position = new Vector2(0, 0);

        ResetSnake();
        InvokeRepeating(nameof(ProccessSnake), 0, SnakeSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
    }

    public void ResetSnake()
    {
        for(int i = 0; i < SnakeChains.Count; i++)
        {
            Destroy(SnakeChains[i].gameObject);
        }
        SnakeChains.Clear();
        Transform head = Instantiate(SnakeHeadPrefab);
        head.SetParent(transform, true);
        head.position = new Vector2(3, 3);

        SnakeChains.Add(head);
        Direction = Direction.Right;
    }

    private void ProccessSnake()
    {
        Transform newChain = Instantiate(SnakeHeadPrefab);
        switch (Direction)
        {
            case Direction.Up:
                newChain.position = new Vector2(SnakeChains[0].position.x, SnakeChains[0].position.y + 1);
                break;
            case Direction.Down:
                newChain.position = new Vector2(SnakeChains[0].position.x, SnakeChains[0].position.y - 1);
                break;
            case Direction.Left:
                newChain.position = new Vector2(SnakeChains[0].position.x - 1, SnakeChains[0].position.y);
                break;
            default:
                newChain.position = new Vector2(SnakeChains[0].position.x + 1, SnakeChains[0].position.y);
                break;
        }
        newChain.parent = transform;
        MoveSnake(newChain);
    }

    private void MoveSnake(Transform newPoint)
    {
        PrevDirection = Direction;
        if (CheckObstacle(newPoint)) 
        {
            Destroy(newPoint.gameObject);
            ResetSnake();
            return;
        }
        SnakeChains.Reverse();

        if (SnakeChains.Count > 1)
        {
            int indexOfLastInReversed = SnakeChains.Count - 1;
            Transform secondElement = Instantiate(SnakeChainPrefab);
            secondElement.parent = transform;
            secondElement.position = SnakeChains[indexOfLastInReversed].localPosition;
            Destroy(SnakeChains[indexOfLastInReversed].gameObject);
            SnakeChains.RemoveAt(indexOfLastInReversed);
            SnakeChains.Add(secondElement);
        }

        SnakeChains.Add(newPoint);
        SnakeChains.Reverse();

        bool ateFood = newPoint.localPosition.x == food.X && newPoint.localPosition.y == food.Y;
        if (ateFood)
        {
            var snakePosition = SnakeChains.Select(sc => sc.localPosition);
            food.GenerateNewPosition(snakePosition);
        }
        else
        {
            var lastChain = SnakeChains[SnakeChains.Count - 1];
            SnakeChains.RemoveAt(SnakeChains.Count - 1);
            Destroy(lastChain.gameObject);
        }
    }


    private void HandleInput()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            if (PrevDirection != Direction.Down)
                Direction = Direction.Up;
            return;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            if (PrevDirection != Direction.Up)
                Direction = Direction.Down;
            return;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (PrevDirection != Direction.Right)
                Direction = Direction.Left;
            return;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (PrevDirection != Direction.Left)
                Direction = Direction.Right;
            return;
        }
    }

    private bool CheckObstacle(Transform newPoint)
    {
        var vector3NewPoint = new Vector3Int(
            Convert.ToInt32(newPoint.localPosition.x), //x
            Convert.ToInt32(newPoint.localPosition.y), //y
            1);                                        //z

        bool isNewPointTail = SnakeChains.Any(sc =>
         sc.localPosition.x == newPoint.localPosition.x &&
         sc.localPosition.y == newPoint.localPosition.y);

        if (isNewPointTail || field.CheckPointForObstacle(vector3NewPoint))
             return true;
        else return false;
    }
}