using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishFood : MonoBehaviour {

	public float moveSpeed;
	float foodAmnt;

	Rigidbody2D rb;
	
	void Start ()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update () 
	{
		if (transform.position.y > -2.5f)
		{
			rb.velocity = Vector2.down * Time.deltaTime * moveSpeed;
		} else {
			rb.velocity = Vector2.zero;
		}
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.name == "Mouth")
		{
			switch (transform.name)
			{
				case "FishFood1(Clone)":
					foodAmnt = 5;
				break;

				case "FishFood2(Clone)":
					foodAmnt = 10;
				break;

				case "FishFood3(Clone)":
					foodAmnt = 20;
				break;

				case "FishFood4(Clone)":
					foodAmnt = 25;
				break;
			}
			if (other.transform.parent.GetComponent<FishController>().hungerAmnt + foodAmnt <= 100)
			{
				other.transform.parent.GetComponent<FishController>().hungerAmnt += foodAmnt;
			} else
			{
				other.transform.parent.GetComponent<FishController>().hungerAmnt = 100;
			}
			Destroy(gameObject);	
		}
	}
}
