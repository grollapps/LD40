using System.Collections.Generic;
using UnityEngine;

//A mover that circles around a central point
public class RadialMover : Mover {

    public float orbitDist = 0.5f;

    public void setParent(Transform parentT) {
        transform.SetParent(parentT, false);

        float extents = orbitDist + orbitDist * 0.3f;
        orbitDist = Random.Range(-extents, extents);
        transform.localPosition = new Vector3(0, 0, orbitDist);
        curSpeed = new Vector3(1.0f, 0f, 1.0f);
    }

    protected override void Start() {

    }

    protected override void FixedUpdate() {

        //orbit the parent
        Quaternion rot = Quaternion.AngleAxis(120.0f, transform.up);
        Quaternion curAngle = Quaternion.Slerp(Quaternion.identity, rot, curSpeed.magnitude * Time.deltaTime);
        transform.localPosition = curAngle * transform.localPosition;

    }
}
