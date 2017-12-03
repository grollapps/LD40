using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Spin around the Y axis
/// </summary>
public class Rotate : MonoBehaviour {

    public float speed = 0.45f;
    public float startAng = 0;

	// Use this for initialization
	void Start () {
        startAng = Random.Range(-40, 40);
	}
	
	// Update is called once per frame
	void Update () {

        transform.Rotate(speed * Vector3.up, Space.World);
		
	}
}
