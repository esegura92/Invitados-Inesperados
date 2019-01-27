using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppManager : MonoBehaviour
{
    private static AppManager instance;

    public static AppManager Instance
    {
        get { return instance; }
    }
    [HideInInspector]
    public int miedometro
    {
        set
        {
            PlayerPrefs.SetInt("miedometro", value);
        }

        get
        {
            return PlayerPrefs.GetInt("miedometro", 0);
        }
    }
    public DiaryEntry[] AllEntries;

    public List<DiaryEntry> unlockedEntries;

    public DiaryEntry CurrentEntry()
    {
        if(unlockedEntries.Count > currentEntryIndex)
        {
            return unlockedEntries[currentEntryIndex];
        }
        else
        {
            return null;
        }
    }

    public int currentEntryIndex = 0;

    public DiaryEntry BackEntry()
    {
        currentEntryIndex--;
        if (currentEntryIndex < 0)
            currentEntryIndex = unlockedEntries.Count - 1;
        return CurrentEntry();
    }

    public DiaryEntry NextEntry()
    {
        currentEntryIndex++;
        if (currentEntryIndex >= unlockedEntries.Count)
            currentEntryIndex = 0;

        return CurrentEntry();
    }

    private void Awake()
    {
        // if the singleton hasn't been initialized yet
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }

        instance = this;
        unlockedEntries = new List<DiaryEntry>();
        AppManager.Instance.AddEntries();
        //DontDestroyOnLoad(this.gameObject);
    }

    public void AddEntries()
    {
        unlockedEntries.Clear();
        if(PlayerPrefs.GetInt("Entry1", 0) == 1)
        {
            unlockedEntries.Add(AllEntries[0]);
        }
        if (PlayerPrefs.GetInt("Entry2", 0) == 1)
        {
            unlockedEntries.Add(AllEntries[1]);
        }
        if (PlayerPrefs.GetInt("Entry3", 0) == 1)
        {
            unlockedEntries.Add(AllEntries[2]);
        }
        if (PlayerPrefs.GetInt("Entry4", 0) == 1)
        {
            unlockedEntries.Add(AllEntries[3]);
        }
        if (PlayerPrefs.GetInt("Entry5", 0) == 1)
        {
            unlockedEntries.Add(AllEntries[4]);
        }
        

    }
}
