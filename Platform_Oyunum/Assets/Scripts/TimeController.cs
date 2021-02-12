using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeController : MonoBehaviour
{
    [SerializeField] private Text timeText;
    [SerializeField] private float time;
    private bool gameActive;
    void Start()
    {
        timeText.text = time.ToString();
        gameActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameActive == true)
        {
            time -= Time.deltaTime;

            timeText.text = ((int)time).ToString();
        }
        


        if (time < 0)
        {
            time = 0;
            gameActive = false;
            GetComponent<PlayerController>().Die();
            
        }
        
    }
}
