using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class UI_Dialog : MonoBehaviour
{
    [SerializeField]RebuildLayout mainTextLayout;
    [SerializeField]RebuildLayout optionsLayout;
    [SerializeField]RebuildLayout fullDialogueLayout;
    public GameObject UIDialog;
    public Button ActionButton;
    public Text text;
    public GameObject OptionsContainer;
    public Button[] Options;
    public float textSpeed;
    private Dialog dialogText;

    public void FillText()
    {
        Debug.Log("cancelando coroutina y poniendo el texto full");
        ActionButton.interactable = false;
        ActionButton.gameObject.SetActive(false);
        InCoroutine = false;
        
        StopAllCoroutines();
        text.text = dialogText.DialogText;
             mainTextLayout.ForceRebuild();
        if (dialogText.GetType() == typeof(DecisionDialog))
        {
            OptionsContainer.SetActive(true);
             mainTextLayout.ForceRebuild();
            fullDialogueLayout.ForceRebuild();
            ShowOptions(dialogText as DecisionDialog);
            optionsLayout.ForceRebuild();
        }
        else
        {
        ActionButton.gameObject.SetActive(true);
            Invoke("ButtonCooldown", 0.2f);
        }
    }

    public void SetText(Dialog _dialogText)
    {
        Debug.Log("Entrando a set text");
        ActionButton.interactable = false;
        ActionButton.gameObject.SetActive(true);
        UIDialog.SetActive(true);
        text.text = "";
        HideOptions();
        dialogText = _dialogText;
        Invoke("ButtonCooldown", 0.2f);
        if (dialogText.GetType() == typeof(DecisionDialog))
        {
            StartCoroutine(SetTextSequence(true));
        }
        else
        {
            StartCoroutine(SetTextSequence(false));
            
        }
        
    }

    public void ButtonCooldown()
    {
        ActionButton.interactable = true;
        ActionButton.gameObject.SetActive(true);
    }

    public bool InCoroutine = false;

    public IEnumerator SetTextSequence(bool decition)
    {
        Debug.Log("empezando coroutina" + dialogText.DialogText);
        ActionButton.gameObject.SetActive(false);
        InCoroutine = true;
        for(int i = 0; i < dialogText.DialogText.Length; i++)
        {
            text.text += dialogText.DialogText[i];
            yield return new WaitForSeconds(textSpeed);
        } // end for
        if(decition)
        {

            OptionsContainer.SetActive(true);
            mainTextLayout.ForceRebuild();
            fullDialogueLayout.ForceRebuild();
            ActionButton.interactable = false;
            ActionButton.gameObject.SetActive(false);
            ShowOptions(dialogText as DecisionDialog);
            optionsLayout.ForceRebuild();
        }
            
        InCoroutine = false;
        Debug.Log("Terminando coroutina" + text.text);
    }

    public void ShowOptions(DecisionDialog dialog)
    {
        //OptionsContainer.SetActive(true);
        for(int i = 0; i < dialog.DecisionDialogs.Length; i++)
        {
            Options[i].GetComponentInChildren<Text>().text = dialog.DecisionDialogs[i].shortText;
            Options[i].gameObject.SetActive(true);
        } // end for
    }

    public void HideOptions()
    {
        for (int i = 0; i < Options.Length; i++)
        {
            Options[i].gameObject.SetActive(false);
        } // end for
        OptionsContainer.SetActive(false);
    }

    public void HidePanel()
    {
        text.text = "";
        HideOptions();
        UIDialog.SetActive(false);
        ActionButton.gameObject.SetActive(false);
        ActionButton.interactable = true;
    }

    public void AddActionToButton(UnityAction action, bool enableButton){
        ActionButton.onClick.AddListener(action);
        ActionButton.gameObject.SetActive(enableButton);
    }

    public void RemoveActionToButton(UnityAction action,bool enableButton){
        ActionButton.onClick.RemoveListener(action);
        ActionButton.gameObject.SetActive(enableButton);
    }
}
