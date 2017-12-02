using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Object that receives a callback from a Hitable when the two objects interact
/// </summary>
public class HitReceiver : MonoBehaviour {

    public virtual void receiveHit(Hitable objHit) {
        Debug.Log("Object " + gameObject.name + " hit " + objHit.gameObject.name);
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
