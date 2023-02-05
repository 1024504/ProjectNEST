using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public interface IControllable
{
	public void MovePerformed(InputAction.CallbackContext context);

	public void MoveCancelled(InputAction.CallbackContext context);

	public void JumpPerformed(InputAction.CallbackContext context);

	public void JumpCancelled(InputAction.CallbackContext context);

	public void ShootPerformed(InputAction.CallbackContext context);
	
	public void ShootCancelled(InputAction.CallbackContext context);

	public void Action1Performed(InputAction.CallbackContext context);

	public void Action1Cancelled(InputAction.CallbackContext context);
}
