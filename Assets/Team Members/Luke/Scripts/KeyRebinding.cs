using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class KeyRebinding : MonoBehaviour
{
    [SerializeField] private InputActionReference inputActionReference;
    private InputAction _inputAction;
    [SerializeField] private GameObject pcStartRebindButton;
    [SerializeField] private GameObject controllerStartRebindButton;
    [SerializeField] private GameObject pcWaitingForInputText;
    [SerializeField] private GameObject controllerWaitingForInputText;
    [SerializeField] private TMP_Text pcKeyText;
    [SerializeField] private TMP_Text controllerKeyText;
    
    private PlayerControls _playerControls;
    
    private InputActionRebindingExtensions.RebindingOperation _rebindOperation;

    private void OnEnable()
    {
	    if (pcKeyText != null) pcKeyText.text = InputControlPath.ToHumanReadableString(inputActionReference.action.bindings[0].effectivePath,
		    InputControlPath.HumanReadableStringOptions.OmitDevice);
	    if (controllerKeyText != null) controllerKeyText.text = InputControlPath.ToHumanReadableString(inputActionReference.action.bindings[1].effectivePath,
		    InputControlPath.HumanReadableStringOptions.OmitDevice);
	    _playerControls = GameManager.Instance.playerControls;
	    foreach (InputAction action in _playerControls.Player.Get().actions)
	    {
		    if (action.name == inputActionReference.action.name)
		    {
			    _inputAction = action;
		    }
	    }
    }

    public void ChangeInputBindingKeyboard()
    {
	    if (Keyboard.current == null)
	    {
		    Debug.Log("no keyboard connected");
		    return;
	    }

	    pcStartRebindButton.SetActive(false);
	    pcWaitingForInputText.SetActive(true);

	    _rebindOperation = _inputAction.PerformInteractiveRebinding()
		    .WithControlsExcluding("Mouse")
		    .OnMatchWaitForAnother(0.1f)
		    .OnComplete(operation => RebindCompletePC())
		    .Start();
    }

    public void ChangeInputBindingGamepad()
    {
	    if (Gamepad.current == null)
	    {
		    Debug.Log("no gamepad connected");
		    return;
	    }

	    controllerStartRebindButton.SetActive(false);
	    controllerWaitingForInputText.SetActive(true);

	    _rebindOperation = _inputAction.PerformInteractiveRebinding()
		    .WithControlsExcluding("Mouse")
		    .OnMatchWaitForAnother(0.1f)
		    .OnComplete(operation => RebindCompleteController())
		    .Start();
    }

    private void RebindCompletePC()
    {
	    pcKeyText.text = InputControlPath.ToHumanReadableString(_inputAction.bindings[0].effectivePath,
		    InputControlPath.HumanReadableStringOptions.OmitDevice);
	    
	    _rebindOperation.Dispose();
	    
	    pcStartRebindButton.SetActive(true);
	    pcWaitingForInputText.SetActive(false);
	    
	    GameManager gm = GameManager.Instance;
	    gm.saveData.SettingsData.ControlsOverrides = _playerControls.SaveBindingOverridesAsJson();
	    GameManager.Instance.SaveGame();
    }
    
    private void RebindCompleteController()
    {
	    controllerKeyText.text = InputControlPath.ToHumanReadableString(_inputAction.bindings[1].effectivePath,
		    InputControlPath.HumanReadableStringOptions.OmitDevice);
	    
	    _rebindOperation.Dispose();
	    
	    controllerStartRebindButton.SetActive(true);
	    controllerWaitingForInputText.SetActive(false);

	    GameManager gm = GameManager.Instance;
	    gm.saveData.SettingsData.ControlsOverrides = _playerControls.SaveBindingOverridesAsJson();
	    GameManager.Instance.SaveGame();
    }
}
