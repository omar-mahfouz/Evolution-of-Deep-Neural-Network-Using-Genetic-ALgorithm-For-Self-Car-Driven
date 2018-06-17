using UnityEngine;
using UnityEngine.UI;


public class InformationDisplay : MonoBehaviour
{

    public GameObject SuccessPanel;

    public Text GenerationNumberText;

    public Text BestFitnessText;

    public Text CarStayAliveText;

    private void Start()
    {
        EventsManager.singleton.OnTestSucces += OnTestSucces;
    }

    private void OnTestSucces()
    {
        ShowSuccessPanel();
    }

    private void ShowSuccessPanel()
    {
        SuccessPanel.SetActive(true);

        SuccessPanel.GetComponent<SuccessPanel>().SetInformation(
            GeneticManager.singleton.GenerationNumber
            , GeneticManager.singleton.mutationRate
            , GeneticManager.singleton.crossOverWeightRate);
    }

    private void Update()
    {
        GenerationNumberText.text = GeneticManager.singleton.GenerationNumber.ToString();
        BestFitnessText.text = GameManager.singleton.bestFitness.ToString();
        CarStayAliveText.text = GameManager.singleton.carStayAlive.ToString();
    }

}
