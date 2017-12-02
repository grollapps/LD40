using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : Bee {

    public GameObject workerPrefab;

    private Mover myMover;

    //public GameObject dronePrefab;
    //public Vector3 droneSpawnOffset;

    void Awake() {
        myMover = GetComponent<Mover>();
    }

    public bool SpawnWorker() {
        GameObject worker = Instantiate(workerPrefab);
        worker.GetComponent<RadialMover>().setParent(transform);
        return true;
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
        SpawnWorker(); //Test, remove
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
