using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other)
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
