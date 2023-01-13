using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
	private PlayerControls _controls;
	private InputAction _moveInput;
	private InputAction _jumpInput;
	// Add attack/interact/etc.


	[SerializeField] private Player _player;

	private void OnEnable()
	{
		_controls = new();
		_controls.UI.Disable();

		_moveInput = _controls.Player.Move;
		_moveInput.Enable();
		_moveInput.performed += MovePerformed;
		_moveInput.canceled += MoveCancelled;
		
		_jumpInput = _controls.Player.Jump;
		_jumpInput.Enable();
		_jumpInput.performed += JumpPerformed;
		_jumpInput.canceled += JumpCancelled;
	}
	
	private void OnDisable()
	{
		_moveInput.Disable();
		_moveInput.performed -= MovePerformed;
		_moveInput.canceled -= MoveCancelled;

		_jumpInput.Disable();
		_jumpInput.performed -= JumpPerformed;
		_jumpInput.canceled -= JumpCancelled;
	}

	private void MovePerformed(InputAction.CallbackContext context)
	{
		_player.lateralMoveInput = context.ReadValue<Vector2>().x;
	}

	private void MoveCancelled(InputAction.CallbackContext context)
	{
		_player.lateralMoveInput = 0;
	}

	private void JumpPerformed(InputAction.CallbackContext context)
	{
		_player.Jump();
	}
	
	private void JumpCancelled(InputAction.CallbackContext context)
	{
		_player.CancelJump();
	}
}
