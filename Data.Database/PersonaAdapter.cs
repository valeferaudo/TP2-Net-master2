 using System;
using System.Collections.Generic;
using System.Text;
using Business.Entities;
using System.Data;
using System.Data.SqlClient;

namespace Data.Database
{
    public class PersonaAdapter:Adapter
    {
        #region DatosEnMemoria
        // Esta región solo se usa en esta etapa donde los datos se mantienen en memoria.
        // Al modificar este proyecto para que acceda a la base de datos esta será eliminada
        private static List<Usuario> _Usuarios;

        private static List<Usuario> Usuarios
        {
            get
            {
                if (_Usuarios == null)
                {
                    _Usuarios = new List<Business.Entities.Usuario>();
                    Business.Entities.Usuario usr;
                    usr = new Business.Entities.Usuario();
                    usr.ID = 1;
                    usr.State = Business.Entities.BusinessEntity.States.Unmodified;
                    usr.Nombre = "Casimiro";
                    usr.Apellido = "Cegado";
                    usr.NombreUsuario = "casicegado";
                    usr.Clave = "miro";
                    usr.EMail = "casimirocegado@gmail.com";
                    usr.Habilitado = true;
                    _Usuarios.Add(usr);

                    usr = new Business.Entities.Usuario();
                    usr.ID = 2;
                    usr.State = Business.Entities.BusinessEntity.States.Unmodified;
                    usr.Nombre = "Armando Esteban";
                    usr.Apellido = "Quito";
                    usr.NombreUsuario = "aequito";
                    usr.Clave = "carpintero";
                    usr.EMail = "armandoquito@gmail.com";
                    usr.Habilitado = true;
                    _Usuarios.Add(usr);

                    usr = new Business.Entities.Usuario();
                    usr.ID = 3;
                    usr.State = Business.Entities.BusinessEntity.States.Unmodified;
                    usr.Nombre = "Alan";
                    usr.Apellido = "Brado";
                    usr.NombreUsuario = "alanbrado";
                    usr.Clave = "abrete sesamo";
                    usr.EMail = "alanbrado@gmail.com";
                    usr.Habilitado = true;
                    _Usuarios.Add(usr);

                }
                return _Usuarios;
            }
        }
        #endregion

        public List<Personas> GetAll()
        {
            List<Personas> personas = new List<Personas>();

            try { 
                this.OpenConnection();
                
                SqlCommand cmdUsuarios = new SqlCommand("SELECT * FROM personas WHERE deleted is null", sqlConn);
                SqlDataReader drPersonas = cmdUsuarios.ExecuteReader();
                while (drPersonas.Read())
                {
                    
                    Personas per = new Personas();
                    per.ID = (int)drPersonas["id_persona"];
                    per.IDPlan = (int)drPersonas["id_plan"];
                    per.Legajo = (int)drPersonas["legajo"];
                    per.Nombre = (string)drPersonas["nombre"];
                    per.Apellido = (string)drPersonas["apellido"];
                    per.Direccion = (string)drPersonas["direccion"];
                    per.Email = (string)drPersonas["email"];
                    per.FechaNacimiento = (DateTime)drPersonas["fecha_nac"];
                    per.Telefono = (string)drPersonas["telefono"];
                    if((int)drPersonas["tipo_persona"] == 1)
                    {
                        per.TipoPersona = Personas.tipopersona.Alumno;
                        
                    }
                    if ((int)drPersonas["tipo_persona"] == 2)
                    {
                        per.TipoPersona = Personas.tipopersona.Docente;

                    }
                    if ((int)drPersonas["tipo_persona"] == 3)
                    {
                        per.TipoPersona = Personas.tipopersona.Admin;

                    }



                    personas.Add(per);

                }
                drPersonas.Close();
            }
            catch (Exception Ex) {
                Exception ExcepcionManejada = new Exception("Error al recuperar lista de personas", Ex);
                throw ExcepcionManejada;
            }
            this.CloseConnection();
            return personas;
        }

        public Business.Entities.Personas GetOne(int ID)
        {
            Personas per = new Personas();
            try
            {
                this.OpenConnection();
                SqlCommand cmdUsuarios = new SqlCommand("SELECT * FROM personas WHERE id_persona = @id", sqlConn);
                cmdUsuarios.Parameters.Add("@id", SqlDbType.Int).Value = ID;
                SqlDataReader drPersona = cmdUsuarios.ExecuteReader();
                if (drPersona.Read())
                {

                    per.ID = (int)drPersona["id_persona"];
                    per.IDPlan = (int)drPersona["id_plan"];
                    per.Legajo = (int)drPersona["legajo"];
                    per.Nombre = (string)drPersona["nombre"];
                    per.Apellido = (string)drPersona["apellido"];
                    per.Direccion = (string)drPersona["direccion"];
                    per.Email = (string)drPersona["email"];
                    per.FechaNacimiento = (DateTime)drPersona["fecha_nac"];
                    per.Telefono = (string)drPersona["telefono"];
                    if ((int)drPersona["tipo_persona"] == 1)
                    {
                        per.TipoPersona = Personas.tipopersona.Alumno;

                    }
                    if ((int)drPersona["tipo_persona"] == 2)
                    {
                        per.TipoPersona = Personas.tipopersona.Docente;

                    }
                    if ((int)drPersona["tipo_persona"] == 3)
                    {
                        per.TipoPersona = Personas.tipopersona.Admin;

                    }


                }
                drPersona.Close();
               
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada = new Exception("Error al recuperar datos de persona", Ex);
                throw ExcepcionManejada;
            }
            this.CloseConnection();
            return per;
        }

        public void Delete(int ID)
        {
            try
            {
                this.OpenConnection();
                SqlCommand cmdDelete = new SqlCommand("Update personas set deleted=1 where id_persona=@id", sqlConn);
                cmdDelete.Parameters.Add("@id", SqlDbType.Int).Value = ID;
                cmdDelete.ExecuteNonQuery();

            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada = new Exception("Error al eliminar persona", Ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
        }
        protected void Update(Personas persona)
        {
            try
            {
                this.OpenConnection();
                SqlCommand cmdSave = new SqlCommand(
                    "UPDATE personas SET nombre = @nombre, apellido = @apellido, " + "id_plan = @idplan, fecha_nac = @fecnac, telefono = @tel, legajo = @legajo, direccion = @direccion, email = @email " + "WHERE id_persona=@id", sqlConn);
                cmdSave.Parameters.Add("@id", SqlDbType.Int).Value = persona.ID;
                cmdSave.Parameters.Add("@nombre", SqlDbType.VarChar, 50).Value = persona.Nombre;
                cmdSave.Parameters.Add("@apellido", SqlDbType.VarChar, 50).Value = persona.Apellido;
                cmdSave.Parameters.Add("@idplan", SqlDbType.Int).Value = persona.IDPlan;
                cmdSave.Parameters.Add("@legajo", SqlDbType.VarChar, 50).Value = persona.Legajo;
                cmdSave.Parameters.Add("@direccion", SqlDbType.VarChar, 50).Value = persona.Direccion;
                cmdSave.Parameters.Add("@email", SqlDbType.VarChar, 50).Value = persona.Email;
                cmdSave.Parameters.Add("@fecnac", SqlDbType.DateTime, 50).Value = persona.FechaNacimiento;
                cmdSave.Parameters.Add("@tel", SqlDbType.VarChar, 50).Value = persona.Telefono;
                cmdSave.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada = new Exception("Error al modificar datos de la persona", Ex);
                throw ExcepcionManejada;

            }
            finally
            {
                this.CloseConnection();
            }
        }
        protected void Insert(Personas persona)
        {


            try { 
                this.OpenConnection();
            
           
                SqlCommand cmdSave = new SqlCommand(
               "INSERT INTO personas(nombre, apellido, tipo_persona, id_plan, fecha_nac, telefono, legajo, direccion, email)" +
               " VALUES (@nombre, @apellido, @tipoper, @idplan, @fecnac, @tel, @legajo, @direccion, @email )" + "select @@identity", sqlConn);
                
                cmdSave.Parameters.Add("@nombre", SqlDbType.VarChar, 50).Value = persona.Nombre;
                cmdSave.Parameters.Add("@apellido", SqlDbType.VarChar, 50).Value = persona.Apellido;
                cmdSave.Parameters.Add("@idplan", SqlDbType.Int).Value = persona.IDPlan;
                cmdSave.Parameters.Add("@legajo", SqlDbType.VarChar, 50).Value = persona.Legajo;
                cmdSave.Parameters.Add("@direccion", SqlDbType.VarChar, 50).Value = persona.Direccion;
                cmdSave.Parameters.Add("@email", SqlDbType.VarChar, 50).Value = persona.Email;
                cmdSave.Parameters.Add("@fecnac", SqlDbType.DateTime, 50).Value = persona.FechaNacimiento;
                cmdSave.Parameters.Add("@tel", SqlDbType.VarChar, 50).Value = persona.Telefono;
                int nrotipo = 0;
                if(persona.TipoPersona == Personas.tipopersona.Admin)
                {
                    nrotipo = 3;
                }
                if (persona.TipoPersona == Personas.tipopersona.Alumno)
                {
                    nrotipo = 1;
                }
                if (persona.TipoPersona == Personas.tipopersona.Docente)
                {
                    nrotipo = 2;
                }

                cmdSave.Parameters.Add("@tipoper", SqlDbType.VarChar, 50).Value = nrotipo;
                persona.ID = Decimal.ToInt32((decimal)cmdSave.ExecuteScalar());

            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada = new Exception("Error al crear usuario", Ex);
                throw ExcepcionManejada;

            }
            finally
            {
                this.CloseConnection();
            }
        }

        public void Save(Personas persona)
        {
            if (persona.State == BusinessEntity.States.New)
            {
                this.Insert(persona);
            }
            else if (persona.State == BusinessEntity.States.Deleted)
            {
                this.Delete(persona.ID);
            }
            else if (persona.State == BusinessEntity.States.Modified)
            {
                this.Update(persona);
            }
            persona.State = BusinessEntity.States.Unmodified;            
        }
        public List<Personas> GetAllAlumnos()
        {
            List<Personas> personas = new List<Personas>();

            try
            {
                this.OpenConnection();

                SqlCommand cmdUsuarios = new SqlCommand("SELECT * FROM personas WHERE tipo_persona = 1 and deleted is null", sqlConn);
                SqlDataReader drPersonas = cmdUsuarios.ExecuteReader();
                while (drPersonas.Read())
                {

                    Personas per = new Personas();
                    per.ID = (int)drPersonas["id_persona"];
                    per.IDPlan = (int)drPersonas["id_plan"];
                    per.Legajo = (int)drPersonas["legajo"];
                    per.Nombre = (string)drPersonas["nombre"];
                    per.Apellido = (string)drPersonas["apellido"];
                    per.Direccion = (string)drPersonas["direccion"];
                    per.Email = (string)drPersonas["email"];
                    per.FechaNacimiento = (DateTime)drPersonas["fecha_nac"];
                    per.Telefono = (string)drPersonas["telefono"];
                    if ((int)drPersonas["tipo_persona"] == 1)
                    {
                        per.TipoPersona = Personas.tipopersona.Alumno;

                    }
                    if ((int)drPersonas["tipo_persona"] == 2)
                    {
                        per.TipoPersona = Personas.tipopersona.Docente;

                    }
                    if ((int)drPersonas["tipo_persona"] == 3)
                    {
                        per.TipoPersona = Personas.tipopersona.Admin;

                    }



                    personas.Add(per);

                }
                drPersonas.Close();
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada = new Exception("Error al recuperar lista de alumnos", Ex);
                throw ExcepcionManejada;
            }
            this.CloseConnection();
            return personas;

        }


    }
}
