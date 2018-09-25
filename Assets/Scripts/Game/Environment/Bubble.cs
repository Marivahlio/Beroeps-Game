using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour {

	Rigidbody2D rb;

	public float lifetime;
	public float moveSpeed;

	void Start ()
	{
		rb = GetComponent<Rigidbody2D>();
		float rand = Random.Range(0.5f, 4f);
		transform.localScale = transform.localScale * rand;
		moveSpeed = rand * 2 + 5;
		GetComponent<SpriteRenderer>().sortingOrder = (int)rand;
	}

	// Update is called once per frame
	void Update () 
	{
		rb.velocity = new Vector2(0, 2) * Time.deltaTime * moveSpeed;

		lifetime -= Time.deltaTime;

		if (lifetime <= 0)
		{
			Destroy(gameObject);
		}
	}
}
