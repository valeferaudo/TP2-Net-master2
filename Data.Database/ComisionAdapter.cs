 using System;
using System.Collections.Generic;
using System.Text;
using Business.Entities;
using System.Data;
using System.Data.SqlClient;

namespace Data.Database
{
    public class ComisionAdapter:Adapter
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

        public List<Comision> GetAll()
        {
            List<Comision> comisiones = new List<Comision>();

            try { 
                this.OpenConnection();
                
                SqlCommand cmdUsuarios = new SqlCommand("SELECT * FROM comisiones", sqlConn);
                SqlDataReader drComision = cmdUsuarios.ExecuteReader();
                while (drComision.Read())
                {
                    Comision com = new Comision();
                    com.ID = (int)drComision["id_comision"];
                    com.Descripcion = (string)drComision["desc_comision"];
                    com.AnioEspecialidad = (int)drComision["anio_especialidad"];
                    com.IDPlan = (int)drComision["id_plan"];


                    comisiones.Add(com);

                }
                drComision.Close();
            }
            catch (Exception Ex) {
                Exception ExcepcionManejada = new Exception("Error al recuperar lista de comisiones", Ex);
                throw ExcepcionManejada;
            }
            this.CloseConnection();
            return comisiones;
        }

        public Business.Entities.Comision GetOne(int ID)
        {
           Comision com = new Comision();
            try
            {
                this.OpenConnection();
                SqlCommand cmdUsuarios = new SqlCommand("SELECT * FROM comisiones WHERE id_comision = @id", sqlConn);
                cmdUsuarios.Parameters.Add("@id", SqlDbType.Int).Value = ID;
                SqlDataReader drComision = cmdUsuarios.ExecuteReader();
                if (drComision.Read())
                {


                    com.ID = (int)drComision["id_comision"];
                    com.Descripcion = (string)drComision["desc_comision"];
                    com.AnioEspecialidad = (int)drComision["anio_especialidad"];
                    com.IDPlan = (int)drComision["id_plan"];



                }
                drComision.Close();
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada = new Exception("Error al recuperar datos de la comision", Ex);
                throw ExcepcionManejada;
            }
            this.CloseConnection();
            return com;
        }

        public void Delete(int ID)
        {
            try
            {
                this.OpenConnection();
                SqlCommand cmdDelete = new SqlCommand("UPDATE comisiones set deleted = 1 where id_comision=@id", sqlConn);
                cmdDelete.Parameters.Add("@id", SqlDbType.Int).Value = ID;
                cmdDelete.ExecuteNonQuery();

            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada = new Exception("Error al eliminar comision", Ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
        }
        protected void Update(Comision comision)
        {
            try
            {
                this.OpenConnection();
                SqlCommand cmdSave = new SqlCommand(
                    "UPDATE comisiones SET desc_comision = @desc, anio_especialidad = @anio, " + "id_plan = @id_plan " + "WHERE id_comision=@id", sqlConn);
                cmdSave.Parameters.Add("@id", SqlDbType.Int).Value = comision.ID;
                cmdSave.Parameters.Add("@desc", SqlDbType.VarChar).Value = comision.Descripcion;
                cmdSave.Parameters.Add("@anio", SqlDbType.Int).Value = comision.AnioEspecialidad;
                cmdSave.Parameters.Add("@id_plan", SqlDbType.Int).Value = comision.IDPlan;
                cmdSave.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada = new Exception("Error al modificar datos de la comision", Ex);
                throw ExcepcionManejada;

            }
            finally
            {
                this.CloseConnection();
            }
        }
        protected void Insert(Comision comision)
        {


            try { 
                this.OpenConnection();
            
           
                SqlCommand cmdSave = new SqlCommand(
                    "INSERT INTO comisiones(desc_comision,anio_especialidad,id_plan) " + 
                    "values(@desc, @anio, @id_plan) " + 
                    "select @@identity", sqlConn);
                
                cmdSave.Parameters.Add("@desc", SqlDbType.VarChar).Value = comision.Descripcion;
                cmdSave.Parameters.Add("@anio", SqlDbType.Int).Value = comision.AnioEspecialidad;
                cmdSave.Parameters.Add("@id_plan", SqlDbType.Int).Value = comision.IDPlan;

                comision.ID = Decimal.ToInt32((decimal)cmdSave.ExecuteScalar());
                
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada = new Exception("Error al crear comision", Ex);
                throw ExcepcionManejada;

            }
            finally
            {
                this.CloseConnection();
            }
        }

        public void Save(Comision comision)
        {
            if (comision.State == BusinessEntity.States.New)
            {
                this.Insert(comision);
            }
            else if (comision.State == BusinessEntity.States.Deleted)
            {
                this.Delete(comision.ID);
            }
            else if (comision.State == BusinessEntity.States.Modified)
            {
                this.Update(comision);
            }
            comision.State = BusinessEntity.States.Unmodified;            
        }
        
    }
}
