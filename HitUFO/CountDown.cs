﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CountDown : MonoBehaviour  
{  
	public float timer = 3;  
	public Text CountdownText;
	public int go = 0;

	void Update()  
	{  
		timer -= Time.deltaTime;  
		  
	}
	public void show(){
		if (timer < 0) {
			CountdownText.text = "";
			go = 1;
		} else {
			CountdownText.text = timer.ToString ();
			go = 0;
		}
	}
	public int If_go(){
		return go;
	}
}
