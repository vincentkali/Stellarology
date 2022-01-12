using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckInCamera : MonoBehaviour
{
    //public Text gameOver;
    PlayerMove playerMove;

    void Start()
    {
        playerMove = gameObject.transform.parent.gameObject.GetComponent<PlayerMove>();
        //gameOver.enabled = false;
    }
    void OnBecameInvisible()
    {
        Debug.Log("out");
        //gameOver.enabled = true;
        playerMove.Lose();
    }
}
