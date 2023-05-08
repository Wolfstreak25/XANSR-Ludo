using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnTile : Tile
{
    [SerializeField] protected NextTile m_turnTile;
    [SerializeField] protected PlayerType m_turnType;
    Tile next;
    protected override void GetNextTile()
    {
        base.GetNextTile();
        var board = GameBoard.Instance;
            var foundNeighbor = board.Tiles.Find(n => n.Coordinate == Coordinate + m_turnTile.Direction);
            m_turnTile.tile = foundNeighbor;
            StartCoroutine(m_turnTile.tile.InitializeTile());
    }
    public override Tile MoveToNext(PlayerType player)
    {
        anime.SetTrigger("jump");
        if(m_turnType == player)
        {
            return m_turnTile.tile;
        }
        return nextTile;
    }
}
