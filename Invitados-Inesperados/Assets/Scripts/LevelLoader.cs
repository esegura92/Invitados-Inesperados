﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(BoxCollider2D))]
public class LevelLoader : MonoBehaviour
{
    public int levelIndex;
    [SerializeField] bool interactImmediately;
    [SerializeField] bool disableInteractionAfterCollision;
    bool interactionStarted;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //add listener
        if (!interactionStarted || !disableInteractionAfterCollision)
            ShowButton();

        interactionStarted = true;
        //show ui interaction if player havent interact
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //if pressed show dialog
        //if have some inventory stuff apply it to the player
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        //delete listener
        if (!disableInteractionAfterCollision)
            HideButton();
        // hide ui interaction
    }
    public void ShowButton()
    {
        if (interactImmediately)
        {
            Action();
        }
        else
        {
            bool enableButton = true;
            MainCanvas.Instance.AddListenerToInteractButton(Action);
        }
    }

    public void HideButton()
    {
        bool enableButton = false;
        MainCanvas.Instance.RemoveLintenerToInteractButton(Action);
    }

    public void Action()
    {
        HideButton();
        SceneManager.LoadScene(levelIndex);
    }
}
