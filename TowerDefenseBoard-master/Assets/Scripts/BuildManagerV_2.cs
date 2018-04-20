using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class BuildManagerV_2 : MonoBehaviour
{
    [SerializeField]
    private Stack<GameObject> stack;

    public int maxX, maxY, minX, minY;
    private Vector3 upOffset    = new Vector3(0, 0, 4);
    private Vector3 downOffset  = new Vector3(0, 0, -4);
    private Vector3 rightOffset = new Vector3(4, 0, 0);
    private Vector3 leftOffset  = new Vector3(-4, 0, 0);

    private bool upKeyPressed, downKeyPressed, rightKeyPressed, leftKeyPressed, dontMoveTop, dontMoveBot, dontMoveRight, dontMoveLeft;


    public Color nodesColor;
    public Color startpointColor;
    public Color endpointColor;
    public Color canBuildThereColor;
    public Color selectedNodeColor;

    private Vector3 pointerVector;
    private Vector3 startpoint;
    private Vector3 endpoint;

    public GameObject[] nodesCanBuildThere;
    public GameObject[] nodes;
    private GameObject pointer;

    void Start()
    {
        nodes = GameObject.FindGameObjectsWithTag("Node");
        BuildStartSettings();
        BuildMapStartSettings();
        StartCoroutine(BuildWay());

        //INPUTS
    }

    void BuildStartSettings()
    {
        dontMoveTop     =   false;
        dontMoveBot     =   false;
        dontMoveRight   =   false;
        dontMoveLeft    =   false;
        stack           =   new Stack<GameObject>();
        upKeyPressed    =   false;
        downKeyPressed  =   false;
        rightKeyPressed =   false;
        leftKeyPressed  =   false;
        startpoint      =   new Vector3(2, 1.5f, 2);
        endpoint        =   new Vector3(62, 1.5f, 38);
        pointerVector   =   startpoint;
    }

    void BuildMapStartSettings()
    {
        foreach (GameObject node in nodes)
        {

            if (node.transform.position == startpoint)
            {
                node.GetComponent<Renderer>().material.color = startpointColor;
                pointer = node;
                pointer.name = "Way";
                stack.Push(pointer);
            }
            if (node.transform.position == endpoint)
            {
                node.GetComponent<Renderer>().material.color = endpointColor;
            }
        }
    }

    IEnumerator BuildWay()
    {
        Debug.Log("Empezando camino.");
        yield return new WaitForSeconds(1f);//Esperamos un segundo para marcar el siguiente nodo.
        Debug.Log("Avanzaste?");
        PaintNodesWhereCanBuild();
        StartCoroutine(WaitForKeyDown(KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.RightArrow, KeyCode.LeftArrow, KeyCode.Space)); //Esperamos hasta que el usuario presione alguna flecha.
        Debug.Log("Voy a mover el puntero.");//Pintamos los nodos donde se puede construir.

    }

    IEnumerator WaitForKeyDown(KeyCode upArrow, KeyCode downArrow, KeyCode rightArrow, KeyCode leftArrow, KeyCode space)
    {
        Debug.Log("Vamos a ver que esta pasando aqui.");
        bool done = false;
        while (!done) // essentially a "while true", but with a bool to break out naturally
        {
            foreach (GameObject node in nodesCanBuildThere)
            {
                Debug.Log("Esperando Input");
                if (Input.GetKeyDown(space))
                {
                    Debug.Log("Espacio Presionado.");
                    if (pointerVector != startpoint)
                    {
                        pointer = stack.Pop();
                        pointerVector = pointer.transform.position;
                        StartCoroutine(BuildWay());
                        done = true;
                        
                    }
                    else
                    {
                        Debug.Log("No hay nodos anteriores a este.");
                        // breaks the loop
                    }
                }
                else
                if (Input.GetKeyDown(upArrow))
                {
                    Debug.Log("Flecha Arriba Presionado.");
                    if (pointerVector.z<maxY)
                    {
                        node.GetComponent<Renderer>().material.color = selectedNodeColor;
                        node.tag = "Way";
                        pointer = node;
                        stack.Push(pointer);
                        pointerVector = pointer.transform.position;
                        done = true; // breaks the loop
                        Debug.Log("Nueva Posicion de Puntero: " + pointerVector);
                        StartCoroutine(BuildWay());
                    }
                    else
                    {
                        node.tag = "Node";
                        node.GetComponent<Renderer>().material.color = nodesColor;
                    }
                }
                else
                if (Input.GetKeyDown(downArrow))
                {
                    Debug.Log("Flecha Abajo Presionado.");
                    if (node.transform.position.z < pointerVector.z)
                    {
                        node.GetComponent<Renderer>().material.color = selectedNodeColor;
                        node.tag = "Way";
                        pointer = node;
                        stack.Push(pointer);
                        pointerVector = pointer.transform.position;
                        done = true; // breaks the loop
                        Debug.Log("Nueva Posicion de Puntero: " + pointerVector);
                        StartCoroutine(BuildWay());
                    }
                    else
                    {
                        node.tag = "Node";
                        node.GetComponent<Renderer>().material.color = nodesColor;
                    }
                }
                else
                if (Input.GetKeyDown(rightArrow))
                {
                    Debug.Log("Flecha Derecha Presionado.");
                    if (node.transform.position.x > pointerVector.x)
                    {
                        node.GetComponent<Renderer>().material.color = selectedNodeColor;
                        node.tag = "Way";
                        pointer = node;
                        stack.Push(pointer);
                        pointerVector = pointer.transform.position;
                        done = true; // breaks the loop
                        Debug.Log("Nueva Posicion de Puntero: " + pointerVector);
                        StartCoroutine(BuildWay());
                        
                    }
                    else
                    {
                        node.tag = "Node";
                        node.GetComponent<Renderer>().material.color = nodesColor;
                    }
                }
                else
                if (Input.GetKeyDown(leftArrow))
                {
                    Debug.Log("Flecha Left Presionado.");
                    if (node.transform.position.x < pointerVector.x)
                    {
                        node.GetComponent<Renderer>().material.color = selectedNodeColor;
                        node.tag = "Way";
                        pointer = node;
                        stack.Push(pointer);
                        pointerVector = pointer.transform.position;
                        //NotMove();
                        Debug.Log("Nueva Posicion de Puntero: " + pointerVector);
                        done = true; // breaks the loop
                        StartCoroutine(BuildWay());

                    }
                    else
                    {
                        node.tag = "Node";
                        node.GetComponent<Renderer>().material.color = nodesColor;
                    }
                }
                yield return null; // wait until next frame, then continue execution from here (loop continues)
            }
        }
    }
    void PaintNodesWhereCanBuild()
    {
        foreach (GameObject node in nodes)
        {
            if ((node.GetComponent<Renderer>().material.color != startpointColor) && node.GetComponent<Renderer>().material.color != endpointColor)
            {
                if (node.tag != "Way")
                {
                    node.GetComponent<Renderer>().material.color = nodesColor;
                }
            }
            if ((pointerVector + upOffset) == node.transform.position && (pointerVector.z + upOffset.z) <= maxY) //REPARAR QUE CUANDO SE SALGA DE LOS LÍMITES VUELVA A REPETIR EL CICLO HACIA DONDE QUIERE AVANZAR
            {
                    if ((node.GetComponent<Renderer>().material.color != startpointColor) && node.GetComponent<Renderer>().material.color != endpointColor)
                    {
                        if (node.tag != "Way")
                        {
                            node.GetComponent<Renderer>().material.color = canBuildThereColor;
                            node.tag = "CanBuildThere";
                        }
                    }
            }
            if ((pointerVector + downOffset) == node.transform.position && (pointerVector.z + downOffset.z) >= minY) //REPARAR QUE CUANDO SE SALGA DE LOS LÍMITES VUELVA A REPETIR EL CICLO HACIA DONDE QUIERE AVANZAR
            {
                if ((node.GetComponent<Renderer>().material.color != startpointColor) && node.GetComponent<Renderer>().material.color != endpointColor)
                {
                    if (node.tag != "Way")
                    {
                        node.GetComponent<Renderer>().material.color = canBuildThereColor;
                        node.tag = "CanBuildThere";
                    }
                }
            }
            if ((pointerVector + rightOffset) == node.transform.position && (pointerVector.z + rightOffset.z) <= maxX)//REPARAR QUE CUANDO SE SALGA DE LOS LÍMITES VUELVA A REPETIR EL CICLO HACIA DONDE QUIERE AVANZAR
            {
                if ((node.GetComponent<Renderer>().material.color != startpointColor) && node.GetComponent<Renderer>().material.color != endpointColor)
                {
                    if (node.tag != "Way")
                    {
                        node.GetComponent<Renderer>().material.color = canBuildThereColor;
                        node.tag = "CanBuildThere";
                    }
                }
                
            }
            if ((pointerVector + leftOffset) == node.transform.position && (pointerVector.z + leftOffset.z) >= minY) //REPARAR QUE CUANDO SE SALGA DE LOS LÍMITES VUELVA A REPETIR EL CICLO HACIA DONDE QUIERE AVANZAR
            {
                if ((node.GetComponent<Renderer>().material.color != startpointColor) && node.GetComponent<Renderer>().material.color != endpointColor)
                {
                    if (node.tag != "Way")
                    {
                        node.GetComponent<Renderer>().material.color = canBuildThereColor;
                        node.tag = "CanBuildThere";
                    }
                }
            }
        }
    }
}