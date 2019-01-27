using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsequenceDialog : Dialog
{
    public Dialog[] concequencesDialogs;

    public void SetConsequence()
    {
        nextDialog = concequencesDialogs[AppManager.Instance.miedometro];
    }
}
