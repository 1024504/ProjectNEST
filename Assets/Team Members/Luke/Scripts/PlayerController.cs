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
	
	//testing for weapons
	private InputAction _weapon1Input;
	private InputAction _weapon2Input;
	private InputAction _weapon3Input;

	private void OnEnable()
	{
		_controls = new();
		_controls.UI.Disable();
		_moveInput = _controls.Player.Move;
		_aimInput = _controls.Player.Aim;
		_jumpInput = _controls.Player.Jump;
		_shootInput = _controls.Player.Fire;
		_action1Input = _controls.Player.Action1;
		_weapon1Input = _controls.Player.Weapon1;
		_weapon2Input = _controls.Player.Weapon2;
		_weapon3Input = _controls.Player.Weapon3;

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
		
		Cursor.lockState = CursorLockMode.Confined;
		Cursor.visible = false;
		_aimInput.Enable();
		_aimInput.performed += AimPerformed;
		_aimInput.canceled += AimCancelled;
		
		_jumpInput.Enable();
		_jumpInput.performed += JumpPerformed;
		_jumpInput.canceled += JumpCancelled;
		
		_shootInput.Enable();
		_shootInput.performed += ShootPerformed;
		_shootInput.canceled += ShootCancelled;
		
		_action1Input.Enable();
		_action1Input.performed += Action1Performed;
		_action1Input.canceled += Action1Cancelled;
		
		//weapons test
		_weapon1Input.Enable();
		_weapon1Input.performed += Weapon1Performed;
		_weapon1Input.canceled += Weapon1Cancelled;
		
		_weapon2Input.Enable();
		_weapon2Input.performed += Weapon2Performed;
		_weapon2Input.canceled += Weapon2Cancelled;
		
		_weapon3Input.Enable();
		_weapon3Input.performed += Weapon3Performed;
		_weapon3Input.canceled += Weapon3Cancelled;
	}

	protected override void DisableInputs(IControllable iControllable)
	{
		_moveInput.Disable();
		_moveInput.performed -= MovePerformed;
		_moveInput.canceled -= MoveCancelled;
		
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
		_aimInput.Disable();
		_aimInput.performed -= AimPerformed;
		_aimInput.canceled -= AimCancelled;

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

	private void MovePerformed(InputAction.CallbackContext context)
	{
		((IControllable)Agent).MovePerformed(Mathf.Ceil(context.ReadValue<Vector2>().x));
	}

	private void MoveCancelled(InputAction.CallbackContext context)
	{
		((IControllable)Agent).MoveCancelled();
	}

	private void AimPerformed(InputAction.CallbackContext context)
	{
		if (context.control.device == Mouse.current) ((IControllable)Agent).AimPerformedMouse(context.ReadValue<Vector2>());
		else if (context.control.device == Gamepad.current) ((IControllable)Agent).AimPerformedGamepad(context.ReadValue<Vector2>());
	}

	private void AimCancelled(InputAction.CallbackContext context)
	{
		((IControllable)Agent).AimCancelled();
	}

	private void JumpPerformed(InputAction.CallbackContext context)
	{
		((IControllable)Agent).JumpPerformed();
	}

	private void JumpCancelled(InputAction.CallbackContext context)
	{
		((IControllable)Agent).JumpCancelled();
	}

	private void ShootPerformed(InputAction.CallbackContext context)
	{
		((IControllable)Agent).ShootPerformed();
	}

	private void ShootCancelled(InputAction.CallbackContext context)
	{
		((IControllable)Agent).ShootCancelled();
	}

	private void Action1Performed(InputAction.CallbackContext context)
	{
		((IControllable)Agent).Action1Performed();
	}

	private void Action1Cancelled(InputAction.CallbackContext context)
	{
		((IControllable)Agent).Action1Cancelled();
	}

	#region Weapons Testing

	public void Weapon1Performed(InputAction.CallbackContext context)
	{
		((IControllable)Agent).Weapon1Performed();
	}
	
	public void Weapon1Cancelled(InputAction.CallbackContext context)
	{
		((IControllable)Agent).Weapon1Cancelled();
	}
	public void Weapon2Performed(InputAction.CallbackContext context)
	{
		((IControllable)Agent).Weapon2Performed();
	}
	
	public void Weapon2Cancelled(InputAction.CallbackContext context)
	{
		((IControllable)Agent).Weapon2Cancelled();
	}
	
	public void Weapon3Performed(InputAction.CallbackContext context)
	{
		((IControllable)Agent).Weapon3Performed();
	}
	
	public void Weapon3Cancelled(InputAction.CallbackContext context)
	{
		((IControllable)Agent).Weapon3Cancelled();
	}

	#endregion
	
}
