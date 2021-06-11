using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public string playerName;
    public int moneyAmount;
    public int customerAmount;

    public List<Node> nodes;

    private void Start()
    {
        moneyAmount = 100;
        customerAmount = 0;
        nodes = new List<Node>();
    }
}
