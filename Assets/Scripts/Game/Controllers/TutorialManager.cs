using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour {

	public GameObject[] popUps;
	public int popUpIndex;

	public GameObject tutorialParent;
	public static bool tutorialActive = true;

	// Update is called once per frame
	void Update () 
	{
		for (int i = 0; i < popUps.Length; i++)
		{
			if (i == popUpIndex)
			{
				popUps[i].SetActive(true);
			} else
			{
				popUps[i].SetActive(false);
			}
		}

		if (tutorialActive && Input.GetKeyDown("space"))
		{
			popUpIndex = popUps.Length;
		}

		switch (popUpIndex)
		{
			case 0:
				popUps[popUpIndex].transform.GetChild(0).GetComponent<Text>().text = "Welcome to Aquarium game! \n This short tutorial will teach you how to play. \n To skip the tutorial at any time press 'Spacebar'. \n \n (Left click to continue)";
				if (Input.GetMouseButtonDown(0))
				{
					popUpIndex++;
				}
			break;

			case 1:
				popUps[popUpIndex].transform.GetChild(0).GetComponent<Text>().text = "Tutorial" + popUpIndex.ToString();
				if (Input.GetMouseButtonDown(0))
				{
					popUpIndex++;
				}
			break;

			case 2:
				popUps[popUpIndex].transform.GetChild(0).GetComponent<Text>().text = "BLYAT" + popUpIndex.ToString();
				if (Input.GetMouseButtonDown(0))
				{
					popUpIndex++;
				}
			break;

			case 3:
				tutorialParent.SetActive(false);
				tutorialActive = false;
			break;
		}
	}
}
