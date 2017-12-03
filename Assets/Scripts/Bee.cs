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
                if (HealthPickup(objHit)) {
                    objHit.Consume();
                }
                break;

            case HitObjType.FixedBlock:
                DamagePickup(objHit);
                break;

            case HitObjType.DamageBlock:
                if (DamagePickup(objHit)) {
                    objHit.Consume();
                }
                break;

            case HitObjType.Slow:
                if (SlowPickup(objHit)) {
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

    protected virtual bool HealthPickup(Hitable objHit) {
        Debug.Log("Health Pickup");
        return false;
    }

    protected virtual bool DamagePickup(Hitable objHit) {
        Debug.Log("Damage Pickup");
        return false;
    }
    protected virtual bool SlowPickup(Hitable objHit) {
        Debug.Log("Slow Pickup");
        return false;
    }

    protected virtual bool EndRound() {
        Debug.Log("End Round");
        Global.instance.EndRound();
        return false;
    }

}