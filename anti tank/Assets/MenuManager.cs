using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    private static MenuManager _instance;
    public static MenuManager Instance
    {
        get
        {
            if (_instance is null)
            {
                Debug.LogError("MenuManager is NULL");
            }
            return _instance;
        }
    }
    private void Awake()
    {
        _instance = this;
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }
}
