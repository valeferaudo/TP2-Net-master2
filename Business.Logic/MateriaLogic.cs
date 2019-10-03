using Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Business.Logic
{
    public class MateriaLogic : BusinessLogic
    {
        private Data.Database.MateriaAdapter _MateriaData;
        public Data.Database.MateriaAdapter MateriaData
        {
            get { return _MateriaData; }
            set { _MateriaData = value; }
        }
        public MateriaLogic()
        {
            this.MateriaData = new Data.Database.MateriaAdapter();
        }
        public Materia GetOne(int id)
        {
            return MateriaData.GetOne(id);
        }
        public List<Materia> GetAll()
        {
           return MateriaData.GetAll();
        }
        public void Delete(int id)
        {
            MateriaData.Delete(id);
        }
        public void Save(Materia cr)
        {
            MateriaData.Save(cr);
        }
        
    }
    
}
