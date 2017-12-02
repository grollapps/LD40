using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : Bee {

    private Mover myMover;

    //public GameObject dronePrefab;
    //public Vector3 droneSpawnOffset;

    void Awake() {
        myMover = GetComponent<Mover>();
    }

    protected override bool SpawnDrone() {
        //GameObject drone = Instantiate(dronePrefab);
        //drone.transform.position += droneSpawnOffset;
        //drone.GetComponent<Mover>().setStartSpeed(myMover.curSpeed);
        return false;
    }

    protected override bool DamagePickup() {
        return true; //TODO
    }

    protected override bool HealthPickup() {
        return true; //TODO
    }

    protected override bool SlowPickup() {
        return true; //TODO
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
