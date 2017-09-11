using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class InformationDisplay : MonoBehaviour
{

    public GameObject SuccessPanel;

    public Text GenerationNumberText;

    public Text BestFitnessText;

    public Text CarStayAliveText;

    private GeneticManager Genetic_Manager;
    private GameManager Game_Manager;

    

	void Start ()
    {
        Genetic_Manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GeneticManager>();
        Game_Manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }
	
	
	void Update ()
    {
        GenerationNumberText.text = Genetic_Manager.GenerationNumber.ToString();

        BestFitnessText.text = Game_Manager.BestFitness.ToString();

        CarStayAliveText.text = Game_Manager.CarStayAlive.ToString();

    }

    public void ShowSuccessPanel()
    {
        SuccessPanel.SetActive(true);

        SuccessPanel.GetComponent<SuccessPanel>().SetInformation(Genetic_Manager.GenerationNumber, Genetic_Manager.MutationRate, Genetic_Manager.CrossOverWeightRate);
    }



   




}
