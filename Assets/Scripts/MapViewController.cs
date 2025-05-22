using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MapViewController : MonoBehaviour
{
    public MapData map;
    public Graph graph;
    public Pathfinder pathfinder;
    public GraphView graphView;
    public int startX = 0;
    public int startY = 0;
    public int goalX = 4;
    public int goalY = 5;
    public float timeStep = 0.5f;

    Coroutine pathfindingCoroutine;

    void Start()
    {
        if (map != null && graph != null)
        {
            int[,] mData = map.mData;
            graph.Init(mData);
            graphView = graph.gameObject.GetComponent<GraphView>();
            if (graphView != null)
            {
                graphView.Init(graph);
            }
            if (graph.IsInBounds(startX, startY) && graph.IsInBounds(goalX, goalY) && pathfinder != null)
            {
                Node startNode = graph.nodes[startX, startY];
                Node goalNode = graph.nodes[goalX, goalY];
                pathfinder.Init(graph, graphView, startNode, goalNode);
                pathfindingCoroutine = StartCoroutine(pathfinder.SearchRoutine(timeStep));
                //StartCoroutine(pathfinder.SearchRoutine(timeStep));
            }
        }
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                GameObject gameObj = hit.collider.gameObject;
                if (gameObj.CompareTag("NodeView"))
                {
                    NodeView nodeView = gameObj.GetComponent<NodeView>();
                    if (nodeView.mNode.nodeType != NodeType.BLOCK)
                    {
                        startX = nodeView.mNode.xIndex;
                        startY = nodeView.mNode.yIndex;

                        StopCoroutine(pathfindingCoroutine);
                        graphView.Reset();
                        StartPathfinder();
                    }
                }
            }
        }
    }
    void StartPathfinder()
    {
        if (graph.IsInBounds(startX, startY) && graph.IsInBounds(goalX, goalY) && pathfinder != null)
        {
            Node startNode = graph.nodes[startX, startY];
            Node goalNode = graph.nodes[goalX, goalY];
            pathfinder.Init(graph, graphView, startNode, goalNode);
            pathfindingCoroutine = StartCoroutine(pathfinder.SearchRoutine(timeStep));
        }
    }

}
