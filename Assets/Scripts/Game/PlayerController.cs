using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    // [SerializeField] private int m_totalMoves = 56;
    private int m_remainingMoves = 56;
    [SerializeField] private Tile m_currentTile;
    [SerializeField] private PlayerType m_currentType;
    [SerializeField] private float m_moveDelay;
    [SerializeField] private Color m_color;
    public Color color { 
                            get{
                                m_color.a = 1f;
                                return m_color;
                                }
                        }
    public PlayerType Type  {get{return m_currentType;}}
    
    [SerializeField] private Animator anim;
    private void Start() 
    {
        // anim = gameObject.GetComponent<Animator>();
        m_currentTile.SetPlayerTile(this);
    }
    public void MyTurn(int move)
    {
        if(m_currentTile.homeTile)
        {
            if(move == 6)
            {
                StartCoroutine(Move(1));
            }
            else
            {
                Manager.Instance.PlayerMove(m_currentType);
            }
        }
        else
        {
            if(m_remainingMoves - move < 0)
            {
                Manager.Instance.PlayerMove(m_currentType);
                return;
            }
            else
            {
                m_remainingMoves = m_remainingMoves - move;
                StartCoroutine(Move(move));
            }
        }
    }
    public IEnumerator Move(int move)
    {
        int steps = move;
        var thisTile = m_currentTile;
        while(steps > 0)
        {
            steps --;
            thisTile = thisTile.MoveToNext(m_currentType);
            yield return new WaitForSeconds(m_moveDelay);
            // jump animation can be added here
            anim.SetTrigger("jump");
            transform.position = thisTile.Coordinate;
        }
        if(steps == 0)
        {
            m_currentTile.UnsetPlayerTile(this);
            m_currentTile = thisTile;
            m_currentTile.SetPlayerTile(this);
            Manager.Instance.PlayerMove(m_currentType);
        }
    }
}
