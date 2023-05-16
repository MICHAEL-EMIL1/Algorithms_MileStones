using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Problem
{
    // *****************************************
    // DON'T CHANGE CLASS OR FUNCTION NAME
    // YOU CAN ADD FUNCTIONS IF YOU NEED TO
    // *****************************************
    public static class IntegerMultiplication
    {
        #region YOUR CODE IS HERE

        //Your Code is Here:
        //==================
        /// <summary>
        /// Multiply 2 large integers of N digits in an efficient way [Karatsuba's Method]
        /// </summary>
        /// <param name="X">First large integer of N digits [0: least significant digit, N-1: most signif. dig.]</param>
        /// <param name="Y">Second large integer of N digits [0: least significant digit, N-1: most signif. dig.]</param>
        /// <param name="N">Number of digits (power of 2)</param>
        /// <returns>Resulting large integer of 2xN digits (left padded with 0's if necessarily) [0: least signif., 2xN-1: most signif.]</returns>

        static public byte[] Karatsuba(byte[] X, byte[] Y)
        {
            int N = Math.Max(X.Length, Y.Length);

            if (X.Length == 1) 
            { 
                return Multt(Y, X[0]);
            } 
            if (Y.Length == 1)
            {
                return Multt(X, Y[0]);
            }

            int half = N / 2;
            byte[] a, b, c, d;
            Splitt(X, half, out a, out b);
            Splitt(Y, half, out c, out d);

            byte[] x0 = Karatsuba(a, c);
            byte[] x2 = Karatsuba(b, d);
            byte[] x1 = Karatsuba(Add(a, b), Add(c, d));

            x1 = Subtractt(Subtractt(x1, x0), x2);
            
            byte[] resultArray = Add(x0, Add(Shiftt(x1, half), Shiftt(x2, 2 * half)));
            
            return Normalize(resultArray);
        }

        private static void Splitt(byte[] x, int half, out byte[] low, out byte[] high)
        {
            int n = x.Length;
            int m = Math.Min(n, half);
            low = new byte[m];
            high = new byte[Math.Max(0, n - half)];
            if (n > half)
            {
                Array.Copy(x, half, high, 0, n - half);
            }
            if (m > 0)
            {
                Array.Copy(x, 0, low, 0, m);
            }
        }

        private static byte[] Normalize(byte[] a)
        {
            int n = a.Length - 1;

            while (n > 0 && a[n] == 0)
            {
                n--;
            }

            byte[] result;

            if (n == a.Length - 1)
            {
                result = a; // No change needed
            }
            else
            {
                result = new byte[n + 1];
                Array.Copy(a, result, n + 1);
            }

            return result;
        }

        private static byte[] Add(byte[] a, byte[] b)
        {
            int n = a.Length;
            int m = b.Length;
            int length = Math.Max(n, m);
            byte[] result = new byte[length + 1];
            byte carry = 0;
            for (int i = 0; i < length; i++)
            { // Wrong direction
                byte sum = (byte)((i < n ? a[i] : 0) + (i < m ? b[i] : 0) + carry);
                result[i] = (byte)(sum % 10);
                carry = (byte)(sum / 10);
            }
            result[length] = carry;
            return Normalize(result);
        }

        private static byte[] Subtractt(byte[] a, byte[] b)
        {
            int n = a.Length;
            int m = b.Length;
            byte[] result = new byte[n];
            int carry = 0;
            for (int i = 0; i < n; i++)
            {
                int sub = a[i] - (i < m ? b[i] : 0) - carry;
                if (sub < 0)
                {
                    sub += 10;
                    carry = 1;
                }
                else
                {
                    carry = 0;
                }
                result[i] = (byte)sub;
            }
            return Normalize(result);
        }

        private static byte[] Multt(byte[] a, byte b)
        {
            byte[] result = new byte[a.Length + 1];
            byte carry = 0;

            for (int i = 0; i < a.Length; i++)
            {
                byte product = (byte)(a[i] * b + carry);
                result[i] = (byte)(product % 10);
                carry = (byte)(product / 10);
            }

            if (carry != 0)
            {
                result[a.Length] = carry;
                return result;
            }

            byte[] normalizedResult = new byte[a.Length];
            Array.Copy(result, normalizedResult, a.Length);
            return normalizedResult;
        }
        
        private static byte[] Shiftt(byte[] a, int shift)
        {
            byte[] result = new byte[a.Length + shift];

            for (int i = 0; i < a.Length; i++)
            {
                result[i + shift] = a[i];
            }

            return result;
        }

        static public byte[] IntegerMultiply(byte[] X, byte[] Y, int N)
        {
            //REMOVE THIS LINE BEFORE START CODING
            //throw new NotImplementedException();
            return Karatsuba(X, Y);
        }
        #endregion
    }
}
