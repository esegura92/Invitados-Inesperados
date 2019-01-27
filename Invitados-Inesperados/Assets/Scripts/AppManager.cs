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
    public int miedometro;
    public List<DiaryEntry> Inventory;



    private void Awake()
    {
        // if the singleton hasn't been initialized yet
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }

        instance = this;
        //DontDestroyOnLoad(this.gameObject);
        miedometro = 0;
    }
}
