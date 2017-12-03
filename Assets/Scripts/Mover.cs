using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controls character speed.  Assign to any object that will be moving with a variable speed.
/// </summary>
public class Mover : MonoBehaviour {

    public float ypos = 1.0f;
    public float maxFwdSpeed = 20.0f;

    //UI text that displays the key mapped to this object
    public Text myIdText;

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

    private bool isFroze = false;

	// Use this for initialization
	protected virtual void Start () {
        prevPosition = transform.position;
        lastSpeed = curSpeed;
        int moverIdx = Global.instance.inputHandler.addMover(this);
        string keyText = Global.instance.inputHandler.moverKeys[moverIdx];
        myIdText.text = keyText;
        Global.instance.hudManager.registerMiniHud(moverIdx, myIdText);
	}

    public void setStartParams(Vector3 position, Vector3 speed) {
        prevPosition = position;
        transform.position = position;
        lastSpeed = speed;
        curSpeed = speed;
    }

    public virtual void Freeze() {
        isFroze = true;
    }
	
	// Update is called once per frame
	protected virtual void Update () {
        if (isFroze) {
            return;
        }

        curSpeed = (lastSpeed.magnitude + (speedUpRate * speedUpFactor) * Time.deltaTime) * transform.forward;
        if (curSpeed.magnitude > maxFwdSpeed) {
            curSpeed = curSpeed.normalized * maxFwdSpeed;
        }
        lastSpeed = curSpeed;
        //curSpeed *= Time.deltaTime  * transform.forward;
        Vector3 influence = new Vector3(nextLeftRight, 0, nextDownUp); //TODO cap influence amounts
        //Debug.Log("Update pos");
        //Debug.Log(influence);
        //Debug.Log(curSpeed);

        //Debug.Log("prev pos");
        //Debug.Log(prevPosition);
        //Vector3 nextPos = prevPosition + influence + curSpeed;
        Vector3 nextPos = transform.position + influence + curSpeed;
        //Debug.Log("new pos");
        //Debug.Log(nextPos);

        //prevPosition = transform.position;
        nextPos = new Vector3(nextPos.x, ypos, nextPos.z);
        transform.position = nextPos;
        nextLeftRight = 0;
        nextDownUp = 0;
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
