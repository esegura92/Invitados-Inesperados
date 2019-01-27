using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogController : MonoBehaviour
{
    public MainCanvas mainCanvas;
    private bool areDialogsActive;
    public bool AreDialogsActive
    {
        get { return areDialogsActive; }
    }
    private static DialogController instance;
    public static DialogController Instance
    {
        get { return instance; }
    }

    private Dialog currentDialog;


    private void Awake()
    {
        // if the singleton hasn't been initialized yet
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }

        instance = this;
        areDialogsActive = false;
        mainCanvas.dialogueControllerReference = this;
    }

    public void StarDialogSequence(Dialog dialog)
    {
        Debug.Log("entrando a dialog sequence " + dialog.gameObject.name);
        dialog.OnDialogStart();
        if(dialog.consequence)
        {
            AppManager.Instance.miedometro++;
        }
        if (dialog.GetType() == typeof(ConsequenceDialog))
        {
            ((ConsequenceDialog)dialog).SetConsequence();
        }
        areDialogsActive = true;
        currentDialog = dialog;
        mainCanvas.Dialogue.gameObject.SetActive(true);
        mainCanvas.Dialogue.SetText(dialog);
        //dialog ui show dialog: currentdialog.showText with time
    }
    public void FinishText()
    {
        mainCanvas.Dialogue.FillText();
    }

    public void NextDialog(Dialog nextDialog)
    {
        currentDialog.OnDialogEnd();
        if (nextDialog != null)
        {
            Debug.Log("llendo al next dialog " + nextDialog.gameObject.name);
            StarDialogSequence(currentDialog.NextDialog);
        } // end if
        else
        {
            Debug.Log("Dialogo nulo, se acabo");
            currentDialog = null;
            mainCanvas.Dialogue.HidePanel();
            areDialogsActive = false;
        } // end else
    }

    public void ChooseOption(int i)
    {
        ((DecisionDialog)currentDialog).ChooseDecision(i);
        NextDialog(currentDialog.NextDialog);
    }

    public void ActionListener()
    {
        Debug.Log("puchando boton");
        if (mainCanvas.Dialogue.InCoroutine)
            FinishText();
        else NextDialog(currentDialog.NextDialog);
    }
}
