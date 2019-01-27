using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDialogs : MonoBehaviour
{
    public Dialog firstDialog;
    public DialogController controller;
    public TopDownMove player;
    // Start is called before the first frame update
    void Start()
    {
        //controller.StarDialogSequence(firstDialog);
    }

    private void FixedUpdate()
    {
        if(Input.GetMouseButtonUp(0))
        {

            Vector2 worldPos = this.GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);
            player.destination = worldPos;
        }
    }

}
