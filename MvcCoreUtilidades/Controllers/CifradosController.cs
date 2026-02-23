using Microsoft.AspNetCore.Mvc;
using MvcCoreUtilidades.Helpers;

namespace MvcCoreUtilidades.Controllers
{
    public class CifradosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CifradoBasico()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CifradoBasico(string contenido, string resultado, string accion)
        {
            //CIFRAMOS EL CONTENIDO
            string response = HelperCryptography.EncriptarTextoBasico(contenido);
            if(accion.ToLower() == "cifrar")
            {
                ViewData["TEXTOCIFRADO"] = response;
            }else if(accion.ToLower() == "comparar")
            {
                //SI EL USUARIO QUIERE COMPARAR, NO ESTARA ENVIANDO
                //EL TEXTO PARA COMPARAR EN resultado
                if(response != resultado)
                {
                    ViewData["MENSAJE"] = "Los datos no coinciden";
                }else
                {
                    ViewData["MENSAJE"] = "Contenidos iguales!!!";
                }
            }
            return View();
        }

        public IActionResult CifradoEficiente()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CifradoEficiente(string contenido, string resultado, string accion)
        {
            if(accion.ToLower() == "cifrar")
            {
                string response = HelperCryptography.CifrarContenido(contenido, false);
                ViewData["TEXTOCIFRADO"] = response;
                ViewData["SALT"] = HelperCryptography.Salt;
            }else if(accion.ToLower() == "comparar")
            {
                string response = HelperCryptography.CifrarContenido(contenido, true);
                if(response != resultado)
                {
                    ViewData["MENSAJE"] = "Los datos son distintos...";
                }else
                {
                    ViewData["MENSAJE"] = "Los datos son IGUALES!!!";
                }
            }
            return View();
        }
    }

}
