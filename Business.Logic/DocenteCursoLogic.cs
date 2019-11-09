using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Business.Entities;

namespace Business.Logic
{
    public class DocenteCursoLogic
    {
        private Data.Database.DocenteCursoAdapter _DocenteCursoAdapter;
        public Data.Database.DocenteCursoAdapter DocenteCursoAdapter
        {
            get { return _DocenteCursoAdapter; }
            set { _DocenteCursoAdapter = value; }
        }
        public DocenteCursoLogic()
        {
            this.DocenteCursoAdapter = new Data.Database.DocenteCursoAdapter();
        }
        public List<DocenteCurso> GetInscripcionesDocente(int id)
        {
            return DocenteCursoAdapter.GetInscripcionesDocente(id);
        }
        public void Save(DocenteCurso inscrip)
        {
            DocenteCursoAdapter.Save(inscrip);
        }
    }
}
