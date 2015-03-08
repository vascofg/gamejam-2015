using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour
{
	public Rigidbody2D rocket;				// Prefab of the rocket.
	public float speed = 20f;				// The speed the rocket will fire at.
	public float bulletOffset = 1;

	private PlayerControl playerCtrl;		// Reference to the PlayerControl script.
	private Animator anim;					// Reference to the Animator component.
	private Rigidbody2D rgbody;

	void Awake()
	{
		// Setting up the references.
		anim = this.GetComponentInParent<Animator>();
		playerCtrl = this.GetComponentInParent<PlayerControl>();
		rgbody = this.GetComponentInParent<Rigidbody2D> ();
	}


	void Update ()
	{
		// If the fire button is pressed...
		if(Input.GetButtonDown("Fire1"))
		{
			// ... set the animator Shoot trigger parameter and play the audioclip.
			anim.SetTrigger("Shoot");
			GetComponent<AudioSource>().Play();

			Vector2 bulletPos = transform.position;

			// If the player is facing right...
			if(playerCtrl.facingRight)
			{
				bulletPos.x+=bulletOffset;
				// ... instantiate the rocket facing right and set it's velocity to the right. 
				Rigidbody2D bulletInstance = Instantiate(rocket, bulletPos, Quaternion.Euler(new Vector3(3f,-1f,0))) as Rigidbody2D;
				bulletInstance.velocity = new Vector2(rgbody.velocity.x + speed, Random.Range(-3,3));
			}
			else
			{
				bulletPos.x-=bulletOffset;
				// Otherwise instantiate the rocket facing left and set it's velocity to the left.
				Rigidbody2D bulletInstance = Instantiate(rocket, bulletPos, Quaternion.Euler(new Vector3(-3f,-1f,0))) as Rigidbody2D;
				bulletInstance.velocity = new Vector2(rgbody.velocity.x - speed, Random.Range(-3,3));
			}
		}
	}
}
