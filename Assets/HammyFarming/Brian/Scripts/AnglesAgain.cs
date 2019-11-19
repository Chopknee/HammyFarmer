using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnglesAgain : MonoBehaviour {

    public float angle = 0;
    public float distance = 1;
    public Color angleMarkerColor = new Color(0, 1, 1, 1);
    public Color reverseTester = new Color(1, 0, 1, 1);

    public Vector2 offset = new Vector2(1, 1);

    public float reversedAngle = 0;

    private void OnDrawGizmos () {
        angle += Time.deltaTime * 15;
        Gizmos.color = angleMarkerColor;
        Vector3 off = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), 0, Mathf.Sin(angle * Mathf.Deg2Rad)) * distance;
        Gizmos.DrawCube(transform.position + off, Vector3.one);
        offset.x = off.x;
        offset.y = off.z;

        //I will show you da math bruddah

        reversedAngle = Mathf.Atan(offset.y / offset.x) * Mathf.Rad2Deg;
        if (offset.x < 0) {
            reversedAngle += 180;
        }


        Gizmos.color = reverseTester;
        Gizmos.DrawCube(transform.position + new Vector3(offset.x, 0, offset.y), Vector3.one);
        Gizmos.DrawCube(transform.position + ( new Vector3(Mathf.Cos(reversedAngle * Mathf.Deg2Rad), 0, Mathf.Sin(reversedAngle * Mathf.Deg2Rad))), Vector3.one);
    }

}
