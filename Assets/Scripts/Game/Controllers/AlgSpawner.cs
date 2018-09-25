using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlgSpawner : MonoBehaviour {

	public GameObject[] algen;

	void Start () 
	{
		InvokeRepeating("SpawnAlg", 60f, Random.Range(60f, 90f));
	}

	void SpawnAlg ()
	{
		int randAlg = Random.Range(0, algen.Length);
		Instantiate(algen[randAlg], new Vector2(Random.Range(-7.5f, 7.5f), Random.Range(-5f, 5f)), Quaternion.identity);
	}
}
