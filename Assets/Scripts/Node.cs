using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    bool player1Owned = false;
    bool player2Owned = false;
    bool selected = false;
    
    public GameObject[] nearbyTerrains = new GameObject[6];

    public int price;
    public Material normalMat;
    public Material selectedMat;
    public Material player1Mat;
    public Material player2Mat;
    public Material bothPlayerMat;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Debug.Log("Mouse is down");

            RaycastHit hitInfo = new RaycastHit();
            bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
            if (hit)
            {
                Debug.Log("Hit " + hitInfo.transform.gameObject.name);
                if (hitInfo.transform.gameObject.name == "Node" && !selected)
                {
                    //Highlight Node
                    Debug.Log("Selected");
                    selected = true;
                    hitInfo.transform.gameObject.GetComponent<MeshRenderer>().material = selectedMat;
                }
                else if (hitInfo.transform.gameObject.name == "Node" && selected)
                {
                    Debug.Log("Unselected");
                    selected = false;
                    if (!player1Owned && !player2Owned)
                    {
                        hitInfo.transform.gameObject.GetComponent<MeshRenderer>().material = normalMat;
                    }
                    else if (player1Owned && !player2Owned)
                    {
                        hitInfo.transform.gameObject.GetComponent<MeshRenderer>().material = player1Mat;
                    }
                    else if (!player1Owned && player2Owned)
                    {
                        hitInfo.transform.gameObject.GetComponent<MeshRenderer>().material = player2Mat;
                    }
                    else
                    {
                        hitInfo.transform.gameObject.GetComponent<MeshRenderer>().material = bothPlayerMat;
                    }
                    if (this.gameObject.name == "Node")
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
                        else
                        {
                            this.gameObject.GetComponent<MeshRenderer>().material = bothPlayerMat;
                        }
                    }
                }
                else
                {
                    Debug.Log("Not Node");
                    selected = false;
                    if (this.gameObject.name == "Node")
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
                        else
                        {
                            this.gameObject.GetComponent<MeshRenderer>().material = bothPlayerMat;
                        }
                    }
                }
            }
            else
            {
                Debug.Log("No Hit");
                selected = false;
                if (this.gameObject.name == "Node")
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
                    else
                    {
                        this.gameObject.GetComponent<MeshRenderer>().material = bothPlayerMat;
                    }
                }
            }
            selected = false;
            Debug.Log("Mouse is down");
        }
    }
}
