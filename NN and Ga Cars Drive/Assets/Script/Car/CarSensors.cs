using UnityEngine;

public class CarSensors : MonoBehaviour
{
    public Transform[] sensorsPos;

    public double[] CalculateInputLayer()
    {
        double[] distances = new double[sensorsPos.Length];

        for (int i = 0; i < sensorsPos.Length; i++)
        {
            RaycastHit hit;
            if (Physics.Raycast(sensorsPos[i].position, sensorsPos[i].forward, out hit, 10f))
            {
                distances[i] = Vector3.Distance(sensorsPos[i].position, hit.point);
            }
            else
            {
                distances[i] = 0;
            }
        }
        return distances;
    }
}
