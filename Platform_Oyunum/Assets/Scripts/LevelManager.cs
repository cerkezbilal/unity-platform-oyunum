using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] Text scoreValueText;
     int scoreValue;
    
    

    private void Start()
    {
       scoreValueText = GameObject.Find("puanText").GetComponent<Text>();
        

        if (PlayerPrefs.HasKey("Puan") && PlayerPrefs.GetInt("Puan") != 0)
        {
            scoreValue = PlayerPrefs.GetInt("Puan");

            scoreValueText.text = scoreValue.ToString();

        }
        else
        {
            scoreValue = 0;
            scoreValueText.text = scoreValue.ToString();
        }




    }
     void Update()
    {
        PlayerPrefs.SetInt("Puan", scoreValue);
        if (PlayerPrefs.HasKey("Puan") && scoreValue != 0)
        {
            scoreValue = PlayerPrefs.GetInt("Puan");
            scoreValueText.text = scoreValue.ToString();
            
        }
        else
        {
            scoreValue = 0;
            scoreValueText.text = scoreValue.ToString();
        }

        
    }
    public void NextLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        if (PlayerPrefs.HasKey("Puan"))
        {

            scoreValue = PlayerPrefs.GetInt("Puan");
            scoreValueText.text = scoreValue.ToString();

        }

        Debug.Log(PlayerPrefs.GetInt("Puan").ToString());


    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        scoreValue = 0;
        scoreValueText.text = scoreValue.ToString();



    }
    public void Quit()
    {
        Application.Quit();
        PlayerPrefs.DeleteAll();
        scoreValueText.text = "";
    }

    public void ClosePanel(string parentName)
    {
        GameObject.Find(parentName).SetActive(false);

    }
    public void AddScore(int score)
    {
        /*
        scoreValue = int.Parse(scoreValueText.text);
        scoreValue += score;
        scoreValueText.text = scoreValue.ToString();
        
        */
        scoreValue += score;
        scoreValueText.text = scoreValue.ToString();
        
    }

}
