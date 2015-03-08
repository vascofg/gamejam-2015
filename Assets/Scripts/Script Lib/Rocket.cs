using UnityEngine;
using System.Collections;

public class Rocket : MonoBehaviour 
{
	public GameObject explosion;		// Prefab of explosion effect.


	void Start () 
	{
		// Destroy the rocket after 2 seconds if it doesn't get destroyed before then.
		Destroy(gameObject, 1);
	}


	void OnExplode()
	{
		// Create a quaternion with a random rotation in the z-axis.
	//	Quaternion randomRotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));

		// Instantiate the explosion where the rocket is with the random rotation.
	//	Instantiate(explosion, transform.position, randomRotation);
	}
	
	void OnTriggerEnter2D (Collider2D col) 
	{
		// If it hits an enemy...
		if(col.CompareTag("Enemy"))
		{
			// ... find the Enemy script and call the Hurt function.
			col.gameObject.GetComponent<EnemyController>().Kill();
			// Call the explosion instantiation.
			OnExplode();
		}
		if(col.CompareTag("Enemy") || col.CompareTag("Terrain"))
			Destroy (gameObject);

	}
}
