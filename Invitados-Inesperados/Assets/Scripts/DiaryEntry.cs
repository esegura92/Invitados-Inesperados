using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiaryEntry : MonoBehaviour
{
    public int id;
    [TextArea]
    public string text;
    public void Collect(){

        AppManager.Instance.Inventory.Add(this);
        gameObject.SetActive(false);
    }
}
