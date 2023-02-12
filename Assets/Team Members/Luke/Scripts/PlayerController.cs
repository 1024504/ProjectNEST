using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : ControllerBase
{
	private PlayerControls _controls;
	private InputAction _moveInput;
	private InputAction _aimInput;
	private InputAction _jumpInput;
	private InputAction _shootInput;
	private InputAction _action1Input;

	private void OnEnable()
	{
		_controls = new();
		_controls.UI.Disable();
		_moveInput = _controls.Player.Move;
		_aimInput = _controls.Player.Aim;
		_jumpInput = _controls.Player.Jump;
		_shootInput = _controls.Player.Fire;
		_action1Input = _controls.Player.Action1;

		if ((IControllable)Agent != null) EnableInputs((IControllable)Agent);
	}

	private void OnDisable()
	{
		if ((IControllable)Agent != null) DisableInputs((IControllable)Agent);
	}

	protected override void EnableInputs(IControllable iControllable)
	{
		_moveInput.Enable();
		_moveInput.performed += MovePerformed;
		_moveInput.canceled += MoveCancelled;
		
		_jumpInput.Enable();
		_jumpInput.performed += JumpPerformed;
		_jumpInput.canceled += JumpCancelled;
		
		_shootInput.Enable();
		_shootInput.performed += ShootPerformed;
		_shootInput.canceled += ShootCancelled;
		
		_action1Input.Enable();
		_action1Input.performed += Action1Performed;
		_action1Input.canceled += Action1Cancelled;
	}

	protected override void DisableInputs(IControllable iControllable)
	{
		_moveInput.Disable();
		_moveInput.performed -= MovePerformed;
		_moveInput.canceled -= MoveCancelled;

		_jumpInput.Disable();
		_jumpInput.performed -= JumpPerformed;
		_jumpInput.canceled -= JumpCancelled;
		
		_shootInput.Disable();
		_shootInput.performed -= ShootPerformed;
		_shootInput.canceled -= ShootCancelled;
		
		_action1Input.Disable();
		_action1Input.performed -= Action1Performed;
		_action1Input.canceled -= Action1Cancelled;
	}

	public void MovePerformed(InputAction.CallbackContext context)
	{
		
		((IControllable)Agent).MovePerformed(Mathf.Ceil(context.ReadValue<Vector2>().x));
	}

	public void MoveCancelled(InputAction.CallbackContext context)
	{
		((IControllable)Agent).MoveCancelled();
	}

	public void AimPerformed(InputAction.CallbackContext context)
	{
		((IControllable)Agent).AimPerformed(context.ReadValue<Vector2>());
	}

	public void AimCancelled(InputAction.CallbackContext context)
	{
		((IControllable)Agent).AimCancelled();
	}

	public void JumpPerformed(InputAction.CallbackContext context)
	{
		((IControllable)Agent).JumpPerformed();
	}

	public void JumpCancelled(InputAction.CallbackContext context)
	{
		((IControllable)Agent).JumpCancelled();
	}

	public void ShootPerformed(InputAction.CallbackContext context)
	{
		((IControllable)Agent).ShootPerformed();
	}

	public void ShootCancelled(InputAction.CallbackContext context)
	{
		((IControllable)Agent).ShootCancelled();
	}

	public void Action1Performed(InputAction.CallbackContext context)
	{
		((IControllable)Agent).Action1Performed();
	}

	public void Action1Cancelled(InputAction.CallbackContext context)
	{
		((IControllable)Agent).Action1Cancelled();
	}
}
