using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class PlayerController : MonoBehaviour
{
	[SerializeField] private MonoBehaviour gameplayAgent;
	
	public MonoBehaviour GameplayAgent
	{
		get => gameplayAgent;
		set
		{
			if ((IGameplayControllable)value == null) return;
			DisableGameplayInputs();
			gameplayAgent = value;
			EnableGameplayInputs();
		}
	}

	public PlayerControls Controls;
	
	// Game Controls
	private InputAction _moveInput;
	private InputAction _aimInput;
	private InputAction _jumpInput;
	private InputAction _shootInput;
	private InputAction _action1Input;
	private InputAction _action2Input;
	private InputAction _action3Input;

	//pause game
	private InputAction _pauseInput;
	
	//testing for weapons
	private InputAction _weapon1Input;
	private InputAction _weapon2Input;
	private InputAction _weapon3Input;
	
	//use medkit
	private InputAction _useMedKit;
	
	//dash
	private InputAction _dashInput;

	private void Start()
	{
		if (GameManager.Instance != null) GameManager.Instance.playerController = this;
		Controls = new();

		if ((IGameplayControllable) GameplayAgent != null)
		{
			//Gameplay inputs
			_moveInput = Controls.Player.Move;
			_aimInput = Controls.Player.Aim;
			_jumpInput = Controls.Player.Jump;
			_shootInput = Controls.Player.Fire;
			_action1Input = Controls.Player.Action1;
			_action2Input = Controls.Player.Action2;
			_action3Input = Controls.Player.Action3;
			_weapon1Input = Controls.Player.Weapon1;
			_weapon2Input = Controls.Player.Weapon2;
			_weapon3Input = Controls.Player.Weapon3;
			_pauseInput = Controls.Player.Pause;
			_useMedKit = Controls.Player.MedKit;
			_dashInput = Controls.Player.Dash;
			
			EnableGameplayInputs();
		}
	}

	private void OnDisable()
	{
		if ((IGameplayControllable)GameplayAgent != null) DisableGameplayInputs();
	}

	private void EnableGameplayInputs()
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
		
		_action3Input.Enable();
		_action3Input.performed += Action3Performed;
		_action3Input.canceled += Action3Cancelled;
		
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
		
		//dash
		_dashInput.Enable();
		_dashInput.performed += DashPerformed;
		_dashInput.canceled += DashCancelled;
	}

	private void DisableGameplayInputs()
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
		
		//dash
		_dashInput.Disable();
		_dashInput.performed -= DashPerformed;
		_dashInput.canceled -= DashCancelled;
	}

	private void MovePerformed(InputAction.CallbackContext context)
	{
		((IGameplayControllable)GameplayAgent).MovePerformed(Mathf.Ceil(context.ReadValue<float>()));
	}

	private void MoveCancelled(InputAction.CallbackContext context)
	{
		((IGameplayControllable)GameplayAgent).MoveCancelled();
	}

	private void AimPerformed(InputAction.CallbackContext context)
	{
		if (context.control.device == Mouse.current) ((IGameplayControllable)GameplayAgent).AimPerformedMouse(context.ReadValue<Vector2>());
		else if (context.control.device == Gamepad.current) ((IGameplayControllable)GameplayAgent).AimPerformedGamepad(context.ReadValue<Vector2>());
	}

	private void AimCancelled(InputAction.CallbackContext context)
	{
		((IGameplayControllable)GameplayAgent).AimCancelled();
	}

	private void JumpPerformed(InputAction.CallbackContext context)
	{
		((IGameplayControllable)GameplayAgent).JumpPerformed();
	}

	private void JumpCancelled(InputAction.CallbackContext context)
	{
		((IGameplayControllable)GameplayAgent).JumpCancelled();
	}

	private void ShootPerformed(InputAction.CallbackContext context)
	{
		((IGameplayControllable)GameplayAgent).ShootPerformed();
	}

	private void ShootCancelled(InputAction.CallbackContext context)
	{
		((IGameplayControllable)GameplayAgent).ShootCancelled();
	}

	private void Action1Performed(InputAction.CallbackContext context)
	{
		((IGameplayControllable)GameplayAgent).Action1Performed();
	}

	private void Action1Cancelled(InputAction.CallbackContext context)
	{
		((IGameplayControllable)GameplayAgent).Action1Cancelled();
	}

	private void Action2Performed(InputAction.CallbackContext context)
	{
		((IGameplayControllable)GameplayAgent).Action2Performed();
	}

	private void Action2Cancelled(InputAction.CallbackContext context)
	{
		((IGameplayControllable)GameplayAgent).Action2Cancelled();
	}
	
	private void Action3Performed(InputAction.CallbackContext context)
	{
		((IGameplayControllable)GameplayAgent).Action3Performed();
	}

	private void Action3Cancelled(InputAction.CallbackContext context)
	{
		((IGameplayControllable)GameplayAgent).Action3Cancelled();
	}

	private void PausePerformed(InputAction.CallbackContext context)
	{
		((IGameplayControllable)GameplayAgent).PausePerformed();
	}
	private void PauseCancelled(InputAction.CallbackContext context)
	{
		((IGameplayControllable)GameplayAgent).PauseCancelled();
	}
	#region Weapons Testing

	private void Weapon1Performed(InputAction.CallbackContext context)
	{
		((IGameplayControllable)GameplayAgent).Weapon1Performed();
	}
	
	private void Weapon1Cancelled(InputAction.CallbackContext context)
	{
		((IGameplayControllable)GameplayAgent).Weapon1Cancelled();
	}
	private void Weapon2Performed(InputAction.CallbackContext context)
	{
		((IGameplayControllable)GameplayAgent).Weapon2Performed();
	}
	
	private void Weapon2Cancelled(InputAction.CallbackContext context)
	{
		((IGameplayControllable)GameplayAgent).Weapon2Cancelled();
	}
	
	private void Weapon3Performed(InputAction.CallbackContext context)
	{
		((IGameplayControllable)GameplayAgent).Weapon3Performed();
	}
	
	private void Weapon3Cancelled(InputAction.CallbackContext context)
	{
		((IGameplayControllable)GameplayAgent).Weapon3Cancelled();
	}

	#endregion

	#region MedKit Inputs

	private void MedKitPerformed(InputAction.CallbackContext context)
	{
		((IGameplayControllable)GameplayAgent).MedKitPerformed();
	}
	private void MedKitCancelled(InputAction.CallbackContext context)
	{
		((IGameplayControllable)GameplayAgent).MedKitCancelled();
	}
	#endregion

	#region Dash Inputs

	private void DashPerformed(InputAction.CallbackContext context)
	{
		if (context.interaction is TapInteraction) ((IGameplayControllable)GameplayAgent).DashPerformed();
		else if (context.interaction is HoldInteraction) ((IGameplayControllable)GameplayAgent).DashHeld();
	}
	private void DashCancelled(InputAction.CallbackContext context)
	{
		((IGameplayControllable)GameplayAgent).DashCancelled();
	}
	
	#endregion
}
