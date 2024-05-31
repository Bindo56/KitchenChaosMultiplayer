using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameStartCountdownUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI countdownText;

    int perviousCountDownNo; 
    private void Start()
    {
        gameObject.SetActive(false);
        GameManager.Instane.OnStateChange += GameManager_OnStateChange;
        Hide();
    }

    private void GameManager_OnStateChange(object sender, System.EventArgs e)
    {
        if (GameManager.Instane.IsCountdownToStartActive())
        {
           // Debug.Log(GameManager.Instane.IsCountdownToStartActive());
            Show();
        }
        else
        {
            Hide();
        }
    }
    private void Update()
    {
      int countdownNumber  = Mathf.CeilToInt(GameManager.Instane.GetCountdownToStartTimer());
        countdownText.text = countdownNumber.ToString();
        if (perviousCountDownNo != countdownNumber)
        {
            perviousCountDownNo = countdownNumber;
            SoundManager.Instance.PlayCountdown(Vector3.zero);
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
