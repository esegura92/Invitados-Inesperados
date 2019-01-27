using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiaryEntry : MonoBehaviour
{
    public int id;
    [TextArea]
    public string text;
    public string header;
    public string footer;
    public void Collect(){

        AppManager.Instance.Inventory.Add(this);
        MainCanvas.Instance.diaryEntry.SetDiaryEntry(this);
        gameObject.SetActive(false);
    }
}
