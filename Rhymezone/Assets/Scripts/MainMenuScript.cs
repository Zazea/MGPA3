using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{

    
    public Text HighScore;



    private void Awake()
    {

        HighScore.text = "" + PlayerPrefs.GetInt("Highscore");

    }


    public void LoadEasy()
    {
        PlayerPrefs.SetInt("Difficulty", 1);
        SceneManager.LoadScene(1);
    }
    public void LoadMed()
    {
        PlayerPrefs.SetInt("Difficulty", 2);
        SceneManager.LoadScene(1);
    }
    public void LoadHard()
    {
        PlayerPrefs.SetInt("Difficulty", 3);
        SceneManager.LoadScene(1);
    }
}
