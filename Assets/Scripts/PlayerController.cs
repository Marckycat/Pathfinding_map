using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    public List<Node> path;
    Vector3 targetPos;
    int pathCurrentIndex;

    public GraphView mGraphView;
    public NavMeshAgent agent;

    bool isMoving;
    Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
        isMoving = true;
        path = new List<Node>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) {
             anim.SetTrigger("walk");
         }
         if (Input.GetKeyDown(KeyCode.B)) {
             anim.SetTrigger("idle");
         }
        if (isMoving)
        {
            if (Vector3.Distance(transform.position, targetPos) < 0.05f)
            {
                if (pathCurrentIndex < path.Count - 1)
                {
                    pathCurrentIndex++;
                    targetPos = GetNodeViewPosition(path[pathCurrentIndex]);
                }
                else
                {
                    isMoving = false;
                    anim.SetTrigger("idle");
                }
            }

            Vector3 dir = targetPos - transform.position;
            Quaternion newRot = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Slerp(transform.rotation, newRot, Time.deltaTime * 3.0f);
            transform.Translate(Vector3.forward * 0.6f * Time.deltaTime);
        }

        if (Input.GetMouseButtonDown(1))
        {
            Ray movePosition = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(movePosition, out var hitInfo))
            {
                agent.SetDestination(hitInfo.point);
            }
        }

    }
    Vector3 GetNodeViewPosition(Node n)
    {
        Vector3 pos = Vector3.zero;
        if (n != null)
        {
            NodeView nview = mGraphView.nodeViews[n.xIndex, n.yIndex];
            pos = nview.gameObject.transform.position;
        }
        return pos;
    }
    public void FollowPath()
    {
        pathCurrentIndex = 0;
        transform.position = GetNodeViewPosition(path[pathCurrentIndex]);
        pathCurrentIndex++;
        targetPos = GetNodeViewPosition(path[pathCurrentIndex]);
        isMoving = true;
        anim.SetTrigger("walk");
    }
    public void StopPlayer()
    {
        if (isMoving)
        {
            anim.SetTrigger("idle");
            isMoving = false;
        }
    }
}
