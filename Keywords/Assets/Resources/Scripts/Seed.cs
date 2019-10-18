using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : MonoBehaviour
{
    public int seed;
    public bool randomSeed;
    private void Awake() {
        if (randomSeed)
        {
            seed = Random.Range(1, 100000000);
        }
        Random.InitState(seed);
    }
}
