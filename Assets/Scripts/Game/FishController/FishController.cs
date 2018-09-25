using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishController : MonoBehaviour {

	public float moveSpeed;
	public float bubbleTime;
	public float startBubbleDelay;

	public float totalHappiness;
	public float hungerAmnt;
	public float decorationAmnt;
	public float friendsAmnt;
	public float dirtynessAmnt;

	public float happinessPerFriend;
	public float happinessPerDecoration;
	public float maxAlgen;
	public int moneyPerSecond;
	int moneyToAdd;

	public bool controlThis = false;
	public GameObject bubble;
	public GameObject mouth;
	string lastlook;
	string fishToBuy;

	public GameObject ClownfishObject;
	public GameObject SunfishObject;
	public GameObject BassObject;

	public float Xmovement;
	public float Ymovement;
	float bubbleDelay;

	Animator anim;
	Fishes fishes;
	
	void Start ()
	{
		anim = GetComponent<Animator>();
		fishes = GameObject.FindWithTag("GameController").GetComponent<Fishes>();
		bubbleTime = Random.Range(2f, 4f);
		InvokeRepeating("AutoMoney", 1f, 1f);
		hungerAmnt = Random.Range(40f, 60f);
	}

	// Update is called once per frame
	void Update () 
	{
		totalHappiness = (hungerAmnt + friendsAmnt + dirtynessAmnt + decorationAmnt) / 4;

		Fishes.fishCount = GameObject.FindGameObjectsWithTag("fish").Length;
		Fishes.decorationCount = GameObject.FindGameObjectsWithTag("decoration").Length;
		friendsAmnt = happinessPerFriend * (Fishes.fishCount - 1);
		decorationAmnt = happinessPerDecoration * Fishes.decorationCount;

		if (Fishes.algCount < maxAlgen)
		{
			dirtynessAmnt += 0.5f;
		} else
		if (Fishes.algCount >= maxAlgen && dirtynessAmnt > 0)
		{
			dirtynessAmnt -= 0.2f;
		} else
		{
			return;
		}

		if (hungerAmnt > 0)
		{
			if (totalHappiness >= 66)
			{
				hungerAmnt -= Time.deltaTime / 4;
			} else if (totalHappiness >= 33 && totalHappiness < 66)
			{
				hungerAmnt -= Time.deltaTime / 2;
			} else if (totalHappiness > 0 && totalHappiness < 33)
			{
				hungerAmnt -= Time.deltaTime;
			}
		}
		else
		{
			print("VERY HUNGRY FISHE");
		}

		if (controlThis && Shop.shopActive == false || controlThis && FishFoodSpawner.feeding)
		{
			Xmovement = Input.GetAxis("Horizontal");
			Ymovement = Input.GetAxis("Vertical");
			anim.SetFloat("Xmovement", Xmovement);
			anim.SetFloat("Ymovement", Ymovement);
			bubbleDelay -= Time.deltaTime;

			transform.Translate(new Vector2(Xmovement, Ymovement).normalized * Time.deltaTime * moveSpeed, Space.World);

			if (Input.GetKeyDown("space") && bubbleDelay <= 0)
			{
				Instantiate(bubble, mouth.transform.position, Quaternion.identity);
				bubbleDelay = startBubbleDelay;
			}

			if (Input.GetMouseButtonDown(1))
			{
				Deselect ();
			}
		} else
		{
			bubbleTime -= Time.deltaTime;
		}

		if (Xmovement > 0)
		{
			lastlook = "right";
		}
		if (Xmovement < 0)
		{
			lastlook = "left";
		}

		if (Xmovement != 0 || Ymovement != 0)
		{
			anim.SetBool("Swimming", true);
			if (Input.GetKeyDown("left shift"))
			{
				moveSpeed = 3;
				anim.speed = 2;
			}
		} else
		{
			anim.SetBool("Swimming", false);
		}

		if (Input.GetKeyUp("left shift"))
		{
			moveSpeed = 1.5f;
			anim.speed = 1;
		}

		if (bubbleTime <= 0)
		{
			Instantiate(bubble, mouth.transform.position, Quaternion.identity);
			bubbleTime = Random.Range(2f, 4f);
		}

		if (totalHappiness > Fishes.maxHappiness)
		{
			totalHappiness = Fishes.maxHappiness;
		}
		if (hungerAmnt > Fishes.maxHunger)
		{
			hungerAmnt = Fishes.maxHunger;
		}
		if (decorationAmnt > Fishes.maxDecoration)
		{
			decorationAmnt = Fishes.maxDecoration;
		}
		if (friendsAmnt > Fishes.maxFriends)
		{
			friendsAmnt = Fishes.maxFriends;
		}
		if (dirtynessAmnt > Fishes.maxDirtyness)
		{
			dirtynessAmnt = Fishes.maxDirtyness;
		}

		if (totalHappiness > 90)
		{
			StartCoroutine(Sell());
		}
	}
	
	void OnMouseOver ()
	{
		if (Input.GetMouseButtonDown(0) && Shop.shopActive != true && FishFoodSpawner.feeding != true)
		{
			Cleaner.cleaning = false;
			if (fishes.currentFish != null)
			{
				if (fishes.currentFish.GetComponent<FishController>().lastlook == "right")
				{
					fishes.currentFish.GetComponent<FishController>().anim.Play("IdleRight");
				} else if (fishes.currentFish.GetComponent<FishController>().lastlook == "left")
				{
					fishes.currentFish.GetComponent<FishController>().anim.Play("IdleLeft");
				}
				
				fishes.currentFish.GetComponent<FishController>().controlThis = false;
				fishes.currentFish.GetComponent<FishController>().Xmovement = 0f;
				fishes.currentFish.GetComponent<FishController>().Ymovement = 0f;
			}
			controlThis = true;
			fishes.currentFish = this.gameObject;			
			Xmovement = 0f;
			Ymovement = 0f;
		}
	}

	void AutoMoney ()
	{
		Shop.money += moneyPerSecond;
	}

	public void Deselect ()
	{
		if (fishes.currentFish.GetComponent<FishController>().lastlook == "right")
		{
			fishes.currentFish.GetComponent<FishController>().anim.Play("IdleRight");
		} else if (fishes.currentFish.GetComponent<FishController>().lastlook == "left")
		{
			fishes.currentFish.GetComponent<FishController>().anim.Play("IdleLeft");
		}
		fishes.currentFish.GetComponent<FishController>().controlThis = false;
		fishes.currentFish.GetComponent<FishController>().Xmovement = 0f;
		fishes.currentFish.GetComponent<FishController>().Ymovement = 0f;
		fishes.currentFish = null;
	}

	IEnumerator Sell ()
	{
		switch (transform.name)
		{
			case "Clownfish(Clone)":
				moneyToAdd = 90;
				anim.Play("Sell");
				fishToBuy = "clownfish";
			break;

			case "Sunfish(Clone)":
				moneyToAdd = 600;
				fishToBuy = "sunfish";
			break;

			case "Bass(Clone)":
				moneyToAdd = 1500;
				fishToBuy = "bass";
			break;
		}
		yield return new WaitForSeconds(1.0f);
		switch (fishToBuy)
		{
			case "clownfish":
				Shop.clownfishCount--;
			break;

			case "sunfish":
				Shop.sunfishCount--;
			break;

			case "bass":
				Shop.bassCount--;
			break;
		}
		Shop.money += moneyToAdd;
		Destroy(gameObject);
	}
}