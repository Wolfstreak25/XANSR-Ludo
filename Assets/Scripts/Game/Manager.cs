using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class Manager : Singleton<Manager>
{
    [SerializeField] private TextMeshProUGUI m_currentTurn;
    [SerializeField] private TextMeshProUGUI m_diceRoll;
    [SerializeField] private Image m_diceButton;
    [SerializeField] private PlayerController m_redPlayer;
    [SerializeField] private PlayerController m_greenPlayer;
    [SerializeField] private PlayerController m_yellowPlayer;
    [SerializeField] private PlayerController m_bluePlayer;
    // [SerializeField] private GameObject m_gameOver;
    private PlayerController m_currentPlayer;
    private PlayerType m_currentType;
    bool movePlayer = false;
    private void Start() 
    {
        PlayerMove((PlayerType)Random.Range(0,4));
    }
    public void PlayerMove(PlayerType playerType)
    {
        if(movePlayer)
        {
            movePlayer = false;
        }
        m_currentType = playerType;
        switch(playerType)
        {
            case PlayerType.Red:
                m_currentPlayer = m_greenPlayer;
            break;
            case PlayerType.Green:
                m_currentPlayer = m_yellowPlayer;
            break;
            case PlayerType.Yellow:
                m_currentPlayer = m_bluePlayer;
            break;
            case PlayerType.Blue:
                m_currentPlayer = m_redPlayer;
            break;
        }
        m_diceButton.color = m_currentPlayer.color;
    }
    public void Roll()
    {   
        if(!movePlayer )
        {
            m_currentPlayer.MyTurn(ThrowDice());
        }
    }
    // public void Reroll()
    // {
    //     if(movePlayer == null )
    //     {
    //         movePlayer  = StartCoroutine(m_currentPlayer.Move(ThrowDice()));
    //     }
    // }
    private int ThrowDice()
    {
        int i = Random.Range(1,7);
        m_diceRoll.text = $"{i}";
        return i;
    }
    public void GameOver()
    {
        // m_gameOver.SetActive(true);
        LobbyManager.Instance.ChangeScene(2);
    }

}
