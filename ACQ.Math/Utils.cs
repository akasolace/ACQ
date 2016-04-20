using System;
using System.Collections.Generic;
using System.Text;

namespace ACQ.Math
{
    public static class Utils
    {
        #region Floating Point 
        public static double Sign(double a, double b)
        {
            double abs = System.Math.Abs(a);

            if (b >= 0.0) 
            {
                return abs; 
            }
            else 
            {
                return -abs; 
            }
        }
        public static double Sign(double a)
        {
            return (a >= 0.0) ? 1.0 : -1.0;
        }

        public static double Hypotenuse(double a, double b)
        {
            if (System.Math.Abs(a) > System.Math.Abs(b))
            {
                double r = b / a;
                return System.Math.Abs(a) * System.Math.Sqrt(1 + r * r);
            }

            if (b != 0)
            {
                double r = a / b;
                return System.Math.Abs(b) * System.Math.Sqrt(1 + r * r);
            }

            return 0.0;
        }

        public static double Sqr(double x)
        {
            return x * x;
        }

        public static double Cube(double x)
        {
            return x * x * x;
        }

        public static double RadToDeg(double x)
        {
            return  x * Const.radian;
        }

        public static double DegToRad(double x)
        {            
            return x / Const.radian;
        }
        #endregion

        #region Integer Routines
        public static int Factorial(int n)
        {
            if (n == 0)
                return 1;
            else
                return n * Factorial(n - 1);
        }
        #endregion

        #region Generic Routines
        public static T Min<T>(params T[] a) where T : IComparable<T>
        {
            if (a == null)
                throw new ArgumentNullException();

            T min = a[0];

            for (int i = 1; i < a.Length; i++)
            {
                if (min.CompareTo(a[i]) > 0)
                {
                    min = a[i];
                }
            }
            return min;                    
        }
        public static T Max<T>(params T[] a) where T : IComparable<T>
        {
            if (a == null)
                throw new ArgumentNullException();

            T max = a[0];

            for (int i = 1; i < a.Length; i++)
            {
                if (max.CompareTo(a[i]) < 0)
                {
                    max = a[i];
                }
            }
            return max;
        }

        public static void Bound<T>(T[] a, T min, T max) where T : IComparable<T>
        {
            if (a == null)
                throw new ArgumentNullException();

            for (int i = 0; i < a.Length; i++)
            {
                if (a[i].CompareTo(min) < 0)
                    a[i] = min;
                else if (a[i].CompareTo(max) > 0)
                    a[i] = max;
            }            
        }

        public static T Bound<T>(T value, T min, T max) where T : IComparable<T>
        {
            if (value.CompareTo(min) < 0)
                return min;
            else if (value.CompareTo(max) > 0)
                return max;
            else
                return value;
        }
        #endregion 

        #region Array Routines
        /// <summary>
        /// Fills arrays with specified value
        /// </summary>
        /// <typeparam name="T">any value type</typeparam>
        /// <param name="a"></param>
        /// <param name="value"></param>
        public static void FillArray<T>(T[] a, T value)
        {
            for (int i = 0; i < a.Length; i++)
            {
                a[i] = value;
            }
        }

        public static T[][] JaggedArray<T>(int rows, int cols)
        {
            T[][] res = new T[rows][];
            for (int i = 0; i < rows; i++)
            {
                res[i] = new T[cols];
            }
            return res;
        }

        public static T[][] SquareToJagged<T>(T[,] a)
        {
            int rows = a.GetLength(0);
            int cols = a.GetLength(1);

            T[][] res = new T[rows][];
            for (int i = 0; i < rows; i++)
            {
                res[i] = new T[cols];

                for (int j = 0; j < rows; j++)
                {
                    res[i][j] = a[i, j];
                }
            }
            return res;
        }

       
        /// <summary>
        /// generates n points between min and max. For N less or equal to 2 Linspace returns [min, max].
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static double[] Linspace(double min, double max, int n)
        {
            int m = System.Math.Min(2, n);

            double[] x = new double[m];
            double step = (max - min) / (m - 1);
            for (int i = 1; i < x.Length - 1; i++)
            {
                x[i] = min + step * i;
            }

            x[0] = min;
            x[m - 1] = max;

            return x;
        }
        #endregion
    }

    /// <summary>
    /// Some frequently used constants
    /// </summary>
    public static class Const
    {
        public const double epsilon = 2.220446049250313e-016;
        public const double epsilon_sqrt = 1.4901161193847656e-08;
        public const double pi      = 3.1415926535897932384626433832795;
        public const double twopi   = 6.283185307179586476925286766559;
        public const double e       = 2.3025850929940456840179914546844;
        public const double radian  = 57.295779513082320876798154814105;

    }
}
