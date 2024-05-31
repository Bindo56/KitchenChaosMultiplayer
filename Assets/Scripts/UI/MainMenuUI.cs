using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] Button playBtb;
    [SerializeField] Button quitBtb;


    private void Awake()
    {
       
        playBtb.onClick.AddListener(() =>
        {
            Loader.Load(Loader.Scene.GameScene);
        });
            quitBtb.onClick.AddListener(() =>
        {
            Application.Quit();
        });
        Time.timeScale = 1;
    }

    
}
