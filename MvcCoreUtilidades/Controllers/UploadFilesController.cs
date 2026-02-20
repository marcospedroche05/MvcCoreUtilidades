using Microsoft.AspNetCore.Mvc;
using MvcCoreUtilidades.Helpers;

namespace MvcCoreUtilidades.Controllers
{
    public class UploadFilesController : Controller
    {
        private HelperPathProvider helper;

        public UploadFilesController(HelperPathProvider helper)
        {
            this.helper = helper;
        }

        public IActionResult SubirFile()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SubirFile(IFormFile fichero)
        {
            string fileName = fichero.FileName;
            string path = this.helper.MapPath(fileName, Folders.Images);
            string urlPath = this.helper.MapUrlPath(fileName, Folders.Images);

            //PARA SUBIR FICHEROS UTILIZAMOS Stream
            using (Stream stream = new FileStream(path, FileMode.Create))
            {
                await fichero.CopyToAsync(stream);
            }
            ViewData["MENSAJE"] = "Fichero subido a " + path;
            ViewData["PATH"] = path;
            ViewData["URLPATH"] = urlPath;
            return View();
        }
    }
}
