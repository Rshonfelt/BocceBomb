using UnityEngine;
using System.Collections;

public class buldingSupervisor : MonoBehaviour {
	public ParticleSystem explosion;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void playExplosion(){
		explosion.Play ();
		Destroy (gameObject);
	}

}
