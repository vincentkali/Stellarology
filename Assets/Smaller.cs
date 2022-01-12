using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

public class Smaller : MonoBehaviour
{
    GameObject Player;
    PlayerMove playerMove;
    PlayerSprite playerSprite;
    [SerializeField] LayerMask PlayerCenter;

    Vector3 CenterDirection;
    Vector3 ExitDirection;
    float x;
    float y;

    bool HasPassed = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("Main Camera").GetComponent<LoseCameraEffect>().lose) HasPassed = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerMove = collision.gameObject.GetComponent<PlayerMove>();
            Player = collision.gameObject;
            playerSprite = Player.transform.GetChild(3).gameObject.GetComponent<PlayerSprite>();
            playerMove.playerStage = "OnMachine";

            if (playerMove != null && playerSprite.Scaled != -1)
            {

                CenterDirection = transform.position - Player.transform.position;
                x = -CenterDirection.x;
                y = -CenterDirection.y;
                ExitDirection = CenterDirection;


                playerMove.rb2D.AddForce(-playerMove.currentJumpDirection * playerMove.jumpForce);
                playerMove.rb2D.AddForce(CenterDirection * playerMove.jumpForce);

                Debug.Log("Enter");
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        //playerMove = collision.gameObject.GetComponent<PlayerMove>();
        //Player = collision.gameObject;
        //playerSprite = Player.transform.GetChild(3).gameObject.GetComponent<PlayerSprite>();
        if (playerMove != null)
        {
            if (Physics2D.OverlapCircle(this.gameObject.transform.position, 0.1f, PlayerCenter) && !HasPassed)
            {
                //Debug.Log("HasPassed: " + HasPassed);
                FindObjectOfType<AudioManager>().Play("Smaller");


                StartCoroutine(SmallerSize(playerMove, playerSprite));
                HasPassed = true;

                if ((x > y && x > -y) || (x < y && x < -y))
                {
                    ExitDirection.y = 0;
                }
                else if ((x < y && x > -y) || (x > y && x < -y))
                {
                    ExitDirection.x = 0;
                }

            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerMove.playerStage = "OnSpace";
        }
    }

    IEnumerator SmallerSize(PlayerMove playerMove_script, PlayerSprite playerSprite_script)
    {
        Debug.Log("smaller");

        playerMove_script.rb2D.AddForce(-playerMove_script.currentJumpDirection * playerMove_script.jumpForce);


        yield return new WaitForSeconds(0.2f);
        playerSprite_script.ModifiedScale /= 1.145f;
        yield return new WaitForSeconds(0.2f);
        playerSprite_script.ModifiedScale /= 1.145f;
        yield return new WaitForSeconds(0.2f);
        playerSprite_script.ModifiedScale /= 1.145f;

        playerSprite_script.Scaled = -1;

        playerMove_script.rb2D.AddForce(ExitDirection * playerMove_script.jumpForce);


    }
}
