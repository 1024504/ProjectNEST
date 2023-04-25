using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResidentialElevator : MonoBehaviour
{
    [SerializeField] float moveSpeed = 0.1f;
    bool movingUp = false;

    [SerializeField] Transform elevatorTransform;

    private Vector3 startingPosition = new Vector3(0, -0.5f, 0);
    public Vector3 targetPosition;
    
    private void OnTriggerEnter2D( Collider2D collision )
    {
        if (collision.gameObject.layer == 9 )
        {
            movingUp = true;
        }
    }

    private void OnTriggerExit2D( Collider2D collision )
    {
        if( collision.gameObject.layer == 9 )
        {
            movingUp = false;
        }
    }

    private void Update()
    {
        //will run when player ENTERS hitbox and elevator is lower than target position
        if( movingUp && elevatorTransform.localPosition.y < targetPosition.y)
        {
            elevatorTransform.localPosition += new Vector3(0, moveSpeed, 0);
            //as long as this is happening, play elevator moving sound
        }

        //will run when player EXITS hitbox and elevator is higher than starting position
        if( !movingUp && elevatorTransform.localPosition.y > startingPosition.y )
        {
            elevatorTransform.localPosition -= new Vector3(0, moveSpeed, 0);
            //as long as this is happening, play elevator moving sound
        }
    }
}
