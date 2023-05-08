using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeTile : Tile
{
    public override Tile MoveToNext(PlayerType player)
    {
        
        // if(m_turnType == player)
        // {
        //     return m_turnTile.tile;
        // }
        return nextTile;
    }
}
