using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum GameMode {
	idle,
	playing,
	levelEnd
}


public class MissionDemolition : MonoBehaviour {
	static public MissionDemolition S;

	public GameObject [] castles;
	public Text textLevel;
	public Text textScore;
	public Vector3 castlePos;
	public bool __________;
	public int level;
	public int levelMax;
	public int shotsTaken;
	public GameObject castle;
	public GameMode mode = GameMode.idle;
	public string showing = "Slingshot";


	// Use this for initialization
	void Start () {
		S = this;
		level = 0;
		levelMax = castles.Length;
		StartLevel ();
	
	}

	void StartLevel(){
		if (castle != null) {
			Destroy(castle);
		}

		GameObject[] gos = GameObject.FindGameObjectsWithTag("Projectile");
		foreach (GameObject pTemp in gos) {
			Destroy(pTemp);
		}
		castle = Instantiate (castles [level])as GameObject;
		castle.transform.position = castlePos;
		shotsTaken = 0;

		SwitchView ("Both");
		ProjectileLine.S.Clear();

		Goal.goalMet = false;

		ShowText();
		mode = GameMode.playing;
	}

	void ShowText(){
		textLevel.text = "Level: " + (level + 1) + " of " + levelMax;
		textScore.text = "Shots Taken: " + shotsTaken;
	}
	
	// Update is called once per frame
	void Update () {
		ShowText ();
		if (mode == GameMode.playing && Goal.goalMet) {
			mode = GameMode.levelEnd;
			SwitchView("Both");
			Invoke("NextLevel",2f);
		}

	
	}

	void NextLevel(){
		level++;
		if (level == levelMax) {
			level = 0;
		}
		StartLevel();
	}

	void OnGUI(){
		Rect buttonRect = new Rect ((Screen.width / 2) - 50, 10, 100, 24);

		switch (showing) {
		case "Slingshot":
			if (GUI.Button (buttonRect, "Show Slingshot")) {
				SwitchView ("Slingshot");
			}
			break;

		case "Castle":
			if (GUI.Button (buttonRect, "Show Castle")) {
				SwitchView ("Castle");
			}
			break;

		case "Both":
			if (GUI.Button (buttonRect, "Show Both")) {
				SwitchView ("Both");
			}
			break;
		}
	}


	 public static void SwitchView(string eView){
		S.showing = eView;
		switch (S.showing) {
		case "Slingshot":
			FollowCam.S.poi = null;
			break;

		case "Castle":
			FollowCam.S.poi = S.castle;
			break;

		case "Both":
			FollowCam.S.poi = GameObject.Find("ViewBoth");
			break;
		}
	}

	public static void ShotFired(){
		S.shotsTaken++;
	}



















}
