using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queen : Bee {

    public GameObject dronePrefab;
    public Vector3 droneSpawnOffset;

    protected override bool SpawnDrone() {
        GameObject drone = Instantiate(dronePrefab);
        drone.transform.position += droneSpawnOffset;
        return true;
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
