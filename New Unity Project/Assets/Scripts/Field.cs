using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Field : MonoBehaviour
{
    [SerializeField]
    public Tilemap tilemap;

    [SerializeField]
    public TileBase FieldTilePrefab, BorderTilePrefab;

    public int Width { get; set; } = 10;
    public int Height { get; set; } = 5;


    // Start is called before the first frame update
    void Start()
    {
        Draw();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Draw()
    {
        for (int i = 0; i <= Width; i++)
        {
            for (int k = 0; k <= Height; k++)
            {
                tilemap.SetTile(new Vector3Int(i, k, 1), FieldTilePrefab);
            }
        }
        DrawBorder();
    }

    public void DrawBorder()
    {
        for(int i = -1; i <= Width + 1; i++)
        {
            tilemap.SetTile(new Vector3Int(i, -1, 1), BorderTilePrefab);
            tilemap.SetTile(new Vector3Int(i, Height + 1, 1), BorderTilePrefab);
        }

        for (int i = -1; i <= Height + 1; i++)
        {
            tilemap.SetTile(new Vector3Int(-1, i, 1), BorderTilePrefab);
            tilemap.SetTile(new Vector3Int(Width + 1, i, 1), BorderTilePrefab);
        }
    }

    public bool CheckPointForObstacle(Vector3Int point)
    {
        TileData tileData = new TileData();
        tilemap.GetTile(point).GetTileData(point, null, ref tileData);
        if (tileData.sprite.name == "BorderTitle")
        {
            return true;
        }
        else return false;
    }
}
