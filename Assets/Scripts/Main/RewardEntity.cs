using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardEntity : MonoBehaviour
{
    void OnTriggerEnter2D (Collider2D other)
    {
        FindObjectOfType<AudioManager>().Play("Diamond");
        GameObject.Destroy(this.gameObject);
        //Debug.Log("diamond");

    }
}
