using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class for all Bee objects
/// </summary>
public class Bee : HitReceiver {

    public override void receiveHit(Hitable objHit) {
        base.receiveHit(objHit);
        switch (objHit.objType) {
            case HitObjType.Spawn_Drone:
                if (SpawnDrone()) {
                    objHit.Consume();
                }
                break;

            case HitObjType.Health:
                if (HealthPickup()) {
                    objHit.Consume();
                }
                break;

            case HitObjType.DamageBlock:
                if (DamagePickup()) {
                    objHit.Consume();
                }
                break;
                
            case HitObjType.Slow:
                if (SlowPickup()) {
                    objHit.Consume();
                }
                break;

            case HitObjType.Hive:
                EndRound();
                break;

        }
    }

    protected virtual bool SpawnDrone() {
        Debug.Log("Spawn Drone");
        return false;
    }

    protected virtual bool HealthPickup() {
        Debug.Log("Health Pickup");
        return false;
    }

    protected virtual bool DamagePickup() {
        Debug.Log("Damage Pickup");
        return false;
    }
    protected virtual bool SlowPickup() {
        Debug.Log("Slow Pickup");
        return false;
    }

    protected virtual bool EndRound() {
        Debug.Log("End Round");
        Global.instance.EndRound();
        return false;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
