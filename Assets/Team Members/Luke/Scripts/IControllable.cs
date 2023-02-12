using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public interface IControllable
{
	public void MovePerformed(float lateralInput);

	public void MoveCancelled();

	public void AimPerformed(Vector2 input);
	
	public void AimCancelled();
	
	public void JumpPerformed();

	public void JumpCancelled();

	public void ShootPerformed();
	
	public void ShootCancelled();

	public void Action1Performed();

	public void Action1Cancelled();
}
