using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneticManager : MonoBehaviour
{
    public GameObject CarPrefab;

    public Agent[] Populition;

    public int PopulitionSize = 40;

    //The Number Of The Best Agent Will Keep it To The Next generation 
    public int NumberOfBestAgentFitness = 6; 

    //The Number Of The Best Agent Will Keep it To Cross Over It
    public int NumberOfAgentToCrossOver = 4; 

    //The Size And Number Of Layers In Neural Network 
    public int[] NeuralNetworkShape;


    //The Percent Value of Weight Mutated
    public double MutationRate = 0.05;
    //The Percent Value of Weight Cross Over
    public double CrossOverWeightRate = 0.3;
         

    //The Generation Number We Reach It
    public  int GenerationNumber = 1;

    //The Weights Number In All Neural Network Layers
    private int WeightNum;
    //The Biases Number In All Neural Network Layers
    private int BiasesNum;


    //References
    private GameManager Game_Manager;


    private void Start()
    {
        Game_Manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        //Calculate Weights Number In Neural Network
        for (int i = 1; i < NeuralNetworkShape.Length; i++)
        {
            WeightNum += NeuralNetworkShape[i - 1] * NeuralNetworkShape[i];
        }

        //Calculate Biases Number In Neural Network
        BiasesNum = NeuralNetworkShape.Length - 1;
   
        //Instantiate Populition 
        Populition = new Agent[PopulitionSize];

        for (int i = 0; i < PopulitionSize; i++)
        {
            //Instantiate Agent Dna 
            Populition[i] = new Agent();

            Populition[i].Fitness = 0;
            //Instantiate new Agent Weights
            Populition[i].Weights = new double[WeightNum];
            //Instantiate new Agent Biases
            Populition[i].Biases = new double[BiasesNum];

            //Random Weights Value
            for (int j = 0; j < WeightNum; j++)
            {
                Populition[i].Weights[j] = Random.Range(-1.0f, 1.0f);
            }

            //Random Weights Value
            for (int j = 0; j < BiasesNum; j++)
            {
                Populition[i].Biases[j] = Random.Range(-1.0f, 1.0f);
            }

        }


        StartTheGenetationTest();

    }

    public void StartTheGenetationTest()
    {
        for (int i = 0; i < PopulitionSize; i++)
        {
            //Instantiate new Car Prefab
            Populition[i].Prefab = Instantiate(CarPrefab, new Vector3(-4, 0.035f, 4.116f), new Quaternion(0, 0, 0, 0));
            //Instantiate new Car Neural Network
            Populition[i].Prefab.GetComponent<Car>().InstNeuralNetwork(NeuralNetworkShape, Populition[i].Weights,Populition[i].Biases);
            //Assign Dna Id By Index of Populition array
            Populition[i].Prefab.GetComponent<Car>().Id = i;

        }


        //Start The Test
        Game_Manager.StartNewTest(PopulitionSize);

    }


    public void RePopulition()
    {


        Debug.Log("Generation : " + GenerationNumber + "|" + "Test End Best Fitness :" + Game_Manager.BestFitness);



        //Sort Populition By fitness Value
        for (int i = 0; i < PopulitionSize; i++)
        {
            for (int j = i; j < PopulitionSize; j++)
            {
                if (Populition[i].Fitness < Populition[j].Fitness)
                {
                    Agent Temp = Populition[i];
                    Populition[i] = Populition[j];
                    Populition[j] = Temp;
                }
            }
        }


        int IndexforNewPopulition = 0;

        Agent[] NewPopulition = new Agent[PopulitionSize];

        //Pick The Best 25% from Populition And Stay it 
        for (int i = 0; i < NumberOfBestAgentFitness; i++)
        {

            NewPopulition[IndexforNewPopulition] = Populition[i];

            NewPopulition[IndexforNewPopulition].Fitness = 0;

            IndexforNewPopulition++;

        }

      
        //Cross Over :
        for (int i = 0; i < NumberOfAgentToCrossOver; i+=2)
        {
            Agent Child1 = new Agent();
            Agent Child2 = new Agent();

            Child1.Weights = new double[WeightNum];
            Child2.Weights = new double[WeightNum];

            Child1.Biases = new double[BiasesNum];
            Child2.Biases = new double[BiasesNum];

            Child1.Fitness = 0;
            Child2.Fitness = 0;

            for (int j=0;j<WeightNum;j++)
            {
                if(Random.Range(0.0f,1.0f)<CrossOverWeightRate)
                {
                    Child1.Weights[j] = Populition[i].Weights[j];
                    Child2.Weights[j] = Populition[i + 1].Weights[j];
                }
                else
                {
                    Child1.Weights[j] = Populition[i + 1].Weights[j];
                    Child2.Weights[j] = Populition[i ].Weights[j];
                }
            }

            for (int j = 0; j < BiasesNum; j++)
            {
                if (Random.Range(0.0f, 1.0f) < CrossOverWeightRate)
                {
                    Child1.Biases[j] = Populition[i].Biases[j];
                    Child2.Biases[j] = Populition[i + 1].Biases[j];
                }
                else
                {
                    Child1.Biases[j] = Populition[i + 1].Biases[j];
                    Child2.Biases[j] = Populition[i].Biases[j];
                }
            }


            NewPopulition[IndexforNewPopulition] = Child1;

            IndexforNewPopulition++;

            NewPopulition[IndexforNewPopulition] = Child2;

            IndexforNewPopulition++;

        }


      


        //Make mutated      
        for (int i=0;i<IndexforNewPopulition;i++)
        {
            if (Random.Range(0.0f, 1.0f) < MutationRate)
            {
            

                for(int j=0;j<WeightNum;j++)
                {

                    if(Random.Range(0.0f, 1.0f) < MutationRate)
                    {
                        NewPopulition[i].Weights[j] = Random.Range(-1.0f, 1.0f);
                    }
                    
                }


                for (int j = 0; j < BiasesNum; j++)
                {

                    if (Random.Range(0.0f, 1.0f) < MutationRate )
                    {
                        NewPopulition[i].Biases[j] = Random.Range(-1.0f, 1.0f);
                    }

                }


            

          


            }

        }


        //Make The Rest New Agent
        while (IndexforNewPopulition<PopulitionSize)
        {
            NewPopulition[IndexforNewPopulition] = new Agent();

            NewPopulition[IndexforNewPopulition].Fitness = 0;
            //Instantiate new Dna Weights
            NewPopulition[IndexforNewPopulition].Weights = new double[WeightNum];
            //Instantiate new Agent Biases
            NewPopulition[IndexforNewPopulition].Biases = new double[BiasesNum];
            //Random Weights Value
            for (int j = 0; j < WeightNum; j++)
            {
                NewPopulition[IndexforNewPopulition].Weights[j] = Random.Range(-1.0f, 1.0f);
            }
            
            //Random Biases Value
            for (int j = 0; j < BiasesNum; j++)
            {
                NewPopulition[IndexforNewPopulition].Biases[j] = Random.Range(-1.0f, 1.0f);
            }

            IndexforNewPopulition++;
        }
    


        //Assign New Populition 
        Populition = NewPopulition;

        //Start New Generation Test 
        StartTheGenetationTest();

        //Increase Generation Number
        GenerationNumber++;



    }
}