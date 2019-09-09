using System;
using UnityEngine;

namespace UnityStandardAssets._2D {
    public class Camera2DFollow : MonoBehaviour {
        public Transform target;
        public float damping = 1;
        public float lookAheadFactor = 3;
        public float lookAheadReturnSpeed = 0.5f;
        public float lookAheadMoveThreshold = 0.1f;

        private float m_OffsetZ;
        private Vector3 m_LastTargetPosition;
        private Vector3 m_CurrentVelocity;
        private Vector3 m_LookAheadPos;

        // Use this for initialization
        private void Start() {
            transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
            m_LastTargetPosition = target.position;
            m_OffsetZ = (transform.position - target.position).z;
            transform.parent = null;
            ConstructCullingMask(target.gameObject.GetComponent<PlayerInfo>().playerNum);
        }

        private void ConstructCullingMask(int playerNum) {
            Camera cam = GetComponent<Camera>();
            cam.cullingMask &= ~(1 << LayerMask.NameToLayer("P1"));
            cam.cullingMask &= ~(1 << LayerMask.NameToLayer("P2"));
            cam.cullingMask &= ~(1 << LayerMask.NameToLayer("P3"));
            cam.cullingMask &= ~(1 << LayerMask.NameToLayer("P4"));
            cam.cullingMask |= 1 << LayerMask.NameToLayer("P" + playerNum);
            if (playerNum == 1) {
                for (int i = 1; i < 16; i += 2) {
                    cam.cullingMask &= ~(1 << LayerMask.NameToLayer(Convert.ToString(i, 2).PadLeft(4, '0')));
                }
            } else if (playerNum == 2) {
                for (int i = 2; i < 16; i += 4) {
                    cam.cullingMask &= ~(1 << LayerMask.NameToLayer(Convert.ToString(i, 2).PadLeft(4, '0')));
                    cam.cullingMask &= ~(1 << LayerMask.NameToLayer(Convert.ToString(i + 1, 2).PadLeft(4, '0')));
                }
            } else if (playerNum == 3) {
                for (int i = 4; i < 16; i += 8) {
                    cam.cullingMask &= ~(1 << LayerMask.NameToLayer(Convert.ToString(i, 2).PadLeft(4, '0')));
                    cam.cullingMask &= ~(1 << LayerMask.NameToLayer(Convert.ToString(i + 1, 2).PadLeft(4, '0')));
                    cam.cullingMask &= ~(1 << LayerMask.NameToLayer(Convert.ToString(i + 2, 2).PadLeft(4, '0')));
                    cam.cullingMask &= ~(1 << LayerMask.NameToLayer(Convert.ToString(i + 3, 2).PadLeft(4, '0')));
                }
            } else if (playerNum == 4) {
                for (int i = 8; i < 16; i++) {
                    cam.cullingMask &= ~(1 << LayerMask.NameToLayer(Convert.ToString(i, 2).PadLeft(4, '0')));
                }
            } else {
                print("ConstructCullingMask called on weird value of playerNum");
            }
        }

        // Update is called once per frame
        private void Update() {
            // only update lookahead pos if accelerating or changed direction
            float xMoveDelta = (target.position - m_LastTargetPosition).x;

            bool updateLookAheadTarget = Mathf.Abs(xMoveDelta) > lookAheadMoveThreshold;

            if (updateLookAheadTarget) {
                m_LookAheadPos = lookAheadFactor * Vector3.right * Mathf.Sign(xMoveDelta);
            } else {
                m_LookAheadPos = Vector3.MoveTowards(m_LookAheadPos, Vector3.zero, Time.deltaTime * lookAheadReturnSpeed);
            }

            Vector3 aheadTargetPos = target.position + m_LookAheadPos + Vector3.forward * m_OffsetZ;
            Vector3 newPos = Vector3.SmoothDamp(transform.position, aheadTargetPos, ref m_CurrentVelocity, damping);

            transform.position = newPos;

            m_LastTargetPosition = target.position;
        }
    }
}
