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
        this.entry = entry;
        txtHeader.text = entry.header;
        txtBody.text = entry.text;
        txtFooter.text = entry.footer;
    }
}
