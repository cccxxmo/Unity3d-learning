using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Com.game;

public enum State { WIN, LOSE, PAUSE, CONTINUE, START };

public class RoundController : MonoBehaviour, ISceneController, IUserAction{

	public UFOFactory ufoFactory;
	public RoundActionManager actionManager;
	public PhysisManager physisManager;
	public ScoreRecorder scoreRecorder;
	private List<GameObject> ufos;
	private int round;
	private GameObject shootAtSth;
	GameObject explosion;

	public int CoolTimes = 3;
	public Text CountdownText;

	public State state { get; set; }

	public int leaveSeconds;
	public int leaveSecond2;

	public int count;

	IEnumerator DoCountDown()
	{
		while (leaveSeconds >= 0)
		{
			if (leaveSeconds >= 30) {
				CountdownText.text = (leaveSeconds - 30).ToString ();
			} else {
				CountdownText.text = "";
			}
			yield return new WaitForSeconds(1);
			leaveSeconds--;
		}
	}

	void Awake()
	{
		SSDirector director = SSDirector.getInstance();
		director.setFPS(60);
		director.currentScenceController = this;

		LoadResources();

		ufoFactory = Singleton<UFOFactory>.Instance;
		scoreRecorder = Singleton<ScoreRecorder>.Instance;
		actionManager = Singleton<RoundActionManager>.Instance;
		physisManager = Singleton<PhysisManager>.Instance;

		leaveSeconds = 33;
		leaveSecond2 = 30;

		count = leaveSecond2;

		state = State.PAUSE;

		ufos = new List<GameObject>();
	}


	void Start () {
		round = 1;
		LoadResources();
	}

	void Update()
	{
		LaunchUFO();
		Check();
		RecycleUFO();
	}

	public void LoadResources()
	{
		Camera.main.transform.position = new Vector3(0, 0, -15);
		explosion = Instantiate(Resources.Load("prefabs/ParticleSys"), new Vector3(-40, 0, 0), Quaternion.identity) as GameObject;

	}
	public int getRound(){
		return round;
	}
	public void shoot()
	{
		if (Input.GetMouseButtonDown(0) && (state == State.START || state == State.CONTINUE))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit))
			{
				if ((SSDirector.getInstance().currentScenceController.state == State.START || SSDirector.getInstance().currentScenceController.state == State.CONTINUE))
				{
					shootAtSth = hit.transform.gameObject;

					explosion.transform.position = hit.collider.gameObject.transform.position;
					explosion.GetComponent<Renderer>().material = hit.collider.gameObject.GetComponent<Renderer>().material;
					explosion.GetComponent<ParticleSystem>().Play();
				}
			}
		}
	}

	public void LaunchUFO()
	{
		if(count - leaveSeconds== 1)
		{
			count = leaveSeconds;
			GameObject ufo = ufoFactory.GetUFO(round);
			Debug.Log(ufo);
			ufos.Add(ufo);
			if(actionManager.If_Active == 1)
			    actionManager.addRandomAction (ufo);
			else
			    physisManager.addRandomAction (ufo);
		}
	}

	public void RecycleUFO()
	{
		for(int i = 0; i < ufos.Count; i++)
		{
			if( ufos[i].transform.position.z < -18)
			{
				ufoFactory.FreeUFO(ufos[i]);
				ufos.Remove(ufos[i]);
			}
		}
	}



	public void Check()
	{
		if(shootAtSth != null && shootAtSth.transform.tag == "UFO" && shootAtSth.activeInHierarchy)
		{
			scoreRecorder.Record(shootAtSth);
			ufoFactory.FreeUFO(shootAtSth);
			shootAtSth = null;
		}

		if(scoreRecorder.getScore() > 500 * round)
		{
			round++;
			leaveSeconds = count = 30;
		}

		if (round == 4) 
		{
			StopAllCoroutines();
			state = State.WIN;
		}
		else if (leaveSeconds == 0 && scoreRecorder.getScore() < 500 * round)
		{
			StopAllCoroutines();
			state = State.LOSE;
		} 
		else
			state = State.CONTINUE;

	}

	public void Pause()
	{
		state = State.PAUSE;
		CoolTimes = 3;
		StopAllCoroutines();
		for (int i = 0; i < ufos.Count; i++)
		{
			ufos[i].SetActive(false);
		}
	}

	public void Resume()
	{
		StartCoroutine(DoCountDown());         
		state = State.CONTINUE;
		for (int i = 0; i < ufos.Count; i++)
		{
			ufos[i].SetActive(true);
		}
	}

	public void Restart()
	{
		CoolTimes = 3;
		scoreRecorder.Reset();
		Application.LoadLevel(Application.loadedLevelName);
		SSDirector.getInstance().currentScenceController.state = State.START;
	}

}
