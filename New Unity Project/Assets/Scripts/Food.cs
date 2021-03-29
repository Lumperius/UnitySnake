using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Food : MonoBehaviour
{
    public int X;
    public int Y;

    private Field field;
    private Game game;

    [SerializeField]
    Transform FoodObj;

    [SerializeField]
    Transform FoodPrefab;


    // Start is called before the first frame update
    void Start()
    {
        game = FindObjectOfType<Game>();
        field = FindObjectOfType<Field>();

        GenerateNewPosition();
        FoodObj = Instantiate(FoodPrefab);
        FoodObj.SetParent(transform, true);
        FoodObj.localPosition = new Vector2(X, Y);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(0, 0);
        FoodObj.localPosition = new Vector2(X, Y);
    }

    public void GenerateNewPosition(IEnumerable<Vector3> snakePosition)
    {
        var listedSnake = snakePosition.ToList();
        var possiblePositionList = new List<(int x, int y)>();
        for (int i = 0; i <= field.Height; i++)
        {
            for (int k = 0; k <= field.Width; k++)
            {
                if (!snakePosition.Any(p => p.x == k && p.y == i))
                {
                    var possiblePosition = (k, i);
                    possiblePositionList.Add(possiblePosition);
                }
            }
        }
        var randomPosition = possiblePositionList[Random.Range(0, possiblePositionList.Count)];
        X = randomPosition.x;
        Y = randomPosition.y;
    }

    public void GenerateNewPosition()
    {
        X = Random.Range(2, field.Width + 1);
        Y = Random.Range(2, field.Height + 1);
    }
}
