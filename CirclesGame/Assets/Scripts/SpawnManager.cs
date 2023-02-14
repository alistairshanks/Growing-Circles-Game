using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    /*This class is responsible for spawning in the circle targets which
      and then adding references to them to the appropriate manager classes
      to allow the manager classes to listen for events.
    */

    [Header("References")]

    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private FlatFxManager flatFxManager;
    [SerializeField] private GameObject circleTarget;     //ref for prefab

    [Header("Timer settings")]

    [SerializeField] private float timerDuration = 2f;
    [SerializeField] private float timeRemaining = 1f;


    private bool readyToSpawn = true;
    private Bounds cameraBounds;

    private void Start()
    {
        CalculateCameraBounds();
    }


    private void Update()
    {
        if (GameStateManager.instance.currentGameState == GameState.inGame)
        {
            SpawnTimer();

            if (readyToSpawn == true)
            {
                SpawnNew();
            }

            CalculateTimerDuration();
        }
    }


    private void CalculateCameraBounds()
    {
        Camera cam = Camera.main;
        float cameraHeight = 2f * cam.orthographicSize;
        float cameraWidth = cameraHeight * cam.aspect;
        cameraBounds = new Bounds(Vector3.zero, new Vector3(cameraWidth, cameraHeight, 0f));

        cameraBounds.Expand(-3f);
    }

    private Vector3 GetRandomPointOnCamera(Bounds bounds)
    {
        return new Vector3(
        Random.Range(bounds.min.x, bounds.max.x),
        Random.Range(bounds.min.y, bounds.max.y),
        Random.Range(bounds.min.z, bounds.max.z)
        );

    }


    private void SpawnNew()
    {
        if (readyToSpawn == true)
        {
            Vector3 pointToSpawn = GetRandomPointOnCamera(cameraBounds);

            if ((Physics2D.CircleCast(pointToSpawn, 2.5f, Vector2.up) == false))
            {
                // instantiate a new Circle target 
                var newInstance = Instantiate(circleTarget, pointToSpawn, Quaternion.identity);

                //get component CircleScaleDetector on this instance
                var circleScaleDetectorRef = newInstance;
                var outerRingSize = newInstance.transform.GetChild(0).GetComponent<SpriteRenderer>().bounds.size;

                //pass CircleScaleDetectorInstace to list on the manager class
                scoreManager.AddRef(circleScaleDetectorRef);

                //lets us know we have spawned something
                readyToSpawn = false;
            }
        }
    }


    //timer for how often the circles spawn

    private void SpawnTimer()
    {

        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
        }

        else
        {
            
            timeRemaining = timerDuration;
            readyToSpawn = true;
        }
    }


    private void CalculateTimerDuration()
    {
        timerDuration = Mathf.Clamp((1f - scoreManager.CurrentTime/150), 0.45f, 1f);
    }



    //Some gizmos and de-bugging functions

    /*
    private void OnDrawGizmos()
        {
            Camera cam = Camera.main;

            float height = 2f * cam.orthographicSize;
            float width = height * cam.aspect;

            Vector3 screenSize = new Vector3((float)width -5, (float)height -5, (float)0);

            Gizmos.color = Color.green;
        }


    void DrawBounds(Bounds b, float delay = 0)
    {

        Debug.Log("bounds drawn");
        // bottom
        var p1 = new Vector3(b.min.x, b.min.y, b.min.z);
        var p2 = new Vector3(b.max.x, b.min.y, b.min.z);
        var p3 = new Vector3(b.max.x, b.min.y, b.max.z);
        var p4 = new Vector3(b.min.x, b.min.y, b.max.z);

        Debug.DrawLine(p1, p2, Color.blue, delay);
        Debug.DrawLine(p2, p3, Color.red, delay);
        Debug.DrawLine(p3, p4, Color.yellow, delay);
        Debug.DrawLine(p4, p1, Color.magenta, delay);

        // top
        var p5 = new Vector3(b.min.x, b.max.y, b.min.z);
        var p6 = new Vector3(b.max.x, b.max.y, b.min.z);
        var p7 = new Vector3(b.max.x, b.max.y, b.max.z);
        var p8 = new Vector3(b.min.x, b.max.y, b.max.z);

        Debug.DrawLine(p5, p6, Color.blue, delay);
        Debug.DrawLine(p6, p7, Color.red, delay);
        Debug.DrawLine(p7, p8, Color.yellow, delay);
        Debug.DrawLine(p8, p5, Color.magenta, delay);

        // sides
        Debug.DrawLine(p1, p5, Color.white, delay);
        Debug.DrawLine(p2, p6, Color.gray, delay);
        Debug.DrawLine(p3, p7, Color.green, delay);
        Debug.DrawLine(p4, p8, Color.cyan, delay);
    }
    */
}
