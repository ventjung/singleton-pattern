using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Singleton
{
    public class MySingletonClass
    {
        // 2- solusi pertama thread problem
        //private static MySingletonClass Instance = new MySingletonClass();

        // 3- solusi kedua thread problem
        private static MySingletonClass Instance;
        private static readonly object PadLock = new object();
        private bool MyFlag = false;

        // 1- constructornya private
        private MySingletonClass()
        {

        }

        // 4- attribute solusi ketiga dari thread problem
        //[MethodImpl(MethodImplOptions.Synchronized)]
        public static MySingletonClass GetInstance()
        {
            if(Instance == null)
            {
                lock (PadLock)
                {
                    if (Instance == null)
                    {
                        Instance = new MySingletonClass();
                    }
                }
            }

            return Instance;
        }

        public void MyMethod()
        {
            if(MyFlag == false)
            {
                MyFlag = true;
                Console.WriteLine("Success Flagging");
            }
            else
            {
                Console.WriteLine("Failed Flagging");

            }
        }
    }

    class Program
    {
        static void FirstThread()
        {
            MySingletonClass obj = MySingletonClass.GetInstance();
            Console.WriteLine("First Thread - " + obj.GetHashCode());
            obj.MyMethod();
        }

        static void SecondThread()
        {
            MySingletonClass obj2 = MySingletonClass.GetInstance();
            Console.WriteLine("Second Thread - " + obj2.GetHashCode());
            obj2.MyMethod();
        }

        static void Main(string[] args)
        {
            Parallel.Invoke(
                () => FirstThread(),
                () => SecondThread()
            );
            

            Console.WriteLine("Hello World!");
        }
    }
}
