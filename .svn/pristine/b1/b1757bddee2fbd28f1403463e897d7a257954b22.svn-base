using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVN_BackUp
{
    class Program
    {
        static void Main(string[] args)
        {            
            Opciones opciones = new Opciones();
            if (args.Count() != 0)
            {
                string arg = "";
                string arg1 = "";
                string arg2 = "";

                if (args.Count() == 1)
                {
                    arg = args[0].ToLower();
                    switch (arg)
                    {
                        case "-help":
                        case "/help":
                        case "help":
                        case "-?":
                        case "/?":
                            ayuda();
                            break;
                        case "start":
                        case "scan":
                        case "-l":
                        case "-ll":
                        case "-l-conf":
                            opciones.ejecutar(arg);
                            break;                        
                        default:
                            error();
                            break;
                    }
                }
                else
                {
                    arg = args[0].ToLower();
                    switch (arg)
                    {
                        case "add-svn":
                            if (args.Count() == 3)
                            {
                                arg = args[0];
                                arg1 = args[1];
                                arg2 = args[2];
                                opciones.ejecutar(arg, arg1, arg2);
                            }
                            else
                            {
                                error();
                            }
                            break;                            
                        case "del-svn":
                        case "conf-dest":
                        case "conf-dir-repo":
                        case "conf-svn":
                            if (args.Count() == 2)
                            {
                                arg = args[0];
                                arg1 = args[1];
                                opciones.ejecutar(arg, arg1);
                            }
                            else
                            {
                                error();
                            }
                            break;                        
                        default:
                            error();
                            break;
                    }

                }
            }
            else
            {
                System.Console.WriteLine("Debe ingresar al menos un parametro");
            }
            //System.Console.ReadLine();

        }

        private static void error()
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            System.Console.WriteLine("Comando no valido");
        }


        private static void ayuda()
        {            
            string var1 = @"https://111.111.111.111/svn";
            string var2 = @"C:/carpeta";
            Console.ForegroundColor = ConsoleColor.Gray;
            System.Console.WriteLine("Lista de comandos:");
            
            System.Console.WriteLine("Start: incia el proceso de descarga");
            System.Console.WriteLine("ejemplo: SVN_BackUp.exe Start");
            System.Console.WriteLine("");
            System.Console.WriteLine("-l: lista de repositorios almacenados (formateado)");
            System.Console.WriteLine("ejemplo: SVN_BackUp.exe -l");
            System.Console.WriteLine("");
            System.Console.WriteLine("-ll: lista de repositorios almacenados (sin formato)");
            System.Console.WriteLine("ejemplo: SVN_BackUp.exe -ll");
            System.Console.WriteLine("");
            System.Console.WriteLine("-l-conf: muestra ruta de destino de repositorios");
            System.Console.WriteLine("ejemplo: SVN_BackUp.exe -l-conf");
            System.Console.WriteLine("");
            System.Console.WriteLine("add-svn: agrega un nuevo repositorio a la lista");
            System.Console.WriteLine("ejemplo: SVN_BackUp.exe add-svn [arg1] [arg2]");
            System.Console.WriteLine("         SVN_BackUp.exe add-svn \"nombre\" {0}", var1);
            System.Console.WriteLine("");
            System.Console.WriteLine("del-svn: elimina un repositorio de la lista");
            System.Console.WriteLine("ejemplo: SVN_BackUp.exe del-svn [arg1]");
            System.Console.WriteLine("         SVN_BackUp.exe del-svn 1");
            System.Console.WriteLine("");
            System.Console.WriteLine("conf: actualiza la ruta de destino");
            System.Console.WriteLine("ejemplo SVN_BackUp.exe conf [arg1]");
            System.Console.WriteLine("	SVN_BackUp.exe conf \"{0}\"", var2);
        }
    }
}
