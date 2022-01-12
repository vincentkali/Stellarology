using UnityEngine;

public class LoseCameraEffect : MonoBehaviour
{
    private Vector3 startingPosition;
    public float smoothSpeed = 2.0f;
    public float cameraZoomingTime = 1.0f;
    
    public bool lose = false;
    public float cameraZoomOutSize = 5.0f;
    private float cameraInitSize;
    private bool toStartingPosition;
    private float initPositionZ;
    private Vector3 temp;
    private float StartTime;
    private bool getStartTime;

    void Start()
    {
        startingPosition = GameObject.Find("Player").GetComponent<Transform>().position;
        // Debug.Log(startingPosition);
        cameraInitSize = Camera.main.orthographicSize;
        initPositionZ = transform.position.z;
        getStartTime = false;
        toStartingPosition = true;
    }
    
    void LateUpdate()
    {
        if (!lose) return;

        if (!getStartTime)
        {
            StartTime = Time.time;
            getStartTime = true;
        }

        else if (toStartingPosition && Time.time - StartTime < 10.0f)
        {
            transform.position = Vector3.Lerp(transform.position, startingPosition, smoothSpeed * Time.deltaTime);
            temp = transform.position;
            temp.z = initPositionZ;
            transform.position = temp;
            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, cameraZoomOutSize, smoothSpeed * Time.deltaTime);
            
            if (Time.time - StartTime >= cameraZoomingTime)
            {
                StartTime = Time.time;
                toStartingPosition = false; 
            } 
        }
        else if (!toStartingPosition && Time.time - StartTime < cameraZoomingTime)
        {
            transform.position = Vector3.Lerp(transform.position, Vector3.zero, smoothSpeed * Time.deltaTime);
            temp = transform.position;
            temp.z = initPositionZ;
            transform.position = temp;
            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, cameraInitSize, smoothSpeed * Time.deltaTime);
        }
        else
        {
            toStartingPosition = true;
            lose = false;
            getStartTime = false;
            Camera.main.orthographicSize = cameraInitSize;
            temp = Vector3.zero;
            temp.z = initPositionZ;
            transform.position = temp;
            Debug.Log("lose = false");
        }
        
    }
}
