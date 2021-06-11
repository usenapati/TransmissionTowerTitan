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

    public List<Node> nodes;

    public Text dialogueText;
    public Text revenueText;

    public GameState state;

    // Start is called before the first frame update
    void Start()
    {
        nodes = new List<Node>();
        foreach (Node node in Resources.FindObjectsOfTypeAll<Node>())
        {
            nodes.Add(node);
        }
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
        GenerateRevenue(true);
        revenueText.text = "Player 1: $" + player1.moneyAmount + "  Player 2: $" + player2.moneyAmount;
        CheckRevenue();
        CheckEndGame();
    }

    void Player2Turn()
    {
        dialogueText.text = "Player 2, choose an action:";
        GenerateRevenue(false);
        revenueText.text = "Player 1: $" + player1.moneyAmount + "  Player 2: $" + player2.moneyAmount;
        CheckRevenue();
        CheckEndGame();
    }

    void CheckEndGame()
    {
        if (nodes.Count == player1.nodes.Count + player2.nodes.Count)
        {
            if (player1.moneyAmount < player2.moneyAmount)
            {
                state = GameState.PLAYER2WIN;
                Player2Won();
            }
            if (player2.moneyAmount < player1.moneyAmount)
            {
                state = GameState.PLAYER1WIN;
                Player1Won();
            }
        }
        else
        {
            return;
        }
    }

    void Player1Won()
    {
        dialogueText.text = "Player 1 has won";
    }

    void Player2Won()
    {
        dialogueText.text = "Player 2 has won";
    }

    public void OnBuyButton()
    {
        Debug.Log("Buy Action");
        if ((state == GameState.PLAYER1TURN || state == GameState.PLAYER2TURN) && selectedNode != null)
            if (state == GameState.PLAYER1TURN)
                Player1Buy();
            else
                Player2Buy();
        else
            Debug.Log("You must select a node before buying it");
        return;
    }

    public void OnPassButton()
    {
        Debug.Log("Pass Turn");
        PassTurn();
    }

    void Player1Buy()
    {
        if (player1.moneyAmount >= selectedNode.price && !selectedNode.player1Owned && !selectedNode.player2Owned)
        {
            player1.moneyAmount -= selectedNode.price;
            selectedNode.player1Owned = true;
            player1.nodes.Add(selectedNode);
            Debug.Log("Player 1 has bought a node");
            foreach (GameObject terrain in selectedNode.nearbyTerrains)
            {
                if (terrain.GetComponent<Terrain>().player2Market)
                {
                    terrain.GetComponent<Terrain>().BothPlayerMarket();
                }
                else
                {
                    terrain.GetComponent<Terrain>().Player1Market();
                }
            }
            state = GameState.PLAYER2TURN;
            Player2Turn();
        }
    }

    void Player2Buy()
    {
        if (player2.moneyAmount >= selectedNode.price && !selectedNode.player1Owned && !selectedNode.player2Owned)
        {
            player2.moneyAmount -= selectedNode.price;
            selectedNode.player2Owned = true;
            player2.nodes.Add(selectedNode);
            Debug.Log("Player 2 has bought a node");
            foreach (GameObject terrain in selectedNode.nearbyTerrains)
            {
                if (terrain.GetComponent<Terrain>().player1Market)
                {
                    terrain.GetComponent<Terrain>().BothPlayerMarket();
                }
                else
                {
                    terrain.GetComponent<Terrain>().Player2Market();
                }
            }
            state = GameState.PLAYER1TURN;
            Player1Turn();
        }
    }

    void PassTurn()
    {
        if (state == GameState.PLAYER1TURN)
        {
            state = GameState.PLAYER2TURN;
            Player2Turn();
        }
        else if (state == GameState.PLAYER2TURN)
        {
            state = GameState.PLAYER1TURN;
            Player1Turn();
        }
    }

    void CheckRevenue()
    {
        if (player1.moneyAmount <= 0)
        {
            state = GameState.PLAYER2WIN;
            Player2Won();
        }
        if (player2.moneyAmount <= 0)
        {
            state = GameState.PLAYER1WIN;
            Player1Won();
        }
    }

    void GenerateRevenue(bool isPlayer1)
    {
        if (isPlayer1)
        {
            foreach (Node node in player1.nodes)
            {
                foreach (GameObject terrain in node.nearbyTerrains)
                {
                    if (!terrain.GetComponent<Terrain>().player1Market)
                    {
                        player1.moneyAmount += Mathf.CeilToInt(terrain.GetComponent<Terrain>().population) * 15;
                    }
                    else
                    {
                        player1.moneyAmount += Mathf.CeilToInt(terrain.GetComponent<Terrain>().population) / 2 * 15;
                    }
                }
            }
        }
        else
        {
            foreach (Node node in player2.nodes)
            {
                foreach (GameObject terrain in node.nearbyTerrains)
                {
                    if (!terrain.GetComponent<Terrain>().player1Market)
                    {
                        player2.moneyAmount += Mathf.CeilToInt(terrain.GetComponent<Terrain>().population) * 15;
                    }
                    else
                    {
                        player2.moneyAmount += Mathf.CeilToInt(terrain.GetComponent<Terrain>().population) / 2 * 15;
                    }
                }
            }
        }
    }

    void Update()
    {
        string nodeName = "";
        Node node = null;
        if (Input.GetMouseButtonDown(0) && (state == GameState.PLAYER1TURN || state == GameState.PLAYER2TURN))
        {
            Debug.Log("Mouse is down");

            RaycastHit hitInfo = new RaycastHit();
            bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
            if (hitInfo.transform.gameObject.GetComponent<Node>())
            {
                node = hitInfo.transform.gameObject.GetComponent<Node>();
            }
            node = hitInfo.transform.gameObject.GetComponent<Node>();
            if (node != null)
            {
                nodeName = node.name;
            }
            if (hit)
            {
                Debug.Log("Hit " + hitInfo.transform.gameObject.name);
                if (nodeName == "Node" && !node.Selected)
                {
                    //Highlight Node
                    Debug.Log("Selected");
                    node.Selected = true;
                    node.HighlightNode();
                    selectedNode = node;
                    foreach (Node n in nodes)
                    {
                        if (n != node)
                        {
                            n.UnhighlightNode();
                            n.Selected = false;
                        }
                    }
                }
                else if (nodeName == "Node" && node.Selected)
                {
                    Debug.Log("Unselected");
                    node.Selected = false;
                    node.UnhighlightNode();
                    selectedNode = null;

                }
                else
                {
                    Debug.Log("Not Node");
                    foreach (Node n in nodes)
                    {
                        n.UnhighlightNode();
                        n.Selected = false;
                    }
                    selectedNode = null;
                }
            }
            else
            {
                Debug.Log("No Hit");
                foreach (Node n in nodes)
                {
                    n.UnhighlightNode();
                    n.Selected = false;
                }
                selectedNode = null;
            }
            Debug.Log("Mouse is down");
        }

    }
}
