using System;

namespace MathLibTests
{
    public class MathLibTestBase 
    {
        // const float DEFAULT_TOLERANCE = float.Epsilon;
        protected const float DEFAULT_TOLERANCE = 0.00001f;

        protected static bool Compare(float _a, float _b)
        {
            float dif = Math.Abs(_a - _b);
            return dif >= -DEFAULT_TOLERANCE && dif <= DEFAULT_TOLERANCE;
        }
        
        /*protected static bool Compare(Vec2 a, Vec2 b)
        {
            return Compare(a.x, b.x) && Compare(a.y, b.y);
        }

        protected static bool Compare(Vec3 a, Vec3 b)
        {
            return Compare(a.x, b.x) && Compare(a.y, b.y) && Compare(a.z, b.z);
        }

        protected static bool Compare(Vec4 a, Vec4 b)
        {
            return Compare(a.x, b.x) && Compare(a.y, b.y) && Compare(a.z, b.z) && Compare(a.w, b.w);
        }

        protected static bool Compare(Mat3 a, Mat3 b)
        {
            return
                Compare(a.m1, b.m1) && Compare(a.m2, b.m2) && Compare(a.m3, b.m3) &&
                Compare(a.m4, b.m4) && Compare(a.m5, b.m5) && Compare(a.m6, b.m6) &&
                Compare(a.m7, b.m7) && Compare(a.m8, b.m8) && Compare(a.m9, b.m9);
        }

        protected static bool Compare(Mat4 a, Mat4 b)
        {
            return
                Compare(a.m1, b.m1) &&   Compare(a.m2, b.m2) &&   Compare(a.m3, b.m3) &&   Compare(a.m4, b.m4)   &&
                Compare(a.m5, b.m5) &&   Compare(a.m6, b.m6) &&   Compare(a.m7, b.m7) &&   Compare(a.m8, b.m8)   &&
                Compare(a.m9, b.m9) &&   Compare(a.m10, b.m10) && Compare(a.m11, b.m11) && Compare(a.m12, b.m12) &&
                Compare(a.m13, b.m13) && Compare(a.m14, b.m14) && Compare(a.m15, b.m15) && Compare(a.m16, b.m16);
        }*/
    }
}
