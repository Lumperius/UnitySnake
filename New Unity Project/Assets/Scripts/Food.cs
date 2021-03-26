using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Food : MonoBehaviour
{
    public int X;
    public int Y;

    private Field field;

    [SerializeField]
    Transform FoodObj;

    [SerializeField]
    Transform FoodPrefab;


    // Start is called before the first frame update
    void Start()
    {
        field = GameObject.FindObjectOfType<Field>();

        GenerateNewPosition();
        FoodObj = Instantiate(FoodPrefab);
        FoodObj.localPosition = new Vector2(X, Y);
    }

    // Update is called once per frame
    void Update()
    {
        FoodObj.localPosition = new Vector2(X, Y);
    }

    public void GenerateNewPosition(IEnumerable<Vector3> snakePosition)
    {
        var possiblePositions = new List<(int x, int y)>();
        for (int i = 0; i < field.Height; i++)
        {
            for (int k = 0; k < field.Width; k++)
            {
                if (!snakePosition.Any(p => p.x == i && p.y == k))
                {
                    var possiblePosition = (i, k);
                    possiblePositions.Add(possiblePosition);
                }
            }
        }
        var randomPosition = possiblePositions[Random.Range(0, possiblePositions.Count)];
        X = randomPosition.x;
        Y = randomPosition.y;
    }

    public void GenerateNewPosition()
    {
        X = Random.Range(2, field.Width + 1);
        Y = Random.Range(2, field.Height + 1);
    }
}
