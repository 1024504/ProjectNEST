using System;
using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;

public class AlveriumSoldier : MonoBehaviour, IControllable, ISense
{
	// [SerializeField] private Transform[] joints;
	// [SerializeField] private float[] neutralAngles;
	// [SerializeField] private float[] motionAmplitudes;
	// [SerializeField] private float[] motionFrequencies;
	// [Range(0,1)]
	// [SerializeField] private float[] motionPhaseOffsets;
	
	// [SerializeField] private float motionProgress;
	
	// private float MotionProgress
	// {
	// 	get => motionProgress;
	// 	set => motionProgress = value % (2*Mathf.PI);
	// }
	
	private Transform _t;
	private Rigidbody2D _rb;
	public Animator anim;

	[SerializeField] private Transform _view;

	public List<Transform> targetLocations;
	public Transform currentTarget;
	public float lastKnownTargetDirection;

    private SoldierTerrainDetection _terrainDetection;

    public bool beginsPatrolLeft = true;
    public float patrolSpeed = 8f;
    public float chaseSpeed = 15f;
    public float attackRange = 2.5f;
    
	public float currentMoveSpeed = 15;
	[SerializeField] private float gravityScale = 1;
	[SerializeField] private float jumpSpeed = 15;
	[SerializeField] private float wallSuctionStrength = 1;

	public float idleDuration = 2f;
	public float patrolDuration = 3f;
	public float memoryDuration = 5f;
	public float attackDamage = 10f;
	public float attackCooldown = 2f;
	
	private bool _isJumping = false;
	private bool _canPatrol = true;
	public bool targetWithinRange = false;
	public bool canAttack = true;

	public float lateralMoveInput;

	public float rotateSpeed = 1;
	
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
		_t = transform;
		_rb = GetComponent<Rigidbody2D>();
        _terrainDetection = (SoldierTerrainDetection)GetComponentInChildren<TerrainDetection>();
		// neutralAngles = new float[joints.Length];
		// for (int i = 0; i < joints.Length; i++)
		// {
		// 	neutralAngles[i] = joints[i].localEulerAngles.z;
		// }
		anim = GetComponent<Animator>();
	}

	private void FixedUpdate()
	{
		Move(lateralMoveInput);
		RotateToWall();
		// MotionProgress += Time.fixedDeltaTime;
		// MoveBody(Vector2.down*Mathf.Sin(MotionProgress*motionFrequencies[0]+motionPhaseOffsets[0]*2*Mathf.PI)*motionAmplitudes[0]);
		// for (int i = 1; i < joints.Length; i++)
		// {
		// 	RotateJoint((Joints) i, Mathf.Sin(MotionProgress*motionFrequencies[i]+motionPhaseOffsets[i]*2*Mathf.PI)*motionAmplitudes[i], neutralAngles[i]);
		// }
	}

	// private void MoveBody(Vector2 deltaPosition)
	// {
	// 	joints[0].localPosition += new Vector3(deltaPosition.x, deltaPosition.y, 0);
	// }
	//
	// private void RotateJoint(Joints joint, float deltaAngle, float neutralAngle = 0)
	// {
	// 	joints[(int) joint].localRotation = Quaternion.Euler(0, 0, neutralAngle + deltaAngle);
	// }
	
	
	public void CollectConditions(AntAIAgent aAgent, AntAICondition aWorldState)
	{
		aWorldState.Set(SoldierScenario.CanPatrol, _canPatrol);
		aWorldState.Set(SoldierScenario.SeesTarget, ChooseTarget());
		aWorldState.Set(SoldierScenario.TargetWithinRange, targetWithinRange);
		aWorldState.Set(SoldierScenario.CanReachTarget, true);
		aWorldState.Set(SoldierScenario.CanAttack, canAttack);
		aWorldState.Set(SoldierScenario.AllTargetsDead, false);
	}

	public IEnumerator CanPatrolTimer(float delay)
	{
		yield return new WaitForSeconds(delay);
		_canPatrol = !_canPatrol;
	}

	[SerializeField] private LayerMask visionLayerMask = 520;

	private bool ChooseTarget()
	{
		if (targetLocations.Count == 0)
		{
			currentTarget = null;
			return false;
		}
		
		KeyValuePair<float, int> closestTarget = new KeyValuePair<float, int>(Mathf.Infinity, -1);
		foreach (Transform target in targetLocations)
		{
			RaycastHit2D hit = Physics2D.Linecast(_t.position, target.position, visionLayerMask);
			if (hit.collider.gameObject.GetComponentInParent<Player>() == null) continue;
			float distance = Vector2.Distance(_t.position, target.position);
			if (distance < closestTarget.Key) closestTarget = new KeyValuePair<float, int>(distance, targetLocations.IndexOf(target));
		}

		if (closestTarget.Value == -1)
		{
			return true;
		}
		
		currentTarget = targetLocations[closestTarget.Value];

		float lateralDistance = _t.InverseTransformDirection(currentTarget.position - _t.position).x;
		if (Mathf.Abs(lateralDistance) > attackRange) lastKnownTargetDirection = Mathf.Sign(_t.InverseTransformDirection(currentTarget.position - _t.position).x);
		else lastKnownTargetDirection = 0;
		return true;
	}

	public void CooldownAttack()
	{
		StartCoroutine(CooldownAttackTimer());
	}
	
	private IEnumerator CooldownAttackTimer()
	{
		canAttack = false;
		yield return new WaitForSeconds(attackCooldown);
		canAttack = true;
	}

	private void Move(float lateralInput)
    {
        if (!_terrainDetection.isGrounded)
        {
	        _rb.gravityScale = gravityScale;
	        _rb.velocity = new Vector2(lateralInput * currentMoveSpeed, _rb.velocity.y);
        }
        else
        {
	        _rb.gravityScale = 0;
	        _rb.velocity = new Vector2(lateralInput * currentMoveSpeed * _terrainDetection.mainNormal.y-_terrainDetection.mainNormal.x*wallSuctionStrength,
                lateralInput * currentMoveSpeed * -_terrainDetection.mainNormal.x-_terrainDetection.mainNormal.y*wallSuctionStrength);
        }
    }
    
    private void RotateToWall()
	{
		_rb.angularVelocity = Vector3.SignedAngle(_t.TransformDirection(Vector3.up),_terrainDetection.mainNormal, Vector3.forward)*rotateSpeed;
	}

	public void MovePerformed(float lateralInput)
	{
		if (lateralMoveInput >= 0 && lateralInput < 0)
		{
			_view.localRotation = Quaternion.identity;
		}
		else if (lateralMoveInput <= 0 && lateralInput > 0)
		{
			_view.localRotation = Quaternion.Euler(0, 180, 0);
		}
		lateralMoveInput = lateralInput;
        _terrainDetection.lateralMoveInput = -Mathf.Abs(lateralInput);
    }

	public void MoveCancelled()
    {
        lateralMoveInput = 0;
        _terrainDetection.lateralMoveInput = 0;
    }

	public void AimPerformedMouse(Vector2 input)
	{
		
	}

	public void AimPerformedGamepad(Vector2 input)
	{
		
	}

	public void AimCancelled()
	{
		
	}

	public void JumpPerformed()
    {
        StartCoroutine(JumpTimer());
    }

    private IEnumerator JumpTimer()
    {
        _rb.gravityScale = 0;
        _rb.velocity += _terrainDetection.mainNormal * jumpSpeed;
        yield return new WaitForSeconds(1f);
        
        _rb.gravityScale = gravityScale;
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

	public void Action2Performed()
	{
		
	}

	public void Action2Cancelled()
	{
		
	}

	public void PausePerformed()
	{
		
	}

	public void PauseCancelled()
	{
		
	}

	#region Weapons Testing

	public void Weapon1Performed()
	{
		
	}

	public void Weapon1Cancelled()
	{
		
	}

	public void Weapon2Performed()
	{
		
	}

	public void Weapon2Cancelled()
	{
		
	}

	public void Weapon3Performed()
	{
		
	}

	public void Weapon3Cancelled()
	{
		
	}

	public void MedKitPerformed()
	{
		
	}

	public void MedKitCancelled()
	{
		
	}

	#endregion

	public enum SoldierScenario
	{
		SeesTarget = 0,
		AllTargetsDead = 1,
		TargetWithinRange = 2,
		CanReachTarget = 3,
		CanAttack = 4,
		CanPatrol = 5
	}
	
	// Everything below is for Debugging and can be deleted eventually

	// private void Awake()
	// {
	// 	for (int i = 0; i < joints.Length; i++)
	// 	{
	// 		originalPositions[i] = joints[i].position;
	// 		frontFootPosition = frontFoot.position;
	// 		rearFootPosition = rearFoot.position;
	// 		clawPosition = claw.position;
	// 	}
	// }
	//
	// public bool debugMode = false;
	//
	// public Vector3[] originalPositions = new Vector3[13];
	// public Transform rearFoot;
	// public Transform frontFoot;
	// public Transform claw;
	// private Vector3 frontFootPosition;
	// private Vector3 rearFootPosition;
	// private Vector3 clawPosition;
	// public bool drawRearThighPath = true;
	// public bool drawRearCalfPath = true;
	// public bool drawRearFootPath = true;
	// public bool drawFrontThighPath = true;
	// public bool drawFrontCalfPath = true;
	// public bool drawFrontFootPath = true;
	// public bool drawShoulderPath = true;
	// public bool drawForearmPath = true;
	// public bool drawWristPath = true;
	// public bool drawClawPath = true;
	//
	// private void OnDrawGizmos()
	// {
	// 	if (!debugMode) return;
	// 	Gizmos.color = Color.red;
	// 	float radians1 = 0;
	// 	Quaternion rotation = transform.rotation;
	// 	for (int j = 1; j < 360; j += 5)
	// 	{
	// 		float radians2 = Mathf.Deg2Rad * j;
	// 		
	// 		// Rear Thigh
	// 		float angle1 = GetAngle(radians1, 0);
	// 		float angle2 = GetAngle(radians2, 0);
	//
	// 		Quaternion rotation1 = rotation*Quaternion.Euler(0, 0, Mathf.Rad2Deg * angle1);
	// 		Quaternion rotation2 = rotation*Quaternion.Euler(0, 0, Mathf.Rad2Deg * angle2);
	// 		
	// 		Vector3 diff1 = originalPositions[11] - originalPositions[0];
	// 		Vector3 pos1 = joints[0].position + rotation1 * diff1;
	// 		Vector3 pos2 = joints[0].position + rotation2 * diff1;
	// 		if (drawRearThighPath) Gizmos.DrawLine(pos1, pos2);
	//
	// 		//Rear Calf
	// 		diff1 = rotation1*(originalPositions[12] - originalPositions[11]);
	// 		Vector3 diff2 = rotation2*(originalPositions[12] - originalPositions[11]);
	// 		angle1 = GetAngle(radians1, 11);
	// 		angle2 = GetAngle(radians2, 11);
	// 		Quaternion rotation3 = Quaternion.Euler(0, 0, Mathf.Rad2Deg * angle1);
	// 		Quaternion rotation4 = Quaternion.Euler(0, 0, Mathf.Rad2Deg * angle2);
	// 		Vector3 pos3 = pos1 + rotation3*diff1;
	// 		Vector3 pos4 = pos2 + rotation4*diff2;
	// 		if (drawRearCalfPath) Gizmos.DrawLine(pos3, pos4);
	// 		
	// 		//Rear Foot
	// 		if (Application.isPlaying)
	// 		{
	// 			diff1 = rotation1*rotation3*(rearFootPosition-originalPositions[12]);
	// 			diff2 = rotation2*rotation4*(rearFootPosition-originalPositions[12]);
	// 		}
	// 		else
	// 		{
	// 			Vector3 position = rearFoot.position;
	// 			diff1 = rotation1*rotation3*(position - originalPositions[12]);
	// 			diff2 = rotation2*rotation4*(position - originalPositions[12]);
	// 		}
	// 		angle1 = GetAngle(radians1, 12);
	// 		angle2 = GetAngle(radians2, 12);
	// 		Quaternion rotation5 = Quaternion.Euler(0, 0, Mathf.Rad2Deg * angle1);
	// 		Quaternion rotation6 = Quaternion.Euler(0, 0, Mathf.Rad2Deg * angle2);
	// 		Vector3 pos5 = pos3 + rotation5*diff1;
	// 		Vector3 pos6 = pos4 + rotation6*diff2;
	// 		if (drawRearFootPath) Gizmos.DrawLine(pos5, pos6);
	// 		
	// 		// Front Thigh
	// 		angle1 = GetAngle(radians1, 0);
	// 		angle2 = GetAngle(radians2, 0);
	//
	// 		rotation1 = rotation*Quaternion.Euler(0, 0, Mathf.Rad2Deg * angle1);
	// 		rotation2 = rotation*Quaternion.Euler(0, 0, Mathf.Rad2Deg * angle2);
	// 		
	// 		diff1 = originalPositions[9] - originalPositions[0];
	// 		pos1 = joints[0].position + rotation1 * diff1;
	// 		pos2 = joints[0].position + rotation2 * diff1;
	// 		if (drawFrontThighPath) Gizmos.DrawLine(pos1, pos2);
	// 		
	// 		// Front Calf
	// 		diff1 = rotation1*(originalPositions[10] - originalPositions[9]);
	// 		diff2 = rotation2*(originalPositions[10] - originalPositions[9]);
	// 		angle1 = GetAngle(radians1, 9);
	// 		angle2 = GetAngle(radians2, 9);
	// 		rotation3 = Quaternion.Euler(0, 0, Mathf.Rad2Deg * angle1);
	// 		rotation4 = Quaternion.Euler(0, 0, Mathf.Rad2Deg * angle2);
	// 		pos3 = pos1 + rotation3*diff1;
	// 		pos4 = pos2 + rotation4*diff2;
	// 		if (drawFrontCalfPath) Gizmos.DrawLine(pos3, pos4);
	// 		
	// 		//Front Foot
	// 		if (Application.isPlaying)
	// 		{
	// 			diff1 = rotation1*rotation3*(frontFootPosition-originalPositions[10]);
	// 			diff2 = rotation2*rotation4*(frontFootPosition-originalPositions[10]);
	// 		}
	// 		else
	// 		{
	// 			Vector3 position = frontFoot.position;
	// 			diff1 = rotation1*rotation3*(position - originalPositions[10]);
	// 			diff2 = rotation2*rotation4*(position - originalPositions[10]);
	// 		}
	// 		angle1 = GetAngle(radians1, 10);
	// 		angle2 = GetAngle(radians2, 10);
	// 		rotation5 = Quaternion.Euler(0, 0, Mathf.Rad2Deg * angle1);
	// 		rotation6 = Quaternion.Euler(0, 0, Mathf.Rad2Deg * angle2);
	// 		pos5 = pos3 + rotation5*diff1;
	// 		pos6 = pos4 + rotation6*diff2;
	// 		if (drawFrontFootPath) Gizmos.DrawLine(pos5, pos6);
	// 		
	// 		// Shoulder
	// 		angle1 = GetAngle(radians1, 0);
	// 		angle2 = GetAngle(radians2, 0);
	//
	// 		rotation1 = rotation*Quaternion.Euler(0, 0, Mathf.Rad2Deg * angle1);
	// 		rotation2 = rotation*Quaternion.Euler(0, 0, Mathf.Rad2Deg * angle2);
	// 		
	// 		diff1 = originalPositions[3] - originalPositions[0];
	// 		pos1 = joints[0].position + rotation1 * diff1;
	// 		pos2 = joints[0].position + rotation2 * diff1;
	// 		if (drawShoulderPath) Gizmos.DrawLine(pos1, pos2);
	// 		
	// 		// Forearm
	// 		diff1 = rotation1*(originalPositions[4] - originalPositions[3]);
	// 		diff2 = rotation2*(originalPositions[4] - originalPositions[3]);
	// 		angle1 = GetAngle(radians1, 3);
	// 		angle2 = GetAngle(radians2, 3);
	// 		rotation3 = Quaternion.Euler(0, 0, Mathf.Rad2Deg * angle1);
	// 		rotation4 = Quaternion.Euler(0, 0, Mathf.Rad2Deg * angle2);
	// 		pos3 = pos1 + rotation3*diff1;
	// 		pos4 = pos2 + rotation4*diff2;
	// 		if (drawForearmPath) Gizmos.DrawLine(pos3, pos4);
	// 		
	// 		// Wrist
	// 		diff1 = rotation1*rotation3*(originalPositions[5] - originalPositions[4]);
	// 		diff2 = rotation2*rotation4*(originalPositions[5] - originalPositions[4]);
	// 		angle1 = GetAngle(radians1, 4);
	// 		angle2 = GetAngle(radians2, 4);
	// 		rotation5 = Quaternion.Euler(0, 0, Mathf.Rad2Deg * angle1);
	// 		rotation6 = Quaternion.Euler(0, 0, Mathf.Rad2Deg * angle2);
	// 		pos5 = pos3 + rotation5*diff1;
	// 		pos6 = pos4 + rotation6*diff2;
	// 		if (drawWristPath) Gizmos.DrawLine(pos5, pos6);
	// 		
	// 		// Claw
	// 		if (Application.isPlaying)
	// 		{
	// 			diff1 = rotation1*rotation3*rotation5*(clawPosition-originalPositions[5]);
	// 			diff2 = rotation2*rotation4*rotation6*(clawPosition-originalPositions[5]);
	// 		}
	// 		else
	// 		{
	// 			Vector3 position = claw.position;
	// 			diff1 = rotation1*rotation3*rotation5*(position - originalPositions[5]);
	// 			diff2 = rotation2*rotation4*rotation6*(position - originalPositions[5]);
	// 		}
	// 		angle1 = GetAngle(radians1, 5);
	// 		angle2 = GetAngle(radians2, 5);
	// 		rotation5 = Quaternion.Euler(0, 0, Mathf.Rad2Deg * angle1);
	// 		rotation6 = Quaternion.Euler(0, 0, Mathf.Rad2Deg * angle2);
	// 		pos5 += rotation5*diff1;
	// 		pos6 += rotation6*diff2;
	// 		if (drawClawPath) Gizmos.DrawLine(pos5, pos6);
	// 		
	// 		radians1 = radians2;
	// 	}
	// }
	//
	// private float GetAngle(float angle, int index) => Mathf.Deg2Rad *
	//                                                   (Mathf.Sin(angle * motionFrequencies[index] +
	//                                                              motionPhaseOffsets[index] * 2 * Mathf.PI) *
	//                                                    motionAmplitudes[index]);
}
