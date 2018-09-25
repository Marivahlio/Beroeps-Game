using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Eastereggs : MonoBehaviour {

	public Text first, second, third, fourth, fifth, sixth, seventh, eight;
	public string finalString;

	void Start ()
	{
		MakeString ();
	}

	void Update () 
	{
		print(finalString);
	}

	public void MakeString ()
	{
		if (first.text == null){first.text = "";}
		if (first.text == null){second.text = "";}
		if (first.text == null){third.text = "";}
		if (first.text == null){fourth.text = "";}
		if (first.text == null){fifth.text = "";}
		if (first.text == null){sixth.text = "";}
		if (first.text == null){seventh.text = "";}
		if (first.text == null){eight.text = "";}

		finalString = first.text + second.text + third.text + fourth.text + fifth.text + sixth.text + seventh.text + eight.text;
		BubbleTransition.secretCode = finalString;
	}
}