using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToDoor : MonoBehaviour {

    public bool traversing = false;
    public Transform target;
    public Vector3 velocity;
    // Start is called before the first frame update
    void Start () {
        traversing = false;
    }

    public void GoTo(Transform t) {
        target = t;
        traversing = true;
        //print("Going to " + target.name + " Traversing = " + traversing);
    }

    // Update is called once per frame
    void Update () {
        if (!target) return;
        transform.position = Vector3.SmoothDamp(transform.position, target.position, ref velocity, 1f);

        if (Vector3.SqrMagnitude(transform.position - target.position) < 0.1f * 0.1f) {
            foreach (ParticleSystem system in transform.GetComponentsInChildren<ParticleSystem>()) {
                ParticleSystem.EmissionModule emission = system.emission;
                emission.enabled = false;
            }
            Destroy(gameObject, 5f);
        }
    }
}
