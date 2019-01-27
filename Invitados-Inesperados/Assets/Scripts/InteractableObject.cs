using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Collider2D))]
public class InteractableObject : MonoBehaviour
{
    public Button UIButton;
    public DiaryEntry Entry;
    public Dialog InteractionDialog;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //add listener
        ShowButton();
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
        HideButton();
        // hide ui interaction
    }
    public void ShowButton()
    {
        UIButton.onClick.AddListener(Action);
        UIButton.gameObject.SetActive(true);
    }

    public void HideButton()
    {
        UIButton.onClick.RemoveListener(Action);
        UIButton.gameObject.SetActive(false);
    }

    public void Action()
    {
        HideButton();
        if(Entry != null)
        {
            AppManager.Instance.Inventory.Add(Entry);
            Entry = null;
        }
        DialogController.Instance.StarDialogSequence(InteractionDialog);
    }
}
