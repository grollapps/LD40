using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Put on any object that can hit another object and receive a pickup or damage
/// </summary>
public class Hitable : MonoBehaviour {

    private Collider myCollider;

    public HitObjType objType;

    void Awake() {
        myCollider = gameObject.GetComponent<Collider>();
        if (myCollider == null) {
            Debug.LogError("Missing collider on gameobject " + gameObject.name);
        }
    }

    void OnCollisionEnter(Collision other) {
        //Debug.Log("Collider " + name  + " hit other " + other.gameObject.name);
        HitReceiver hr = other.gameObject.GetComponent<HitReceiver>();
        if (hr != null) {
            hr.receiveHit(this);
        }
    }

    /// <summary>
    /// Destroys this Hitable
    /// </summary>
    public void Consume() {
        Destroy(gameObject);
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
