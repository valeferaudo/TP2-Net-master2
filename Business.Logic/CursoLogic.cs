using Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Business.Logic
{
    public class CursoLogic : BusinessLogic
    {
        private Data.Database.CursoAdapter _CursoData;
        public Data.Database.CursoAdapter CursoData
        {
            get { return _CursoData; }
            set { _CursoData = value; }
        }
        public CursoLogic()
        {
            this.CursoData = new Data.Database.CursoAdapter();
        }
        public Curso GetOne(int id)
        {
            return CursoData.GetOne(id);
        }
        public List<Curso> GetAll()
        {
           return CursoData.GetAll();
        }
        public List<Curso> GetDispAlumno(int idalu)
        {
            return CursoData.GetInscribibleAlumno(idalu);
        }
        public void Delete(int id)
        {
            CursoData.Delete(id);
        }
        public void Save(Curso cr)
        {
            CursoData.Save(cr);
        }
        public bool YaAprobado(int idalumno, Curso curso)
        {
            if (CursoData.VerificaAprobado(idalumno, curso.IDMateria))
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }
        public List<Curso> GetCursosDisponiblesDocente(int id)
        {
            return CursoData.GetCursosDisponiblesDocente(id);
        }
    }
    
}
