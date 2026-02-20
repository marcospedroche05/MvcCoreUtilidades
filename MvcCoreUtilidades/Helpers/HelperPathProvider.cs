using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;

namespace MvcCoreUtilidades.Helpers
{
    //ENUMERACION CON LAS CARPETAS QUE DESEEMOS SUBIR FICHEROS
    public enum Folders { Uploads, Images, Facturas, Temporal }
    public class HelperPathProvider
    {
        private IWebHostEnvironment hostEnvironment;
        private IServer server;
        public HelperPathProvider(IWebHostEnvironment hostEnvironment, IServer server)
        {
            this.server = server;
            this.hostEnvironment = hostEnvironment;
        }
        //TENDREMOS UN METODO QUE SE ENCARGARÁ DE RESOLVER LA RUTA COMO STRING
        //CUANDO RECIBAMOS EL FICHERO Y LA CARPETA
        public string MapPath(string filename, Folders folder)
        {
            string carpeta = "";
            if(folder == Folders.Images)
            {
                carpeta = "images";
            } else if (folder == Folders.Facturas)
            {
                carpeta = "facturas";
            } else if (folder == Folders.Uploads)
            {
                carpeta = "uploads";
            }
            string rootPath = this.hostEnvironment.WebRootPath;
            string path = Path.Combine(rootPath, carpeta, filename);
            return path;
        }

        public string MapUrlPath(string fileName, Folders folder)
        {
            string carpeta = "";
            if (folder == Folders.Images)
            {
                carpeta = "images";
            }
            else if (folder == Folders.Facturas)
            {
                carpeta = "facturas";
            }
            else if (folder == Folders.Uploads)
            {
                carpeta = "uploads";
            }
            var addresses = this.server.Features.Get<IServerAddressesFeature>().Addresses;
            string serverUrl = addresses.FirstOrDefault();
            //DEVOLVEMOS LA RUTA URL
            string urlPath = serverUrl + "/" + carpeta + "/" + fileName;
            return urlPath;
            //string rootPath = this.hostEnvironment.WebRootPath;
            //string path = Path.Combine(carpeta, fileName);
            //return path;
        }
    }
}
