using Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Business.Logic
{
    public class PersonaLogic : BusinessLogic
    {
        private Data.Database.PersonaAdapter _PersonaData;
        public Data.Database.PersonaAdapter PersonaData
        {
            get { return _PersonaData; }
            set { _PersonaData = value; }
        }
        public PersonaLogic()
        {
            this.PersonaData = new Data.Database.PersonaAdapter();
        }
        public Personas GetOne(int id)
        {
            return PersonaData.GetOne(id);
        }
        public List<Personas> GetAll()
        {
           return PersonaData.GetAll();
        }
        public void Delete(int id)
        {
            PersonaData.Delete(id);
        }
        public void Save(Personas per)
        {
            PersonaData.Save(per);
        }
        public List<Personas> GetAllAlumnos()
        {
            return PersonaData.GetAllAlumnos();
        }
        
    }
    
}
