﻿using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour
{
	[HideInInspector]
	public bool facingRight = true;			// For determining which way the player is currently facing.
	[HideInInspector]
	public bool jump = false;				// Condition for whether the player should jump.
	[HideInInspector]
	public bool canMove = true;
	private bool dead = false;

	[HideInInspector]
	public bool sanityEnabled = false;
	
	public float moveForce = 365f;			// Amount of force added to move the player left and right.
	public float maxSpeed = 5f;				// The fastest the player can travel in the x axis.
	public AudioClip[] jumpClips;			// Array of clips for when the player jumps.
	public AudioClip[] attackClips;			// Array of clips for when the player jumps.
	public AudioClip deathClip;
	public float jumpForce = 1000f;			// Amount of force added when the player jumps.
	public float restartRunTime = 0.3f;

	private Transform groundCheck;			// A position marking where to check if the player is grounded.
	private bool grounded = false;			// Whether or not the player is grounded.
	private float timeSinceAtt;
	private float speedX = 1f;
	private bool attack = false;
	private bool recover = false;
	private Animator anim;					// Reference to the player's animator component.
	//private GameObject weapon;
	
	public bool hasStick = false;
	public bool hasStone = false;
	public bool hasHay = false;
	private GameObject uiStick;
	private GameObject uiStone;
	private int sanity = 1000;
	private GameObject uiHay;
	
	void Awake()
	{
		// Setting up references.
		groundCheck = transform.Find("groundCheck");
		anim = this.GetComponentInChildren<Animator>();
		uiStick = GameObject.Find("ui_stickHUD");
		uiStone = GameObject.Find("ui_stoneHUD");
		uiHay = GameObject.Find("ui_hayHUD");
		//weapon = GameObject.FindGameObjectWithTag ("Weapon");
	}

	void Update()
	{
		// The player is grounded if a linecast to the groundcheck position hits anything on the ground layer.
		grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));  

		// If the jump button is pressed and the player is grounded then the player should jump.
		if(Input.GetButtonDown("Jump") && grounded)
			jump = true;
		
		if(Input.GetButtonDown("Fire1"))
			attack = true;
	}
	
	
	void FixedUpdate ()
	{
		if (!dead) {
			if (sanityEnabled)
				sanity -= 1;
			if(sanity <=0)
			{
				sanity = 0;
				GameObject.FindGameObjectWithTag("Mountain").GetComponent<Animator>().SetTrigger("frito");
				GameObject.FindGameObjectWithTag("Terrain").GetComponent<Animator>().SetTrigger("frito");
			}

			// Cache the horizontal input.
			float speedX = Input.GetAxis("Horizontal");
			
			
			// The Speed animator parameter is set to the absolute value of the horizontal input.
			anim.SetFloat("Speed", Mathf.Abs(speedX));
			anim.SetBool("Grounded", grounded);
			
			// If the player is changing direction (h has a different sign to velocity.x) or hasn't reached maxSpeed yet...
			if(canMove && speedX * GetComponent<Rigidbody2D>().velocity.x < maxSpeed)
				// ... add a force to the player.
				GetComponent<Rigidbody2D>().AddForce(Vector2.right * speedX * moveForce);
			
			// If the player's horizontal velocity is greater than the maxSpeed...
			if(canMove && Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x) > maxSpeed)
				// ... set the player's velocity to the maxSpeed in the x axis.
				GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Sign(GetComponent<Rigidbody2D>().velocity.x) * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);

			// If the input is moving the player right and the player is facing left...
			if(speedX > 0 && !facingRight)
				// ... flip the player.
				Flip();
			// Otherwise if the input is moving the player left and the player is facing right...
			else if(speedX < 0 && facingRight)
				// ... flip the player.
				Flip();
		}
		// If the player should jump...
		if(jump && !dead)
		{
			// Set the Jump animator trigger parameter.
			anim.SetTrigger("Jump");
			
			// Play a random jump audio clip.
			StartCoroutine("JumpSound");
			
			// Add a vertical force to the player.
			GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce));
			
			// Make sure the player can't jump again until the jump conditions from Update are satisfied.
			jump = false;
		}
		
		if(attack && !dead)
		{
			// Set the Jump animator trigger parameter.
			/*anim.SetTrigger("Attack");
			if(anim.GetFloat("Speed") < 0.3f)
				StartCoroutine("AttackSound");*/
			//weapon.SendMessage("Attacking");
			// Play a random jump audio clip.
			//int i = Random.Range(0, jumpClips.Length);
			//AudioSource.PlayClipAtPoint(jumpClips[i], transform.position);
			
			// Add a vertical force to the player.
			//rigidbody2D.AddForce(new Vector2(0f, jumpForce));
			
			// Make sure the player can't jump again until the jump conditions from Update are satisfied.
			//weapon.SendMessage("Idle");
			timeSinceAtt = 0;
			recover = true;
			attack = false;
		}
		
		if (recover && !dead) {
			if(speedX >= 1f)
				recover = false;
			else
				speedX += 0.005f;
			
		}
	}	
	
	void Flip ()
	{
		// Switch the way the player is labelled as facing.
		facingRight = !facingRight;
		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
	
	IEnumerator JumpSound(){
		int i = Random.Range(0, jumpClips.Length);
		//AudioSource.PlayClipAtPoint(jumpClips[i], transform.position, 0.3f);
		yield return null;
	}
	
	IEnumerator AttackSound(){
		int i = Random.Range(0, attackClips.Length);
		AudioSource.PlayClipAtPoint(attackClips[i], transform.position, 1.0f);
		yield return null;
	}
	
	IEnumerator DeathSound(){
		AudioSource.PlayClipAtPoint(deathClip, transform.position);
		yield return null;
	}

	
	public void Stone(){
		Debug.Log("I have a stone!");
		hasStone = true;
		uiStone.SendMessage("Captured");
		//Play Capture Sound
	}
	
	public void Stick(){
		Debug.Log("I have a stick!");
		hasStick = true;
		uiStick.SendMessage("Captured");
		//Play Capture Sound
	}
	
	public void Hay(){
		Debug.Log("I have a Hay!");
		hasHay = true;
		uiHay.SendMessage("Captured");
		//Play Capture Sound
	}
	
	public void Die(){
		dead = true;
		anim.SetTrigger("Die");
		Destroy (gameObject, 0.2f);
	}
	
	public void Jump(){
		if (grounded)
			jump = true;
	}
	
	public void Attack(){
		attack = true;	
		if(grounded)
			speedX = 0;
	}

	public void Hurt(){
		Debug.Log ("HURT!");
	}
	
	IEnumerator ReloadGame()
	{
		// ... pause briefly
		yield return new WaitForSeconds(2);
		// ... and then reload the level.
		Application.LoadLevel("LevelMobile");
	}

	public void gotCandy() {
		sanity += 100;
		if(sanity > 1000)
			sanity = 1000;
	}
}
