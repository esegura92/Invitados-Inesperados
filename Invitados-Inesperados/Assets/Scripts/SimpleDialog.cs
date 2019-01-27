using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleDialog : Dialog
{
    public Dialog NodeDialog;

    private void Awake ()
    {
        nextDialog = NodeDialog;
    }

}
