using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HammyFarming.Brian.Utils {

    public class Easing: MonoBehaviour {

        public enum OverflowMode { Clamp, Loop }
        public static OverflowMode OverflowMethod = OverflowMode.Clamp;

        public static float EaseInQuad(float a) {
            a = OverflowHandle(a);
            return a * a;
        }

        public static float EaseOutQuad(float a) {
            a = OverflowHandle(a);
            return -a * ( a - 2.0f );
        }

        public static float EaseInOutQuad(float a) {
            a = OverflowHandle(a);

            if (a < 0.5f) {
                return 2.0f * a * a;
            } else {
                a = a * 2.0f - 1.0f;
                return -0.5f * ( a * ( a - 2.0f ) - 1.0f);
            }
        }

        public static float EaseInCubic(float a ) {
            a = OverflowHandle(a);
            return a * a * a;
        }

        public static float EaseOutCubic ( float a ) {
            a = OverflowHandle(a);
            a = a - 1.0f;
            return a * a * a + 1.0f;
        }

        public static float EaseInOutCubic(float a) {
            a = OverflowHandle(a);
            a = a * 2.0f;
            if (a < 1.0f)
                return 0.5f * a * a * a;
            else
                a = a - 2.0f;
                return 0.5f * ( a * a * a + 2.0f );
        }

        public static float EaseInQuart(float a) {
            a = OverflowHandle(a);
            return a * a * a * a;
        }

        public static float EaseOutQuart(float a) {
            a = OverflowHandle(a);
            a = a - 1.0f;
            return -( ( a * a * a * a ) - 1.0f );
        }

        public static float EaseInOutQuart(float a) {
            a = OverflowHandle(a);
            a = 2.0f * a;
            if (a < 1.0f)
                return 0.5f * a * a * a * a;
            else {
                a = a - 2.0f;
                return -0.5f * ( ( a * a * a * a ) - 2.0f );
            }
        }

        public static float EaseInSine(float a) {
            a = OverflowHandle(a);
            return -1.0f * Mathf.Cos(a * Mathf.PI / 2.0f) + 1.0f;
        }

        public static float EaseOutSine(float a) {
            a = OverflowHandle(a);
            return Mathf.Sin(a * Mathf.PI / 2.0f);
        }

        public static float EaseInOutSine(float a) {
            a = OverflowHandle(a);
            return -0.5f * ( Mathf.Cos(Mathf.PI * a) - 1.0f );
        }

        public static float EaseInExpo(float a) {
            a = OverflowHandle(a);
            if (a == 0.0f)
                return 0;
            else
                return Mathf.Pow(2.0f, ( 10.0f * ( a - 1.0f ) ));
        }

        public static float EaseOutExpo(float a) {
            a = OverflowHandle(a);
            if (a == 1.0f)
                return 1.0f;
            else
                return -( Mathf.Pow(2.0f, ( -10.0f * a )) ) + 1.0f;
        }

        public static float EaseInOutExpo(float a ) {
            a = OverflowHandle(a);
            if (a == 0.0f)
                return 0.0f;
            else if (a == 1.0f)
                return 1.0f;
            else {
                a = a * 2.0f;
                if (a < 1.0f)
                    return 0.5f * Mathf.Pow(2.0f, ( 10.0f * ( a - 1.0f ) ));
                else
                    return 0.5f * ( -1.0f * (Mathf.Pow(2.0f, ( -10.0f * a ))) + 2.0f );
            }
        }

        public static float EaseInQuint(float a) {
            a = OverflowHandle(a);
            return a * a * a * a * a;
        }

        public static float EaseOutQuint(float a) {
            a = OverflowHandle(a);
            a = a - 1.0f;
            return ( a * a * a * a * a ) - 1.0f;
        }

        public static float EaseInOutQuint(float a) {
            a = OverflowHandle(a);
            a = 2.0f * a;
            if (a < 1.0f)
                return 0.5f * a * a * a * a * a;
            else {
                a = a - 2.0f;
                return 0.5f * ( ( a * a * a * a * a ) + 2.0f );
            }
        }

        public static float EaseInCirc(float a) {
            a = OverflowHandle(a);
            return -1.0f * ( Mathf.Sqrt(1 - a * a) ) - 1.0f;
        }

        public static float EaseOutCirc ( float a ) {
            a = OverflowHandle(a);
            a = a - 1.0f;
            return -1.0f * ( Mathf.Sqrt(1.0f - a * a) );
        }

        public static float EaseInOutCirc(float a) {
            a = OverflowHandle(a);
            a = a * 2.0f;
            if (a < 1.0f)
                return -0.5f * ( Mathf.Sqrt(1.0f - a * a) - 1.0f );
            else {
                a = a - 2.0f;
                return 0.5f * ( Mathf.Sqrt(1.0f - a * a) + 1.0f);
            }
        }

        public static float EaseInElastic(float a, float amplitude = 1f, float period = 0.3f) {
            a = OverflowHandle(a);

            if (period == 0.0f) {
                period = 0.3f;
            }
            if (amplitude == 0.0f) {
                amplitude = 1;
            }

            float s;
            if (amplitude < 1.0f) {
                amplitude = 1;
                s = period / 4.0f;
            } else
                s = period / ( 2.0f * Mathf.PI ) * Mathf.Asin(1.0f / amplitude);

            a -= 1.0f;
            return -1.0f * ( amplitude * Mathf.Pow(2.0f, ( 10.0f * a )) * Mathf.Sin(( a - s ) * ( 2.0f * Mathf.PI ) / period) );
        }

        public static float EaseOutElastic ( float a, float amplitude = 1f, float period = 0.3f ) {
            a = OverflowHandle(a);

            if (period == 0.0f) {
                period = 0.3f;
            }
            if (amplitude == 0.0f) {
                amplitude = 1;
            }

            float s;
            if (amplitude < 1.0f) {
                amplitude = 1.0f;
                s = period / 4.0f;
            } else
                s = period / ( 2.0f * Mathf.PI ) * Mathf.Asin(1.0f / amplitude);

            return amplitude * Mathf.Pow(2.0f, ( -10.0f * a )) * Mathf.Sin(( a - s ) * ( 2.0f * Mathf.PI / period )) + 1;
        }

        public static float EaseInOutElastic ( float a, float amplitude = 1.0f, float period = 0.5f ) {
            a = OverflowHandle(a);

            if (period == 0.0f) {
                period = 0.5f;
	        }
	        if (amplitude == 0.0) {
                amplitude = 1.0f;
	        }
            float s;
	        if (amplitude< 1.0f) {
                amplitude = 1.0f;
                s = period / 4.0f;
	        } else {
                s = period / ( 2.0f * Mathf.PI ) * Mathf.Asin(1.0f / amplitude);
	        }

            a *= 2.0f;
	        if (a< 1.0f) {
                a = a - 1.0f;
                return -0.5f * ( amplitude * Mathf.Pow(2.0f, ( 10.0f * a )) * Mathf.Sin(( a - s ) * 2.0f * Mathf.PI / period) );
	        } else {
                a = a - 1.0f;
                return amplitude * Mathf.Pow(2.0f, ( -10.0f * a )) * Mathf.Sin(( a - s ) * 2.0f * Mathf.PI / period) * 0.5f + 1f;
	        }
        }

        public static float EaseInBack(float a, float s = 1.70158f) {
            a = OverflowHandle(a);
            if (s == 0.0f)
                s = 1.70158f;
            return a * a * ( ( s + 1.0f ) * a - s );
        }

        public static float EaseOutBack(float a, float s = 1.70158f ) {
            a = OverflowHandle(a);
            if (s == 0.0f)
                s = 1.70158f;
            a = a - 1;
            return a * a * ( ( s + 1.0f ) * a + s ) + 1.0f;
        }

        public static float EaseInOutBack(float a, float s = 1.70158f) {
            a = OverflowHandle(a);
            if (s == 0.0f)
                s = 1.70158f;
            a = a * 2.0f;
            if (a < 1.0f) {
                s *= 1.525f;
                return 0.5f * ( a * a * ( ( s + 1.0f ) * a - s ) );
            } else {
                a -= 2.0f;
                s *= 1.525f;
                return 0.5f * ( a * a * ( ( s + 1.0f ) * a + s ) + 2.0f );
            }
        }

        public static float EaseInBounce(float a) {
            a = OverflowHandle(a);
            float eob = EaseOutBounce(1.0f - a);
            return 1.0f - eob;
        }

        public static float EaseOutBounce(float a) {
            a = OverflowHandle(a);

            if (a < ( 1.0f / 2.75f )) {
                return 7.5625f * a * a;
            } else if (a < ( 2.0f / 2.75f )) {
                a -= ( 1.5f / 2.75f );
                return 7.5625f * a * a + 0.75f;
            } else if (a < ( 2.5f / 2.75f )) {
                a -= ( 2.25f / 2.75f );
                return 7.5625f * a * a + 0.9375f;
            } else {
                a -= ( 2.65f / 2.75f );
                return 7.5625f * a * a + 0.984375f;
            }
        }

        public static float EaseInOutBounce(float a) {
            a = OverflowHandle(a);

            if (a < 0.5f)
                return EaseOutBounce(a * 2.0f) * 0.5f;
            else
                return EaseOutBounce(a * 2.0f - 1.0f) * 0.5f + 0.5f;
        }

        private static float OverflowHandle(float a) {
            if (OverflowMethod == OverflowMode.Clamp) {
                return Mathf.Clamp(a, 0.0f, 1.0f);
            } else {
                return a - Mathf.Floor(a);
            }
        }
    }
}