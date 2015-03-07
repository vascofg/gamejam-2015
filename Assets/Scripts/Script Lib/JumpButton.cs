using UnityEngine;
using System.Collections;

public class JumpButton : MonoBehaviour {

	public Transform player;

	void PointerEnter(){
		player.SendMessage ("Jump");
	}

}
