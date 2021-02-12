using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    
    private float myspeedX;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float jumpPower;
    private Rigidbody2D myBody;//Karakter vücudu
    private Vector3 defaultLocalScale;//Var olan Scale değeri
    public bool onGround;//Zemin kontrolü
    private bool canDoubleJump;
    [SerializeField]
    private GameObject arrow;
    [SerializeField]
    bool attacked;
    [SerializeField]
    private float currentAttackTimer;
    [SerializeField]
    private float defaultAttackTimer;
    private Animator myAnimator;
    [SerializeField] int arrowNumber;

    [SerializeField] Text arrowNumberText;
    [SerializeField] AudioClip dieMusic;
    [SerializeField] GameObject winPanel, losePanel;
   

  

  




    void Start()
    {
        myBody = GetComponent<Rigidbody2D>();
        defaultLocalScale = transform.localScale;
        onGround = true;
        canDoubleJump = true;
        attacked = true;
        myAnimator = GetComponent<Animator>();
        arrowNumberText.text = arrowNumber.ToString();
        
        

    }

   
    void Update()
    {
        
        #region Yürüme olayı
        //myspeedX = Input.GetAxis("Horizontal");
        myAnimator.SetFloat("Speed", Mathf.Abs(myspeedX));
       
        myBody.velocity = new Vector2(myspeedX*speed,myBody.velocity.y);

        #endregion Yürüme Olayı bitiş


        #region Karakterin yöne doğru Dönme Olayı
        
        if (myspeedX > 0)
        {
            transform.localScale = new Vector3(defaultLocalScale.x, defaultLocalScale.y, defaultLocalScale.z);
        } else if (myspeedX < 0)
        {
            transform.localScale = new Vector3(-defaultLocalScale.x, defaultLocalScale.y, defaultLocalScale.z);
        }
        #endregion Yön Dönme Olayı Bitiş


        #region Karakterin Zıplama Olayı


        /*  if (Input.GetKeyDown(KeyCode.W))
          {
              if(onGround == true)
              {
                  myAnimator.SetTrigger("Jump");
                  myBody.velocity = new Vector2(myBody.velocity.x, jumpPower);
                  canDoubleJump = true;

              }
              else
              {
                  if (canDoubleJump == true)
                  {
                      myBody.velocity = new Vector2(myBody.velocity.x, jumpPower);
                      canDoubleJump = false;
                  }
              }

          }*/

        #endregion Zıplama Olayı Bitiş

        #region Ok atma Olayı
        //Vector3 okyeri = new Vector3(transform.position.x + 1.12f, transform.position.y, transform.position.z);

        if(arrowNumber > 0 && Input.touchCount>1 )
        {
            
            if (attacked == false)
            {
                
                myAnimator.SetTrigger("Attack");
                Invoke("Fire", 0.5f);




                attacked = true;
            }
        }

       
            
        
             
           
         
        



        #endregion Ok atma Olayı Bitiş

        if (attacked == true)
        {
            currentAttackTimer -= Time.deltaTime;
           
        }else
        {
            currentAttackTimer = defaultAttackTimer;
        }

        if (currentAttackTimer <= 0)
        {
            attacked = false;
        }



    }

    public void Fire()
    {
       
        GameObject okumuz = Instantiate(arrow, transform.position, Quaternion.identity);
        okumuz.transform.parent = GameObject.Find("Arrows").transform;
       

        if (transform.localScale.x > 0)
        {

            okumuz.GetComponent<Rigidbody2D>().velocity = new Vector2(5f, 0f);

        }
        else if (transform.localScale.x < 0)
        {
            Vector3 okumuzunScale = okumuz.transform.localScale;
            okumuz.transform.localScale = new Vector3(-okumuzunScale.x, okumuzunScale.y, okumuzunScale.z);
            okumuz.GetComponent<Rigidbody2D>().velocity = new Vector2(-5f, 0f);
        }

        arrowNumber--;
        arrowNumberText.text = arrowNumber.ToString();
       

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if(collision.gameObject.CompareTag("Canavar"))
        {
            GetComponent<TimeController>().enabled = false;
            Die();
        } else if (collision.gameObject.CompareTag("Finish"))
        {
            Destroy(collision.gameObject);
            // winPanel.SetActive(true);
           
            StartCoroutine(Wait(true));
            


        }
        //Ekrana Ok rozeti ekle 
       /* else if (collision.gameObject.CompareTag("OkBonus"))
        {
            arrowNumber += 5;
            arrowNumberText.text = arrowNumber.ToString();
        }
*/        
        
        
    }

    public void Die()
    {
        GameObject.Find("SoundController").GetComponent<AudioSource>().clip = null;
        GameObject.Find("SoundController").GetComponent<AudioSource>().PlayOneShot(dieMusic);
        myAnimator.SetFloat("Speed", 0);
        myAnimator.SetTrigger("Die");

        //myBody.constraints = RigidbodyConstraints2D.FreezePosition;
        myBody.constraints = RigidbodyConstraints2D.FreezeAll;
        enabled = false;
        StartCoroutine(Wait(false));



    }
    IEnumerator Wait(bool win)
    {
        yield return new WaitForSecondsRealtime(1.5f);
        Time.timeScale = 0;//Süreyi durdurur oyundaki herşeyi durdurur.
        if (win == true)
        {
            winPanel.SetActive(true);
        }
        else
        {
            losePanel.SetActive(true);
        }
        
        
    }

    public void Hareket(float yon)
    {
        myspeedX = yon;
    }

    public void Ziplama()
    {
        if (onGround == true)
        {
            myAnimator.SetTrigger("Jump");
            myBody.velocity = new Vector2(myBody.velocity.x, jumpPower);
            canDoubleJump = true;

        }
        else
        {
            if (canDoubleJump == true)
            {
                myBody.velocity = new Vector2(myBody.velocity.x, jumpPower);
                canDoubleJump = false;
            }
        }
    }

   

    
   
}
