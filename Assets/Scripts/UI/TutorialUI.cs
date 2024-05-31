using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI keyMoveUpTXT;
    [SerializeField] TextMeshProUGUI keyMoveDownTXT;
    [SerializeField] TextMeshProUGUI keyMoveLeftTXT;
    [SerializeField] TextMeshProUGUI keyMoveRightTXT;
    [SerializeField] TextMeshProUGUI keyInteractTXT;
    [SerializeField] TextMeshProUGUI keyInteractATLTXT;
    [SerializeField] TextMeshProUGUI keyPauseTXT;
    [SerializeField] TextMeshProUGUI gamepdInteractTXT;
    [SerializeField] TextMeshProUGUI gamepdALTTXT;
    [SerializeField] TextMeshProUGUI gamepdPauseTXT;


    private void Start()
    {
        Show();
        GameInput.instance.OnRebind += GameInput_OnRebind;
        GameManager.Instane.OnStateChange += Instane_OnStateChange;
        UpdateVisual();
    }

    private void Instane_OnStateChange(object sender, System.EventArgs e)
    {
        if(GameManager.Instane.IsCountdownToStartActive())
        {
            Hide();
        }
    }

    private void GameInput_OnRebind(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        keyMoveUpTXT.text = GameInput.instance.GetBindingtext(GameInput.Binding.MoveUp);
        keyMoveDownTXT.text = GameInput.instance.GetBindingtext(GameInput.Binding.MoveDown);
        keyMoveLeftTXT.text = GameInput.instance.GetBindingtext(GameInput.Binding.MoveLeft);
        keyMoveRightTXT.text = GameInput.instance.GetBindingtext(GameInput.Binding.MoveRight);
        keyInteractTXT.text = GameInput.instance.GetBindingtext(GameInput.Binding.Interact);
        keyInteractATLTXT.text = GameInput.instance.GetBindingtext(GameInput.Binding.InteractALT);
        keyPauseTXT.text = GameInput.instance.GetBindingtext(GameInput.Binding.Pause);
        gamepdInteractTXT.text = GameInput.instance.GetBindingtext(GameInput.Binding.GamepadInteract);
        gamepdALTTXT.text = GameInput.instance.GetBindingtext(GameInput.Binding.GamepadInteractALT);
        gamepdPauseTXT.text = GameInput.instance.GetBindingtext(GameInput.Binding.GamepadPause);
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide() { gameObject.SetActive(false);}
}
