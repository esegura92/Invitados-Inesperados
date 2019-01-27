using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(Rigidbody2D))]
public class TopDownMove : MonoBehaviour
{

    public float speed;
    [HideInInspector]
    public Vector2 destination;
    public Rigidbody2D rigid;
    public float offset;

    private void Awake()
    {
        //rigidBody2D = GetComponent<Rigidbody2D>();
        destination = transform.position;
    }

    /// <summary>
    /// Call on fixed Update
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public void move(Vector2 position)
    {
        //curSpeed = walkSpeed;
        //maxSpeed = curSpeed;
        destination = position;
        // Move senteces
        Debug.Log("Moviendo " + destination);
        Vector2 direction = position - (Vector2)transform.position;
        Vector2 normalizedDir = direction.normalized;
        rigid.velocity = normalizedDir * speed;
    }

    private void FixedUpdate()
    {
        float dist = Vector2.Distance(transform.position, destination);
        if(dist < offset )
        {
            rigid.velocity = Vector2.zero;
        }
    }
}
