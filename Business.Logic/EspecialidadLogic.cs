using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Entities;
using Data.Database;

namespace Business.Logic
{
   public class EspecialidadLogic : BusinessLogic
    {
        private Data.Database.EspecialidadAdapter _EspecialidadData;
        public Data.Database.EspecialidadAdapter EspecialidadData
        {
            get { return _EspecialidadData; }
            set { _EspecialidadData = value; }
        }

        public EspecialidadLogic()
        {
            _EspecialidadData = new EspecialidadAdapter();
        }
        public List<Especialidad> GetAll()
        {
            return (_EspecialidadData.GetAll());
        }
        public Especialidad GetOne(int ID)
        {
            return (_EspecialidadData.GetOne(ID));
        }

        public void Delete(int ID)
        {
            _EspecialidadData.Delete(ID);
        }

        public void Save(Especialidad especialidad)
        {
            _EspecialidadData.Save(especialidad);
        }
    }
}
