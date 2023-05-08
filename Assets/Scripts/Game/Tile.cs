using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool startTile;
    public bool endTile;
    public bool homeTile;
    public bool turnTile;
    private SpriteRenderer sprite;
    protected Animator anime;
    [SerializeField] protected TileType m_tiletype;
    [SerializeField] protected NextTile m_nextTile;
    public Tile  nextTile {get{return m_nextTile.tile;}}
    [SerializeField] protected List<PlayerController> m_currentOccupant = new List<PlayerController>();
    public Vector2 Coordinate {get{return coordinate;}}
    private Vector2 coordinate;
    private void Awake() {
        
        sprite = gameObject.GetComponent<SpriteRenderer>();
        if(!homeTile)
        {
            sprite.enabled = false;
            anime = gameObject.GetComponent<Animator>();
        }
    }
    protected void Start() {
        coordinate = transform.position;
    }
    protected virtual void GetNextTile()
    {
        if(!endTile)
        {
            var board = GameBoard.Instance;
            var foundNeighbor = board.Tiles.Find(n => n.Coordinate == Coordinate + m_nextTile.Direction);
            if(foundNeighbor == null)
            {
                Debug.Log(this);
            }
            m_nextTile.tile = foundNeighbor;
            StartCoroutine(m_nextTile.tile.InitializeTile());
        }
    }
    public virtual IEnumerator InitializeTile()
    {
        sprite = gameObject.GetComponent<SpriteRenderer>();
        sprite.enabled = true;
        yield return new WaitForSeconds(0.1f);
        GetNextTile();
    }
    public virtual void SetPlayerTile(PlayerController player)
    {
        if(endTile)
        {
            Manager.Instance.GameOver();
        }
        m_currentOccupant.Add(player);
    }
    public virtual void UnsetPlayerTile(PlayerController player)
    {
        m_currentOccupant.Remove(player);
    }
    public virtual Tile MoveToNext(PlayerType player)
    {
        // This is for setting tile animation while moving
        anime.SetTrigger("jump");
        return nextTile;
    }
}
[System.Serializable]
public struct NextTile
{
    [SerializeField] private MoveDirection direction;
    public Vector2 Direction
    { 
        get 
        {
            Vector2 nextDirection = new Vector2();
            switch(direction)
            {
                case MoveDirection.Up : 
                nextDirection = Vector2.up;
                break;
                case MoveDirection.Down : 
                nextDirection = Vector2.down;
                break;
                case MoveDirection.Left : 
                nextDirection = Vector2.left;
                break;
                case MoveDirection.Right : 
                nextDirection = Vector2.right;
                break;
                case MoveDirection.RightUp : 
                nextDirection = new Vector2(1,1);
                break;
                case MoveDirection.RightDown : 
                nextDirection = new Vector2(1,-1);
                break;
                case MoveDirection.LeftUp : 
                nextDirection = new Vector2(-1,1);
                break;
                case MoveDirection.LeftDown : 
                nextDirection = new Vector2(-1,-1);
                break;
            }
            return nextDirection;
        }
    }
    public Tile tile;
}
public enum MoveDirection
{
    Up,
    Down,
    Left,
    Right,
    LeftUp,
    LeftDown,
    RightUp,
    RightDown,
    none
}