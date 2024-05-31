using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{

    public static OptionsUI instance;

    [SerializeField] Button soundEffectBTN;
    [SerializeField] TextMeshProUGUI soundEffectText;
    [SerializeField] Button musicBTN;
    [SerializeField] TextMeshProUGUI musicText;
    [SerializeField] Button closeBTN;
    [SerializeField] Button moveUpBTN;
    [SerializeField] TextMeshProUGUI moveUpText;
    [SerializeField] Button moveDownBTN;
    [SerializeField] TextMeshProUGUI moveDownText;
    [SerializeField] Button moveLeftBTN;
    [SerializeField] TextMeshProUGUI moveLeftText;
    [SerializeField] Button moveRightBTN;
    [SerializeField] TextMeshProUGUI moveRightText;
    [SerializeField] Button interactBTN;
    [SerializeField] TextMeshProUGUI interactText;
    [SerializeField] Button interactALTBTN;
    [SerializeField] TextMeshProUGUI interactALTText;
    [SerializeField] Button pauseBTN;
    [SerializeField] TextMeshProUGUI pauseText;
    [SerializeField] Button gamepadInteractBTN;
    [SerializeField] TextMeshProUGUI gamepadInteractText;
    [SerializeField] Button gamepadInteractALTBTN;
    [SerializeField] TextMeshProUGUI gamepadInteractALTText;
    [SerializeField] Button gamepadPauseBTN;
    [SerializeField] TextMeshProUGUI gamepadPauseText;
    [SerializeField] Transform pressKetToRebindTransform;

    private Action onCloseBTNAction;
    private void Awake()
    {
        instance = this;

        soundEffectBTN.onClick.AddListener(() =>
        {
            SoundManager.Instance.ChangeVolume();
            UpdateVisual();
        });


        musicBTN.onClick.AddListener(() =>
        {
           MusicManager.instance.ChangeVolume();
            UpdateVisual();
        }); 
        
        closeBTN.onClick.AddListener(() =>
        {
            Hide();
            onCloseBTNAction();
        });

        moveUpBTN.onClick.AddListener(() => { RebindBinding(GameInput.Binding.MoveUp);});
        moveDownBTN.onClick.AddListener(() => { RebindBinding(GameInput.Binding.MoveDown);});
        moveLeftBTN.onClick.AddListener(() => { RebindBinding(GameInput.Binding.MoveLeft);});
        moveRightBTN.onClick.AddListener(() => { RebindBinding(GameInput.Binding.MoveRight);});
        interactBTN.onClick.AddListener(() => { RebindBinding(GameInput.Binding.Interact);});
        interactALTBTN.onClick.AddListener(() => { RebindBinding(GameInput.Binding.InteractALT);});
        pauseBTN.onClick.AddListener(() => { RebindBinding(GameInput.Binding.Pause);});
        gamepadInteractBTN.onClick.AddListener(() => { RebindBinding(GameInput.Binding.GamepadInteract);});
        gamepadInteractALTBTN.onClick.AddListener(() => { RebindBinding(GameInput.Binding.GamepadInteractALT);});
        gamepadPauseBTN.onClick.AddListener(() => { RebindBinding(GameInput.Binding.GamepadPause);});
    }

    private void GameManager_OnGameUnPaused(object sender, System.EventArgs e)
    {
        Hide();
    }

    private void Start()
    {
        GameManager.Instane.OnGameUnPaused += GameManager_OnGameUnPaused;
        HidePressKeyTransform();
        UpdateVisual();
        Hide();

    }
    private void UpdateVisual()
    {
        soundEffectText.text = "Sound Effects: " + Mathf.Round(SoundManager.Instance.GetVolume() * 10f); 
        musicText.text = "Music Volume: " + Mathf.Round(MusicManager.instance.GetVolume() * 10f);

        moveUpText.text = GameInput.instance.GetBindingtext(GameInput.Binding.MoveUp);
        moveDownText.text = GameInput.instance.GetBindingtext(GameInput.Binding.MoveDown);
        moveLeftText.text = GameInput.instance.GetBindingtext(GameInput.Binding.MoveLeft);
        moveRightText.text = GameInput.instance.GetBindingtext(GameInput.Binding.MoveRight);
        interactText.text = GameInput.instance.GetBindingtext(GameInput.Binding.Interact);
        interactALTText.text = GameInput.instance.GetBindingtext(GameInput.Binding.InteractALT);
        pauseText.text = GameInput.instance.GetBindingtext(GameInput.Binding.Pause);
        gamepadInteractText.text = GameInput.instance.GetBindingtext(GameInput.Binding.GamepadInteract);
        gamepadInteractALTText.text = GameInput.instance.GetBindingtext(GameInput.Binding.GamepadInteractALT);
        gamepadPauseText.text = GameInput.instance.GetBindingtext(GameInput.Binding.GamepadPause);
    }
    public void Show(Action onCloseBTNAction)
    {
       this.onCloseBTNAction = onCloseBTNAction;
        gameObject.SetActive(true);
        soundEffectBTN.Select();
        
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private void RebindBinding(GameInput.Binding binding)
    {
        ShowPressKeyTransform();
        GameInput.instance.RebindBindiing(binding ,() =>
        {
            HidePressKeyTransform();
            UpdateVisual();
        });

    }

    private void ShowPressKeyTransform()
    {
        pressKetToRebindTransform.gameObject.SetActive(true);
    }
    
    private void HidePressKeyTransform()
    {
        pressKetToRebindTransform.gameObject.SetActive(false);
    }



}
