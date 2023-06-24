using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeminarioAlgebra
{
    public class Polinomio
    {
        /*
            NO MODIFICAR EL CODIGO DE LA PLANTILLA DADA, O SEA, EL CONSTRUCTOR, INDEXADOR, 
            LAS DECLARACIONES, NOMBRES, PROPIEDADES O ALGUNA OTRA DEFINICION DADA 
            PARA LOS METODOS DE LA CLASE POLINOMIO.

            SOLO PROGRAMAR LOS METODOS DADOS. SE PUEDEN CREAR TANTOS METODOS AUXILIARES COMO DESEE
            PARA AUXILIARSE DE ESTOS.

                                                GOOD LUCK :) !!!    COLECTIVO DE ALGEBRA
        */


        #region Descripcion
        /// <summary>
        /// Contiene los coeficientes del polinomio en orden creciente del exponente de la indeterminada.
        /// </summary>
        #endregion
        private double[] coeficientes;

        public double[] Coeficientes { get { return coeficientes.ToList<double>().AsReadOnly().ToArray<double>(); } } // El polinomio debe ser inmutable.

        #region Descripcion
        /// <summary>
        /// Calcula el grado del polinomio basado el tamaño del array usado en su representación.
        /// </summary>
        #endregion
        public int Grado { get { return this.CalcularGrado(); } }   // Propiedad que devuelve el grado de un polinomio

        private int CalcularGrado()
        {
            if (this.coeficientes.Length == 1 && this[0] == 0)
                throw new InvalidOperationException("El polinomio nulo no tiene grado definido.");
            return this.coeficientes.Length - 1;
        }

        #region Descripcion
        /// <summary>
        /// Crea una nueva instancia de la clase Polinomio a partir de la lista de los coeficientes.
        /// </summary>
        /// <param name="Coeficientes">
        /// Coeficientes del Polinomio.
        /// </param>
        #endregion
        public Polinomio(params double[] Coeficientes) // Constructor de la clase.
        {
            int end = Coeficientes.Length - 1;

            while (end > 0 && Coeficientes[end] == 0) { end--; }

            this.coeficientes = new double[end + 1];
            Array.Copy(Coeficientes, this.coeficientes, end + 1);
        }

        #region Descripcion
        /// <summary>
        /// Evalúa el polinomio para un valor específico de la indeterminada.
        /// </summary>
        /// <param name="value">
        /// Valor de la indeterminada
        /// </param>
        /// <returns>
        /// Resultado de evaluar el polinomio en el valor especificado.
        /// </returns>
        #endregion
        public double Evaluar(double value)
        {
            double x = 0;
            for (int i = 0; i <this.coeficientes.Length; i++)
            {
               x += coeficientes[i] * Math.Pow(value, i);

            }
            return x;
        }

        public static Polinomio operator +(Polinomio p1, Polinomio p2)  // Redefinición del operador '+' para que sirva de ejemplo.
        {
            double[] suma = new double[Math.Max(p1.Coeficientes.Length, p2.Coeficientes.Length)];
            for (int i = 0; i < suma.Length; i++)
                suma[i] = p1[i] + p2[i];
            return new Polinomio(suma);
        }

        public static Polinomio operator -(Polinomio p1, Polinomio p2)
        {
            double[] resta = new double[Math.Max(p1.Coeficientes.Length, p2.Coeficientes.Length)];
            for (int i = 0; i < resta.Length; i++)
                resta[i] = p1[i] - p2[i];
            return new Polinomio(resta);
        }

        public static Polinomio operator *(Polinomio p1, Polinomio p2)
        {
            if (p1.IsNule || p2.IsNule) return  new Polinomio(0);
            double[] producto = new double[p1.coeficientes.Length + p2.coeficientes.Length - 1];
            for (int i = 0; i < p1.coeficientes.Length; i++)
            {
                for (int j = 0; j < p2.coeficientes.Length; j++)
                {
                    producto[i + j] += p1[i] * p2[j];
                }
            }
            return new Polinomio(producto);
        }

        
        public static Polinomio operator /(Polinomio p1, Polinomio p2)
        {
            if (p2.IsNule) throw new Exception("No se puede dividir entre 0");
            if (p1.IsNule && !p2.IsNule) return new Polinomio(0);
            if (p2.coeficientes.Length > p1.coeficientes.Length) throw new Exception("No se puede efectuar la division");
            double [] cociente = new double [p1.coeficientes.Length - p2.coeficientes.Length + 1];

            for (int i = cociente.Length - 1; i >= 0; i--)
            {
                if (p1.Grado >= p2.Grado)
                {
                    double[] temporal = new double[i + 1];
                    temporal[i] = p1[p1.coeficientes.Length - 1] / p2[p2.coeficientes.Length - 1];
                    p1 -= p2 * new Polinomio(temporal);
                    cociente[i] = temporal[i];
                }
            }
            return new Polinomio(cociente);
        }

        public static Polinomio operator %(Polinomio p1, Polinomio p2)
        {
            Polinomio p3 = p1 / p2;
            if ((p1 - (p2 * p3)).coeficientes.Length == 1 && (p1 - (p2 * p3)).CoeficientePP < Math.Pow(10, -8))
                return new Polinomio(0);
            return p1 - (p2 * p3);
        }

        #region Descripcion
        /// <summary>
        /// Calcula el MCD de dos polinomios.
        /// </summary>
        /// <param name="p1">
        /// Primer polinomio.
        /// </param>
        /// <param name="p2">
        /// Segundo polinomio.
        /// </param>
        /// <returns>
        /// Maximo comun divisor entre p1 y p2.
        /// </returns>
        #endregion
        public static Polinomio MCD(Polinomio p1, Polinomio p2) 
        {
            if (p1.IsNule && p2.IsNule) throw new Exception("Esto constituye una indeterminacion");
            if (p1.IsNule && !p2.IsNule) return p2;
            if (!p1.IsNule && p2.IsNule) return p1;
            Polinomio cd = new Polinomio(0);
            if (p1.coeficientes.Length >= p2.coeficientes.Length)
             cd = p2;
            else cd = p1;
            Polinomio mcd = new Polinomio(1);
            while (!new Polinomio ((p1 % p2).coeficientes).IsNule && (p1 % p2).ToString() !="")
            {
                if (p1.coeficientes.Length >= p2.coeficientes.Length)
                {
                    cd = p1 % cd;
                    cd /= new Polinomio(cd.CoeficientePP);
                    p1 = p2;
                    p2 = cd;
                    mcd = p2;
                }
                else
                {
                    cd = p2 % cd;
                    cd /= new Polinomio(cd.CoeficientePP);
                    p2 = p1;
                    p1 = cd;
                    mcd = p1;
                }
            }
            if (new Polinomio((p1 % p2).coeficientes).IsNule)
            { return mcd = cd; }
            return mcd;
        }

        #region Descripcion
        /// <summary>
        /// Calcula la derivada del polinomio.
        /// </summary>
        /// <param name="Orden">
        /// Orden de la derivada.
        /// </param>
        /// <returns>
        /// La derivada del polinomio del orden deseado.
        /// </returns>
        #endregion
        public Polinomio Derivada(int Orden)
        {
            if (Orden < 0) throw new ArgumentOutOfRangeException();
            
            Polinomio derivada = this;
            for (int i = 0; i < Orden && !derivada.IsNule; i++)
            { 
                derivada = derivada.Derivada();
            }
            return derivada;
        }
        private Polinomio Derivada()
        {
            if (IsNule) return this;
            double[] derivacion = new double[ coeficientes.Length - 1];
            for (int i = 1; i < coeficientes.Length; i++)
            {
                derivacion[i - 1] = coeficientes[i] * i;
            }
            return new Polinomio(derivacion);
        }

        #region Descripcion
        /// <summary>
        /// Determina el Sistema de Sturm asociado al polinomio.
        /// </summary>
        /// <returns>
        /// Sistema de Sturm asociado al polinomio.
        /// </returns>
        #endregion
        public Polinomio[] SistemaDeSturm()
        {
            if (IsNule)
                return new Polinomio[coeficientes.Length];
            Polinomio[] sturm = new Polinomio[coeficientes.Length];
            sturm[0] = new Polinomio(coeficientes);
            if (coeficientes.Length > 1)
            {
                sturm[1] = new Polinomio(Derivada().coeficientes);
                for (int i = 2; i < sturm.Length; i++)
                {
                    sturm[i] = new Polinomio (((sturm[i - 2] % sturm[i - 1]) * new Polinomio(-1)).coeficientes);

                }
            }

            return sturm;          
        }
        
        #region Descripcion
            /// <summary>
            /// Calcula la cantidad de raices reales del polinomio en el intervalo [a,b].
            /// </summary>
            /// <param name="a">
            /// Minimo del intervalo.
            /// </param>
            /// <param name="b">
            /// Maximo del intervalo.
            /// </param>
            /// <returns>
            /// Cantidad de raices reales del polinomio en el intervalo [a,b].
            /// </returns>
            #endregion
        public int CantidadDeRaices(double a, double b)
        {
            
            double[] posiblesRaicesEnA = new double [coeficientes.Length];
            double[] posiblesRaicesEnB = new double[coeficientes.Length];
            Polinomio[] sturm = SistemaDeSturm();
            for (int i = 0; i < coeficientes.Length; i++)
            {
                posiblesRaicesEnA[i] = sturm[i].Evaluar(a);
                posiblesRaicesEnB[i] = sturm[i].Evaluar(b);
            }
            int counterA = 0;
            int counterB = 0;
            for (int i = 1; i < posiblesRaicesEnA.Length; i++)
            {
               if (posiblesRaicesEnA[i] * posiblesRaicesEnA[i - 1] < 0)
                {
                    counterA++;
                }
                if (posiblesRaicesEnB[i] * posiblesRaicesEnB[i - 1] < 0)
                {
                    counterB++;
                }
            }
            return Math.Abs(counterA - counterB);
        }
        

        public override string ToString()  // Se sobreescribe el ToString para que sea mas sencillo la escritura de un polinomio en pantalla
        {
            StringBuilder a = new StringBuilder();
            try
            {
                for (int i = Grado; i >= 0 ; i--)
                {
                    if (i==0)
                        a.Append(this[i] + "x^" + i + " ");
                    else
                        a.Append(this[i] + "x^" + i + " + ");
                }
            }
            catch (InvalidOperationException io) { a.Append("null"); }

            return a.ToString();
        }   
        
        public double this[int i]  // Indexador para acceder a un coeficiente determinado del polinomio.
        {
            get
            {
                if (i >= 0)
                    return (i < this.coeficientes.Length) ? this.Coeficientes[i] : 0;
                throw new IndexOutOfRangeException("El grado de la indeterminada debe ser no negativo.");
            }
        }

        public bool IsNule
        {
            get
            {
                if (coeficientes.Length == 1 && coeficientes[0] == 0)
                    return true;
                return false;
            }
        }

        
        /// <summary>
        /// Halla el coeficiente principal
        /// </summary>
        public double CoeficientePP
        {
            get
            {
                return this[coeficientes.Length - 1];
            }
        }

        public double [] Biseccion(double a, double b)
        { 
            double[] biseccion = new double[CantidadDeRaices(a, b)];
            double[] intervalo = new double[CantidadDeRaices(a, b) + 1];
            double tempA = a;
            double tempB = b;
            double tempC = tempB;
            intervalo[0] = a;
            intervalo[intervalo.Length - 1] = b;
            int k = 1;
            while( k < intervalo.Length - 1)
            {
                if (CantidadDeRaices(tempA, tempB) == 1)
                {
                    intervalo[k] = tempB;
                    tempA = tempB;
                    tempB = b;
                    k++;
                }
                else if (CantidadDeRaices(tempA, tempB) != 0)
                {
                    tempC = tempB;
                    tempB = (tempA + tempB)/2;
                }
                else
                {
                    tempA = tempB;
                    tempB = tempC;
                }
                
            }
            for (int i = intervalo.Length - 1; i > 0; i--)
            {
                double temp = 0;
                while (Math.Abs(Evaluar(intervalo[i])) > 0)
                {
                    if (CantidadDeRaices(intervalo[i - 1], intervalo[i]) == 1)
                    {
                        temp = intervalo[i];
                        intervalo[i] = (intervalo[i - 1] + intervalo[i]) / 2;
                    }
                    else
                    {
                        intervalo[i - 1] = (intervalo[i - 1] + temp) / 2;
                        intervalo[i] = temp;
                    }
                }
                biseccion[i - 1] = intervalo[i];
            }
            
            return biseccion;
        }
        //Arreglar biseccion, redondear para que la resta deje de dar problemas. 
    }
}