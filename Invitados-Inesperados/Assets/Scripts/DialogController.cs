using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogController : MonoBehaviour
{
    public UI_Dialog UI;
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
    }

    public void StarDialogSequence(Dialog dialog)
    {
        Debug.Log("entrando a dialog sequence " + dialog.gameObject.name);
        areDialogsActive = true;
        currentDialog = dialog;
        UI.SetText(dialog);
        //dialog ui show dialog: currentdialog.showText with time
    }
    public void FinishText()
    {
        UI.FillText();
    }

    public void NextDialog(Dialog nextDialog)
    {

        if (nextDialog != null)
        {
            Debug.Log("llendo al next dialog " + nextDialog.gameObject.name);
            StarDialogSequence(currentDialog.NextDialog);
        } // end if
        else
        {
            Debug.Log("Dialogo nulo, se acabo");
            currentDialog = null;
            UI.HidePanel();
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
        if (UI.InCoroutine)
            FinishText();
        else NextDialog(currentDialog.NextDialog);
    }
}
