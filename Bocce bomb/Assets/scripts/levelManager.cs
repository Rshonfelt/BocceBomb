using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class levelManager : MonoBehaviour {

	public int buildingCount; 
	public GameObject building;
	public GameObject menuPanel;
	public GameObject endGame;
	public GameObject youWin;
	public GameObject youLose;
	public GameObject bocce;
	public Vector3 origin;
	public menuControls menu;
	public int bombs = 1;
	public int score;
	public Text bombsLeft;
	public Text scoreText;
	public bool playing;

	public List<GameObject> buildings = new List<GameObject> ();
	// Use this for initialization
	void Start () {
		buildLevel ();
		origin = bocce.transform.position;
		bombs = PlayerPrefs.GetInt("bombs");
        playing = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			menuPanel.SetActive (true);
			Time.timeScale = 0;
			playing = false;
			bocce.GetComponent<addforce> ().taps = 0;
		}
		//if (!menuPanel.activeSelf) {
		//	Time.timeScale = 1;
		//	playing = true;
		//	bocce.GetComponent<addforce> ().taps = 1;
		//}
		//if (Input.GetKeyDown (KeyCode.Escape) && menuPanel.activeSelf && menu.gameInSession) {
		//	menuPanel.SetActive (false);
		//	Time.timeScale = 1;
		//	playing = true;
		//	bocce.GetComponent<addforce> ().taps = 1;
		//}
		bombsLeft.text = bombs.ToString ();
		scoreText.text = score.ToString ();

		if (bombs == 0 || score == buildingCount) {
			bocce.SetActive (false);
			endGame.SetActive (true);
			if (score == buildingCount) {
				youWin.SetActive (true);
				youLose.SetActive (false);
				score = 0;
			} else {
				youWin.SetActive (false);
				youLose.SetActive (true);
			}
			playing = false;

		}
	}
	public void stateController(){
		playing = !playing;
	}

	public void destroyBuildings(){

		foreach (GameObject building in buildings) {
			Destroy (building);
		}
		buildings.Clear ();
	}
	public void setBocce(){
		bocce.transform.position = origin;
	}

	public void setEasy(){
		score = 0;
		bombs = 24;
		buildLevel ();
		setBocce ();
		stateController ();
		bocce.GetComponent<addforce> ().taps = 1;
	}
	public void setMedium(){
		score = 0;
		bombs = 16;
		buildLevel ();
		setBocce ();
		stateController ();
		bocce.GetComponent<addforce> ().taps = 1;
	}
	public void setHard(){
		score = 0;
		bombs = 8;
		buildLevel ();
		setBocce ();
		stateController ();
		bocce.GetComponent<addforce> ().taps = 1;
	}

	public void buildLevel(){
		destroyBuildings ();
		for (int i = 0; i < buildingCount; i++) {
			placeBuilding ();
		}

	}
	public void placeBuilding(){
		
		Vector3 location = spawnLocation ();
		GameObject temp = (GameObject)Instantiate (building, location, Quaternion.identity);
		buildings.Add (temp);

	}

	public Vector3 spawnLocation(){
		float X = Random.Range (10, 90);
		float Z = Random.Range (50, 180);
		float Y = 0;
		Vector3 newLocation = new Vector3 (X, Y, Z);
		for (int i = 0; i < buildings.Count; i++) {
			if (Vector3.Distance (newLocation, buildings [i].transform.position) < 15) {
				newLocation = spawnLocation ();
			}
		}

		return newLocation;
		}


}
