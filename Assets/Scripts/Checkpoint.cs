using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {

    //Flag to ignore receiving events on this particular checkpoint.  Set true on player to avoid extra checks
    public bool ignoreTriggerEnter = false;

    private int cpIndex = -1;
    private bool hit = false;

    // Use this for initialization
    void Start() {
        hit = false;
    }

    public void setCpIndex(int idx) {
        cpIndex = idx;
    }

    public int getIndex() {
        return cpIndex;
    }

    void OnTriggerEnter(Collider collider) {
        if (!ignoreTriggerEnter) {
            if (!hit) {
                //Debug.Log(gameObject.name + ": Checkpoint hit " + collider.gameObject.name);
                hit = true;
                Global.instance.curLevel.advanceCheckpoint(cpIndex);
            }
        }
    }

}