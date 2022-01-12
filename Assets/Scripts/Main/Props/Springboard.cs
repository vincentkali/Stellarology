using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Springboard : MonoBehaviour
{
    //public bool isUpDown;

    Vector3 playerReflectDirection;
    float PLAYER_JUMP_FORCE = 50f;

    //void OnTriggerEnter2D (Collider2D other)
    //{
    //    PlayerMove player = other.gameObject.GetComponent<PlayerMove>();
    //    if (player != null)
    //    {
    //        playerReflectDirection = player.currentJumpDirection;

    //        if (isUpDown) playerReflectDirection.y = -playerReflectDirection.y;
    //        else playerReflectDirection.x = -playerReflectDirection.x;

    //        player.Rotate(playerReflectDirection);

    //        player.rb2D.AddForce(-player.currentJumpDirection * PLAYER_JUMP_FORCE);
    //        player.rb2D.AddForce(playerReflectDirection * PLAYER_JUMP_FORCE);

    //    }
    //}

    private void OnCollisionEnter2D(Collision2D collision)
    {

        PlayerMove player = collision.gameObject.GetComponent<PlayerMove>();
        if (player != null)
        {
            FindObjectOfType<AudioManager>().Play("SpringBoard");

            playerReflectDirection = player.currentJumpDirection;

            float x_diff = Mathf.Abs(collision.GetContact(0).point.x - player.gameObject.transform.position.x);
            float y_diff = Mathf.Abs(collision.GetContact(0).point.y - player.gameObject.transform.position.y);
            if (collision.GetContact(0).point.x < player.gameObject.transform.position.x && x_diff > y_diff)//頭朝右
            {
                playerReflectDirection.x = -playerReflectDirection.x;
            }
            else if (collision.GetContact(0).point.x > player.gameObject.transform.position.x && x_diff > y_diff)
            {
                playerReflectDirection.x = -playerReflectDirection.x;
            }
            else if (collision.GetContact(0).point.y < player.gameObject.transform.position.y && x_diff < y_diff)
            {
                playerReflectDirection.y = -playerReflectDirection.y;
            }
            else if (collision.GetContact(0).point.y > player.gameObject.transform.position.y && x_diff < y_diff)
            {
                playerReflectDirection.y = -playerReflectDirection.y;
            }

            //player.Rotate(playerReflectDirection);

            // player.rb2D.AddForce(-player.currentJumpDirection * PLAYER_JUMP_FORCE);
            player.rb2D.velocity = new Vector3(0, 0, 0);
            player.rb2D.AddForce(playerReflectDirection * PLAYER_JUMP_FORCE);
        }
    }
}