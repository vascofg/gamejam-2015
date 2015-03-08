using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

	[HideInInspector]
	public bool facingRight = true;			// For determining which way the player is currently facing.
	private Transform player;
	public float moveForce = 365f;			// Amount of force added to move the player left and right.
	public float maxSpeed = 5f;				// The fastest the player can travel in the x axis.

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
		float moveDir = (player.position.x - gameObject.transform.position.x)>0?1:-1;

		// If the player is changing direction (h has a different sign to velocity.x) or hasn't reached maxSpeed yet...
		if(moveDir * GetComponent<Rigidbody2D>().velocity.x < maxSpeed)
			// ... add a force to the player.
			GetComponent<Rigidbody2D>().AddForce(Vector2.right * moveDir * moveForce);

		// If the player's horizontal velocity is greater than the maxSpeed...
		if(Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x) > maxSpeed)
			// ... set the player's velocity to the maxSpeed in the x axis.
			GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Sign(GetComponent<Rigidbody2D>().velocity.x) * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);


		if(moveDir > 0 && !facingRight)
			// ... flip the player.
			Flip();
		// Otherwise if the input is moving the player left and the player is facing right...
		else if(moveDir < 0 && facingRight)
			// ... flip the player.
			Flip();
	}

	void OnCollisionEnter2D (Collision2D collision)
	{
		if (collision.collider.CompareTag ("Player")) {
			collision.collider.gameObject.GetComponent<PlayerControl>().Hurt();
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

	public void Kill()
	{
		//anim trigger die
		Destroy (gameObject);
	}
}
