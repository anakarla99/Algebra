using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeminarioAlgebra;

namespace Tester
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
                PUEDE PROBAR SUS METODOS AQUI HACIENDO INSTANCIAS 
                DE LA CLASE POLINOMIO, SEGUN SE EXPLICA EN EL DOCUMENTO .PDF

                                      GOOD LUCK :) !!!   COLECTIVO DE ALGEBRA
            */

            Console.WriteLine("Longitud de a");
            int la = int.Parse(Console.ReadLine());
            Console.WriteLine("Longitud de b");
            int lb = int.Parse(Console.ReadLine());
            double[] a = new double[la];
            double[] b = new double[lb];
            Console.WriteLine("Coeficientes de a");
            for (int i = 0; i < a.Length; i++)
            {
                a[i] = int.Parse(Console.ReadLine());
            }
            Console.WriteLine("Coeficientes de b");
            for (int i = 0; i < b.Length; i++)
            {
                b[i] = int.Parse(Console.ReadLine());
            }
            Console.WriteLine();
            Console.WriteLine("Polinomio 1: {0}", p1.ToString());
            Console.WriteLine("Polinomio 2: {0}", p2.ToString());
            Console.WriteLine("De un intervalo");
            double i1 = double.Parse(Console.ReadLine());
            double i2 = double.Parse(Console.ReadLine());
            Polinomio suma = p1 + p2;
            Polinomio diferencia = p1 - p2;
            Polinomio multiplicacion = p1 * p2;
            Polinomio cociente = p1 / p2;
            Polinomio resto = p1 % p2;
            double evaluar3 = p1.Evaluar(-5);
            Polinomio mcd = Polinomio.MCD(p1, p2);
            Polinomio derivada1 = p1.Derivada(1);
            int cantDeRaices = p1.CantidadDeRaices(i1, i2);
            double[] biseccion = p1.Biseccion(i1, i2);
            Console.WriteLine("Suma: {0}", suma.ToString());
            Console.WriteLine("Resta: {0}", diferencia.ToString());
            Console.WriteLine("Multiplicacion: {0}", multiplicacion.ToString());
            Console.WriteLine("cociente: {0}", cociente.ToString());
            Console.WriteLine("resto: {0}", resto.ToString());
            Console.WriteLine("Evaluar 3: {0}", evaluar3);
            Console.WriteLine("mcd: {0}", mcd.ToString());
            Console.WriteLine("derivada de orden 1: {0}", derivada1.ToString());
            Console.WriteLine("cantidad de raices: {0}", cantDeRaices);
            Polinomio[] asd = p1.SistemaDeSturm();
            for (int i = 0; i < asd.Length; i++)
            {
                Console.Write(asd[i] + "\n");
            }
            for (int i = 0; i < biseccion.Length; i++)
            {
                Console.WriteLine(biseccion[i] + " ");
            }
        }
    }
}
