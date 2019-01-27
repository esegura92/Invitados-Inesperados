using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiaryEntry : MonoBehaviour
{
    public string id;
    [TextArea]
    public string text;
    public string header;
    public string footer;
    public void Collect(){
        PlayerPrefs.SetInt(id, 1);
        /*
                //AppManager.Instance.Inventory.Add(this);

                //gameObject.SetActive(false);
                AppManager.Instance.Inventory.Add(this);
                MainCanvas.Instance.diaryEntry.SetDiaryEntry(this);
                gameObject.SetActive(false);
        */
    }
}
