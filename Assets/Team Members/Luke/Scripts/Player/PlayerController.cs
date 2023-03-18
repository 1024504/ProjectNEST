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
	private InputAction _action2Input;
	

	//pause game
	private InputAction _pauseInput;
	
	//testing for weapons
	private InputAction _weapon1Input;
	private InputAction _weapon2Input;
	private InputAction _weapon3Input;
	
	//use medkit
	private InputAction _useMedKit;

	private void OnEnable()
	{
		_controls = new();
		_controls.UI.Disable();
		_moveInput = _controls.Player.Move;
		_aimInput = _controls.Player.Aim;
		_jumpInput = _controls.Player.Jump;
		_shootInput = _controls.Player.Fire;
		_action1Input = _controls.Player.Action1;
		_action2Input = _controls.Player.Action2;
		_weapon1Input = _controls.Player.Weapon1;
		_weapon2Input = _controls.Player.Weapon2;
		_weapon3Input = _controls.Player.Weapon3;
		_pauseInput = _controls.Player.Pause;
		_useMedKit = _controls.Player.MedKit;
		

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
		
		_action2Input.Enable();
		_action2Input.performed += Action2Performed;
		_action2Input.canceled += Action2Cancelled;
		
		//pause menu
		_pauseInput.Enable();
		_pauseInput.performed += PausePerformed;
		_pauseInput.canceled += PauseCancelled;
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
		
		//medkit
		_useMedKit.Enable();
		_useMedKit.performed += MedKitPerformed;
		_useMedKit.canceled += MedKitCancelled;
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
		
		_action2Input.Disable();
		_action2Input.performed -= Action2Performed;
		_action2Input.canceled -= Action2Cancelled;
		
		//pause menu
		_pauseInput.Enable();
		_pauseInput.performed -= PausePerformed;
		_pauseInput.canceled -= PauseCancelled;
		
		//weapons test
		_weapon1Input.Disable();
		_weapon1Input.performed -= Weapon1Performed;
		_weapon1Input.canceled -= Weapon1Cancelled;
		
		_weapon2Input.Disable();
		_weapon2Input.performed -= Weapon2Performed;
		_weapon2Input.canceled -= Weapon2Cancelled;
		
		_weapon3Input.Disable();
		_weapon3Input.performed -= Weapon3Performed;
		_weapon3Input.canceled -= Weapon3Cancelled;
		
		//medkit
		_useMedKit.Disable();
		_useMedKit.performed -= MedKitPerformed;
		_useMedKit.canceled -= MedKitCancelled;
	}

	private void MovePerformed(InputAction.CallbackContext context)
	{
		((IControllable)Agent).MovePerformed(Mathf.Ceil(context.ReadValue<float>()));
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

	private void Action2Performed(InputAction.CallbackContext context)
	{
		((IControllable)Agent).Action2Performed();
	}

	private void Action2Cancelled(InputAction.CallbackContext context)
	{
		((IControllable)Agent).Action2Cancelled();
	}

	private void PausePerformed(InputAction.CallbackContext context)
	{
		((IControllable)Agent).PausePerformed();
	}
	private void PauseCancelled(InputAction.CallbackContext context)
	{
		((IControllable)Agent).PauseCancelled();
	}
	#region Weapons Testing

	private void Weapon1Performed(InputAction.CallbackContext context)
	{
		((IControllable)Agent).Weapon1Performed();
	}
	
	private void Weapon1Cancelled(InputAction.CallbackContext context)
	{
		((IControllable)Agent).Weapon1Cancelled();
	}
	private void Weapon2Performed(InputAction.CallbackContext context)
	{
		((IControllable)Agent).Weapon2Performed();
	}
	
	private void Weapon2Cancelled(InputAction.CallbackContext context)
	{
		((IControllable)Agent).Weapon2Cancelled();
	}
	
	private void Weapon3Performed(InputAction.CallbackContext context)
	{
		((IControllable)Agent).Weapon3Performed();
	}
	
	private void Weapon3Cancelled(InputAction.CallbackContext context)
	{
		((IControllable)Agent).Weapon3Cancelled();
	}

	#endregion

	#region MedKit Inputs

	private void MedKitPerformed(InputAction.CallbackContext context)
	{
		((IControllable)Agent).MedKitPerformed();
	}
	private void MedKitCancelled(InputAction.CallbackContext context)
	{
		((IControllable)Agent).MedKitCancelled();
	}
	#endregion
}
