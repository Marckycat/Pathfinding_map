using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour
{
    public Node[,] nodes;
    public List<Node> walls = new List<Node>();

    int[,] mapData;
    int width;
    int height;

    public int Width { get { return width; } }
    public int Height { get { return height; } }

    public static readonly Vector2[] directions = {
        new Vector2(0f,1f),
        new Vector2(1f,1f),
        new Vector2(1f,0f),
        new Vector2(1f,-1f),
        new Vector2(0f,-1f),
        new Vector2(-1f,-1f),
        new Vector2(-1f,0f),
        new Vector2(-1f,1f),
    };
    public void Init(int[,] map)
    {
        mapData = map;
        width = mapData.GetLength(0);
        height = mapData.GetLength(1);
        nodes = new Node[width, height];

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                NodeType type = (NodeType)mapData[x, y];
                Node newNode = new Node(x, y, type);
                nodes[x, y] = newNode;
                newNode.position = new Vector3(x, 0, y);
                if (type == NodeType.BLOCK)
                {
                    walls.Add(newNode);
                }
            }
        }
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (nodes[x, y].nodeType != NodeType.BLOCK)
                {
                    nodes[x, y].neighbors = GetNeighbours(x, y);
                }
            }
        }

    }
    public bool IsInBounds(int x, int y)
    {
        return (x >= 0 && x < width && y >= 0 && y < height);
    }
    List<Node> GetNeighbours(int x, int y, Node[,] nodeArray, Vector2[] dirs)
    {
        List<Node> neighbours = new List<Node>();
        foreach (Vector3 direction in dirs)
        {
            int newX = x + (int)direction.x;
            int newY = y + (int)direction.y;
            if (IsInBounds(newX, newY) && nodeArray[newX, newY] != null && nodeArray[newX, newY].nodeType != NodeType.BLOCK)
            {
                neighbours.Add(nodeArray[newX, newY]);
            }
        }
        return neighbours;
    }
    List<Node> GetNeighbours(int x, int y)
    {
        return GetNeighbours(x, y, nodes, directions);
    }
}
