using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WordWiggle : MonoBehaviour {
    [Range(0.0f, 10.0f)]
    public float wiggleIntensity = 1f;
    [Range(0.001f, 10.0f)]
    public float wiggleSpeedMultiplier = 1f;

    private Vector3[] initialPositionData;
    private bool wiggling = false;

    private void Awake() {
        RectTransform[] transforms = this.transform.GetComponentsInChildren<RectTransform>();
        initialPositionData = new Vector3[transforms.Length];

        // Fill in intiial position data
        for (int i = 0; i < transforms.Length; ++i) {
            initialPositionData[i] = transforms[i].localPosition;
        }
    }

    private void Update() {
        if (wiggling) {
            RectTransform[] transforms = this.transform.GetComponentsInChildren<RectTransform>();

            for (int i = 0; i < transforms.Length; ++i) {
                Vector3 offset = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
                offset *= wiggleIntensity;
                transforms[i].localPosition = initialPositionData[i] + offset;
            }
        }
    }

    public void StartWiggle() {
        wiggling = true;
    }

    public void StopWiggle() {
        wiggling = false;
        ResetPositions();
    }

    public void ResetPositions() {
        RectTransform[] transforms = this.transform.GetComponentsInChildren<RectTransform>();
        for (int i = 0; i < transforms.Length; ++i) {
            transforms[i].localPosition = initialPositionData[i];
        }
    }
}
