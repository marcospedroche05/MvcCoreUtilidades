using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;

namespace MvcCoreUtilidades.Helpers
{
    public class HelperCryptography
    {
        //CREAMOS UN STRING PARA EL SALT
        public static string Salt { get; set; }
        //METODO PARA GENERAR UN SALT ALEATORIO
        private static string GenerateSalt()
        {
            Random random = new Random();
            string salt = "";
            for (int i = 1; i <= 30; i++)
            {
                //GENERAMOS UN ALEATORIO ASCII
                int num = random.Next(1, 255);
                char letra = Convert.ToChar(num);
                salt += letra;
            }
            return salt;
        }

        //CREAMOS UN METODO EFICIENTE PARA EL CIFRADO
        public static string CifrarContenido(string contenido, bool comparar)
        {
            if(comparar == false)
            {
                //NO QUEREMOS COMPARAR
                //CREAMOS UN NUEVO SALT
                Salt = GenerateSalt();
            }
            //REALIZAMOS EL CIFRADO
            string contenidoSalt = contenido + Salt;
            //UTILIZAMOS EL OBJETO GRANDE PARA CIFRAR
            SHA512 managed = SHA512.Create();
            UnicodeEncoding encoding = new UnicodeEncoding();
            byte[] salida;
            salida = encoding.GetBytes(contenidoSalt);
            //REALIZAR n ITERACIONES SOBRE EL PROPIO CIFRADO
            for (int i = 1; i <= 22; i++)
            {
                //CIFRADO SOBRE CIFRADO
                salida = managed.ComputeHash(salida);
            }
            //DEBEMOS LIBERAR LA MEMORIA
            managed.Clear();
            string resultado = encoding.GetString(salida);
            return resultado;
        }

        //CREAMOS LOS METODOS DE TIPO STATIC
        //SIMPLEMENTE DEVOLVEMOS UN TEXTO CIFRADO SIMPLE
        public static string EncriptarTextoBasico(string contenido)
        {
            //EL CIFRADO SE REALIZA A NIVEL DE BYTES
            //DEBEMOS CONVERTIR EL TEXTO DE ENTRADA A BYTES[]
            byte[] entrada;
            //DESPUES DE CIFRAR LOS BYTES, NOS DARA UNA SALIDA DE BYTES[]
            byte[] salida;
            //NECESITAMOS UNA CLASE PARA CONVERTIR DE BYTE[] A STRING Y VICEVERSA
            UnicodeEncoding encoding = new UnicodeEncoding();
            //NECESITAMOS UN OBJETO PARA CIFRAR EL CONTENIDO
            SHA1 managed = SHA1.Create();
            //CONVERTIMOS EL TEXTO DE ENTRADA A BYTES[]
            entrada = encoding.GetBytes(contenido);
            //LOS OBJETOS DE CIFRADO TIENEN UN METODO LLAMADO 
            //ComputeHash() QUE RECIBE UN ARRAY DE BYTES, REALIZAN
            //ACCIONES INTERNAS Y DEVUELVEN EL ARRAY DE BYTES[] CIFRADO
            salida = managed.ComputeHash(entrada);
            //CONVERTIMOS LOS BYTES A TEXTO
            string resultado = encoding.GetString(salida);
            return resultado;
        }
    }
}
