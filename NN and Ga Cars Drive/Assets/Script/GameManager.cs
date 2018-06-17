using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int carStayAlive;
    public double bestFitness = 0;
    public TrackManager trackManager;


    public static GameManager singleton;

    private void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
        }
    }

    private void Start()
    {
        EventsManager.singleton.OnCarCrashed += OnCarCrashed;
        EventsManager.singleton.OnCarReachCheckPoint += OnCarReachCheckPoint;
        EventsManager.singleton.OnTestBegin += OnTestBegin;
        EventsManager.singleton.OnTestEnd += OnTestEnd;
        EventsManager.singleton.OnTestSucces += OnTestSucces;

        Invoke("StartNewTest", 1f);
    }

    private void OnCarCrashed(Car car)
    {
        carStayAlive--;
        double fitness = trackManager.CalculateCarTrackProgress(transform.position
            , car.lastCheckPointNumber);

        GeneticManager.singleton.SetAgentFitnessValue(car.id, fitness);

        if (bestFitness < fitness)
        {
            bestFitness = fitness;
        }

        if (carStayAlive <= 0)
        {
            EventsManager.singleton.CallOnTestEnd();
        }
    }

    private void OnCarReachCheckPoint(int checkPointNumber, bool isFinishLine)
    {
        if (isFinishLine)
        {
            EventsManager.singleton.CallOnTestSucces();
        }
    }

    private void OnTestBegin()
    {
        carStayAlive = GeneticManager.singleton.populationSize;
    }
    private void OnTestEnd()
    {
        StartNewTest();
    }

    private void OnTestSucces()
    {
        Time.timeScale = 0;
    }

    private void StartNewTest()
    {
        EventsManager.singleton.CallOnTestBegin();
    }

}
