using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EmirhanDur
{
    public class RoadController : MonoBehaviour
    {
        public RoadType roadType;
        public Transform connectionPoint;

        private void Awake()
        {
            Debug.Log(transform.forward);
            Debug.Log(-transform.forward);
        }

        private void OnDrawGizmos()
        {
            if (connectionPoint != null)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(transform.position, connectionPoint.position);
                Gizmos.DrawSphere(connectionPoint.position, 0.2f);
            }
        }
    }

    public enum RoadType
    {
        Basic,
        T,
        FourSide,
        Curve
    }
}
