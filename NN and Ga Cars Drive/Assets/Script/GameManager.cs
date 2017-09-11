using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Number Of Car Stay not Crashed in Single Generation Test
    public int CarStayAlive;

    //Check Points Position
    public Transform[] CheckPoints;

    //If The Geneartion Test Is End And All Car Is Crashed
    bool EndTheGenerationTest = true;

    //The Best Fitness Value The Cars Reached
    public double BestFitness = 0;

    //The Start Point For Instantiate Cars
    public Transform StartPosition;

    //References
    private GeneticManager Genetic_Manager;
    private CameraFollow MainCamera;
    private InformationDisplay display;

    //To Store The Length Of All Track
    private double TrackLenght;


    private void Start()
    {

        Genetic_Manager = GetComponent<GeneticManager>();
        MainCamera = Camera.main.GetComponent<CameraFollow>();
        display = GameObject.FindGameObjectWithTag("Display").GetComponent<InformationDisplay>();

        //Calculate Track Length
        TrackLenght = CalculateTrackLength();


    }


    double CalculateTrackLength()
    {

        //Calculate The Distance Between Start Point And Each CheckPoint And Finish Line

        double tracklenght = Vector3.Distance(CheckPoints[0].position, StartPosition.position);

        for (int i = 0; i < CheckPoints.Length - 1; i++)
        {
            tracklenght += Vector3.Distance(CheckPoints[i].position, CheckPoints[i + 1].position);
        }

        return tracklenght;

    }



    //Start New Generation Test 
    public void StartNewTest(int NumOfPopulition)
    {
        //Assign Car Number 
        CarStayAlive = NumOfPopulition;

        //Make The Variable False To Begin Testing
        EndTheGenerationTest = false;

        //Reset Camera Position
        MainCamera.ResetCameraPosition();


    }

	void Update () 
    {

        if(EndTheGenerationTest)
        {
            return;
        }

        //to Check If All Car Crashed to Stop The Test 
        if (CarStayAlive<=0  && EndTheGenerationTest == false)
        {
            EndTheGenerationTest = true;
            Genetic_Manager.RePopulition();
        }
		
	}

    public double CalculateFitness(Vector3 CarPosition,int IndexforLastChekPoint)
    {
       
        if(IndexforLastChekPoint==CheckPoints.Length-1)
        {
            return 1;
        }

        //Calclute The Fitness Value By The Progress To Finish Line


        double Fitness = Vector3.Distance(CheckPoints[IndexforLastChekPoint+1].position,CarPosition);

        for (int i=IndexforLastChekPoint+1;i<CheckPoints.Length-1;i++)
        {
            Fitness += Vector3.Distance(CheckPoints[i].position, CheckPoints[i + 1].position);
        }


        Fitness = (1 - (Fitness / TrackLenght));


        if (BestFitness<Fitness)
        {
            BestFitness = Fitness;
        }    

        return Fitness;

    }


    public void TestFinish()
    {
        display.ShowSuccessPanel();

        Time.timeScale = 0;
    }

}
