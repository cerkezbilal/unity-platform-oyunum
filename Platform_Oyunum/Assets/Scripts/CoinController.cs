using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinController : MonoBehaviour
{
   //[SerializeField] private Text scoreText;
    
    [SerializeField] private float coinRotateSpeed;

    private void Update()
    {
        transform.Rotate(new Vector3(0f,coinRotateSpeed,0f));
    }

   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            /* score = int.Parse(scoreText.text);
             score += 10;
             scoreText.text = score.ToString();*/
            GameObject.Find("Level Manager").GetComponent<LevelManager>().AddScore(10);
            Destroy(gameObject);
          
        }
    }
}
