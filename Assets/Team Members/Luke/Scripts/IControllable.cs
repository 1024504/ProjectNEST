using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public interface IControllable
{
	public void MovePerformed(float lateralInput);

	public void MoveCancelled();

	public void AimPerformedMouse(Vector2 input);
	
	public void AimPerformedGamepad(Vector2 input);
	
	public void AimCancelled();
	
	public void JumpPerformed();

	public void JumpCancelled();

	public void ShootPerformed();
	
	public void ShootCancelled();

	public void Action1Performed();

	public void Action1Cancelled();
	public void Action2Performed();

	public void Action2Cancelled();

	public void PausePerformed();

	public void PauseCancelled();

	public void Weapon1Performed();

	public void Weapon1Cancelled();
	
	public void Weapon2Performed();

	public void Weapon2Cancelled();
	
	public void Weapon3Performed();

	public void Weapon3Cancelled();

	public void MedKitPerformed();

	public void MedKitCancelled();

	public void DashPerformed();

	public void DashCancelled();

}
