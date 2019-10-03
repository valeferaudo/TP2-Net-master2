using Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Business.Logic
{
    public class ComisionLogic : BusinessLogic
    {
        private Data.Database.ComisionAdapter _ComisionData;
        public Data.Database.ComisionAdapter ComisionData
        {
            get { return _ComisionData; }
            set { _ComisionData = value; }
        }
        public ComisionLogic()
        {
            this.ComisionData = new Data.Database.ComisionAdapter();
        }
        public Comision GetOne(int id)
        {
            return ComisionData.GetOne(id);
        }
        public List<Comision> GetAll()
        {
           return ComisionData.GetAll();
        }
        public void Delete(int id)
        {
            ComisionData.Delete(id);
        }
        public void Save(Comision com)
        {
            ComisionData.Save(com);
        }
        
    }
    
}
