using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using UnityEngine;

public class Boss : EnemyBody, IControllable, ISense
{
	
	
	public void CollectConditions(AntAIAgent aAgent, AntAICondition aWorldState)
	{
		aWorldState.Set(BossScenario.CanAttack, false);
		aWorldState.Set(BossScenario.CanRun, false);
		aWorldState.Set(BossScenario.CanShoot, false);
		aWorldState.Set(BossScenario.TargetInMeleeRange, false);
		aWorldState.Set(BossScenario.TargetIsBelowHead, false);
		aWorldState.Set(BossScenario.TargetIsDead, false);
	}

    public void MovePerformed(float lateralInput)
    {
	    
    }

    public void MoveCancelled()
    {
	    
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

    public void Action3Performed()
    {
	    
    }

    public void Action3Cancelled()
    {
	    
    }

    public void PausePerformed()
    {
	    
    }

    public void PauseCancelled()
    {
	    
    }

    public void ResumePerformed()
    {
	    
    }

    public void ResumeCancelled()
    {
	    
    }

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

    public void WeaponScrollPerformed()
    {
	    
    }

    public void WeaponScrollCancelled()
    {
	    
    }

    public void MedKitPerformed()
    {
	    
    }

    public void MedKitCancelled()
    {
	    
    }

    public void SprintPerformed()
    {
	    
    }

    public void SprintCancelled()
    {
	    
    }
    
    public enum BossScenario
    {
	    TargetIsDead = 0,
	    TargetIsBelowHead = 1,
	    TargetInMeleeRange = 2,
	    CanRun = 3,
	    CanShoot = 4,
	    CanAttack = 5
    }
}