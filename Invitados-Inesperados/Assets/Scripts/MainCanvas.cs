using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCanvas : MonoBehaviour
{
    [HideInInspector]
    public DialogController dialogueControllerReference;
    private static MainCanvas instance;
    public static MainCanvas Instance{
        get{
            return instance;
        }
    }
    [SerializeField]UI_Dialog dialogue;
    public UI_Dialog Dialogue{
        get{
            return dialogue;
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        if(instance)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        dialogue.gameObject.SetActive(false);
    }

    public void DialogueControllerNextDialogue(){
        dialogueControllerReference.ActionListener();
    }
}
