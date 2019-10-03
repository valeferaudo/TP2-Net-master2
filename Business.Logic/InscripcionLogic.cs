using Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;


namespace Business.Logic
{
    public class InscripcionLogic : BusinessLogic
    {
        private Data.Database.InscripcionAdapter _InscripcionData;
        public Data.Database.InscripcionAdapter InscripcionData
        {
            get { return _InscripcionData; }
            set { _InscripcionData = value; }
        }
        public InscripcionLogic()
        {
            this.InscripcionData = new Data.Database.InscripcionAdapter();
        }
        public AlumnoInscripcion GetOne(int id)
        {
            return InscripcionData.GetOne(id);
        }
        public List<AlumnoInscripcion> GetAll()
        {
           return InscripcionData.GetAll();
        }
        public DataTable GetInscAlumno(Personas alumno)
        {
            return InscripcionData.GetInscAlumno(alumno);
        }
        public void Delete(int id)
        {
            InscripcionData.Delete(id);
        }
        public void Save(AlumnoInscripcion ins)
        {
            InscripcionData.Save(ins);
        }
        public void LogicDelete(int id)
        {
            InscripcionData.LogicDelete(id);
        }
        
        
        
    }
    
}
