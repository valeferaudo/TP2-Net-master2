using Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Business.Logic
{
    public class PlanLogic : BusinessLogic
    {
        private Data.Database.PlanAdapter _PlanData;
        public Data.Database.PlanAdapter PlanData
        {
            get { return _PlanData; }
            set { _PlanData = value; }
        }
        public PlanLogic()
        {
            this.PlanData = new Data.Database.PlanAdapter();
        }
        public Plan GetOne(int id)
        {
            return PlanData.GetOne(id);
        }
        public List<Plan> GetAll()
        {
           return PlanData.GetAll();
        }
        public void Delete(int id)
        {
            PlanData.Delete(id);
        }
        public void Save(Plan pl)
        {
            PlanData.Save(pl);
        }
     
    }
    
}
