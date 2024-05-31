using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI recipeDelivered;
    private void Start()
    {
        gameObject.SetActive(false);
        GameManager.Instane.OnStateChange += GameManager_OnStateChange;
        Hide();
    }

    private void GameManager_OnStateChange(object sender, System.EventArgs e)
    {
        if (GameManager.Instane.IsGameOverActive())
        {
            recipeDelivered.text = DeliveryManager.Instance.GetSucessfulRecipeDelivered().ToString();
            // Debug.Log(GameManager.Instane.IsCountdownToStartActive());
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
