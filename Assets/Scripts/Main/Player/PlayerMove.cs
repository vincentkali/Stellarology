using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    public Vector3 currentJumpDirection;
    //public static Vector3 Direction;

    public string playerStage;
    public float jumpForce = 50f;
    public Rigidbody2D rb2D;
    //public Text gameOver;
    // public gravity gra;
    //public Transform? gravityMachines;
    //public GameObject[] gravity_machine;
    //public Gravity gravity;

    public bool Draw_Arrow;
    public float MaxLineLength = 15.0f;
    public float MinLineLength = 3.0f;
    public float MaxAngle;
    public float MinAngle;
    public float MaxLineAngle = 80.0f;
    public float MinLineAngle = -80.0f;

    private Vector3 mousePosStart;
    private Vector3 mousePosEnd;
    public Transform Arrow_Start;
    public Transform Arrow_End;
    public SpriteRenderer Arrow;
    public float collidForce = 10.0f;
    private Vector3 StartingPosition;
    private Quaternion StartingRotation;

    PlayerSprite playerSprite;
    public int protectCount;


    void Start()
    {
        playerStage = "OnGround";
        Draw_Arrow = false;
        rb2D = GetComponent<Rigidbody2D>();
        //gameOver.enabled = false;
        MaxAngle = MaxLineAngle;
        MinAngle = MinLineAngle;
        StartingPosition = transform.position;
        StartingRotation = transform.rotation;

        playerSprite = gameObject.transform.GetChild(3).gameObject.GetComponent<PlayerSprite>();
        protectCount = 3;
    }

    public void Move()
    {
        currentJumpDirection = rb2D.velocity;
        if (playerStage == "OnSpace")
        {
            FaceDirection(currentJumpDirection);
        }

        //??????
        if (Input.GetMouseButtonDown(0) && playerStage == "OnGround" && Draw_Arrow == false && !EventSystem.current.IsPointerOverGameObject())
        {
            Draw_Arrow = true;
            mousePosStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Arrow.enabled = true;

        }

        //????
        else if (Input.GetMouseButtonUp(0) && playerStage == "OnGround" && Draw_Arrow == true && !EventSystem.current.IsPointerOverGameObject())
        {
            Draw_Arrow = false;
            mousePosEnd = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 jumpDirection = Arrow_End.position - Arrow_Start.position;

            //FaceDirection(jumpDirection);

            rb2D.AddForce(jumpDirection * jumpForce);
            currentJumpDirection = jumpDirection;
            playerStage = "OnSpace";
            protectCount = 3;
            Debug.Log("playerStage = 'OnSpace';");
            Arrow.enabled = false;

            FindObjectOfType<AudioManager>().Play("Jump");

        }


        if (Draw_Arrow == true)
        {
            // adjust ARROW end position
            Vector3 mouseNow = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Arrow_End.position = Arrow_Start.position + (mouseNow - mousePosStart);

            // limit ARROW in [MinAngle,MaxAngle], [MinLength, MaxLength]
            Vector3 direction = Arrow_End.position - Arrow_Start.position;
            float[] ret = Vector2Polar(direction);

            float length = ret[0];
            float degrees = ret[1];
            //Debug.Log("Origin degrees: " + degrees.ToString());

            if (transform.rotation.eulerAngles.z == 180.0f)
            {
                if (degrees > MaxAngle && degrees < 0)
                {
                    degrees = MaxAngle;
                }
                else if (degrees < MinAngle && degrees >= 0)
                {
                    degrees = MinAngle;
                }
            }
            else if (transform.rotation.eulerAngles.z == 0.0f)
            {
                if (degrees > MaxAngle)
                {
                    degrees = MaxAngle;
                }
                else if (degrees < MinAngle)
                {
                    degrees = MinAngle;
                }
            }
            else if (transform.rotation.eulerAngles.z == 90.0f)
            {
                if (degrees > MaxAngle || (degrees >= -180.0f && degrees <= -90.0f))
                {
                    degrees = MaxAngle;
                }
                else if (degrees < MinAngle)
                {
                    degrees = MinAngle;
                }
            }
            else if (transform.rotation.eulerAngles.z == 270.0f)
            {
                if (degrees < MinAngle || (degrees < 180.0f && degrees >= 90.0f))
                {
                    degrees = MinAngle;
                }
                else if (degrees > MaxAngle)
                {
                    degrees = MaxAngle;
                }
            }


            length = Mathf.Min(length, MaxLineLength);
            length = Mathf.Max(length, MinLineLength);

            Arrow_End.position = Arrow_Start.position + Polar2Vector(degrees, length);



            // adjust ARROW rotation
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
            //Arrow_End.eulerAngles = Vector3.forward * angle;
            Arrow_End.eulerAngles = Vector3.forward * degrees;
        }
        else
        {
            Arrow_End.position = Arrow_Start.position;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground" && protectCount == 0)
        {
            FindObjectOfType<AudioManager>().Play("Landing");

            rb2D.velocity = Vector2.zero;
            rb2D.angularVelocity = 0;
            playerStage = "OnGround";
            // Debug.Log(collision.GetContact(0).point);
            // Debug.Log(transform.position);
            float x_diff = Mathf.Abs(collision.GetContact(0).point.x - transform.position.x);
            float y_diff = Mathf.Abs(collision.GetContact(0).point.y - transform.position.y);
            if (collision.GetContact(0).point.x < transform.position.x && x_diff > y_diff)//?????
            {
                gameObject.transform.rotation = Quaternion.identity;
                gameObject.transform.Rotate(0.0f, 0.0f, -90.0f, Space.World);
            }
            else if (collision.GetContact(0).point.x > transform.position.x && x_diff > y_diff)
            {
                gameObject.transform.rotation = Quaternion.identity;
                gameObject.transform.Rotate(0.0f, 0.0f, 90.0f, Space.World);
            }
            else if (collision.GetContact(0).point.y < transform.position.y && x_diff < y_diff)
            {
                gameObject.transform.rotation = Quaternion.identity;
                //gameObject.transform.Rotate(0.0f, 0.0f, 0.0f, Space.World);
            }
            else if (collision.GetContact(0).point.y > transform.position.y && x_diff < y_diff)
            {
                gameObject.transform.rotation = Quaternion.identity;
                gameObject.transform.Rotate(0.0f, 0.0f, 180.0f, Space.World);
            }

            MaxAngle = MaxLineAngle + transform.rotation.eulerAngles.z;
            MinAngle = MinLineAngle + transform.rotation.eulerAngles.z;
            //Debug.Log("MaxLineAngle: " + MaxAngle.ToString());
            //Debug.Log("MinLineAngle: " + MinAngle.ToString());

            if (MaxAngle > 180)
                MaxAngle -= 360;
            else if (MaxAngle < -180)
                MaxAngle += 360;

            if (MinAngle > 180)
                MinAngle -= 360;
            else if (MinAngle < -180)
                MinAngle += 360;
        }
        else if (collision.collider.tag == "Obstacle")
        {
            Lose();
        }
        else if (collision.collider.tag == "MachineButton")
        {
            Gravity gravity = collision.gameObject.GetComponent<Gravity>();
            gravity.canDrawLine = false;
            gravity.MachineOn = false;
            Lose();
        }
        else if (collision.collider.tag == "PushMachine")
        {
            Pushgravity PushgGavity = collision.gameObject.GetComponent<Pushgravity>();
            PushgGavity.MachineOn = false;
            Lose();
        }
    }
    void FixedUpdate()
    {
        if (protectCount > 0 && playerStage == "OnSpace")
        {
            protectCount--;
            Debug.Log(protectCount);
        }
    }
    private Vector3 Polar2Vector(float degrees, float length)
    {
        float radians = degrees * -1 * Mathf.Deg2Rad;
        return new Vector3(Mathf.Sin(radians), Mathf.Cos(radians), 0.0f) * length;
    }
    private float[] Vector2Polar(Vector3 vector)
    {
        float length = vector.magnitude;
        //float degrees = Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg - transform.rotation.eulerAngles.z - 90;
        float degrees = Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg - 90;
        if (degrees > 180)
        {
            degrees -= 360;
        }
        else if (degrees < -180)
        {
            degrees += 360;
        }
        float[] ret = new float[2];
        ret[0] = length;
        ret[1] = degrees;
        return ret;
    }

    public void FaceDirection(Vector3 jumpDirection)
    {
        float jumpAngle = Mathf.Atan2(jumpDirection.y, jumpDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.identity;
        transform.Rotate(Vector3.forward * jumpAngle);
        transform.Rotate(0, 0, -90f);
        //Debug.Log("rotate~~~");
    }

    public void Lose()
    {
        // gameOver.enabled = true;
        // this.rb2D.bodyType = RigidbodyType2D.Static;

        FindObjectOfType<AudioManager>().Play("Dead");

        rb2D.velocity = Vector2.zero;
        rb2D.angularVelocity = 0;
        playerStage = "OnGround";

        GameObject.Find("Main Camera").GetComponent<LoseCameraEffect>().lose = true;
        transform.position = StartingPosition;
        transform.rotation = StartingRotation;
        transform.localScale = playerSprite.OriginalScale;
        playerSprite.Scaled = 0;
        playerSprite.ModifiedScale = playerSprite.OriginalScale;

    }

}