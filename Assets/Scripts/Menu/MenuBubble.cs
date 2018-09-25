using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuBubble : MonoBehaviour {

	float Yspeed;
	float timer;
	public float moveSpeed;
	public float startTimer;
	public float delay;
	bool played = false;
	bool clickedAt = true;

	float startDelay = 4.2f;

	public GameObject bubbles;
	Eastereggs secretScript;

	void Awake ()
	{
		Yspeed = moveSpeed;
		secretScript = GameObject.Find("PlayBubble").GetComponent<Eastereggs>();
	}

	void Update () 
	{
		if (delay > 0)
		{
			delay -= Time.deltaTime;
		} else
		{
			if (!played)
			{
				played = true;
				GetComponent<Animator>().Play("Popped", 0, 1.5f);
			}
			transform.Translate(new Vector2(0, Yspeed / 10) * Time.deltaTime);
			timer -= Time.deltaTime;

			if (timer < 0 && Yspeed > 0)
			{
				Yspeed = -moveSpeed;
				timer = startTimer;
			} else if (timer < 0 && Yspeed < 0)
			{
				Yspeed = moveSpeed;
				timer = startTimer;
			}
		}

		if (startDelay > 0)
		{
			startDelay -= Time.deltaTime;
		} else
		{
			clickedAt = false;
		}
	}

	void OnMouseOver ()
	{
		if (clickedAt == false)
		{
			if (Input.GetMouseButtonDown(0))
			{
				clickedAt = true;
				GetComponent<Animator>().Play("Pop");
				if (transform.name != "PlayBubble")
				{
					StartCoroutine(clicked());
				} else
				{
					StartCoroutine(StartGame());
				}
				if (transform.name == "OptionsBubble")
				{
					Options();
				}
				if (transform.name == "ExitBubble")
				{
					ExitGame();
				}
			}
		}
	}

	IEnumerator StartGame ()
	{
		BubbleTransition.active = true;
		yield return new WaitForSeconds(1.8f);
		SceneManager.LoadScene("Aquarium");
	}

	void ExitGame ()
	{
		Application.Quit();
	}

	void Options ()
	{
		print ("Options");
	}

	IEnumerator clicked ()
	{
		string letter = GetComponentInChildren<Text>().text;
		GetComponentInChildren<Text>().text = "";
		secretScript.MakeString();
		yield return new WaitForSeconds(2.4f);
		GetComponentInChildren<Text>().text = letter;
		clickedAt = false;
	}
}
