using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphView : MonoBehaviour
{
    public GameObject nodeViewPrefab;
    public NodeView[,] nodeViews;
    public Graph mGraph;

    public Color pathColor = Color.white;
    public Color wallColor = Color.black;

    public void Init(Graph graph)
    {
        if (graph == null)
        {
            Debug.LogWarning("GraphViwe: Init: graph null");
            return;
        }
        mGraph = graph;
        nodeViews = new NodeView[graph.Width, graph.Height];
        foreach (Node node in graph.nodes)
        {
            GameObject instance = Instantiate(nodeViewPrefab, Vector3.zero, Quaternion.identity);
            NodeView nodeView = instance.GetComponent<NodeView>();

            if (nodeView != null)
            {
                nodeView.Init(node);
                nodeViews[node.xIndex, node.yIndex] = nodeView;
                if (node.nodeType == NodeType.BLOCK)
                {
                    nodeView.SetColor(wallColor);
                }
                else
                {
                    nodeView.SetColor(pathColor);
                }
            }
        }
    }
    public void ColorNodes(List<Node> nodes, Color color)
    {
        foreach (Node node in nodes)
        {
            if (node != null)
            {
                NodeView nodeView = nodeViews[node.xIndex, node.yIndex];
                if (nodeView != null)
                {
                    nodeView.SetColor(color);
                }
            }
        }
    }
    public void ShowNodeArrows(Node node)
    {
        if (node != null)
        {
            NodeView nodeView = nodeViews[node.xIndex, node.yIndex];
            if (nodeView != null)
            {
                nodeView.ShowArrow();
            }
        }
    }
    public void ShowNodeArrows(List<Node> nodes)
    {
        foreach (Node node in nodes)
        {
            ShowNodeArrows(node);
        }
    }
    public void Reset()
    {
        if (mGraph == null)
        {
            return;
        }
        foreach (Node n in mGraph.nodes)
        {
            if (n != null)
            {
                NodeView nodeView = nodeViews[n.xIndex, n.yIndex];
                if (nodeView != null)
                {
                    nodeView.HideArrow();
                    if (n.nodeType == NodeType.BLOCK)
                    {
                        nodeView.SetColor(wallColor);
                    }
                    else
                    {
                        nodeView.SetColor(pathColor);
                    }
                }
            }
        }
    }
}
