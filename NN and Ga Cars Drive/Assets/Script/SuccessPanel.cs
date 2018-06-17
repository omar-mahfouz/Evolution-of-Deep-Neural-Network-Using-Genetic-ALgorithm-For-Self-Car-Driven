using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SuccessPanel : MonoBehaviour {

    public Text generationNumberText;
    public Text mutationRateText;
    public Text crossOverRateText;

    public void SetInformation(int generationNumber, double mutationRate, double crossOverRate)
    {
        generationNumberText.text = generationNumber.ToString();
        mutationRateText.text = mutationRate.ToString();
        crossOverRateText.text = crossOverRate.ToString();
    }

    public void RestartTest()
    {
        SceneManager.LoadScene(0);
    }
}
