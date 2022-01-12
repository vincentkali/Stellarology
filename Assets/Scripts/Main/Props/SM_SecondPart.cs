using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SM_SecondPart : MonoBehaviour
{
    [SerializeField] SM_FirstPart firstPart;
    
    public float scale = 0.5f;

    float PLAYER_JUMP_FORCE = 50f;



    void Start()
    {
        this.gameObject.GetComponent<Collider2D>().isTrigger = false;
    }

    void Update()
    {
        if (firstPart.accessFirstPart)
        {
            this.gameObject.GetComponent<Collider2D>().isTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerMove playerMove = collision.gameObject.GetComponent<PlayerMove>();
        
        if (playerMove != null)
        {
            this.gameObject.GetComponent<Collider2D>().isTrigger = false;
            firstPart.accessFirstPart = false;

            //scalePlayer(playerMove, scale);

            float angle = this.gameObject.transform.parent.transform.eulerAngles.z;
            Vector3 TowardDirection = new Vector3(-Mathf.Sin(angle * Mathf.Deg2Rad), Mathf.Cos(angle * Mathf.Deg2Rad), 0);

            Debug.Log(TowardDirection);

            playerMove.rb2D.AddForce(-playerMove.currentJumpDirection * PLAYER_JUMP_FORCE);
            playerMove.rb2D.AddForce(TowardDirection * playerMove.currentJumpDirection.magnitude * PLAYER_JUMP_FORCE);

        }
    }

    //private void scalePlayer(PlayerMove player, float size)
    //{
        
    //    player.gameObject.transform.localScale = player.gameObject.transform.localScale * size;
    //}
}