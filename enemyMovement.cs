using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.Collections.AllocatorManager;
using static UnityEngine.GraphicsBuffer;

public class enemyMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Queue<Vector3> pointsToMove = new Queue<Vector3>(10);
    //private Vector3 pointToMoveTo;
    [SerializeField] private bool waiting;
    [SerializeField] private int queueNum;

    private void Update()
    {
        if (pointsToMove.Count > 0 && !waiting)
        {
            bool blocked = false;
            blocked = checkNearby();
            if (!blocked)
            {
                var step = speed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, pointsToMove.Peek(), step);
            }
            if (Vector3.Distance(transform.position, pointsToMove.Peek()) < 0.001f)
            {
                pointsToMove.Dequeue();
            }
        }
    }
 
    public void setTargetPos(Transform targetPos)
    {
        pointsToMove.Enqueue(targetPos.position);
    }
    private bool checkNearby()
    {
        RaycastHit2D hitUp = Physics2D.Raycast(transform.position, transform.up, 1f);
        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, -transform.right, 0.73f);

        if (hitUp.collider != null)
        {
            if (hitUp.collider.CompareTag("Consumer"))
            {
                if (!hitUp.collider.GetComponent<Consumer>().isSelling())
                {
                    return true;
                }
            }
        }
        if (hitLeft.collider != null)
        {
            if (hitLeft.collider.CompareTag("Consumer"))
            {
                if (!hitLeft.collider.GetComponent<Consumer>().isSelling())
                {
                    return true;
                }
            }
        }
        return false;
    }
    public void joinqueue(int j,Transform queuePoint, Transform[] leavePoints)
    {
        pointsToMove.Enqueue(queuePoint.position);
        for (int i = 0; i < leavePoints.Length; i++)
        {
            pointsToMove.Enqueue(leavePoints[i].position);
        }
        queueNum = j;
        waiting = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    { 
        if (collision.CompareTag("Selling"))
        {
            waiting = true;
            GetComponent<Consumer>().startSelling();
        }
    }
    public void stopWaiting()
    {
        waiting = false;
    }
    public int getQueuePlace()
    {
        return queueNum;
    }
}
