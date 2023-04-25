using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class ResidentialElevator : MonoBehaviour
{
	public GameObject elevatorSFX;

	[SerializeField] float moveSpeed = 0.1f;
    bool moving = false;
    bool movingUp = false;

    [SerializeField] Transform elevatorTransform;

    private Vector3 startingPosition = new Vector3(0, -0.5f, 0);
    public Vector3 targetPosition;
    
    private void OnTriggerEnter2D( Collider2D collision )
    {
        if (collision.gameObject.layer == 9 )
        {
            movingUp = true;
            moving = true;
        }
    }

    private void OnTriggerExit2D( Collider2D collision )
    {
        if( collision.gameObject.layer == 9 )
        {
            movingUp = false;
            moving = true;
        }
    }

    private void FixedUpdate()
    {
        if( moving )
        {
            MoveElevator();
        }
    }

    private void MoveElevator()
    {
        //will run when player ENTERS hitbox and elevator is lower than target position
        if( movingUp )
        {
            //as long as this is happening, play elevator moving sound
            if( elevatorTransform.localPosition.y < targetPosition.y )
            {
                elevatorTransform.localPosition += new Vector3(0, moveSpeed, 0);
                elevatorSFX.SetActive(true);
            } else
            {
                elevatorSFX.SetActive(false);
                moving = false;
            }
        } else
        {
            //will run when player EXITS hitbox and elevator is higher than starting position
            if( elevatorTransform.localPosition.y > startingPosition.y )
            {
                elevatorTransform.localPosition -= new Vector3(0, moveSpeed, 0);
                elevatorSFX.SetActive(true);
            } else
            {
                elevatorSFX.SetActive(false);
                moving = false;
            }
        }
    }
}
