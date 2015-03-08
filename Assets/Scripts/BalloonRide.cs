﻿using UnityEngine;
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
				player.GetComponent<Rigidbody2D>().gravityScale=1;
				player.GetComponent<PlayerControl>().canMove = true;
				Destroy (this.gameObject);
			}
		}
	}
}
