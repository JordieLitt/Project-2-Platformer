using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;
    private Animator animator;
    public Text score;
    private int scoreValue = 0;
    public Text livesText;
    private int livesCount = 3;
    public Text winText;
    private float hozMovement;
    private float verMovement;
    [Header("Movement")]
    public float currentSpd;
    public float maxSpd;
    public float accel;
    private bool facingRight;
    public LayerMask whatIsGround;
    public float jumpForce;
    public AudioSource musicSource;
    public AudioClip musicClipDeath;
    public AudioClip musicClipHit;
    

    

    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        winText.text = " ";
        SetScore();
        SetLivesText();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        hozMovement = Input.GetAxisRaw("Horizontal");
        verMovement = Input.GetAxis("Vertical");

        if (Mathf.Abs (hozMovement) > 0)
        {
            currentSpd += Time.deltaTime * accel;
            currentSpd = Mathf.Clamp(currentSpd, 5, maxSpd);
            
        }
        else
        {
            currentSpd = Time.deltaTime * 0;
            currentSpd = Mathf.Clamp(currentSpd, 0, maxSpd);
        }
        bool isTouchingGround = Physics2D.Raycast(transform.position, Vector2.down, 2.205f, whatIsGround);
        if (Input.GetKeyDown(KeyCode.W) && isTouchingGround == true)
        {
            rd2d.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            
        }
        if (scoreValue == 8)
        {
            winText.text = "You Win! Game Created By:Jordan Little";
        }
      
       if ( isTouchingGround == false)
       {
           animator.SetInteger("State",1);
           animator.SetFloat("Speed", 0);
       }
       else
       {
            animator.SetInteger("State",0);
            
       }
         animator.SetFloat("Speed", currentSpd);
       
      
       
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
        
        Flip(hozMovement);
        
    }

    private void Flip(float hozMovement)
    {
        if (hozMovement > 0 && facingRight || hozMovement < 0 && !facingRight)
        {
            facingRight = !facingRight;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }
    void FixedUpdate()
    {
       
        rd2d.velocity = new Vector2(hozMovement * currentSpd, rd2d.velocity.y); 
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Coin"))
        {
            scoreValue += 1;
            SetScore();
            Destroy(collision.gameObject);
        }
        if(collision.CompareTag("Enemy"))
        {
            livesCount -= 1;
            SetLivesText();
        }
        if (scoreValue == 4)
        {
            transform.position = new Vector3(80, 14, 0);
        }
        

    }
    void SetScore()
    {
        score.text = "Score: " +scoreValue.ToString ();
        if (scoreValue == 4)
        {
            livesCount = 3;
            livesText.text = "Lives: " + livesCount.ToString();
            
        }
    }
    void SetLivesText()
    {
        livesText.text = "Lives: " + livesCount.ToString();
        if (livesCount == 0)
        {
            winText.text = "Game Over";
            Destroy(this.gameObject);
            musicSource.clip = musicClipDeath;
            musicSource.Play();
        }
        if(livesCount < 3 && livesCount > 0)
        {
            musicSource.clip = musicClipHit;
            musicSource.Play();
        }
    }
}
