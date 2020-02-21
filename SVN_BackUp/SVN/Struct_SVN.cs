using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace SVN_BackUp.SVN
{
    public class Struct_SVN
    {
        [Serializable]
        public struct List_SVN
        {
            public string nombre;
            public string url_svn;
        }

        [Serializable]
        public struct Conf
        {
            public string rutaDestino;
            public string rutaRepo;
            public string rutaSVN;
        }

        //==========================================================================================
        //===============Constante==================================================================
        //==========================================================================================
        const string lst_svn = "data_svn.dat";
        const string conf_svn = "cnf_svn.conf";


        //==========================================================================================
        //===============Serialización==============================================================
        //==========================================================================================

        public static void Serializar(List<List_SVN> list_dir)
        {
            //AppDomain.CurrentDomain.SetupInformation.ApplicationBase + 
            FileStream _fs = new FileStream(lst_svn, FileMode.Create);
            BinaryFormatter _formatter = new BinaryFormatter();
            _formatter.Serialize(_fs, list_dir);
            _fs.Close();
        }

        public static List<List_SVN> Deserializar()
        {
            List<List_SVN> _list_dir = new List<List_SVN>();
            if (System.IO.File.Exists(lst_svn))
            {
                FileStream _fs = new FileStream(AppDomain.CurrentDomain.SetupInformation.ApplicationBase + lst_svn, FileMode.Open);
                BinaryFormatter _formatter = new BinaryFormatter();
                _list_dir = _formatter.Deserialize(_fs) as List<List_SVN>;
                _fs.Close();
            }
            return _list_dir;

        }

        public static void Conf_Serializar(Conf conf)
        {
            //AppDomain.CurrentDomain.SetupInformation.ApplicationBase + 
            FileStream _fs = new FileStream(conf_svn, FileMode.Create);
            BinaryFormatter _formatter = new BinaryFormatter();
            _formatter.Serialize(_fs, conf);
            _fs.Close();
        }

        public static Conf Conf_Deserializar()
        {
            Conf? _conf = new Conf();
            Conf _conf_final;

            if (System.IO.File.Exists(conf_svn))
            {
                FileStream _fs = new FileStream(AppDomain.CurrentDomain.SetupInformation.ApplicationBase + conf_svn, FileMode.Open);
                BinaryFormatter _formatter = new BinaryFormatter();

                _conf = _formatter.Deserialize(_fs) as Conf?;
                _fs.Close();
            }

            _conf_final = (Conf)_conf;

            if (_conf_final.rutaDestino == null)
            {
                _conf_final.rutaDestino = "";
            }
            if (_conf_final.rutaRepo == null)
            {
                _conf_final.rutaRepo = "";
            }
            if (_conf_final.rutaSVN == null)
            {
                _conf_final.rutaSVN = "";
            }


            return _conf_final;

        }

    }
}
