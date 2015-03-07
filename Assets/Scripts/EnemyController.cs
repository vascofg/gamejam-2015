using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

	private GameObject player;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit2D hit = Physics2D.Linecast (this.gameObject.transform.position, player.transform.position,
		                                       1 << LayerMask.NameToLayer ("Player"));
		Debug.Log (hit.distance);

	}
}
