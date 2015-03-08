using UnityEngine;
using System.Collections;

public class GetCandy : MonoBehaviour {

	void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.CompareTag ("Player")) {
			other.gameObject.GetComponent<PlayerControl> ().gotCandy ();
			Destroy (gameObject);
		}
	}
}
