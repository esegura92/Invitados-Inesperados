using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(Rigidbody2D))]
public class TopDownMove : MonoBehaviour
{

    public float speed;
    [HideInInspector]
    public Vector2 destination;

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
    private void FixedUpdate()
    {
        //curSpeed = walkSpeed;
        //maxSpeed = curSpeed;

        // Move senteces
        Debug.Log("Moviendo " + destination);
        transform.position = Vector2.MoveTowards(transform.position, destination, speed * Time.deltaTime);
    }
}
