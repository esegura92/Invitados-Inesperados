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
    public DiaryEntry[] Inventory;



    private void Awake()
    {
        // if the singleton hasn't been initialized yet
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }

        instance = this;
        //DontDestroyOnLoad(this.gameObject);
    }
}
