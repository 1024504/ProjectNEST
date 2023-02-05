using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
	private PlayerControls _controls;
	private InputAction _moveInput;
	private InputAction _jumpInput;
	private InputAction _shootInput;
	private InputAction _action1Input;

	[SerializeField] private MonoBehaviour player;
	
	public MonoBehaviour Agent
	{
		get => player;
		set
		{
			if ((IControllable)value == null) return;
			DisableInputs((IControllable)player);
			player = value;
			EnableInputs((IControllable)player);
		}
	}

	private void OnEnable()
	{
		_controls = new();
		_controls.UI.Disable();
		_moveInput = _controls.Player.Move;
		_jumpInput = _controls.Player.Jump;
		_shootInput = _controls.Player.Jump;
		_action1Input = _controls.Player.Jump;

		if ((IControllable)player != null) EnableInputs((IControllable)player);
	}

	private void OnDisable()
	{
		if ((IControllable)player != null) DisableInputs((IControllable)player);
	}
	
	private void EnableInputs(IControllable iControllable)
	{
		_moveInput.Enable();
		_moveInput.performed += iControllable.MovePerformed;
		_moveInput.canceled += iControllable.MoveCancelled;
		
		_jumpInput.Enable();
		_jumpInput.performed += iControllable.JumpPerformed;
		_jumpInput.canceled += iControllable.JumpCancelled;
		
		_shootInput.Enable();
		_shootInput.performed += iControllable.ShootPerformed;
		_shootInput.canceled += iControllable.ShootCancelled;
		
		_action1Input.Enable();
		_action1Input.performed += iControllable.Action1Performed;
		_action1Input.canceled += iControllable.Action1Cancelled;
	}

	private void DisableInputs(IControllable iControllable)
	{
		_moveInput.Disable();
		_moveInput.performed -= iControllable.MovePerformed;
		_moveInput.canceled -= iControllable.MoveCancelled;

		_jumpInput.Disable();
		_jumpInput.performed -= iControllable.JumpPerformed;
		_jumpInput.canceled -= iControllable.JumpCancelled;
		
		_shootInput.Disable();
		_shootInput.performed -= iControllable.ShootPerformed;
		_shootInput.canceled += iControllable.ShootCancelled;
		
		_action1Input.Disable();
		_action1Input.performed -= iControllable.Action1Performed;
		_action1Input.canceled -= iControllable.Action1Cancelled;
	}
}
