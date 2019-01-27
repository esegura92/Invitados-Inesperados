using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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

    private void Update()
    {
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
        if (Input.GetMouseButtonDown(0) && Input.touchCount == 0)
        {
            if (!EventSystem.current.IsPointerOverGameObject(-1))
            {
                Debug.Log("entrando");
                Vector2 worldPos = this.GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);
                //player.destination = worldPos;
                player.move(worldPos);
            }
        }
#endif
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                if (!EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                {
                    Debug.Log("entrando");
                    Vector2 worldPos = this.GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);
                    player.move(worldPos);
                    //player.destination = worldPos;
                }
            }
        }
    }

}
