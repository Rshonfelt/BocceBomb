using UnityEngine;
using System.Collections;

public class lookAt : MonoBehaviour {
	public Transform sphere;
	Vector3 position;
	public int cameraDistance;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		position = transform.position;
		position.x = sphere.position.x;
		position.z = sphere.position.z - cameraDistance;
		transform.LookAt (sphere);

		transform.position = Vector3.Lerp (transform.position, position, Time.deltaTime);
	}
}
