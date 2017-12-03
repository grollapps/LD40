using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queen : Bee {

    private HitPoints hp;
    private Mover myMover;

    public GameObject dronePrefab;
    public Vector3 droneSpawnOffset;

    void Awake() {
        myMover = GetComponent<Mover>();
        hp = GetComponent<HitPoints>();
        if (hp == null) {
            Debug.LogError("No hit point component on Queen");
        }
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

    protected override bool DamagePickup(Hitable objHit) {
        bool dead = hp.decreaseHp((int)objHit.customValue);
        if (dead) {
            Global.instance.FailLevel();
        }
        return true;
    }

    protected override bool HealthPickup(Hitable objHit) {
        hp.increaseHp((int)objHit.customValue);
        return true;
    }

    protected override bool SlowPickup(Hitable objHit) {
        Global.instance.inputHandler.DecreaseAllSpeed(objHit.customValue);
        return true;
    }

}