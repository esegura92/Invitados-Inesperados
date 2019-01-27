using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiaryEntry : MonoBehaviour
{
    public string id;
    [TextArea]
    public string text;
    public void Collect(){

        //AppManager.Instance.Inventory.Add(this);
        PlayerPrefs.SetInt(id, 1);
        //gameObject.SetActive(false);
    }
}
