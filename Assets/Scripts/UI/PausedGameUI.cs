using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PausedGameUI : MonoBehaviour
{

    [SerializeField] Button resumeBtn;
    [SerializeField] Button optionsBtn;
    [SerializeField] Button mainMenuBtn;
    private void Start()
    {
        resumeBtn.onClick.AddListener(() =>
        {
            GameManager.Instane.TogglePauseGame();
        });

        mainMenuBtn.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(0);
        }); 
        
        optionsBtn.onClick.AddListener(() =>
        {
            Hide();
            OptionsUI.instance.Show(Show);
        });

        GameManager.Instane.OnGamePaused += GameManager_OnGamePaused;
        GameManager.Instane.OnGameUnPaused += GameManager_OnGameUnPaused;
        Hide();
    }

    private void GameManager_OnGameUnPaused(object sender, System.EventArgs e)
    {
        Hide();
    }

    private void GameManager_OnGamePaused(object sender, System.EventArgs e)
    {
        Show();
    }

    private void Show()
    {
        gameObject.SetActive(true);
        resumeBtn.Select();
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
