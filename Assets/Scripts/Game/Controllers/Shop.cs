using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour {

	public int clownfishPrice;
	public int sunfishPrice;
	public int bassPrice;
	public int plantsPrice;
	public int rocksPrice;
	public int foodPrice;
	public int foodUpgradePrice;
	public int maxPlantsUpgradeAmnt;
	public int maxRocksUpgradeAmnt;
	public int maxFoodUpgradeAmnt;
	int plantsUpgradeAmnt;
	int rocksUpgradeAmnt;
	int foodUpgradeAmnt;
	public static int clownfishCount;
	public static int sunfishCount;
	public static int bassCount;

	public GameObject Board;
	public GameObject shopPanel;
	public GameObject buyButton;
	public GameObject upgradeButton;

	public GameObject ClownfishObject;
	public GameObject SunfishObject;
	public GameObject BassObject;
	public GameObject[] plantArray;
	public GameObject[] RockArray;

	public GameObject FishSpawnSplashEffect;

	Fishes fishes;
	public FishFoodSpawner foodSpawner;
	string item;

	public Text fishFoodText;
	public Text moneyText;
	public Text infoText;
	public Text buyText;
	public Text upgradeText;
	public static int money;

	Vector3 targetPos = new Vector3(450, -500, 0);
	public static bool shopActive = false;

	void Start ()
	{
		shopPanel.transform.position = targetPos;
		fishes = GameObject.FindWithTag("GameController").GetComponent<Fishes>();
		money = 50;
	}

	void Update ()
	{
		fishFoodText.text = FishFoodSpawner.amntOfFood.ToString();
		moneyText.text = money.ToString();
		shopPanel.transform.position = Vector3.Lerp(shopPanel.transform.position, targetPos, 0.2f);
		if (Input.GetKeyDown("escape") && shopActive || Input.GetMouseButtonDown(1) && shopActive)
		{
			HideShop();
		}
	}

	public void ShowShop (string animationName)
	{
		if (!shopActive && TutorialManager.tutorialActive == false)
		{
			Board.GetComponent<Animator>().Play(animationName);
			targetPos = new Vector3(450, 300, 0);
			shopActive = true;
			infoText.text = "";
			item = "";
			buyButton.SetActive(false);
			upgradeButton.SetActive(false);
			Cleaner.cleaning = false;
			if (fishes.currentFish != null)
			fishes.currentFish.GetComponent<FishController>().Deselect();
		} else
		{
			HideShop();
			Board.GetComponent<Animator>().Play(animationName);
		}
		if (fishes.currentFish != null)
		{
			fishes.currentFish.GetComponent<FishController>().controlThis = false;
			fishes.currentFish.GetComponent<FishController>().Xmovement = 0f;
			fishes.currentFish.GetComponent<FishController>().Ymovement = 0f;
		}
	}

	public void ShowItem(string name)
	{
		switch (name)
		{
			case "Clownfish":
				infoText.text = "Clownfish: A clownfish is easy to manage and easily satisfied. +1 CPS";
				buyText.text = "Buy(" + clownfishPrice.ToString() + ")";
				item = "Clownfish";
				buyButton.SetActive(true);
				upgradeButton.SetActive(false);
			break;

			case "Sunfish":
				infoText.text = "Sunfish: Sunfishes are a bit harder to manage and will require more of everything to make it as happy as the clownfish. +2 CPS";
				buyText.text = "Buy(" + sunfishPrice.ToString() + ")";
				item = "Sunfish";
				buyButton.SetActive(true);
				upgradeButton.SetActive(false);
			break;

			case "Bass":
				infoText.text = "Bass: Bass' are the hardest to manage and is only for advanced players. +4 CPS";
				buyText.text = "Buy(" + bassPrice.ToString() + ")";
				item = "Bass";
				buyButton.SetActive(true);
				upgradeButton.SetActive(false);
			break;

			case "Plants":
				infoText.text = "Plants: boosts your decoration to increase happiness.";
				if (plantsUpgradeAmnt != maxPlantsUpgradeAmnt)
				{
					upgradeText.text = "Upgrade(" + plantsPrice.ToString() + ")" + "\n" + "(" + plantsUpgradeAmnt +"/4)";
				}
				else 
				{
					upgradeText.text = "Maxed out" + "\n" + "(" + plantsUpgradeAmnt +"/4)";
				}
				item = "Plants";
				buyButton.SetActive(false);
				upgradeButton.SetActive(true);
			break;
			
			case "Rocks":
				infoText.text = "Rocks: boosts your decoration to increase happiness.";
				if (rocksUpgradeAmnt != maxRocksUpgradeAmnt)
				{
					upgradeText.text = "Upgrade(" + rocksPrice.ToString() + ")" + "\n" + "(" + rocksUpgradeAmnt +"/4)";
				}
				else 
				{
					upgradeText.text = "Maxed out" + "\n" + "(" + rocksUpgradeAmnt +"/4)";
				}
				item = "Rocks";
				buyButton.SetActive(false);
				upgradeButton.SetActive(true);
			break;

			case "Food":
				infoText.text = "Food: Buy food to gain fishfood, upgrade food to unlock better fishfood types.";
				if (foodUpgradeAmnt != maxFoodUpgradeAmnt)
				{
					upgradeText.text = "Upgrade(" + foodUpgradePrice.ToString() + ")" + "\n" + "(" + foodUpgradeAmnt +"/3)";
				}
				else 
				{
					upgradeText.text = "Maxed out" + "\n" + "(" + foodUpgradeAmnt +"/3)";
				}
				buyText.text = "Buy(" + foodPrice.ToString() + ")";
				item = "Food";
				buyButton.SetActive(true);
				upgradeButton.SetActive(true);
			break;
		}
	}


	public void HideShop ()
	{
		targetPos = new Vector3(450, -500, 0);
		shopActive = false;
	}

	public void Buy ()
	{
		switch (item)
		{
			case "Clownfish":
				if (money < clownfishPrice || clownfishCount >= 2)
				return;
				money -= clownfishPrice;
				clownfishCount++;
				Instantiate(ClownfishObject, Vector2.zero, Quaternion.identity);
				Instantiate(FishSpawnSplashEffect, Vector2.zero, Quaternion.identity);
				buyText.text = "Buy(" + clownfishPrice.ToString() + ")";
			break;

			case "Sunfish":
				if (money < sunfishPrice || sunfishCount >= 2)
				return;
				money -= sunfishPrice;
				sunfishCount++;
				Instantiate(SunfishObject, Vector2.zero, Quaternion.identity);
				Instantiate(FishSpawnSplashEffect, Vector2.zero, Quaternion.identity);
				buyText.text = "Buy(" + sunfishPrice.ToString() + ")";
			break;

			case "Bass":
				if (money < bassPrice || bassCount >= 2)
				return;
				money -= bassPrice;
				bassCount++;
				Instantiate(BassObject, Vector2.zero, Quaternion.identity);
				Instantiate(FishSpawnSplashEffect, Vector2.zero, Quaternion.identity);
				buyText.text = "Buy(" + bassPrice.ToString() + ")";
			break;

			case "Food":
				if (money < foodPrice)
				return;
				money -= foodPrice;
				FishFoodSpawner.amntOfFood += 5;
				buyText.text = "Buy(" + foodPrice.ToString() + ")";
			break;
		}
	}

	public void Upgrade ()
	{
		switch (item)
		{
			case "Plants":
				if (money < plantsPrice)
				return;
				if (plantsUpgradeAmnt < maxPlantsUpgradeAmnt)
				{
					money -= plantsPrice;
					plantsPrice += (int)plantsPrice * 2;
					plantsUpgradeAmnt++;
					for (int i = 0; i < plantsUpgradeAmnt; i++)
					{
						plantArray[i].SetActive(true);
					}
					if (plantsUpgradeAmnt != maxPlantsUpgradeAmnt)
					{
						upgradeText.text = "Upgrade(" + plantsPrice.ToString() + ")" + "\n" + "(" + plantsUpgradeAmnt +"/4)";
					}
					else 
					{
						upgradeText.text = "Maxed out" + "\n" + "(" + plantsUpgradeAmnt +"/4)";
					}
				}
			break;
			
			case "Rocks":
				if (money < rocksPrice)
				return;
				if (rocksUpgradeAmnt < maxRocksUpgradeAmnt)
				{
					money -= rocksPrice;
					rocksPrice += (int)rocksPrice * 2;
					rocksUpgradeAmnt++;
					for (int i = 0; i < rocksUpgradeAmnt; i++)
					{
						RockArray[i].SetActive(true);
					}

					if (rocksUpgradeAmnt != maxRocksUpgradeAmnt)
					{
						upgradeText.text = "Upgrade(" + rocksPrice.ToString() + ")" + "\n" + "(" + rocksUpgradeAmnt +"/4)";
					}
					else 
					{
						upgradeText.text = "Maxed out" + "\n" + "(" + rocksUpgradeAmnt +"/4)";
					}
				}
				
			break;

			case "Food":
				if (money < foodUpgradePrice || foodUpgradeAmnt >= 3)
				return;
				money -= foodUpgradePrice;
				foodSpawner.foodLvl++;
				foodUpgradePrice += (int)foodUpgradePrice * 3;
				foodUpgradeAmnt++;
				if (foodUpgradeAmnt != maxFoodUpgradeAmnt)
				{
					upgradeText.text = "Upgrade(" + foodUpgradePrice.ToString() + ")" + "\n" + "(" + foodUpgradeAmnt +"/3)";
				}
				else 
				{
					upgradeText.text = "Maxed out" + "\n" + "(" + foodUpgradeAmnt +"/3)";
				}
			break;
		}
	}
}