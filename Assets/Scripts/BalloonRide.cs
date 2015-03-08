using UnityEngine;
using System.Collections;

public class BalloonRide : MonoBehaviour {

	void OnTriggerEnter2D (Collider2D collider)
	{
		if(collider.CompareTag("Player")) {
			collider.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up*1000);
		}
	}
}
