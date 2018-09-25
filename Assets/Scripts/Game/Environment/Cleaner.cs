using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cleaner : MonoBehaviour {

	public static bool cleaning = false;
	Vector2 targetPos;
	Vector2 startPos = new Vector2(7f, -4.1f);

	public Fishes fishes;
	
	void Start ()
	{
		transform.position = startPos;
	}

	// Update is called once per frame
	void Update () 
	{
		if (cleaning)
		{
			targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		} else
		{
			targetPos = startPos;
		}

		transform.position = Vector2.Lerp(transform.position, targetPos, 25f * Time.deltaTime);
	}

	void OnMouseOver()
	{
		if (!cleaning && Input.GetMouseButtonDown(0))
		{
			if (fishes.currentFish != null)
			{
				fishes.currentFish.GetComponent<FishController>().Deselect();
			}
			cleaning = true;
		} else if(cleaning && Input.GetMouseButtonDown(0) || cleaning && Input.GetMouseButtonDown(1))
		{
			cleaning = false;
		}
	}
}
