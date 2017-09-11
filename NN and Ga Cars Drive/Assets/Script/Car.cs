using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{

    //Reference For The Current Agent Index in Populition Array
    public int Id;

    public NeuralNetwork NN;
    //Reference For Sensors Position In The Car
    public Transform[] SensorsPosition;

 
    private bool CarCrashed= false;

    //To know What The Last Check Point the Car Reach it
    private int LastIndexCheckPoint = -1;

    //Reference
    private GameManager Game_Manager;
    private GeneticManager Genetic_Manager;
    private CameraFollow MainCamera;

    private void Start()
    {
        //Assign The References
        Game_Manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        Genetic_Manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GeneticManager>();
        MainCamera = Camera.main.GetComponent<CameraFollow>();
    }

    public void InstNeuralNetwork(int[] Layers, double[] Weights,double[] Biases)
    {

        NN = GetComponent<NeuralNetwork>();

        NN.inst(Layers, Weights, Biases);

    }



    void Update ()
    {
 
        //Check If Car Crashed To Stop All Movements
        if (CarCrashed)
        {
            return;
        }
  


        //to Check if The Neural Network Instantiate And The Test Begin
        if (NN==null)
        {
            return;
        }



        //Calculate Input Layer From Car Sensors 
        double[] InputLayer = CalculateInputLayer();

        //Run The Neural Network And Get Output Layer
        double[] Output = NN.Run(InputLayer);


        //The First Value In output layer is the Engine Power
          double EngineValue = NN.Sigmoid(Output[0]);

        //The Second Value In output layer is Turn Value of the car
           double TurnValue = Output[1];


        //Apply Moving
        transform.Translate(new Vector3((float)EngineValue * 2f * Time.deltaTime, 0, 0));

        //Apply Rotation
        transform.Rotate(new Vector3(0, (float)TurnValue * 90f * Time.deltaTime, 0));

  
    }

   






    double[] CalculateInputLayer()
    {
        //Instantiate Distance array
        double[] Distances = new double[SensorsPosition.Length];

        //Calculate Distances from Sensors
        for (int i = 0; i < SensorsPosition.Length; i++)
        {
            // Ray Cast From Sensor To Calclute Distance 
            Ray ray = new Ray(SensorsPosition[i].position, SensorsPosition[i].forward);

            RaycastHit hit;

            Physics.Raycast(SensorsPosition[i].position, SensorsPosition[i].forward, out hit,10f);

            if(hit.collider!=null)
            {
                //Distances Between Sensor Position and Wall
                Distances[i] = Vector3.Distance(SensorsPosition[i].position, hit.point);

            }
            else
            {
                Distances[i] = 0;
            }         

        }

        return Distances;

    }

    //To Check If The Car Crashed in The Wall
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Wall"  && CarCrashed==false)
        {
            Crashed();
        }
    }
    //To Check If The Car Reached The Check Points Or The Finish Line
    private void OnTriggerExit(Collider other)
    {
        if(other.tag=="CheckPoint")
        {   
            LastIndexCheckPoint++;

            //Update Last Check Point Reach it 
            MainCamera.UpdateTarget(LastIndexCheckPoint + 1);

        }

        if(other.tag=="FinishLine")
        {

            Game_Manager.TestFinish();

        }
    }


    void Crashed()
    {

        CarCrashed = true;
        //Decrease Number Of Car They Stay Alive
        Game_Manager.CarStayAlive--;
        //Calculate Fitness Value
        double fitness = Game_Manager.CalculateFitness(transform.position, LastIndexCheckPoint);
        //Assign Fitness value
        Genetic_Manager.Populition[Id].Fitness = fitness;

        Destroy(gameObject, 1f);

    }
}
