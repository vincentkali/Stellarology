using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprite : MonoBehaviour
{
    PlayerMove playerMove;
    SpriteRenderer m_spriteRenderer;
    GameObject Player;
    public Sprite Idle, Flying, Arrow, Gravity;

    public Vector3 OriginalScale;
    public Vector3 ModifiedScale;
    public float MicroScale = 0.8f;//For Gravity
    public bool HadBeenScaled = false;
    public int Scaled = 0;

    // Start is called before the first frame update
    void Start()
    {
        Player = this.gameObject.transform.parent.gameObject;
        playerMove = Player.GetComponent<PlayerMove>();
        m_spriteRenderer = Player.transform.GetChild(3).gameObject.GetComponent<SpriteRenderer>();
        OriginalScale = Player.transform.localScale;
        ModifiedScale = OriginalScale;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(playerMove.playerStage == "OnGround" && playerMove.Draw_Arrow == false)
        {
            m_spriteRenderer.sprite = Idle;
            Player.transform.localScale = ModifiedScale;
        }
        if(playerMove.playerStage == "OnGround" && playerMove.Draw_Arrow == true)
        {
            m_spriteRenderer.sprite = Arrow;
            Player.transform.localScale = ModifiedScale;
        }
        if(playerMove.playerStage == "OnSpace")
        {
            m_spriteRenderer.sprite = Flying;
            Player.transform.localScale = ModifiedScale;
        }
        if (playerMove.playerStage == "OnGravity")
        {
            m_spriteRenderer.sprite = Gravity;
            if(Scaled != -1) Player.transform.localScale = ModifiedScale * MicroScale;
        }
        if(playerMove.playerStage == "OnMachine")
        {
            Player.transform.localScale = ModifiedScale;
        }
    }
}
