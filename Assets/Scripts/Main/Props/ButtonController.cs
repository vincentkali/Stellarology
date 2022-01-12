using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ButtonController : MonoBehaviour
{
    // Start is called before the first frame update
    private bool toLeft;
    private bool toRight;
    private string stage; // middle, left, right
    private Vector3 temp;
    public float smoothSpeed = 2.0f;
    public float duration = 1.0f;
    public float moveDistance = 9f;
    private bool getStartTime;
    private float startTime;
    private Vector3 originPosition;
    private GameObject Player;
    private float diff;
    public float objectHeight;
    public float littleError = 0.05f;

    PlayerSprite playerSprite;


    void Start()
    {
        toLeft = toRight = false;
        getStartTime = false;
        stage = "middle";
        originPosition = transform.position;
        Player = GameObject.Find("Player");
        playerSprite = Player.transform.GetChild(3).gameObject.GetComponent<PlayerSprite>();


    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player" && playerSprite.Scaled == 1)
        // if (collision.transform.tag == "Player") // for testing
        {
            foreach (ContactPoint2D missileHit in collision.contacts)
            {
                Debug.Log(transform.position);
                Vector2 hitPoint = missileHit.point;
                Debug.Log(hitPoint);
                if (hitPoint.x >= transform.position.x &&
                    hitPoint.y >= transform.position.y - objectHeight + littleError &&
                    hitPoint.y <= transform.position.y + objectHeight - littleError)
                {
                    Debug.Log("right hit");
                    if (stage != "right")
                    {
                        toLeft = true;
                        FindObjectOfType<AudioManager>().Play("ButtonSet");
                    }
                }
                else if (hitPoint.x <= transform.position.x &&
                    hitPoint.y >= transform.position.y - objectHeight + littleError &&
                    hitPoint.y <= transform.position.y + objectHeight - littleError)
                {
                    Debug.Log("left hit");
                    if (stage != "left")
                    {
                        toRight = true;
                        FindObjectOfType<AudioManager>().Play("ButtonSet");
                    }
                }
                else
                {
                    Debug.Log("up/low hit");
                }
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("Main Camera").GetComponent<LoseCameraEffect>().lose) transform.position = originPosition;
        // in the first frame, we need to get the start time to know how many time passed
        // (toLeft || toRight), with this condition, mean the move begin
        if (!getStartTime && (toLeft || toRight))
        {
            startTime = Time.time;
            getStartTime = true;
        }

        if (toLeft && getStartTime)
        {
            if (Time.time - startTime < duration)
            {
                //FindObjectOfType<AudioManager>().Play("ButtonSet");

                temp = transform.position;
                temp.x = Mathf.Lerp(temp.x, originPosition.x - moveDistance, smoothSpeed * Time.deltaTime);
                diff = temp.x - transform.position.x;
                transform.position = temp;

                // player also nee to move, and move the same distance
                temp = Player.transform.position;
                temp.x += diff;
                Player.transform.position = temp;
            }
            else
            {
                temp = transform.position;
                temp.x = originPosition.x - moveDistance;
                diff = temp.x - transform.position.x;
                transform.position = temp;

                temp = Player.transform.position;
                temp.x += diff;
                Player.transform.position = temp;

                getStartTime = false;
                toLeft = false;
            }
        }
        else if (toRight && getStartTime)
        {
            if (Time.time - startTime < duration)
            {
                //FindObjectOfType<AudioManager>().Play("Dead");

                temp = transform.position;
                temp.x = Mathf.Lerp(temp.x, originPosition.x + moveDistance, smoothSpeed * Time.deltaTime);
                diff = temp.x - transform.position.x;
                transform.position = temp;


                temp = Player.transform.position;
                temp.x += diff;
                Player.transform.position = temp;
            }
            else
            {
                temp = transform.position;
                temp.x = originPosition.x + moveDistance;
                diff = temp.x - transform.position.x;
                transform.position = temp;

                temp = Player.transform.position;
                temp.x += diff;
                Player.transform.position = temp;

                getStartTime = false;
                toRight = false;
            }
        }
    }
}
