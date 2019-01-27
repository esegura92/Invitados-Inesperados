using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecisionDialog : Dialog
{
    public Dialog[] DecisionDialogs;

    public void ChooseDecision(int decision)
    {
        nextDialog = DecisionDialogs[decision];
    }

}
