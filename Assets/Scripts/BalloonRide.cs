using UnityEngine;
using System.Collections;

public class BalloonRide : MonoBehaviour {
	public GameObject[] waypoints;
	public float speed;
	private GameObject player;
	private int i = 0;
	private bool moving = false;

	void OnTriggerEnter2D (Collider2D collider)
	{
		if(collider.CompareTag("Player")) {
			//collider.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up*1000);
			player = collider.gameObject;
			moving = true;
			Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
			rb.gravityScale = 0;
			rb.velocity = Vector2.zero;
			player.GetComponent<PlayerControl>().canMove = false;
			Destroy(GameObject.FindGameObjectWithTag("Title"));
		}
	}

	void Update()
	{
		if (moving) {
			float step = speed * Time.deltaTime;
			transform.position = Vector3.MoveTowards (transform.position, waypoints[i].transform.position, step);
			player.transform.position = transform.position;
			if(transform.position.Equals(waypoints[i].transform.position)) {
				i++;
			}
			if(i==waypoints.Length) {
				moving = false;
				player.GetComponent<Rigidbody2D>().gravityScale=1;
				player.GetComponent<PlayerControl>().canMove = true;
				gameObject.GetComponent<Animator>().SetTrigger("pop");
				GameObject.FindGameObjectWithTag("Mountain").GetComponent<Animator>().SetTrigger("frito");
				GameObject.FindGameObjectWithTag("Terrain").GetComponent<Animator>().SetTrigger("frito");
				Camera.main.GetComponent<UnityStandardAssets.ImageEffects.NoiseAndGrain>().enabled = true;
				Camera.main.GetComponent<AudioSource>().enabled = true;
				GameObject.FindGameObjectWithTag("Bar").GetComponent<Canvas>().enabled = true;
				Destroy (gameObject,1.5f);
			}
		}
	}

	void OnDestroy()
	{
		Camera.main.GetComponent<UnityStandardAssets.ImageEffects.NoiseAndGrain>().enabled = false;
		Camera.main.GetComponent<AudioSource> ().enabled = false;
	}
}
