using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowController : MonoBehaviour
{

    [SerializeField] GameObject effect;
   // [SerializeField] private Text scoreText;

    private void Start()
    {
       
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!(collision.gameObject.tag == "Player"))
        {
            Destroy(gameObject);
            if (collision.gameObject.CompareTag("Canavar"))
            {
                Destroy(collision.gameObject);
                GameObject.Find("Level Manager").GetComponent<LevelManager>().AddScore(20);

                Instantiate(effect, collision.transform.position,Quaternion.identity);
            }
        }
        
    }
    //Ok Sahneyi terk ettiğinde yok olması için
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
