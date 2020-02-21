using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using SVN_BackUp.rutinas;
using SVN_BackUp.SVN;

namespace SVN_BackUp.SVN
{
    class Command
    {
        private List<Struct_SVN.List_SVN> _lst_svn;
        private Struct_SVN.Conf _conf;

        public Command(List<Struct_SVN.List_SVN> lst_svn, Struct_SVN.Conf conf)
        {
            _lst_svn = lst_svn;
            _conf = conf;
        }

        public Command(Struct_SVN.Conf conf)
        {            
            _conf = conf;
        }

        public void export()
        {
            string name = "";
            long cant_bytes = 0;
            long cant_ar = 0;

            if (_conf.rutaDestino != "")
            {
                foreach (Struct_SVN.List_SVN url_svn in _lst_svn)
                {
                    name = _conf.rutaDestino + "\\" + url_svn.nombre;
                    if (!Directory.Exists(name))
                    {
                        try
                        {
                            Directory.CreateDirectory(name);

                            System.Console.WriteLine("Iniciando copiado de {0} a {1} ", url_svn.nombre.ToString(), name.ToString());
                            System.Console.WriteLine("URL: {0}", url_svn.url_svn.ToString());

                            System.Threading.Thread operacion_export = new System.Threading.Thread(() =>
                            {
                                cmd_export(url_svn.url_svn, name);

                            });

                            operacion_export.Start();
                            System.Threading.Thread operacion_peso = new System.Threading.Thread(() =>
                            {
                                while (operacion_export.IsAlive)
                                {
                                    DirectoryInfo di = new DirectoryInfo(name);
                                    cant_bytes = 0;
                                    cant_ar = 0;

                                    foreach (FileInfo info in di.GetFiles("*", SearchOption.AllDirectories))
                                    {
                                        cant_bytes += info.Length;
                                        cant_ar += 1;
                                    }
                                    cant_bytes = cant_bytes / 1048576;
                                    System.Console.WriteLine("Cantidad de archivos descargados {0:N}, cantidad de Megabytes descargados {1:N}", cant_ar.ToString(), cant_bytes.ToString());
                                    System.Threading.Thread.Sleep(300);
                                }
                            });
                            operacion_peso.Start();
                            operacion_peso.Join();
                            System.Console.WriteLine("Cantidad de archivos descargados {0:N}, cantidad de Megabytes descargados {1:N}", cant_ar.ToString(), cant_bytes.ToString());
                        }
                        catch (System.IO.IOException ioex)
                        {
                            System.Console.WriteLine("Ruta de destino erroneoa");
                            General.eLog(ioex.Message);
                        }
                    }


                }
            }
            else
            {
                System.Console.WriteLine("Ruta de destino vacia");                
            }
        }

        private void cmd_export(string repo, string path)
        {
            try
            {
                Process cmd = new Process();
               
                cmd.StartInfo.FileName = "svn";
                cmd.StartInfo.Arguments = "export " + repo;
                cmd.StartInfo.WorkingDirectory = path;
                cmd.StartInfo.RedirectStandardInput = true;
                cmd.StartInfo.RedirectStandardOutput = true;
                cmd.StartInfo.CreateNoWindow = true;
                cmd.StartInfo.UseShellExecute = false;
                cmd.Start();
                cmd.StandardInput.Flush();
                cmd.StandardInput.Close();

                General.eLog(cmd.StandardOutput.ReadToEnd());
            }
            catch(Exception e)
            {
                General.eLog(e.Message);
            }
        }

        public List<string> scan()
        {
            List<string> lstDirectorios = new List<string>();
            DirectoryInfo directory;
            
            string ruta_repo = "";

            if (_conf.rutaRepo != "")
            {
                ruta_repo = _conf.rutaRepo;
                
                if (Directory.Exists(ruta_repo))
                {                    
                    directory = new DirectoryInfo(ruta_repo);
                    Console.WriteLine("Carpetas encontradas...");

                    foreach (var fi in directory.GetDirectories())
                    {
                        lstDirectorios.Add(fi.Name);
                        Console.WriteLine(fi.Name);
                    }
                }
                    
                
            }
            return lstDirectorios;
        }
    }
}
