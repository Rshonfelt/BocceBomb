using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class menuControls : MonoBehaviour {
	public bool gameInSession;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void setEasy(){
		PlayerPrefs.SetInt ("bombs", 24);
	}
	public void setMedium(){
		PlayerPrefs.SetInt ("bombs", 16);
	}
	public void setHard(){
		PlayerPrefs.SetInt ("bombs", 8);
	}

	public void startGame(){
		SceneManager.LoadScene (1);
	}
	public void clickExit(){
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		#else
		Application.Quit();
		#endif
	}
}
