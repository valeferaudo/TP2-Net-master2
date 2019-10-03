using Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Business.Logic
{
    public class UsuarioLogic : BusinessLogic
    {
        private Data.Database.UsuarioAdapter _UsuarioData;
        public Data.Database.UsuarioAdapter UsuarioData
        {
            get { return _UsuarioData; }
            set { _UsuarioData = value; }
        }
        public UsuarioLogic()
        {
            this.UsuarioData = new Data.Database.UsuarioAdapter();
        }
        public Usuario GetOne(int id)
        {
            return UsuarioData.GetOne(id);
        }
        public List<Usuario> GetAll()
        {
           return UsuarioData.GetAll();
        }
        public void Delete(int id)
        {
            UsuarioData.Delete(id);
        }
        public void Save(Usuario us)
        {
            UsuarioData.Save(us);
        }
        public Usuario Logearse(String usname,String clave)
        {
            int idus;
            idus = UsuarioData.GetIdAtLogin(usname, clave);
            Usuario uact = new Usuario();
            uact = UsuarioData.GetOne(idus);
            return uact;
        }
    }
    
}
