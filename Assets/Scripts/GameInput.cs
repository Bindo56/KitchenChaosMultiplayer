using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public static GameInput instance;

    private const string PLAYER_PREFS_BINDING_DATA = "setPrefs";
    public event EventHandler OnPauseAction;
    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternativeAction;
    public event EventHandler OnRebind;

    PlayerInput playerInput;

    public enum Binding
    {
        MoveUp,
        MoveDown,
        MoveLeft,
        MoveRight,
        Interact,
        InteractALT,
        Pause,
        GamepadInteract,
        GamepadInteractALT,
        GamepadPause

     
    }
    private void Awake()
    {
        instance = this;
        playerInput = new PlayerInput();

        if(PlayerPrefs.HasKey(PLAYER_PREFS_BINDING_DATA) )
        {
            playerInput.LoadBindingOverridesFromJson(PlayerPrefs.GetString(PLAYER_PREFS_BINDING_DATA));
        }

        playerInput.Player.Enable();

        playerInput.Player.Interact.performed += Interact_performed;
        playerInput.Player.InteractAlternate.performed += InteractAlternate_performed;
        playerInput.Player.Pause.performed += Pause_performed;
    }


    private void OnDestroy()
    {
        playerInput.Player.Interact.performed -= Interact_performed;
        playerInput.Player.InteractAlternate.performed -= InteractAlternate_performed;
        playerInput.Player.Pause.performed -= Pause_performed;

        playerInput.Dispose();
    }

    private void Pause_performed(InputAction.CallbackContext obj)
    {
        OnPauseAction?.Invoke(this, EventArgs.Empty);
    }

    public void InteractAlternate_performed(InputAction.CallbackContext obj)
    {
        OnInteractAlternativeAction?.Invoke(this, EventArgs.Empty);
    }

    public void Interact_performed(InputAction.CallbackContext obj)
    {
        OnInteractAction?.Invoke(this, EventArgs.Empty); //for null check then invoke 
    }

    public Vector2 GetMovementVector()
    {

        Vector2 inputVector = playerInput.Player.Move.ReadValue<Vector2>();
        inputVector = inputVector.normalized;
        return inputVector;
    }

    public string GetBindingtext(Binding binding)
    {
        switch (binding)
        {
            default:
            case Binding.MoveUp:
                return playerInput.Player.Move.bindings[1].ToDisplayString();
            case Binding.MoveDown:
                return playerInput.Player.Move.bindings[2].ToDisplayString();
            case Binding.MoveLeft:
                return playerInput.Player.Move.bindings[3].ToDisplayString();
            case Binding.MoveRight:
                return playerInput.Player.Move.bindings[4].ToDisplayString();
            case Binding.Interact:
                return playerInput.Player.Interact.bindings[0].ToDisplayString();
            case Binding.InteractALT:
                return playerInput.Player.InteractAlternate.bindings[0].ToDisplayString();
            case Binding.Pause:
                return playerInput.Player.Pause.bindings[0].ToDisplayString();
            case Binding.GamepadInteract:
                return playerInput.Player.Interact.bindings[1].ToDisplayString();
            case Binding.GamepadInteractALT:
                return playerInput.Player.InteractAlternate.bindings[1].ToDisplayString();
            case Binding.GamepadPause:
                return playerInput.Player.Pause.bindings[1].ToDisplayString();
        }
    }

    public void RebindBindiing(Binding binding, Action onActionRebound)
    {
        playerInput.Player.Disable();
        InputAction inputAction;
        int bindingIndex;


        switch (binding)
        {
            default:
            case Binding.MoveUp:
                inputAction = playerInput.Player.Move;
                bindingIndex = 1;
                break;
            case Binding.MoveDown:
                inputAction = playerInput.Player.Move;
                bindingIndex = 2;
                break;
            case Binding.MoveLeft:
                inputAction = playerInput.Player.Move;
                bindingIndex = 3;
                break;
            case Binding.MoveRight:
                inputAction = playerInput.Player.Move;
                bindingIndex = 4;
                break;
            case Binding.Interact:
                inputAction = playerInput.Player.Interact;
                bindingIndex = 0;
                break;
            case Binding.InteractALT:
                inputAction = playerInput.Player.InteractAlternate;
                bindingIndex = 0;
                break;
            case Binding.Pause:
                inputAction = playerInput.Player.Pause;
                bindingIndex = 0;
                break;
            case Binding.GamepadPause:
                inputAction = playerInput.Player.Pause;
                bindingIndex = 1;
                break;
            case Binding.GamepadInteractALT:
                inputAction = playerInput.Player.InteractAlternate;
                bindingIndex = 1;
                break;
            case Binding.GamepadInteract:
                inputAction = playerInput.Player.Interact;
                bindingIndex = 1;
                break;


        }
        inputAction.PerformInteractiveRebinding(bindingIndex).OnComplete(callback =>
        {
             callback.Dispose();
             playerInput.Player.Enable();
             onActionRebound();

            PlayerPrefs.SetString(PLAYER_PREFS_BINDING_DATA, playerInput.SaveBindingOverridesAsJson());
            PlayerPrefs.Save();
            OnRebind?.Invoke(this, EventArgs.Empty);

         }).Start();



    }



}
