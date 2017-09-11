using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SuccessPanel : MonoBehaviour {

    public Text GenerationNumberText;

    public Text MutationRateText;

    public Text CrossOverRateText;

    public void SetInformation(int GenerationNumber, double MutationRate, double CrossOverRate)
    {

        GenerationNumberText.text = GenerationNumber.ToString();

        MutationRateText.text = MutationRate.ToString();

        CrossOverRateText.text = CrossOverRate.ToString();

    }


    public void RestartTest()
    {
        SceneManager.LoadScene(0);
    }
}
