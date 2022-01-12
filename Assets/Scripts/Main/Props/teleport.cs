using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleport : MonoBehaviour
{
    // Start is called before the first frame update

    Transform tele1;
    Transform tele2;
    Transform player;
    bool CanTele;
    teleport TeleObj;
    

    void Start() {
        tele1 = GameObject.Find("teleport1").GetComponent<Transform>();
        tele2 = GameObject.Find("teleport2").GetComponent<Transform>();
        player = GameObject.Find("Player").GetComponent<Transform>();

        if (gameObject.name == "teleport1"){
            TeleObj = GameObject.Find("teleport2").GetComponent<teleport>();
        }
        else {
            TeleObj = GameObject.Find("teleport1").GetComponent<teleport>();
        }
        CanTele = true;
    }
    
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "Player" && CanTele){

            FindObjectOfType<AudioManager>().Play("Teleport");

            if (gameObject.name == "teleport1"){
                TeleObj.CanTele = false;
                player.position = tele2.position;
                
            }
            else {
                TeleObj.CanTele = false;
                player.position = tele1.position;
            }
        }

    }

    void OnTriggerExit2D(Collider2D col)
    {
        CanTele = true;
    }
    
}
