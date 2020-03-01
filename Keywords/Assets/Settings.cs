using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour {
    public float sfxVolume;
    public float musicVolume;

    private void Awake() {
        DontDestroyOnLoad(gameObject);
    }

    public void ApplyVolumeSettings() {

    }
}
