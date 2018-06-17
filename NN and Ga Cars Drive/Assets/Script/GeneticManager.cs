using UnityEngine;

public class GeneticManager : MonoBehaviour
{
    public GameObject carPrefab;
    public int populationSize = 30;
    public int[] neuralNetworkShape;
    public double mutationRate = 0.2;
    public double crossOverWeightRate = 0.6;
    public int numberOfBestAgentFitness = 6;
    public int numberOfAgentToCrossOver = 4;
    public int GenerationNumber = 1;

    private Agent[] population;
    private int weightNum;
    private int biasesNum;

    public static GeneticManager singleton;

    private void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
        }
        CalculateWeightAndBiasesNumber();
    }

    private void Start()
    {
        EventsManager.singleton.OnTestBegin += OnTestBegin;
    }

    public void SetAgentFitnessValue(int id, double fitness)
    {
        population[id].fitness = fitness;
    }

    private void CalculateWeightAndBiasesNumber()
    {
        for (int i = 1; i < neuralNetworkShape.Length; i++)
        {
            weightNum += neuralNetworkShape[i - 1] * neuralNetworkShape[i];
        }

        biasesNum = neuralNetworkShape.Length - 1;
    }

    private void OnTestBegin()
    {
        if (population == null)
        {
            InstaPopulation();
        }
        else
        {
            RePopulition();
        }
    }

    private void InstaPopulation()
    {
        population = new Agent[populationSize];
        FillPopulationByRandomValues(population, 0);

        BuildCars();
    }

    private void BuildCars()
    {
        for (int i = 0; i < populationSize; i++)
        {
            population[i].car = Instantiate(carPrefab, new Vector3(-4, 0.035f, 4.116f), new Quaternion(0, 0, 0, 0));
            population[i].car.GetComponent<Car>().InstNeuralNetwork(neuralNetworkShape, population[i].weights, population[i].biases);
            population[i].car.GetComponent<Car>().id = i;
        }
    }

    private int newPopulitionIndex = 0;

    private void RePopulition()
    {
        GenerationNumber++;

        newPopulitionIndex = 0;

        SortPopulation();
        Agent[] newPopulation = PickBestPopulation();
        CrossOver(newPopulation);
        Mutate(newPopulation);
        FillPopulationByRandomValues(newPopulation, newPopulitionIndex);

        population = newPopulation;
        BuildCars();
    }

    private void SortPopulation()
    {
        for (int i = 0; i < populationSize; i++)
        {
            for (int j = i; j < populationSize; j++)
            {
                if (population[i].fitness < population[j].fitness)
                {
                    Agent Temp = population[i];
                    population[i] = population[j];
                    population[j] = Temp;
                }
            }
        }
    }

    private Agent[] PickBestPopulation()
    {
        Agent[] newPopulation = new Agent[populationSize];

        for (int i = 0; i < numberOfBestAgentFitness; i++)
        {
            newPopulation[newPopulitionIndex] = population[i];
            newPopulation[newPopulitionIndex].fitness = 0;
            newPopulitionIndex++;
        }

        return newPopulation;
    }

    private void CrossOver(Agent[] newPopulation)
    {
        for (int i = 0; i < numberOfAgentToCrossOver; i += 2)
        {
            Agent Child1 = new Agent();
            Agent Child2 = new Agent();

            Child1.weights = new double[weightNum];
            Child2.weights = new double[weightNum];

            Child1.biases = new double[biasesNum];
            Child2.biases = new double[biasesNum];

            Child1.fitness = 0;
            Child2.fitness = 0;

            for (int j = 0; j < weightNum; j++)
            {
                if (Random.Range(0.0f, 1.0f) < crossOverWeightRate)
                {
                    Child1.weights[j] = population[i].weights[j];
                    Child2.weights[j] = population[i + 1].weights[j];
                }
                else
                {
                    Child1.weights[j] = population[i + 1].weights[j];
                    Child2.weights[j] = population[i].weights[j];
                }
            }

            for (int j = 0; j < biasesNum; j++)
            {
                if (Random.Range(0.0f, 1.0f) < crossOverWeightRate)
                {
                    Child1.biases[j] = population[i].biases[j];
                    Child2.biases[j] = population[i + 1].biases[j];
                }
                else
                {
                    Child1.biases[j] = population[i + 1].biases[j];
                    Child2.biases[j] = population[i].biases[j];
                }
            }
            newPopulation[newPopulitionIndex] = Child1;
            newPopulitionIndex++;
            newPopulation[newPopulitionIndex] = Child2;
            newPopulitionIndex++;
        }
    }

    private void Mutate(Agent[] newPopulation)
    {
        for (int i = 0; i < newPopulitionIndex; i++)
        {
            if (Random.Range(0.0f, 1.0f) < mutationRate)
            {
                for (int j = 0; j < weightNum; j++)
                {
                    if (Random.Range(0.0f, 1.0f) < mutationRate)
                    {
                        newPopulation[i].weights[j] = Random.Range(-1.0f, 1.0f);
                    }
                }
                for (int j = 0; j < biasesNum; j++)
                {
                    if (Random.Range(0.0f, 1.0f) < mutationRate)
                    {
                        newPopulation[i].biases[j] = Random.Range(-1.0f, 1.0f);
                    }
                }
            }
        }
    }

    private void FillPopulationByRandomValues(Agent[] newPopulation, int startIndex)
    {
        while (startIndex < populationSize)
        {
            newPopulation[startIndex] = new Agent();

            newPopulation[startIndex].fitness = 0;
            //Instantiate new Dna Weights
            newPopulation[startIndex].weights = new double[weightNum];
            //Instantiate new Agent Biases
            newPopulation[startIndex].biases = new double[biasesNum];
            //Random Weights Value
            for (int j = 0; j < weightNum; j++)
            {
                newPopulation[startIndex].weights[j] = Random.Range(-1.0f, 1.0f);
            }

            //Random Biases Value
            for (int j = 0; j < biasesNum; j++)
            {
                newPopulation[startIndex].biases[j] = Random.Range(-1.0f, 1.0f);
            }

            startIndex++;
        }
    }
}