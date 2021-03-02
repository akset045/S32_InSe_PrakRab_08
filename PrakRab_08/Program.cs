using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace PrakRab_08
{
    class Program
    {
        public static void keygen(string path1, string path2)
        {
            StreamWriter key1 = new StreamWriter(path1);
            StreamWriter key2 = new StreamWriter(path2); 
            Random myrandom = new Random();  
            int p = 47, q = 17, n, f, a, b = 1;  
            n = p * q;  f = (p - 1) * (q - 1);  
            A: b = 1;  a = myrandom.Next(f); 
            while (b < f)  
            { 
                if ((b * a) % f == 1)  
                    break; 
                b = b + 1;
            } 

            if (b == f) goto A;  
            key1.Write("{0}\n{1}", n, a);  
            key2.Write("{0}\n{1}", n, b); 
            key1.Close();  
            key2.Close();  
            Console.WriteLine("Ключи созданы!\nДиректория открытого: {0}\nДиректория закрытого: {1}", path1, path2);
            Console.ReadKey();
        } 
        public static int fun(int n, int k, int a)  
        {  
            int s = 1; 
            for (int i = 1; i <= a; i++) 
            { 
                s = (s * k) % n; 
            }
            return s;
        }
        public static void crypt(string openkey, string path1, string path2)
        {
            string test = File.ReadAllText(openkey, Encoding.Default); 
            string[] mas = test.Split('\n'); 
            int n = int.Parse(mas[0]);  
            int a = int.Parse(mas[1]);
            StreamReader sr = new StreamReader(path1);
            string str1 = sr.ReadToEnd();
            string str2 = "";
            int k; 
            for (int i = 0;
                i < str1.Length; i++) 
            { 
                k = (int)str1[i];
                k = fun(n, k, a); 
                str2 = str2 + k.ToString() + ".";
            } 
            sr.Close(); 
            FileStream file1 = new FileStream(path2, FileMode.Create); 
            StreamWriter sw = new StreamWriter(file1, Encoding.UTF8); 
            sw.Write(str2); 
            sw.Close();
            Console.ReadKey();

        }
        public static void decrypt(string closekey, string path1, string path2)
            {
                string test = File.ReadAllText(closekey, Encoding.Default);
                string[] mas = test.Split('\n');
                int n = int.Parse(mas[0]); int b = int.Parse(mas[1]); 
                StreamReader sr = new StreamReader(path1);
                string str1 = sr.ReadToEnd();  
                string[] str2 = str1.Split('.');
                string str3 = ""; 
                for (int i = 0; i < str2.Length - 1; i++) 
                { 
                   int ks = 0;  
                   ks = Convert.ToInt32(str2[i]); 
                   ks = fun(n, ks, b);  
                   str3 = str3 + (char)ks; 
                }
            
                  sr.Close(); 
                  FileStream file1 = new FileStream(path2, FileMode.Create);  
                  StreamWriter sw = new StreamWriter(file1, Encoding.Unicode); 
                  sw.Write(str3);  
                  sw.Close();
            Console.ReadKey();


        }
        static void Main(string[] args)  
        {  
            if (args.Length == 0) Console.WriteLine("RSA\n");  
            if (args.Length != 0 && args[0] == " - k") keygen(args[1], args[2]); 
            if (args.Length != 0 && args[0] == " - e") crypt(args[1], args[2], args[3]); 
            if (args.Length != 0 && args[0] == " - d") decrypt(args[1], args[2], args[3]); 
            Console.ReadLine();
            Console.ReadKey();

        }
    }
}
