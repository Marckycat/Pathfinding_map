using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Pathfinder : MonoBehaviour
{
    Node startNode;
    Node goalNode;

    Graph mGraph;
    GraphView mGraphView;

    Queue<Node> frontierNodes;
    List<Node> exploredNodes;
    List<Node> pathNodes;

    public Color startColor = Color.green;
    public Color goalColor = Color.red;
    public Color frontierColor = Color.magenta;
    public Color exploredColor = Color.gray;
    public Color pathColor = Color.cyan;

    public bool isComplete;
    public int mIterations;
    public PlayerController player;

    public void Init(Graph graph, GraphView graphView, Node start, Node goal)
    {
        if (start == null || goal == null || graph == null || graphView == null)
        {
            Debug.Log("A component is missing in the pathfinder");
            return;
        }
        if (start.nodeType == NodeType.BLOCK || goal.nodeType == NodeType.BLOCK)
        {
            Debug.Log("Pathfinder: startNode or goalNode is a wall");
            return;
        }

        mGraph = graph;
        mGraphView = graphView;
        startNode = start;
        goalNode = goal;
        player.StopPlayer();

        ShowColor(graphView, startNode, goalNode);

        frontierNodes = new Queue<Node>();
        frontierNodes.Enqueue(startNode);
        exploredNodes = new List<Node>();
        pathNodes = new List<Node>();

        isComplete = false;
    }
    void ShowColors()
    {
        ShowColor(mGraphView, startNode, goalNode);
    }
    void ShowColor(GraphView graphView, Node start, Node goal)
    {
        if (graphView == null || start == null || goal == null)
        {

            Debug.Log("PathfinderShowcolors:Argument null");
            return;
        }
        if (frontierNodes != null)
        {
            graphView.ColorNodes(frontierNodes.ToList(), frontierColor);
        }
        if (exploredNodes != null)
        {
            graphView.ColorNodes(exploredNodes, exploredColor);
        }
        if (pathNodes != null && pathNodes.Count > 0)
        {
            graphView.ColorNodes(pathNodes, pathColor);
        }
        NodeView startNodeView = graphView.nodeViews[startNode.xIndex, startNode.yIndex];
        if (startNodeView != null)
        {
            startNodeView.SetColor(startColor);
        }
        NodeView goalNodeView = graphView.nodeViews[goalNode.xIndex, goalNode.yIndex];
        if (goalNodeView != null)
        {
            goalNodeView.SetColor(goalColor);
        }
    }
    public IEnumerator SearchRoutine(float timeStep = 0.1f)
    {
        yield return null;
        while (!isComplete)
        {
            if (frontierNodes.Count > 0)
            {
                Node currentNode = frontierNodes.Dequeue();
                if (!exploredNodes.Contains(currentNode))
                {
                    exploredNodes.Add(currentNode);
                }
                ExpandFrontier(currentNode);

                if (frontierNodes.Contains(goalNode))
                {
                    pathNodes = GetPathNodes(goalNode);
                    player.path = pathNodes;
                }

                ShowColors();
                if (mGraphView != null)
                {
                    mGraphView.ShowNodeArrows(frontierNodes.ToList());
                }
                yield return new WaitForSeconds(timeStep);
            }
            else
            {
                isComplete = true;
                player.FollowPath();
            }
        }
    }
    void ExpandFrontier(Node node)
    {
        if (node != null)
        {
            for (int i = 0; i < node.neighbors.Count; i++)
            {
                if (!exploredNodes.Contains(node.neighbors[i])
                    && !frontierNodes.Contains(node.neighbors[i]))
                {
                    node.neighbors[i].previous = node;
                    frontierNodes.Enqueue(node.neighbors[i]);
                }
            }
        }
    }
    List<Node> GetPathNodes(Node endNode)
    {
        List<Node> path = new List<Node>();
        if (endNode == null)
        {
            return path;
        }
        path.Add(endNode);
        Node currentNode = endNode.previous;
        while (currentNode != null)
        {
            path.Insert(0, currentNode);
            currentNode = currentNode.previous;
        }
        return path;
    }

}
