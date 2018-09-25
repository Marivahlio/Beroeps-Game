using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BubbleTransition : MonoBehaviour {

	public static bool active;
	public static string secretCode;
	float timer = 6f;
	bool setCode = false;
	bool SpawnOnce = true;

	public GameObject piratesTheme, KonosubaEffect;
	
	// Update is called once per frame
	void Update () 
	{
		DontDestroyOnLoad(gameObject);

		if (active && timer > 0)
		{
			active = true;
			if (secretCode == "AQUA" && SpawnOnce)
			{
				SpawnOnce =false;
				Instantiate(KonosubaEffect, Vector2.zero, Quaternion.identity);			
			}
			transform.Translate(Vector2.up * 7.345f * Time.deltaTime);
			timer -= Time.deltaTime;
		}

		Scene currentScene = SceneManager.GetActiveScene();

		if (currentScene.name == "Aquarium" && !setCode)
		{
			setCode = true;
			if (secretCode == "AQUA")
			{
				GameObject.Find("EasterAqua").GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
				GameObject.Find("EasterKazuma").GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
			}
			if (secretCode == "RUM")
			{
				Instantiate(piratesTheme, Vector2.zero, Quaternion.identity);
				GameObject.Find("EasterJack").GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
			}
		}
	}
}
