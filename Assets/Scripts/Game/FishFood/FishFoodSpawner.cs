using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishFoodSpawner : MonoBehaviour {

	public static bool feeding = false;
	public static int amntOfFood;
	public Sprite foodSprite;
	public Sprite crossSprite;
	public GameObject[] foodObjects;
	public int foodLvl;
	int rand;

	public float startTimeBtwnFeed;
	float timeBtwnFeed = 0;

	Vector2 spawnpos;
	Fishes fishes;

	void Start ()
	{
		fishes = GameObject.FindWithTag("GameController").GetComponent<Fishes>();
		amntOfFood = 10;
	}

	// Update is called once per frame
	void Update ()
	{
		if (feeding && Input.GetMouseButton(0) && amntOfFood > 0 && timeBtwnFeed <= 0)
		{
			timeBtwnFeed = startTimeBtwnFeed;
			amntOfFood--;
			spawnpos = Camera.main.ScreenToWorldPoint((Vector2)Input.mousePosition);
			switch (foodLvl)
			{
				case 0:
					rand = Random.Range(0, foodObjects.Length - 3);
				break;

				case 1:
					rand = Random.Range(0, foodObjects.Length - 2);
				break;

				case 2:
					rand = Random.Range(0, foodObjects.Length - 1);
				break;

				case 3:
					rand = Random.Range(0, foodObjects.Length);
				break;
			}
			if (spawnpos.x > 6.8)
			{
				spawnpos.x = 6.8f;
			}
			if (spawnpos.x < -6.8)
			{
				spawnpos.x = -6.8f;
			}
			Instantiate(foodObjects[rand], new Vector2(spawnpos.x, 4f), Quaternion.identity);
		}

		if (timeBtwnFeed > 0)
		{
			timeBtwnFeed -= Time.deltaTime;
		}

		if (Input.GetMouseButtonUp(0))
		{
			timeBtwnFeed = 0;
		}

		if (feeding && Input.GetMouseButtonDown(1))
		{
			StopFeeding();
		}
	}

	void OnMouseOver ()
	{
		if (Input.GetMouseButtonDown(0) && !feeding && TutorialManager.tutorialActive == false)
		{
			StartCoroutine(StartFeeding ());
			Cleaner.cleaning = false;
		} else
		if (Input.GetMouseButtonDown(0) && feeding)
		{
			StopFeeding ();
		}
	}

	IEnumerator StartFeeding ()
	{
		GetComponent<SpriteRenderer>().sprite = crossSprite;
		if (fishes.currentFish != null)
		{
			fishes.currentFish.GetComponent<FishController>().controlThis = false;
			fishes.currentFish.GetComponent<FishController>().Xmovement = 0f;
			fishes.currentFish.GetComponent<FishController>().Ymovement = 0f;
		}
		yield return new WaitForSeconds(0.1f);
		feeding = true;
	}

	public void StopFeeding ()
	{
		feeding = false;
		GetComponent<SpriteRenderer>().sprite = foodSprite;
	}
}
