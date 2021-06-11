using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terrain : MonoBehaviour
{
    public Material normalMat;
    //public Material selectedMat;
    public Material player1Mat;
    public Material player2Mat;
    public Material bothPlayerMat;

    public bool player1Market = false;
    public bool player2Market = false;

    public float population;

    // Start is called before the first frame update
    void Start()
    {
        population = Mathf.CeilToInt(Random.Range(0f, 1.0f) * 100);
    }

    public void Player1Market()
    {
        player1Market = true;
        this.gameObject.GetComponent<MeshRenderer>().material = player1Mat;
    }

    public void Player2Market()
    {
        player2Market = true;
        this.gameObject.GetComponent<MeshRenderer>().material = player2Mat;
    }

    public void BothPlayerMarket()
    {
        this.gameObject.GetComponent<MeshRenderer>().material = bothPlayerMat;
    }
}
