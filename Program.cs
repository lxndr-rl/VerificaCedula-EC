using System;
using System.Collections.Generic;

namespace VerificaCedula
{
    class Program
    {
        static void Main(string[] args)
        {
            string cedula;

            Console.WriteLine("Ingrese su número de cédula");
            cedula = Console.ReadLine();


            if (int.TryParse(cedula, out int output) && ValidaLongitud(cedula) && ValidarProvincia(cedula) && ValidaTercer(cedula) && ValidaUltimoDigito(cedula))
            {
                Console.WriteLine("La cédula es válida");
            }
            else
            {
                Console.WriteLine("La cédula no es Válida");
            }
        }

        private static bool ValidaLongitud(string cedula)
        {
            if (cedula.Length < 10 || cedula.Length > 10)
            {
                Console.WriteLine("LONGITUD INVALIDO");
                return false;
            }
            else
            {
                Console.WriteLine("LONGITUD VALIDO");
                return true;
            }
        }
        private static bool ValidarProvincia(string cedula)
        {
            int dosDigitos = Convert.ToInt32(cedula.Substring(0, 2));
            if ((dosDigitos < 1 || dosDigitos > 24) && dosDigitos != 30)        //Los dos primeros dígitos indican la provincia (01-24 y 30 para extranjeros) 
            {
                Console.WriteLine("PROVINCIA INVALIDO");
                return false;
            }
            Console.WriteLine("PROVINCIA VALIDO");
            return true;
        }

        private static bool ValidaTercer(string cedula)
        {
            char digito = cedula[2];
            if (digito >= '0' && digito <= '6')            //El tercer dígito debe estar entre 0 y 6 incluídos
            {
                Console.WriteLine("TERCER DIGITO VALIDO");
                return true;
            }
            Console.WriteLine("TERCER DIGITO INVALIDO");
            return false;
        }

        private static bool ValidaUltimoDigito(string cedula)
        {
            List<char> PosPar = new List<char>();
            List<char> PosImpar = new List<char>();

            int respImp = 0;
            int respPar = 0;
            string nueveDig = cedula.Substring(0, 9);
            for (int i = 0; i < nueveDig.Length; i += 2)
            {
                PosImpar.Add(nueveDig[i]);
            }
            for (int i = 1; i < nueveDig.Length; i += 2)
            {
                PosPar.Add(nueveDig[i]);
            }

            foreach (char num in PosImpar)
            {
                int resp = int.Parse(num.ToString()) * 2;
                if (resp > 9)
                {
                    resp -= 9;
                }
                respImp += resp;
            }

            foreach (char num in PosPar)
            {
                respPar += int.Parse(num.ToString());
            }

            int moduloDiez = 10 - (respImp + respPar) % 10;

            Console.WriteLine($"El dígito verificador de {nueveDig} es {moduloDiez}");

            if (int.Parse(cedula[9].ToString()) == moduloDiez)
            {
                Console.WriteLine("MODULO 10 VALIDO");
                return true;
            }
            else
            {
                Console.WriteLine("MODULO 10 INVALIDO");
                return false;
            }
        }
    }
}
