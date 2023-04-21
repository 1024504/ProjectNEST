using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public interface IUiControllable
{
	public void NavigatePerformed(Vector2 input);
	
	public void NavigateCancelled();
	
	public void PointPerformed(Vector2 input);
	
	public void PointCancelled();
	
	public void ClickPerformed();
	
	public void ClickCancelled();
	
	public void ReturnPerformed();
	
	public void ReturnCancelled();
	
	public void ResumePerformed();
	
	public void ResumeCancelled();

}
