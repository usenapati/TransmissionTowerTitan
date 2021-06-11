using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public bool player1Owned = false;
    public bool player2Owned = false;
    private bool selected = false;

    public GameObject[] nearbyTerrains = new GameObject[6];

    public int price;
    public Material normalMat;
    public Material selectedMat;
    public Material player1Mat;
    public Material player2Mat;

    public bool Selected { get => selected; set => selected = value; }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HighlightNode()
    {
        this.gameObject.GetComponent<MeshRenderer>().material = selectedMat;
    }

    public void UnhighlightNode()
    {
        if (!player1Owned && !player2Owned)
        {
            this.gameObject.GetComponent<MeshRenderer>().material = normalMat;
        }
        else if (player1Owned && !player2Owned)
        {
            this.gameObject.GetComponent<MeshRenderer>().material = player1Mat;
        }
        else if (!player1Owned && player2Owned)
        {
            this.gameObject.GetComponent<MeshRenderer>().material = player2Mat;
        }
    }

    public void Player1Bought()
    {
        this.gameObject.GetComponent<MeshRenderer>().material = player1Mat;
    }

    public void Player2Bought()
    {
        this.gameObject.GetComponent<MeshRenderer>().material = player2Mat;
    }
}
