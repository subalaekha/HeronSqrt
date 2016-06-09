using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeronSqrt
{
    class Program
    {
        public static bool ValidateInterfaceSqrt(ISqrt sqrt1, ISqrt sqrt2, double num, double error)
        {
            if (Math.Abs(sqrt1.SquareRoot(num) - sqrt2.SquareRoot(num)) >= error)
            {
                return false;
            }

            return true;
        }

        public static void ValidateSquareRoot()
        {
            Random random = new Random();
            HeronSqrt heronSqrt = new HeronSqrt(0.0001);
            StandSqrt stdSqrt = new StandSqrt();

            for (int i = 0; i < 10000; i++)
            {
                double num = random.Next(0, 100000);

                double hsqrt = heronSqrt.SquareRoot(num);
                double ssqrt = stdSqrt.SquareRoot(num);

                if (Math.Abs(hsqrt - ssqrt) > 0.0001)
                {
                    Console.WriteLine("Heron sqrt {0} and Standard sqrt {1} of number {2} is greater than the error {3}", hsqrt, ssqrt, num, 0.0001);
                }
            }
        }
        public static bool Validate(double num)
        {
            bool setbol = false;
            try
            {
                if (num < 1)
                {
                    setbol = false;
                    throw (new NumIsNegative("Number is Negative"));
                }
                else
                    setbol = true;
            }
            catch(NumIsNegative e)
            {
                Console.WriteLine("NumIsNegative : {0}",e.Message);
            }

            return setbol;
        }
        public class NumIsNegative : Exception
        {
            public NumIsNegative(string message): base(message)
            { }
        }
        static void Main(string[] args)
        {
            ////double num, error;
            ////Console.WriteLine("Enter the Number to find a ");
            ////num = Convert.ToDouble(Console.ReadLine());

            ////if (!Validate(num))
            ////{
            ////    Console.ReadKey();
            ////    return;
            ////}

            ////Console.WriteLine("Enter the error limit ");
            ////error = Convert.ToDouble(Console.ReadLine());
            
            ////HeronSqrt heronSqrt = new HeronSqrt(error);
            ////StandSqrt stdSqrt = new StandSqrt();

            ////double h = heronSqrt.SquareRoot(num);
            ////Console.WriteLine("The heron calculated sqrt for num {0} :: {1}", num, h);

            ////double r = stdSqrt.SquareRoot(num);
            ////Console.WriteLine("The heron calculated sqrt for num {0} :: {1}", num, r);

            ValidateSquareRoot();

            ValidateInterfaceSqrt(new HeronSqrt(0.001), new StandSqrt(), 100, 0.0001);

            Console.ReadLine();

        }

    }// main

   public class HeronSqrt : ISqrt
    {
        public double limit;
        public HeronSqrt(double errorl)
        {
            this.limit = errorl;
        }
        public double SquareRoot(double N)
        {
            double G = N / 2;
            return HeronSqrtRecur(N, G);
        }

        public double HeronSqrtRecur(double N, double G)
        {
            if (Math.Abs(N - (G * G)) <= limit)
                return G;
            else
                return HeronSqrtRecur(N, (G = (G + N / G) * 0.5));
        }
        
    }
    public class StandSqrt : ISqrt
    {
        public double SquareRoot(double N)
        {
            return Math.Sqrt(N);
        }
    }

    public interface ISqrt
    {
        double SquareRoot(double N);
    }
}
