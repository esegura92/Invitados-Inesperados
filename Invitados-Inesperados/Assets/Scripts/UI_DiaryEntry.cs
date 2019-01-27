using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UI_DiaryEntry : MonoBehaviour
{
    [SerializeField]TextMeshProUGUI txtHeader;
    [SerializeField]TextMeshProUGUI txtBody;
    [SerializeField]TextMeshProUGUI txtFooter;
    DiaryEntry entry;
    public void SetDiaryEntry(DiaryEntry entry){
        if (entry == null)
            return;
        this.entry = entry;
        txtHeader.text = entry.header;
        txtBody.text = entry.text;
        txtFooter.text = entry.footer;
        this.gameObject.SetActive(true);
    }

    public void CurrentEntry()
    {
        MainCanvas.Instance.diaryEntry.SetDiaryEntry(AppManager.Instance.CurrentEntry());
    }

    public void NextButton()
    {
        MainCanvas.Instance.diaryEntry.SetDiaryEntry(AppManager.Instance.NextEntry());
    }

    public void BackButton()
    {
        MainCanvas.Instance.diaryEntry.SetDiaryEntry(AppManager.Instance.BackEntry());
    }
}
