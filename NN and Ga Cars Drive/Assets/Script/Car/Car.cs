using UnityEngine;

public class Car : MonoBehaviour
{
    public int id;
    public int lastCheckPointNumber = -1;

    private NeuralNetwork neuralNetwork;
    private CarSensors carSensors;
    private bool carCrashed = false;

    private void Start()
    {
        carSensors = GetComponent<CarSensors>();
    }

    public void InstNeuralNetwork(int[] layers, double[] weights, double[] biases)
    {
        neuralNetwork = new NeuralNetwork(layers, weights, biases);
    }

    void Update()
    {
        if (carCrashed || neuralNetwork == null)
        {
            return;
        }

        Move();
    }

    private void Move()
    {
        double[] InputLayer = carSensors.CalculateInputLayer();
        double[] Output = neuralNetwork.Run(InputLayer);

        double EngineValue = neuralNetwork.Sigmoid(Output[0]);
        double TurnValue = Output[1];

        transform.Translate(new Vector3((float)EngineValue * 2f * Time.deltaTime, 0, 0));
        transform.Rotate(new Vector3(0, (float)TurnValue * 90f * Time.deltaTime, 0));
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "CheckPoint")
        {
            lastCheckPointNumber++;
            //MainCamera.UpdateTarget(lastCheckPointNumber + 1);
            EventsManager.singleton.CallOnCarReachCheckPoint(
                lastCheckPointNumber, false);
        }

        if (other.tag == "FinishLine")
        {
            EventsManager.singleton.CallOnCarReachCheckPoint(
                lastCheckPointNumber, true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Wall" && carCrashed == false)
        {
            Crashed();
        }
    }

    private void Crashed()
    {
        carCrashed = true;
        EventsManager.singleton.CallOnCarCrashed(this);
        Destroy(gameObject, 1f);
    }
}
