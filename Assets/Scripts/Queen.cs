using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queen : Bee {

    private Mover myMover;

    public GameObject dronePrefab;
    public Vector3 droneSpawnOffset;

    void Awake() {
        myMover = GetComponent<Mover>();
    }

    protected override bool SpawnDrone() {
        GameObject drone = Instantiate(dronePrefab);
        int numDrones = Global.instance.inputHandler.getNumMovers();
        Vector3 position = transform.position + (droneSpawnOffset * numDrones);
        drone.GetComponent<Mover>().setStartParams(position, myMover.curSpeed);
        //Automatically activate input on new movers
        Global.instance.hudManager.toggleTrigger(numDrones, true);
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
