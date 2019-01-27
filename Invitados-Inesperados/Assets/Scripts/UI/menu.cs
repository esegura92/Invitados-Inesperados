using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menu : MonoBehaviour
{
    private void Awake()
    {
        PlayerPrefs.DeleteAll();
    }
    public void play()
    {
        SceneManager.LoadScene(1);
    }
}
