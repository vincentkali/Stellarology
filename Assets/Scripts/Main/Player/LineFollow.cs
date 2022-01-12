using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineFollow : MonoBehaviour
{
    
    // Start is called before the first frame update
    [SerializeField] private Transform[] points;
    [SerializeField] private LineController line;
    void Start()
    {
        line.SetUpLine(points);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
