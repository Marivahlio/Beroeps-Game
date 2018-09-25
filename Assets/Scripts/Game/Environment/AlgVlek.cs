using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlgVlek : MonoBehaviour {

	SpriteRenderer rend;
	byte alpha;

	void Start ()
	{
		rend = GetComponent<SpriteRenderer>();
		InvokeRepeating("CustomUpdate", 0f, 0.2f);
		Fishes.algCount++;
	}

	void Update ()
	{
		rend.color = new Color32(27, 145, 114, alpha);
	}

	void OnMouseOver ()
	{
		if (Input.GetAxis("Mouse X") != 0 && Cleaner.cleaning == true || Input.GetAxis("Mouse Y") != 0 && Cleaner.cleaning == true)
		{
			if(alpha > 5)
			{
				alpha -= 4;
			} else
			{
				Fishes.algCount--;
				Destroy(gameObject);
			}
		}
	}

	void CustomUpdate ()
	{
		if (alpha < 255)
		{
			alpha++;
		}
	}
}
