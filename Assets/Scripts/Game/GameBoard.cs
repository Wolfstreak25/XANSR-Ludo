using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoard : Singleton<GameBoard>
{
    public static float spacing = 1f;
    private List<Tile> tiles = new List<Tile>();
    public List<Tile> Tiles { get   {return tiles;} }
    protected override void Awake()
    {
        base.Awake();
        for(int i = 0; i<gameObject.transform.childCount; i++)
        {
            var obj = gameObject.transform.GetChild(i).GetComponent<Tile>();
            if(obj != null)
            {
                tiles.Add(obj);
            }
        }
    }
    private void Start() 
    {
        foreach (var item in tiles)
        {
            if(item.startTile)
            {
                StartCoroutine(item.InitializeTile());
            }
        }
    }
    public static readonly Vector2[] directions =
    {
        new Vector2(spacing, 0f),
        new Vector2(-spacing, 0f),
        new Vector2(0f, spacing),
        new Vector2(0f, -spacing)
    };
}
