using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum Actor
{
    None,
    You,
    Couple,
    StoryTeller
}

public abstract class Dialog : MonoBehaviour
{

    public UnityEvent StartEvents;
    public UnityEvent ExecutionEvents;
    public UnityEvent EndEvents;
    public string shortText;
    public string DialogText;
    public Actor actor;
    protected Dialog nextDialog;

    public Dialog NextDialog
    {
        get { return nextDialog; }
    }
    

    public void OnDialogStart()
    {
        StartEvents.Invoke();
    }

    public void OnDialogExecute()
    {
        ExecutionEvents.Invoke();
    }

    public void OnDialogEnd()
    {
        EndEvents.Invoke();
    }
}
