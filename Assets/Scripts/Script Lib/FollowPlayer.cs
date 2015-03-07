﻿using UnityEngine;
using System.Collections;

public class FollowObject : MonoBehaviour
{
	public Vector3 offset;			// The offset at which the Health Bar follows the player.
	
	public Transform player;		// Reference to the player.

	void Update ()
	{
		// Set the position to the player's position with the offset.
		transform.position = player.position + offset;
	}
}
