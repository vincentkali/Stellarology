using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pushgravity : MonoBehaviour
{
    public float gravityForce = 1f;

    public bool MachineOn;

    private PlayerMove playerMove;

    public float gravityRadius;

    public float angularChangeInDegrees = 50.0f;

    //public bool canDrawLine = false;
    //private LineRenderer line;

    void Start()
    {
        MachineOn = false;
        gravityRadius = this.gameObject.transform.GetChild(2).gameObject.GetComponent<CircleCollider2D>().radius;

        Debug.Log("gravity radius: " + gravityRadius.ToString());
        playerMove = GameObject.Find("Player").GetComponent<PlayerMove>();

        //line = this.gameObject.GetComponent<LineRenderer>();

    }

    void FixedUpdate()
    {
        Vector2 gravityPosition = new Vector2(transform.position.x, transform.position.y);
        Vector2 playerPosition = new Vector2(playerMove.transform.position.x, playerMove.transform.position.y);
        Vector2 gravityDirection = gravityPosition - playerPosition;

        //if (!(playerMove.playerStage == "OnGravity") || !(gravityDirection.magnitude <= gravityRadius)){
        //    MachineOn = false;

        //    playerMove.rb2D.freezeRotation = true;
        //canDrawLine = false;
        //}

        if (playerMove.playerStage == "OnGround")
        {
            MachineOn = false;
        }


        if (MachineOn)
        {
            //gravityPosition = new Vector2(transform.position.x, transform.position.y);
            //playerPosition = new Vector2(playerMove.transform.position.x, playerMove.transform.position.y);
            //gravityDirection = gravityPosition - playerPosition;
            playerMove.rb2D.AddForce(gravityDirection * gravityForce * Time.deltaTime * -1);
        }
        //if (canDrawLine) DrawLine();
        //else line.positionCount = 0;
    }
    void OnMouseDown()
    {
        Vector2 gravityPosition = new Vector2(transform.position.x, transform.position.y);
        Vector2 playerPosition = new Vector2(playerMove.transform.position.x, playerMove.transform.position.y);
        Vector2 gravityDirection = gravityPosition - playerPosition;

        // Debug.Log("gravityRadius = " + gravityRadius.ToString());
        // Debug.Log("distance = " + gravityDirection.magnitude.ToString());

        Debug.Log("onmousedown");
        if (playerMove.playerStage == "OnSpace" && gravityDirection.magnitude <= gravityRadius)
        {
            FindObjectOfType<AudioManager>().Play("Gravity");

            MachineOn = true;
            playerMove.rb2D.freezeRotation = false;
            var impulse = (angularChangeInDegrees * Mathf.Deg2Rad) * playerMove.rb2D.inertia;

            //playerMove.rb2D.AddTorque(impulse, ForceMode2D.Impulse);
            //canDrawLine = true;

            playerMove.playerStage = "OnGravity";
        }
    }

    void OnMouseUp()
    {
        Debug.Log("onmouseup");
        FindObjectOfType<AudioManager>().Stop("Gravity");
        if (playerMove.playerStage == "OnGravity")
        {
            

            MachineOn = false;

            playerMove.rb2D.freezeRotation = true;
            //canDrawLine = false;
            playerMove.playerStage = "OnSpace";
        }

    }
    //void DrawLine()
    //{
    //    line.material.color = Color.white;
    //    line.positionCount = 2;
    //    line.SetPosition(0, this.gameObject.transform.position);
    //    line.SetPosition(1, playerMove.gameObject.transform.position);
    //}
}