using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class TopDownMove : MonoBehaviour
{
    public float walkSpeed;
    private float curSpeed;
    private float maxSpeed;
    private Rigidbody2D rigidBody2D;

    public float speed;

    private void Awake()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// Call on fixed Update
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public void Move(float x, float y)
    {
        curSpeed = walkSpeed;
        maxSpeed = curSpeed;

        // Move senteces
        rigidBody2D.velocity = new Vector2(Mathf.Lerp(0, Input.GetAxis("Horizontal") * curSpeed, 0.8f),
                                             Mathf.Lerp(0, Input.GetAxis("Vertical") * curSpeed, 0.8f));
    }
}
