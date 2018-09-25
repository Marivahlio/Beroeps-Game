using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fishes : MonoBehaviour {

	public GameObject currentFish;

	public static int fishCount;
	public static int decorationCount;
	public static int algCount; 
	
	public Image happinessImg;
	public Image hungerImg;
	public Image decorationImg;
	public Image friendsImg;
	public Image dirtynessImg;
	public GameObject canvasTopMid;

	public static float maxHappiness = 100f;
	public static float maxHunger = 100f;
	public static float maxDecoration = 100f;
	public static float maxFriends = 100f;
	public static float maxDirtyness = 100f;

	void Update ()
	{
		if (currentFish != null)
		{
			SetImg(true);
			happinessImg.fillAmount = currentFish.GetComponent<FishController>().totalHappiness / maxHappiness;
			hungerImg.fillAmount = currentFish.GetComponent<FishController>().hungerAmnt / maxHunger;
			decorationImg.fillAmount = currentFish.GetComponent<FishController>().decorationAmnt / maxDecoration;
			friendsImg.fillAmount = currentFish.GetComponent<FishController>().friendsAmnt / maxFriends;
			dirtynessImg.fillAmount = currentFish.GetComponent<FishController>().dirtynessAmnt / maxDirtyness;
		} else
		{
			SetImg(false);
		}
	}

	void SetImg (bool state)
	{
		happinessImg.enabled = state;
		hungerImg.enabled = state;	
		decorationImg.enabled = state;	
		friendsImg.enabled = state;
		dirtynessImg.enabled = state;
		canvasTopMid.SetActive(state);
	}

}
