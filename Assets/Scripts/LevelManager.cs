using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    //GameObjects holding checkpoints - used to update the checkpoint list
    public List<GameObject> checkpointParents;

    //Ordered list of checkpoints.  0 is the first checkpoint the player will pass.  Count-1 is the last (goal)
    public List<Checkpoint> orderedCheckpoints;
    public int lastCheckpoint = 0; //last checkpoint passed
    public float[] distRemaining; //distance remaining at each checkpoint

    // Use this for initialization
    void Start() {
        lastCheckpoint = 0; //start cp is always behind where we actually start
        if (orderedCheckpoints == null || orderedCheckpoints.Count < 1) {
            Debug.LogError("Checkpoints not setup");
        }
        int cnt = orderedCheckpoints.Count;
        Debug.Log(cnt + " checkpoints");
        distRemaining = new float[cnt];
        //calc distance starting at the end and working backwards
        Vector3 prev = orderedCheckpoints[cnt - 1].gameObject.transform.position;
        float cumulativeDist = 0;
        for (int i = cnt - 1; i >= 0; i--) {
            Checkpoint cp = orderedCheckpoints[i];
            cp.setCpIndex(i);
            Vector3 cur = cp.gameObject.transform.position;
            float cpDist = (prev - cur).magnitude;
            cumulativeDist += cpDist;
            distRemaining[i] = cumulativeDist;
            Debug.Log("Checkpoint " + i + ", pos=" + cur +", dist remaining " + distRemaining[i]);
            prev = cur;
        }
    }

    /// <summary>
    /// Based on the location between checkpoints this method will calculate the remaining distance to the end
    /// </summary>
    /// <returns></returns>
    public float getDistanceRemaining(Vector3 curPos) {
        Vector3 lastCp = orderedCheckpoints[lastCheckpoint].gameObject.transform.position;
        float distFromLast = (curPos - lastCp).magnitude;
        float d = distRemaining[lastCheckpoint] - distFromLast;
        Debug.Log("getDist: curPos=" + curPos + ", lastCp=" + lastCp + ", distFromLast=" + distFromLast + ", d=" + d);
        return d;
    }

    /// <summary>
    /// Called when a checkpoint is passed. Argument is the index of the checkpoint that was just passed
    /// </summary>
    /// <param name="cpPassed"></param>
    public void advanceCheckpoint(int cpPassed) {
        lastCheckpoint = cpPassed;
        Debug.Log("Just passed checkpoint " + lastCheckpoint);
    }

    // Update is called once per frame
    void Update() {
        float remainingDist = getDistanceRemaining(Global.instance.inputHandler.getQueenCurPos());
        remainingDist = 3.1f * Mathf.Max(0, remainingDist); //enhance our numbers
        Global.instance.hudManager.setRemainingDist(remainingDist);
    }

    /// <summary>
    /// loads ordered checkpoints from the checkpoint parents.  Used in editor only.
    /// </summary>
    public void UpdateCheckpointList() {
        Debug.Log("Updating checkpoint list");
        if (orderedCheckpoints == null) {
            orderedCheckpoints = new List<Checkpoint>();
        }
        if (checkpointParents != null) {
            orderedCheckpoints.Clear();
            for (int i = 0; i < checkpointParents.Count; i++) {
                Checkpoint cp = checkpointParents[i].GetComponentInChildren<Checkpoint>();
                if (cp == null) {
                    Debug.Log("Checkpoint parent does not have child checkpoint: " + checkpointParents[i].name);
                } else {
                    orderedCheckpoints.Add(cp);
                }
            }
        } else {
            Debug.Log("No checkpoints to update");
        }
        Debug.Log(orderedCheckpoints.Count + " checkpoints created");
    }
}
