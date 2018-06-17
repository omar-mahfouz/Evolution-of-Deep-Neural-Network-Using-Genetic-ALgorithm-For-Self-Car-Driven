using UnityEngine;

public class EventsManager : MonoBehaviour
{

    public delegate void OnTest_Begin();
    public event OnTest_Begin OnTestBegin;

    public delegate void OnTest_End();
    public event OnTest_End OnTestEnd;

    public delegate void OnTest_Succes();
    public event OnTest_Succes OnTestSucces;

    public delegate void OnCar_ReachCheckPoint(int checkPointNumber, bool isFinishLine);
    public event OnCar_ReachCheckPoint OnCarReachCheckPoint;

    public delegate void OnCar_Crashed(Car car);
    public event OnCar_Crashed OnCarCrashed;

    public static EventsManager singleton;

    private void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
        }
    }

    public void CallOnTestBegin()
    {
        if (OnTestBegin != null)
        {
            OnTestBegin();
        }
    }

    public void CallOnTestEnd()
    {
        if (OnTestEnd != null)
        {
            OnTestEnd();
        }
    }

    public void CallOnCarReachCheckPoint(int checkPointNumber, bool isFinishLine)
    {
        if (OnCarReachCheckPoint != null)
        {
            OnCarReachCheckPoint(checkPointNumber, isFinishLine);
        }
    }

    public void CallOnCarCrashed(Car car)
    {
        if (OnCarCrashed != null)
        {
            OnCarCrashed(car);
        }
    }

    public void CallOnTestSucces()
    {
        if (OnTestSucces != null)
        {
            OnTestSucces();
        }
    }
}
