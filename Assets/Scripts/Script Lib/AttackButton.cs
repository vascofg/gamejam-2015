using UnityEngine;
using System.Collections;

public class AttackButton : MonoBehaviour {

	public Transform player;

	void PointerEnter(){
		player.SendMessage ("Attack");
	}
}
