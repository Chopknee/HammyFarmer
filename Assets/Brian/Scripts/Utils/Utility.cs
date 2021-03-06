﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HammyFarming.Brian.Utils {

    public class Utility {

        public static Vector3[] FibonacciSphereDistro ( int sampleCount, float radius ) {
            Vector3[] points = new Vector3[sampleCount];
            float offset = 2f / sampleCount;
            float increment = Mathf.PI * ( 3f - Mathf.Sqrt(5f) );
            for (int i = 0; i < sampleCount; i++) {
                float y = ( ( i * offset ) - 1 ) + ( offset / 2f );
                float r = Mathf.Sqrt(1 - Mathf.Pow(y, 2));
                float phi = ( ( i + 1 ) % sampleCount ) * increment;
                float x = Mathf.Cos(phi) * r;
                float z = Mathf.Sin(phi) * r;
                points[i] = new Vector3(x, y, z) * radius;
            }
            return points;
        }

        public static void ClampMinmax(ref float min, ref float max, float absoluteMinimum, float absoluteMaximum) {
            min = Mathf.Clamp(min, absoluteMinimum, max - 0.01f);
            max = Mathf.Clamp(max, min + 0.01f, absoluteMaximum);
        }

        public static string StringFill(string end, float a) {
            int endPos = Mathf.FloorToInt(end.Length * a);
            return end.Substring(0, endPos);
        }

    }
}
