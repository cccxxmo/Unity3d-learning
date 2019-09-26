using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.game;


public class UserGUI : MonoBehaviour
{
	private IUserAction action;
	private float width, height;
	private string stateTitle;

	void Start()
	{
		stateTitle = "Start";
		action = SSDirector.getInstance().currentScenceController as IUserAction;
	}

	float castw(float scale)
	{
		return (Screen.width - width) / scale;
	}

	float casth(float scale)
	{
		return (Screen.height - height) / scale;
	}

	void OnGUI()
	{
		width = Screen.width / 12;
		height = Screen.height / 12;

		GUI.Label(new Rect(10, 130, 80, 30), "Time: "+((RoundController)SSDirector.getInstance().currentScenceController).count.ToString());
		GUI.Label(new Rect(10, 100, 80, 30), "Round "+((RoundController)SSDirector.getInstance().currentScenceController).getRound());
		GUI.Label(new Rect(10, 70, 80, 30), "Score "+((RoundController)SSDirector.getInstance().currentScenceController).scoreRecorder.getScore().ToString());

		if (GUI.Button (new Rect (10, 40, 80, 30), "Restart")) {
			SSDirector.getInstance ().currentScenceController.Restart ();
		}

		if (SSDirector.getInstance().currentScenceController.state != State.WIN && SSDirector.getInstance().currentScenceController.state != State.LOSE
			&& GUI.Button(new Rect(10, 10, 80, 30), stateTitle))
		{
			if (stateTitle == "Start") {
				stateTitle = "Pause";
				SSDirector.getInstance().currentScenceController.Resume();
			}
			else if (stateTitle == "Continue")
			{
				stateTitle = "Pause";
				SSDirector.getInstance().currentScenceController.Resume();
			}
			else
			{
				stateTitle = "Continue";
				SSDirector.getInstance().currentScenceController.Pause();
			}
		}

		if (SSDirector.getInstance().currentScenceController.state == State.WIN)
		{
			GUI.Label(new Rect(castw(2f), casth(6f), width, height), "You Win!");
		}
		else if (SSDirector.getInstance().currentScenceController.state == State.LOSE)
		{
			GUI.Label(new Rect(castw(2f), casth(6f), width, height), "You Lose!");
		}
	}
	
	void Update()
	{
		action.shoot();
	}

}