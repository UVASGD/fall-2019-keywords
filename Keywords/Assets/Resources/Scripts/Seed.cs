using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : MonoBehaviour
{
    public int seed;
    private void Awake() {
        Random.InitState(seed);
    }
}
