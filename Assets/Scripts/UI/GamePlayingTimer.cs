using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayingTimer : MonoBehaviour
{
    [SerializeField] Image timerImage;

    private void Update()
    {
        timerImage.fillAmount = GameManager.Instane.GetGamePlayingTimerNormalized();
    }
}
