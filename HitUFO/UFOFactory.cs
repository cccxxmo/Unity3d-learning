using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UFOFactory : MonoBehaviour {

	private List<GameObject> used_ufo = new List<GameObject>();
	private List<GameObject> free_ufo = new List<GameObject>();

	private Color[] color = { Color.red, Color.green, Color.blue, Color.yellow };

	public GameObject GetUFO(int ruler)
	{
		GameObject ufo;
		if (free_ufo.Count > 0)
		{
			ufo = free_ufo[0];
			free_ufo.Remove(free_ufo[0]);
		}
		else
		{
			ufo = GameObject.Instantiate(Resources.Load("prefabs/UFO")) as GameObject;
			Debug.Log(ufo);
		}

		ufo.GetComponent<UFOData>().size = UnityEngine.Random.Range(0, 7-ruler);
		ufo.GetComponent<UFOData>().color = color[UnityEngine.Random.Range(0, 4)];
		ufo.GetComponent<UFOData>().speed = UnityEngine.Random.Range(5+ruler, 10+ruler);

		ufo.transform.localScale = new Vector3(ufo.GetComponent<UFOData>().size * 2, ufo.GetComponent<UFOData>().size * 0.1f, ufo.GetComponent<UFOData>().size * 2);
		ufo.GetComponent<Renderer>().material.color = ufo.GetComponent<UFOData>().color;
		ufo.SetActive(true);

		used_ufo.Add(ufo);
		return ufo;
	}

	public void FreeUFO(GameObject ufo)
	{
		for(int i = 0; i < used_ufo.Count; i++)
		{
			if(used_ufo[i] == ufo)
			{
				ufo.SetActive(false);
				used_ufo.Remove(used_ufo[i]);
				free_ufo.Add(ufo);
			}
		}
	}
}