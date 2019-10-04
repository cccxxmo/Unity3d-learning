using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreRecorder : MonoBehaviour {
	private float score;

	public float getScore()
	{
		return score;
	}

	public void Record(GameObject ufo)
	{
		score += (100 - ufo.GetComponent<UFOData>().size *(20 - ufo.GetComponent<UFOData>().speed));

		Color c = ufo.GetComponent<UFOData>().color;
		switch (c.ToString())
		{
		case "red":
			score += 50;
			break;
		case "green":
			score += 40;
			break;
		case "blue":
			score += 30;
			break;
		case "yellow":
			score += 10;
			break;
		}
	}

	public void Reset()
	{
		score = 0;
	}
}
