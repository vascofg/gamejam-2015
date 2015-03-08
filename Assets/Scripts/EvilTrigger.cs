using UnityEngine;
using System.Collections;

public class EvilTrigger : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag ("Player")) {
			other.gameObject.GetComponent<PlayerControl>().sanityEnabled = true;
			foreach (Spawner s in gameObject.GetComponentsInChildren<Spawner> ()) {
				s.enabled = true;
			}
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		//should not happen
		if (other.CompareTag ("Player")) {
			other.gameObject.GetComponent<PlayerControl>().sanityEnabled = false;
			foreach (Spawner s in gameObject.GetComponentsInChildren<Spawner> ()) {
				s.enabled = false;
			}
		}
	}

}
