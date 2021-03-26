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

    private Food food;
    private Field field;

    public List<Transform> SnakeChains;
    private Direction Direction = Direction.Right;
    private Direction PrevDirection = Direction.Left;

    // Start is called before the first frame update
    void Start()
    {
        food = GameObject.FindObjectOfType<Food>();
        field = GameObject.FindObjectOfType<Field>();

        Transform Head = Instantiate(SnakeHeadPrefab);
        Head.localPosition = new Vector2(5, 5);

        SnakeChains = new List<Transform>() { Head };

        InvokeRepeating("ProccessSnake", 0, 0.2f);
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
    }

    private void ProccessSnake()
    {
        print(Direction);
        Transform newChain = Instantiate(SnakeHeadPrefab);
        switch (Direction)
        {
            case Direction.Up:
                newChain.localPosition = new Vector2(SnakeChains[0].position.x, SnakeChains[0].position.y + 1);
                break;
            case Direction.Down:
                newChain.localPosition = new Vector2(SnakeChains[0].position.x, SnakeChains[0].position.y - 1);
                break;
            case Direction.Left:
                newChain.localPosition = new Vector2(SnakeChains[0].position.x - 1, SnakeChains[0].position.y);
                break;
            default:
                newChain.localPosition = new Vector2(SnakeChains[0].position.x + 1, SnakeChains[0].position.y);
                break;
        }
        MoveSnake(newChain);
    }

    private void MoveSnake(Transform newPoint)
    {
        PrevDirection = Direction;
        CheckObstacle(newPoint);
        SnakeChains.Reverse();

        if (SnakeChains.Count > 1)
        {
            int indexOfLastInReversed = SnakeChains.Count - 1;
            Transform secondElement = Instantiate(SnakeChainPrefab);
            secondElement.localPosition = SnakeChains[indexOfLastInReversed].localPosition;
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

    private void CheckObstacle(Transform newPoint)
    {
        var vector3NewPoint = new Vector3Int(
            Convert.ToInt32(newPoint.localPosition.x), //x
            Convert.ToInt32(newPoint.localPosition.y), //y
            1);                                        //z
        bool isNewPointTail = SnakeChains.Any(sc =>
         sc.localPosition.x == newPoint.localPosition.x &&
         sc.localPosition.y == newPoint.localPosition.y);
        if (isNewPointTail || field.CheckPointForObstacle(vector3NewPoint))
            print("Yo ded, yo!");
    }
}