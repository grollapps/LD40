using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : Bee {

    private HitPoints hp;
    public GameObject workerPrefab;

    private Mover myMover;

    //public GameObject dronePrefab;
    //public Vector3 droneSpawnOffset;

    void Awake() {
        myMover = GetComponent<Mover>();
        hp = GetComponent<HitPoints>();
        if (hp == null) {
            Debug.LogError("No hit point component on Drone");
        }
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
        bool dead = hp.decreaseHp(1);
        if (dead) {
            //todo
        }
        return true; 
    }

    protected override bool HealthPickup() {
        hp.increaseHp(1);
        return true; //TODO
    }

    protected override bool SlowPickup() {
        Global.instance.inputHandler.DecreaseAllSpeed(1);
        return true; 
    }

    // Use this for initialization
    void Start () {
        int numToSpawn = 1;
        float rand = Random.Range(0, 12);
        if (rand >= 11) {
            numToSpawn = 5;
        } else if (rand > 9) {
            numToSpawn = 4;
        } else if (rand > 6) {
            numToSpawn = 3;
        } else if (rand > 3) {
            numToSpawn = 2;
        } else {
            numToSpawn = 1;
        }
        for (int i = 0; i < numToSpawn; i++) {
            SpawnWorker();
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
