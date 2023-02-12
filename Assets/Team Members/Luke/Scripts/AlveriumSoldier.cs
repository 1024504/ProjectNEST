using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;

public class AlveriumSoldier : MonoBehaviour, IControllable
{
	[SerializeField] private Transform[] joints;
	[SerializeField] private float[] neutralAngles;
	[SerializeField] private float[] motionAmplitudes;
	[SerializeField] private float[] motionFrequencies;
	[SerializeField] private float[] motionOffsets;
	
	[SerializeField] private float motionProgress;
	
	private float MotionProgress
	{
		get => motionProgress;
		set => motionProgress = value % (2*Mathf.PI);
	}
	
	private Rigidbody2D _rb;
	
	[SerializeField] private float moveSpeed = 5;
	
	[SerializeField] private float _lateralMoveInput;
	
	private enum Joints
	{
		Body1,
		Body2,
		Body3,
		Arm1,
		Arm2,
		Arm3,
		Head1,
		Head2,
		Head3,
		FrontLeg1,
		FrontLeg2,
		BackLeg1,
		BackLeg2
	}
	
	private void OnEnable()
	{
		_rb = GetComponent<Rigidbody2D>();
		neutralAngles = new float[joints.Length];
		for (int i = 0; i < joints.Length; i++)
		{
			neutralAngles[i] = joints[i].localEulerAngles.z;
		}
	}

	private void FixedUpdate()
	{
		MotionProgress += Time.fixedDeltaTime;
		Move(_lateralMoveInput);
		for (int i = 0; i < joints.Length; i++)
		{
			RotateJoint((Joints) i, Mathf.Sin(MotionProgress*motionFrequencies[i]+motionOffsets[i])*motionAmplitudes[i], neutralAngles[i]);
		}
	}

	private void RotateJoint(Joints joint, float deltaAngle, float neutralAngle = 0)
	{
		joints[(int) joint].localRotation = Quaternion.Euler(0, 0, neutralAngle + deltaAngle);
	}

	private void Move(float input)
	{
		_rb.velocity = new Vector2(input * moveSpeed, _rb.velocity.y);
	}

	public void MovePerformed(float lateralInput)
	{
		_lateralMoveInput = lateralInput;
	}

	public void MoveCancelled()
	{
		
	}

	public void AimPerformed(Vector2 input)
	{
		
	}

	public void AimCancelled()
	{
		
	}

	public void JumpPerformed()
	{
		
	}

	public void JumpCancelled()
	{
		
	}

	public void ShootPerformed()
	{
		
	}

	public void ShootCancelled()
	{
		
	}

	public void Action1Performed()
	{
		
	}

	public void Action1Cancelled()
	{
		
	}
}
