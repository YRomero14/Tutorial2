using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
 private Rigidbody2D rd2d;
 public float speed;
 private int scoreValue = 0;
 public Text score;
 private int livesValue = 3;
 public Text lives;
 public Text win;
 public Text lose;
 Animator anim;
 private bool facingRight = true;
 public AudioClip musicClipOne;
 public AudioClip musicClipTwo;
 public AudioSource musicSource;
 public bool isGrounded;
 public Transform groundcheck;
 public float checkRadius;
 public LayerMask WhatIsGround;
 // Start is called before the first frame update
  void Start()
  {

   anim = GetComponent<Animator>();
   rd2d = GetComponent<Rigidbody2D>();
   score.text = "Score: " + scoreValue.ToString();
   lives.text = "Lives: " + livesValue.ToString();
   win.text = "";
   lose.text = "";

  }

  void FixedUpdate()
  {
   isGrounded = Physics2D.OverlapCircle (groundcheck.position, checkRadius,WhatIsGround);
   float hozMovement = Input.GetAxis("Horizontal");
   float vertMovement = Input.GetAxis("Vertical");
   rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));
   
      if (facingRight == false && hozMovement > 0)
   {
     Flip();
   }
   else if (facingRight == true && hozMovement < 0)
   {
     Flip();
   }
     if (Input.GetKeyDown(KeyCode.A))

        {
          anim.SetInteger("State", 1);

         }

     if (Input.GetKeyUp(KeyCode.A))

        {
          anim.SetInteger("State", 0);
        }

     if (Input.GetKeyDown(KeyCode.D))

        {
          anim.SetInteger("State", 1);

         }

     if (Input.GetKeyUp(KeyCode.D))

        {
          anim.SetInteger("State", 0);

         }

      if (Input.GetKeyDown(KeyCode.W) && isGrounded)
      {
        JumpAction();
        anim.SetInteger("State", 2);
      }

     if (Input.GetKeyUp(KeyCode.W) && isGrounded)
      {
        JumpAction();
        anim.SetInteger("State", 0);
      }

   if (Input.GetKey("escape"))
    {
     Application.Quit();
    }
  }

  private void OnCollisionEnter2D(Collision2D collision)
  {
    if (collision.collider.tag == "Coin")
    {
     scoreValue += 1;
     score.text = "Score: " + scoreValue.ToString();
     Destroy(collision.collider.gameObject);
          
      if (scoreValue == 4)
      {
       transform.position = new Vector2(-49f,0);
       livesValue = 3;
       lives.text = "Lives: " + livesValue.ToString();
      }

      if (scoreValue == 8)
      {
       win.text = "You win! this game was made by Yusmari Romero";
        {
          musicSource.clip = musicClipTwo;
          musicSource.Play();
        }
      }
    
    }

    if (collision.collider.tag == "Bad Coin")
    {

     livesValue = livesValue + -1;
     lives.text ="Lives: " + livesValue.ToString();
     Destroy(collision.collider.gameObject);
     if (livesValue == 0)

     {
     lose.text = "You Lose! by Yusmari Romero";
     }

    }
  
  }

  private void onCollisionStay2D(Collision2D collision)
  {
    if (collision.collider.tag == "Ground")
    {
      if (Input.GetKey(KeyCode.W))
      {
        rd2d.AddForce(new Vector2(0,3), ForceMode2D.Impulse);
      }
    }
  }

 void Flip()
  {
   facingRight = !facingRight;
   Vector2 Scaler = transform.localScale;
   Scaler.x = Scaler.x * -1;
   transform.localScale = Scaler;
  }
  private void JumpAction()
    {
        rd2d.AddForce(transform.up * 500f);
        anim.SetInteger( "State",2 );
    }
}