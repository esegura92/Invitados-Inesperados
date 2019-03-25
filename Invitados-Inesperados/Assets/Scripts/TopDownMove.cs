using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(Rigidbody2D))]
public class TopDownMove : MonoBehaviour
{
    [SerializeField] bool originalFaceIsRight;
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
        if (DialogController.Instance.AreDialogsActive)
            return;
        //curSpeed = walkSpeed;
        //maxSpeed = curSpeed;
        destination = position;
        // Move senteces
        Debug.Log("Moviendo " + destination);
        Vector2 direction = position - (Vector2)transform.position;
        Vector2 normalizedDir = direction.normalized;
        rigid.velocity = normalizedDir * speed;
        SetCharacterDirection(direction.x>0);
    }

    void SetCharacterDirection(bool faceRight){
        float xScale = 0;
        if(originalFaceIsRight)
            if(faceRight)
            xScale = 1;
            else
            xScale = -1;
        else
            if(faceRight)
            xScale =-1;
            else
            xScale = 1;

        transform.transform.localScale = new Vector3(xScale,1,1);
    }
    private void FixedUpdate()
    {
        float dist = Vector2.Distance(transform.position, destination);
        if(dist < offset )
        {
            rigid.velocity = Vector2.zero;
        }
    }

    void OnCollisionEnter2D(Collision2D col){
        rigid.velocity = Vector2.zero;
    }
}
