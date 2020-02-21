using System;
using System.Collections.Generic;


namespace SVN_BackUp
{
    class Opciones
    {
        private List<SVN.Struct_SVN.List_SVN> ListSVN = new List<SVN.Struct_SVN.List_SVN>();
        private SVN.Struct_SVN.Conf Conf_conf = new SVN.Struct_SVN.Conf();

        public Opciones()
        {
            ListSVN = SVN.Struct_SVN.Deserializar();
            Conf_conf = SVN.Struct_SVN.Conf_Deserializar();
        }


        public void ejecutar(string arg0, string arg1 = "", string arg2 = "")
        {
            string arg = arg0.ToLower();
            switch (arg)
            {
                case "add-svn":
                    agregar(arg1, arg2);
                    break;
                case "del-svn":
                    borrar(arg1);
                    break;
                case "start":
                    start();
                    break;
                case "-l":
                    listar();
                    break;
                case "-ll":
                    listar_l();
                    break;
                case "conf-dest":
                    configurar_destino(arg1);
                    break;
                case "conf-dir-repo":
                    configurar_directorio_repositorio(arg1);
                    break;                
                case "-l-conf":
                    listar_conf();
                    break;
                case "scan":
                    scan();
                    break;
                case "conf-svn":
                    configurar_svn(arg1);
                    break;

            }

        }

        //==========================================================================================
        //===============Metodos Principales========================================================
        //==========================================================================================

        private void borrar(string arg1)
        {
            try
            {
                borrar_1(arg1);
            }
            catch (FormatException ex_format)
            {
                System.Console.WriteLine("Formato de entrada no valido");
                //System.Console.WriteLine(ex_format.Message);
            }
            catch (ArgumentOutOfRangeException ex_argumento)
            {
                System.Console.WriteLine("Rango fuera de intervalo aceptado");
                //System.Console.WriteLine(ex_argumento.Message);

            }
        }

        private void borrar_todo()
        {
            int cantidad = 0;
            try
            {
                cantidad = ListSVN.Count;
                                
                for (int i = 0; i < cantidad; i++)
                {
                    ListSVN.RemoveAt(0);
                }
                SVN.Struct_SVN.Serializar(ListSVN);
                ListSVN = SVN.Struct_SVN.Deserializar();
            }
            catch (FormatException ex_format)
            {
                System.Console.WriteLine("No es posible reiniciar la lista de repositorios");
                //System.Console.WriteLine(ex_format.Message);
            }
        }

        private void start()
        {
            SVN.Command command = new SVN.Command(ListSVN, Conf_conf);
            rutinas.General.eLog("Iniciando proceso de Descarga");
            System.Console.WriteLine("Iniciando proceso de Descarga");
            command.export();
            System.Console.WriteLine("Proceso Finalizado");
            rutinas.General.eLog("Proceso Finalizado");

        }

        private void listar()
        {
            SVN.Struct_SVN.List_SVN dir_cop;

            //System.Console.BufferWidth = 250;


            System.Console.WriteLine("========================================================");
            System.Console.WriteLine("Elemento almacenados:");
            System.Console.WriteLine("");
            System.Console.WriteLine(rutinas.General.formatear("Id", 5) + rutinas.General.formatear("Nombre", 35) + rutinas.General.formatear("URL SVN", 35));
            for (int i = 0; i < ListSVN.Count; i++)
            {
                dir_cop = ListSVN[i];

                System.Console.WriteLine(rutinas.General.formatear(Convert.ToString(i + 1), 5) + " " + rutinas.General.formatear(dir_cop.nombre, 35) + " " + rutinas.General.formatear(dir_cop.url_svn, 35));
            }
        }

        private void listar_l()
        {
            SVN.Struct_SVN.List_SVN lst_svn;

            //System.Console.BufferWidth = 200;


            System.Console.WriteLine("========================================================");
            System.Console.WriteLine("Elemento almacenados:");
            System.Console.WriteLine("");
            System.Console.WriteLine(rutinas.General.formatear("Id", 5) + rutinas.General.formatear("Nombre", 79) + rutinas.General.formatear("URL SVN", 79));
            for (int i = 0; i < ListSVN.Count; i++)
            {
                lst_svn = ListSVN[i];

                System.Console.WriteLine(rutinas.General.formatear(Convert.ToString(i + 1), 5) + " " + rutinas.General.formatear(lst_svn.nombre, 79) + " " + rutinas.General.formatear(lst_svn.url_svn, 79));
            }
        }

        private void listar_conf()
        {

            System.Console.WriteLine("========================================================");
            System.Console.WriteLine("Elemento de configuración:");
            System.Console.WriteLine("");
            //System.Console.WriteLine(rutinas.General.formatear("Id", 5) + rutinas.General.formatear("Nombre", 79) + rutinas.General.formatear("URL SVN", 79));

            System.Console.WriteLine("Ruta de destino:" + rutinas.General.formatear(Conf_conf.rutaDestino.ToString(), 79));
            System.Console.WriteLine("Ruta de directorio  del repositorio:" + rutinas.General.formatear(Conf_conf.rutaRepo.ToString(), 79));
            System.Console.WriteLine("Ruta de repositorio:" + rutinas.General.formatear(Conf_conf.rutaSVN.ToString(), 79));

        }

        /// <summary>
        /// Incorpora un nuevo repositorio a la lista
        /// </summary>
        /// <param name="arg1">Nombre del repositorio</param>
        /// <param name="arg2">Ruta (url) del repositorio</param>
        private void agregar(string arg1, string arg2)
        {
            SVN.Struct_SVN.List_SVN _ListSVN = new SVN.Struct_SVN.List_SVN();
            _ListSVN.nombre = arg1;
            _ListSVN.url_svn = arg2;
            ListSVN.Add(_ListSVN);
            SVN.Struct_SVN.Serializar(ListSVN);
        }

        private void borrar_1(string arg1)
        {
            int valor;
            if (int.TryParse(arg1, out valor))
            {
                if (ListSVN.Count + 1 > valor && ListSVN.Count > 0)
                {
                    ListSVN.RemoveAt(valor - 1);
                    SVN.Struct_SVN.Serializar(ListSVN);
                    ListSVN = SVN.Struct_SVN.Deserializar();
                    System.Console.WriteLine("Registro eliminado correctamente");
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Rango no valido para la eliminación");
                }
            }
            else
            {
                throw new FormatException("Parametro no valido");
            }

        }

        private void configurar_destino(string arg1)
        {            
            string ruta;


            if (!String.IsNullOrEmpty(arg1))
            {
                ruta = arg1;

                Conf_conf.rutaDestino = ruta;

                SVN.Struct_SVN.Conf_Serializar(Conf_conf);
                Conf_conf = SVN.Struct_SVN.Conf_Deserializar();
                System.Console.WriteLine("Registro reconfigurado");
                System.Console.WriteLine("Nueva ruta destino: " + ruta);

            }
            else
            {
                throw new FormatException("Parametro no valido");
            }
        }

        private void scan()
        {
            List<string> lstDirectorios = new List<string>();
            string key = "";
            string urlSvn = "";
            bool isCorrecto = true;

            SVN.Command command = new SVN.Command(Conf_conf);
            rutinas.General.eLog("Iniciando proceso de escaneo");
            System.Console.WriteLine("Iniciando proceso de escaneo");
            System.Console.WriteLine("-----------------------------");
            lstDirectorios = command.scan();
            System.Console.WriteLine("-----------------------------");
            System.Console.WriteLine("¿Desea continuar con estos directorios?");

            while (isCorrecto)
            {
                key = Console.ReadLine().ToLower();
                if (key.Equals("s") || key.Equals("n"))
                {
                    isCorrecto = false;
                }
                else
                {
                    System.Console.WriteLine("Ingrese comando valido");
                    System.Console.WriteLine("¿Desea continuar con estos directorios? S/N");
                }
            }
            if (key == "s")
            {
                if (Conf_conf.rutaSVN != "")
                {
                    borrar_todo();
                    Console.WriteLine("Resetenado lista de repositorios...");
                    System.Console.WriteLine("-----------------------------");
                    foreach (string directorio in lstDirectorios)
                    {                        
                        urlSvn = Conf_conf.rutaSVN + directorio;                                                
                        agregar(directorio, urlSvn);
                        Console.WriteLine(urlSvn);
                    }
                    System.Console.WriteLine("-----------------------------");                    
                }
                else
                {
                    System.Console.WriteLine("Ruta SVN no configurada");
                }

            }

            System.Console.WriteLine("Proceso Finalizado");
            rutinas.General.eLog("Proceso Finalizado");
        }

        private void configurar_directorio_repositorio(string arg1)
        {         
            string ruta;


            if (!String.IsNullOrEmpty(arg1))
            {
                ruta = arg1;

                Conf_conf.rutaRepo = ruta;

                SVN.Struct_SVN.Conf_Serializar(Conf_conf);
                Conf_conf = SVN.Struct_SVN.Conf_Deserializar();
                System.Console.WriteLine("Registro reconfigurado");
                System.Console.WriteLine("Nueva ruta directorio repositorio: " + ruta);

            }
            else
            {
                throw new FormatException("Parametro no valido");
            }
        }        

        private void configurar_svn(string arg1)
        {            
            string ruta;


            if (!String.IsNullOrEmpty(arg1))
            {
                ruta = arg1;

                Conf_conf.rutaSVN = ruta;

                SVN.Struct_SVN.Conf_Serializar(Conf_conf);
                Conf_conf = SVN.Struct_SVN.Conf_Deserializar();
                System.Console.WriteLine("Registro reconfigurado");
                System.Console.WriteLine("Nueva ruta repositorio: " + ruta);

            }
            else
            {
                throw new FormatException("Parametro no valido");
            }
        }
    }



}
