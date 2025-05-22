using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NodeType { OPEN, BLOCK}
public class Node
{
    public NodeType nodeType = NodeType.OPEN;

    public int xIndex = -1;
    public int yIndex = -1;

    public Vector3 position;

    public List<Node> neighbors = new List<Node>();

    public Node previous = null;

    public Node(int x, int y, NodeType type)
    {
        xIndex = x;
        yIndex = y;
        nodeType = type;
    }

    public void Reset()
    {
        previous = null;
    }
}
