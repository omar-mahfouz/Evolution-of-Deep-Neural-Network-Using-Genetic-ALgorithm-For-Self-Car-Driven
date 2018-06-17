using UnityEngine;

public class TrackManager : MonoBehaviour {

    public Transform startPosition;
    public Transform[] checkPoints;
    [HideInInspector]public double trackLenght;

    private void Awake()
    {
        trackLenght = CalculateTrackLength();
    }

    private double CalculateTrackLength()
    {
        double tracklenght = Vector3.Distance(checkPoints[0].position, startPosition.position);

        for (int i = 0; i < checkPoints.Length - 1; i++)
        {
            tracklenght += Vector3.Distance(checkPoints[i].position, checkPoints[i + 1].position);
        }

        return tracklenght;
    }

    public double CalculateCarTrackProgress(Vector3 carPosition, int lastCheckPointNumber)
    {
        if (lastCheckPointNumber == checkPoints.Length - 1)
        {
            return 1;
        }

        double fitness = Vector3.Distance(checkPoints[lastCheckPointNumber + 1].position, carPosition);

        for (int i = lastCheckPointNumber + 1; i < checkPoints.Length - 1; i++)
        {
            fitness += Vector3.Distance(checkPoints[i].position, checkPoints[i + 1].position);
        }

        fitness = (1 - (fitness / trackLenght));

        return fitness;
    }

    public Vector3 GetCheckPointPosition(int index)
    {
        return checkPoints[index].position;
    }
}
