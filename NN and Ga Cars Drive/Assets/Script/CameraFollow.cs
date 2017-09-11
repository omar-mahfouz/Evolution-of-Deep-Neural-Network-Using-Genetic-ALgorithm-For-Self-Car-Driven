using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    private GameManager Game_Manager;


    private Vector3 Target;

    //Index Of The Last Check Point Cars Reach it 
    public int IndexOfLastCheckPoint = 0;

    void Start ()
    {
        Game_Manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();	
	}
	
	
	void Update ()
    {
        //Move To Last Check Point Cars Reach it 
        Target =new Vector3( Game_Manager.CheckPoints[IndexOfLastCheckPoint].position.x-2, transform.position.y, transform.position.z);

        transform.position = Vector3.MoveTowards(transform.position, Target, 0.02f);
	}

    //To Update Last Check Point Cars Reach it
    public void UpdateTarget(int Index)
    {
    
        if(Index> IndexOfLastCheckPoint)
        {
            IndexOfLastCheckPoint = Index;
        }
    }

    public void ResetCameraPosition()
    {
        IndexOfLastCheckPoint = 0;
        transform.position = Target = new Vector3(Game_Manager.CheckPoints[IndexOfLastCheckPoint].position.x - 2, transform.position.y, transform.position.z);
    }

}
