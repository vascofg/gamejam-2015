using UnityEngine;
using System.Collections;

public class Start : MonoBehaviour {	
	// Update is called once per frame
	void onMouseEnter () {
		GetComponent<Renderer>().GetComponent<GUIText>().material.color = Color.yellow;
	}

	void onMouseExit () {
		GetComponent<Renderer>().GetComponent<GUIText>().material.color = Color.white;
	}
}
