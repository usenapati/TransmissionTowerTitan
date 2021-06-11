using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameState { START, PLAYER1TURN, PLAYER2TURN, PLAYER1WIN, PLAYER2WIN }

public class GameManager : MonoBehaviour
{
    public GameObject player1Prefab;
    public GameObject player2Prefab;

    public Transform player1Location;
    public Transform player2Location;

    Player player1;
    Player player2;

    public Node selectedNode = null;

    public Text dialogueText;

    public GameState state;

    // Start is called before the first frame update
    void Start()
    {
        state = GameState.START;
        StartCoroutine(SetupBoard());
    }

    IEnumerator SetupBoard()
    {
        GameObject player1GO = Instantiate(player1Prefab, player1Location);
        GameObject player2GO = Instantiate(player2Prefab, player2Location);

        player1 = player1GO.GetComponent<Player>();
        player2 = player2GO.GetComponent<Player>();

        //HUD Setup

        yield return new WaitForSeconds(2f);

        state = GameState.PLAYER1TURN;
        Player1Turn();
    }

    void Player1Turn()
    {
        dialogueText.text = "Player 1, choose an action:";
    }
    
    void Player2Turn()
    {
        dialogueText.text = "Player 2, choose an action:";
    }

    public void OnBuyButton()
    {
        if (state == GameState.PLAYER1TURN || state == GameState.PLAYER2TURN)
            if (state == GameState.PLAYER1TURN)
                Player1Buy();
            else
                Player2Buy();
        else
            return;
    }

    void Player1Buy()
    {

    }

    void Player2Buy()
    {

    }
}
