using System;
using UnityEngine;

public class Move : MonoBehaviour
{
    public Transform goal;
    public float speed = 1f;
    public float accuracy = 0.01f;

    private void LateUpdate()
    {
        transform.LookAt(goal.position);
        
        Vector3 direction = goal.transform.position - transform.position;
        Debug.DrawRay(transform.position, direction, Color.red);
        if (direction.magnitude > accuracy)
        {
            transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);
        }
    }
}
