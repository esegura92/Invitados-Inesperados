using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UI_DiaryEntry : MonoBehaviour
{
    [SerializeField]TextMeshProUGUI txtHeader;
    [SerializeField]TextMeshProUGUI txtBody;
    [SerializeField]TextMeshProUGUI txtFooter;
    [SerializeField] Button buttonPreviousPage;
    [SerializeField] Button buttonNextPage;
    [SerializeField] int maxWords = 100;

    DiaryEntry entry;

    private List<string> pagesText;
    private int currentPage = 0;
    private void InitializePages()
    {
        pagesText = new List<string>();
        currentPage = 0;
    }

    private void SetTextPages(string fullText)
    {
        string pageText = "";
        int wordCounter = 0;
        string[] words = fullText.Split(' ');
        for(int i = 0; i < words.Length; i++)
        {
            if (i >= maxWords && (i % maxWords == 0))
            {
                pagesText.Add(pageText);
                Debug.Log("Page Text: " + pagesText);
                pageText = "";
            }
            pageText += words[i] + " ";
        }
        /*
        for (int i = 0; i < fullText.Length; i++)
        {
            string[] words = fullText.Split(' ');
            if (fullText[i].Equals(' ') )
            {
                wordCounter++;
                if (wordCounter >= maxWords && (wordCounter % maxWords == 0))
                {
                    Debug.LogError("Entrando wordCounter " + wordCounter + " maxwords " + maxWords + " mod " + (wordCounter % maxWords));
                    pagesText.Add(pageText);
                    Debug.Log("Page Text: " + pagesText);
                    pageText = "";

                }
            }
             
            pageText += fullText[i];
            
        } // end for
    */
        Debug.Log("word counter " + wordCounter);
        if(pagesText.Count > 0)
        {
            buttonPreviousPage.gameObject.SetActive(true);
            buttonNextPage.gameObject.SetActive(true);
        } // end if
        else
        {
            pagesText.Add(pageText);
            pageText = "";
            Debug.Log("Page Text: " + pagesText);
            buttonPreviousPage.gameObject.SetActive(false);
            buttonNextPage.gameObject.SetActive(false);
        }
    }

    public void SetDiaryEntry(DiaryEntry entry)
    {
        if (entry == null)
            return;
        InitializePages();
        this.entry = entry;
        SetTextPages(entry.text);
        SetPageEntry();
        //txtHeader.text = entry.header;
        //txtBody.text = entry.text;
        //txtFooter.text = entry.footer;
        this.gameObject.SetActive(true);
    }

    private void SetPageEntry()
    {
        txtHeader.text = entry.header;
        txtBody.text = pagesText[currentPage];
        if (currentPage == pagesText.Count - 1)
        {
            txtFooter.text = entry.footer;
        }
        else
        {
            txtFooter.text = "";
        }
        
    }

    public void CurrentEntry()
    {
        MainCanvas.Instance.diaryEntry.SetDiaryEntry(AppManager.Instance.CurrentEntry());
    }

    public void NextPageButton()
    {
        currentPage++;
        if (currentPage > pagesText.Count - 1)
        {
            currentPage = 0;
        }

        SetPageEntry();
            
    }

    public void PreviousPageButton()
    {
        currentPage--;
        if (currentPage < 0)
        {
            currentPage = pagesText.Count - 1;
        }

        SetPageEntry();
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
