using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class Node : MonoBehaviour {

    private Transform target;

    public string nodeTag = "Node";

    public Color selectedNodeColor;
    public Color hoverColor;
    public Color startColor;

    public Vector3 NodeRight = new Vector3(4.5f, 0f,0f);
    public Vector3 NodeLeft  = new Vector3(-4.5f, 0f, 0f);
    public Vector3 NodeFront = new Vector3(0f, 0f, 4.5f);
    public Vector3 NodeBack  = new Vector3(0f, 0f, -4.5f);
    public static float range = 13f;
    private Vector3 rangeVector= new Vector3(range, range, range);
    public int wayLenght = 0;
    private int[] way;
    private bool GameStart;
    private bool makeWay;

    public GameObject[] node;

    private Renderer rend;

    public GameObject start;
    public GameObject end;

    public GameObject[] nodeArray;
    public Vector3[] nodeArrayCoord;
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, rangeVector);
    }

    void Start ()
        {
            makeWay = false;
            GameStart = true;
            rend = GetComponent<Renderer>();
            startColor = rend.material.color;
            rend.material.color = startColor;

            nodeArray[0] = start;
            nodeArrayCoord[0] = start.transform.position;
            createTheWay(start);    
        /*InvokeRepeating("UpdateNode", 0f,0.5f);
            nodeStart.tag = "Node";*/
	    }
	
	
	void UpdateNode()
        {
        /*GameObject[] nodes = GameObject.FindGameObjectsWithTag(nodeTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestNode = null;
        foreach (GameObject node in nodes)
            {
                float distanceToChild = Vector3.Distance(transform.position, node.transform.position);
                if (distanceToChild < shortestDistance)
                    {
                        shortestDistance = distanceToChild;
                        nearestNode = node;
                    }
            }
        if (nearestNode != null && shortestDistance <= range)
            {
                target = nearestNode.transform;
            }
                else    {
                            target = null;
                        }*/
        
        }

    void Update()
        {

        
        }

    bool createTheWay(GameObject node)
    {
        
        nodeArray = GameObject.FindGameObjectsWithTag("Node");
        nodeArrayCoord = new Vector3[nodeArray.Length];

        for (int i = 1; i < nodeArray.Length-1; i++) {
            if (nodeArray[i].transform.position + NodeRight == nodeArrayCoord[i])
            {
                nodeArrayCoord[i] = nodeArray[i].transform.position;
                        return makeWay = true;
            }

            if (transform.position + NodeLeft == node.transform.position)
            {
                return makeWay = true;
            }

            if (transform.position + NodeFront == node.transform.position)
            {
                return makeWay = true;
            }

            if (transform.position + NodeBack == node.transform.position)
            {
                return makeWay = true;
            }
            
        }
        return false;
    }

    void OnMouseDown()
    {
        /*bool moveToChild=false;
        GameObject[] nodes = GameObject.FindGameObjectsWithTag(nodeTag);
        foreach (GameObject node in nodes)
        {
            moveToChild=createTheWay(node);*/
            if ((GameStart == true)&&(makeWay))
            {
                if (EventSystem.current.IsPointerOverGameObject())
                    return;
                rend.material.color = selectedNodeColor;

                wayLenght++;
            }
            

        //}

        
    }

    void OnMouseEnter()
        {
            if((GameStart== true)&&(rend.material.color==selectedNodeColor))
                {
                if (EventSystem.current.IsPointerOverGameObject())
                    return;
                rend.material.color = hoverColor;
            }
        }

    void OnMouseExit()
    {
        if ((GameStart == true)&&(rend.material.color!=selectedNodeColor))
        {
            rend.material.color = startColor;
        }
    }
    //PARA EL BETO DEL FUUTURO:
    //USA PILASSSSSS PARA HACER EL CAMINOOO, PORQUE SI QUIERES QUITAR UN NODO TENDRAS QUE QUITAR EL ULTIMO QUE PUSISTEEEE
}