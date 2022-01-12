using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SM_FirstPart : MonoBehaviour
{
    PlayerMove playerMove;
    PlayerSprite playerSprite;
    public bool accessFirstPart = false;
    
    float angle;

    float PLAYER_JUMP_FORCE = 50f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerMove = collision.gameObject.GetComponent<PlayerMove>();
        playerSprite = collision.gameObject.transform.GetChild(3).gameObject.GetComponent<PlayerSprite>();
        if (playerMove != null)
        {
            accessFirstPart = true;

            angle = this.gameObject.transform.parent.transform.eulerAngles.z;
            Vector3 TowardDirection = new Vector3(-Mathf.Sin(angle * Mathf.Deg2Rad), Mathf.Cos(angle * Mathf.Deg2Rad), 0);

            playerMove.rb2D.AddForce(-playerMove.currentJumpDirection * PLAYER_JUMP_FORCE);
            playerMove.rb2D.AddForce(TowardDirection * playerMove.currentJumpDirection.magnitude * PLAYER_JUMP_FORCE);

            //playerMove.Rotate(TowardDirection);

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        

        playerSprite.Scaled = -1;

        playerSprite.ModifiedScale *= 0.5f;
        
    }
}
