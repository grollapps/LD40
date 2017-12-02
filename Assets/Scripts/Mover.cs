using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls character speed.  Assign to any object that will be moving with a variable speed.
/// </summary>
public class Mover : MonoBehaviour {

    //Generally z is forward, x is side to side.  Y is up/down (unused)
    public Vector3 curSpeed = Vector3.zero;

    //Speed will constantly increase at this rate
    public float speedUpRate = 0.5f;

    //Constant factor affecting base speed
    public float speedUpFactor = 1.0f;

    private Vector3 prevPosition = Vector3.zero;
    private Vector3 lastSpeed = Vector3.zero;

    private float nextLeftRight = 0;
    private float nextDownUp = 0;

	// Use this for initialization
	void Start () {
        prevPosition = transform.position;
        lastSpeed = curSpeed;
        Global.instance.inputHandler.addFirstMover(this);
	}
	
	// Update is called once per frame
	void Update () {
        curSpeed = (lastSpeed.magnitude + (speedUpRate * speedUpFactor) * Time.deltaTime) * transform.forward;
        lastSpeed = curSpeed;
        //curSpeed *= Time.deltaTime  * transform.forward;
        Vector3 influence = new Vector3(nextLeftRight, 0, nextDownUp); //TODO cap influence amounts
        Debug.Log("Update pos");
        Debug.Log(influence);
        Debug.Log(curSpeed);

        Debug.Log("prev pos");
        Debug.Log(prevPosition);
        Vector3 nextPos = prevPosition + influence + curSpeed;
        Debug.Log("new pos");
        Debug.Log(nextPos);

        prevPosition = transform.position;
        transform.position = nextPos;
        nextLeftRight = 0;
        nextDownUp = 0;
	}

    void LateUpdate() { //TODO only do once
        Camera cam = Global.instance.mainCamera;
        Vector3 target = transform.position;
        Vector3 curCamPos = cam.transform.position;
        Vector3 nextCamPos = target - Global.instance.camOffset;
        cam.transform.position = Vector3.Lerp(curCamPos, nextCamPos, 0.8f * Time.deltaTime);
    }


    /// <summary>
    /// Add left or right position.  Left is negative values, right is positive.
    /// </summary>
    /// <param name="leftRight"></param>
    public void addLeftRight(float leftRight) {
        nextLeftRight += leftRight;
    }

    /// <summary>
    /// Add down or up position.  Down is negative values, up is positive.
    /// </summary>
    /// <param name="downUp"></param>
    public void addDownUp(float downUp) {
        nextDownUp += downUp;
    }

}
