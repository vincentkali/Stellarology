using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private const float MOVE_SPEED = 15f;
    private int _rewardCount = 0;
    private bool _hasReachedDestination = false;

    public int RewardCount
    {
        get => _rewardCount;

        set
        {
            _rewardCount = value;
        }
    }
    public bool HasReachedDestination
    {
        get => _hasReachedDestination;

        set
        {
            _hasReachedDestination = value;
        }
    }

    public void PlayerControl ()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            this.gameObject.transform.Translate(-MOVE_SPEED * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            this.gameObject.transform.Translate(MOVE_SPEED * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            this.gameObject.transform.Translate(0, MOVE_SPEED * Time.deltaTime, 0);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            this.gameObject.transform.Translate(0, -MOVE_SPEED * Time.deltaTime, 0);
        }
    }

    void OnTriggerEnter2D (Collider2D other)
    {
        // Collecting Bonus Rewards
        RewardEntity reward = other.gameObject.GetComponent<RewardEntity>();
        if (reward != null)
        {
            _rewardCount += 1;
        }

        // Reaching Destination
        Destination destination = other.gameObject.GetComponent<Destination>();
        if (destination != null)
        {
            _hasReachedDestination = true;
        }
    }
}
