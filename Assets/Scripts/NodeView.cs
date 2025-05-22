using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeView : MonoBehaviour
{
    public GameObject tile;
    public GameObject arrow;
    public Node mNode;

    public void Init(Node node)
    {
        if (tile != null)
        {
            gameObject.name = "Node[" + node.xIndex + "," + node.yIndex + "]";
            gameObject.transform.position = node.position;
            mNode = node;
            EnableObject(arrow, false);
        }
    }
    void SetColor(Color color, GameObject ob)
    {
        Renderer mRenderer = ob.GetComponent<Renderer>();
        if (mRenderer != null)
        {
            mRenderer.material.color = color;
        }
    }
    public void SetColor(Color c)
    {
        SetColor(c, tile);
    }
    void EnableObject(GameObject ob, bool enable)
    {
        if (ob != null)
        {
            ob.SetActive(enable);
        }
    }
    public void ShowArrow()
    {
        if (mNode != null && arrow != null && mNode.previous != null)
        {
            EnableObject(arrow, true);
            Vector3 dir = (mNode.previous.position - mNode.position).normalized;
            arrow.transform.rotation = Quaternion.LookRotation(dir);
        }
    }
    public void HideArrow()
    {
        if (mNode != null && arrow != null && mNode.previous != null)
        {
            EnableObject(arrow, false);
        }
    }
}
