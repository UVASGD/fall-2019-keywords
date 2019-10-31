using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FixedJoint2D))]
public class Grabbable : MonoBehaviour {
    FixedJoint2D joint;

    // Start is called before the first frame update
    void Start() {
        joint = GetComponent<FixedJoint2D>();
        joint.enabled = false;
    }

    public void grab(Rigidbody2D source) {
        joint.enabled = true;
        joint.connectedBody = source;
    }

    public void ungrab() {
        joint.enabled = false;
        joint.connectedBody = null;
    }
}
