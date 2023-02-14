using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [Header("References")]

    [SerializeField] private int currentScore = 0;
    [SerializeField] private Text currentScoreText;
    [SerializeField] private Text currentLivesText;
    [SerializeField] private Text timerText;
    [SerializeField] private Text lastPlayTimeText;
   

    [Header("List of current CircleTargets")]
    [SerializeField] private List<GameObject> circleTargets = new List<GameObject>();


    [Header("Difficulty Settings")]
    [SerializeField] private int startingLives;

    private float currentTime;
    private int currentLives;

    public float CurrentTime
    {
        get
        {
            return currentTime;
        }

        set
        {
            currentTime = value;
        }
    }

    private void Start()
    {
        currentTime = 0;
        currentLives = startingLives;
        currentScore = 0;
        UpdateUI();
    }

    public void StartGame()
    {
        currentTime = 0;
        currentLives = startingLives;
        currentScore = 0;
        UpdateUI();
    }

    private void Update()
    {
        if (GameStateManager.instance.currentGameState == GameState.inGame)
        {
            ScoreTimer();
        }
    }

    public void AddRef(GameObject reference)
    {
        //add reference of instance to list
        circleTargets.Add(reference);

        //subscribe to events on instance
        reference.GetComponent<CircleScaleDetector>().InnerRingHit += OnInnerRingHit;
        reference.GetComponent<CircleScaleDetector>().OuterRingHit += OnOuterRingHit;
        reference.GetComponent<CircleScaleDetector>().ObjectDestroyed += OnObjectDestroyed;

    }


    //execute outcome of events when rings are hit
    private void OnInnerRingHit()
    {
        currentScore += 5;

        CheckForAddLife();
    }

    private void OnOuterRingHit()
    {
        currentScore -= 10;

        SubtractLife();
    }

    private void UpdateUI()
    {
        currentScoreText.text = currentScore.ToString();
        currentLivesText.text = currentLives.ToString();
    }


    private void ScoreTimer()
    {
        currentTime += Time.deltaTime;
        timerText.text = currentTime.ToString("0.00");
    }

    private void CheckForAddLife()
    {
        if (currentScore >= 100)
        {
            currentLives++;

            currentScore = 0;
        }

        UpdateUI();
    }

    private void SubtractLife()
    {

        if (currentLives > 0)
        {
            currentLives--;
            UpdateUI();
        }
        else
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        GameStateManager.instance.GameOver();


        lastPlayTimeText.text = currentTime.ToString("0.00");

        

        

        
    }

    // gets event from CircleScaleDetector class that it has been destroyted,
    // so remove it from our list of references
    private void OnObjectDestroyed(GameObject reference)
    {
        circleTargets.Remove(reference);
    }

    //unsubscribe from events
    private void OnDestroy()
    {
        foreach (GameObject reference in circleTargets)
        {
            reference.GetComponent<CircleScaleDetector>().InnerRingHit -= OnInnerRingHit;
            reference.GetComponent<CircleScaleDetector>().OuterRingHit -= OnOuterRingHit;
            reference.GetComponent<CircleScaleDetector>().ObjectDestroyed -= OnObjectDestroyed;
        }
    }

    private void OnDisable()
    {
        foreach (GameObject reference in circleTargets)
        {
            reference.GetComponent<CircleScaleDetector>().InnerRingHit -= OnInnerRingHit;
            reference.GetComponent<CircleScaleDetector>().OuterRingHit -= OnOuterRingHit;
            reference.GetComponent<CircleScaleDetector>().ObjectDestroyed -= OnObjectDestroyed;
        }
    }

}
