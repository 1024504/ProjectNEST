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
		_moveInput.Enable();
		_jumpInput.Enable();
	}
	
	private void OnDisable()
	{
		_moveInput.Disable();
		_jumpInput.Disable();
	}
	
	
}
