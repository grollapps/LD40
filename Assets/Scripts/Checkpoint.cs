using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {

    // Use this for initialization
    void Start() {

    }

    void OnTriggerEnter(Collider collider) {
        Debug.Log(gameObject.name + ": Checkpoint hit " + collider.gameObject.name);
    }

}