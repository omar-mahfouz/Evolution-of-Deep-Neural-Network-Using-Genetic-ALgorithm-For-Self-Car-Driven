using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    private int lastCheckPointNumber = 0;
    private Vector3 target;

    private void Start()
    {
        target = transform.position;
        EventsManager.singleton.OnTestBegin += OnTestBegin;
        EventsManager.singleton.OnCarReachCheckPoint += OnCarReachCheckPoint;
    }

    private void OnTestBegin()
    {
        ResetCameraPosition();
    }

    private void OnCarReachCheckPoint(int checkPointNumber, bool isFinishLine)
    {
        if (checkPointNumber + 1 > lastCheckPointNumber)
        {
            lastCheckPointNumber = checkPointNumber + 1;
        }
        UpdateTarget();
    }

    private void ResetCameraPosition()
    {
        lastCheckPointNumber = 0;
        UpdateTarget();
        transform.position = target;
    }

    private void UpdateTarget()
    {
        Vector3 checkPoint = GameManager.singleton.trackManager.GetCheckPointPosition(lastCheckPointNumber);
        target.x = checkPoint.x;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, 0.02f);
    }

}
