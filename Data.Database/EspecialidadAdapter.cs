using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Entities;
using System.Data;
using System.Data.SqlClient;

namespace Data.Database
{
    public class EspecialidadAdapter : Adapter
    {
        public List<Especialidad> GetAll()
        {
            //se instancia el objeto a retornar
            List<Especialidad> especialidades = new List<Especialidad>();

            try
            {
                //abrir la conexion
                this.OpenConnection();
                //se crea un objeto sqlcomand q sera la sentencia  sql que se va a ejecutar contra la base de datos
                //los datos de la base de datos (usuario,contraseña, servidor,etc) estan en el connectionString

                SqlCommand cmdEspecialidades = new SqlCommand("SELECT * FROM especialidades", sqlConn);
                //se instancia un objeto dataReader que sera el que recupera los datos de la bd
                SqlDataReader drEspecialidades = cmdEspecialidades.ExecuteReader();

                //Read() --> lee una fila devuelta  por el comando sql, carga losdatos en drUsuarios  para poder accederlos
                //devuelve true mientras haya podido  leer datos y avanza a la siguiente fila para el proximo read()
                while (drEspecialidades.Read())
                {
                    //se crea un objeto usuario de la capa de entidades para copiar los datos de la fila del datareader 
                    //al objeto de entidades  
                    Especialidad esp = new Especialidad();
                    //se copian los datos de las filas de abajo
                    esp.ID = (int)drEspecialidades["id_especialidad"];
                    esp.Descripcion = (string)drEspecialidades["desc_especialidad"];

                    //se agrega el objeto con datos  a la lista q devolvemos
                    especialidades.Add(esp);
                }
                //se cierra el data reader 
                drEspecialidades.Close();
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada = new Exception("Error al recuperar lista de Especialidades", Ex);
                throw ExcepcionManejada;
            }
            finally
            {
                //cierra la conexion
                this.CloseConnection();
            }
            // se devuelve el objeto
            return especialidades;
        }
        public Business.Entities.Especialidad GetOne(int ID)
        {
            Especialidad especialidad = new Especialidad();

            try
            {
                this.OpenConnection();

                SqlCommand cmdEspecialidad = new SqlCommand("SELECT * FROM especialidades WHERE id_especialidad = @id", sqlConn);
                cmdEspecialidad.Parameters.Add("@id", SqlDbType.Int).Value = ID;
                SqlDataReader drEspecialidad = cmdEspecialidad.ExecuteReader();

                if (drEspecialidad.Read())
                {
                    especialidad.ID = (int)drEspecialidad["id_especialidad"];
                    especialidad.Descripcion = (string)drEspecialidad["desc_especialidad"];

                }
                drEspecialidad.Close();
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada = new Exception("Error al recuperar datos de Especialidad", Ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
            return especialidad;
        }
        protected void Update(Especialidad especialidad)
        {
            try
            {
                this.OpenConnection();

                SqlCommand cmdUpdate = new SqlCommand("UPDATE especialidades SET desc_especialidad=@desc_especialidad " +
                                                        "WHERE id_especialidad=@id", sqlConn);

                cmdUpdate.Parameters.Add("@id", SqlDbType.Int).Value = especialidad.ID;
                cmdUpdate.Parameters.Add("@desc_especialidad", SqlDbType.VarChar, 50).Value = especialidad.Descripcion;
                cmdUpdate.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada = new Exception("Error al modificar datos de la especialidad", Ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
        }
        public void Delete(int ID)
        {
            try
            {
                this.OpenConnection();

                SqlCommand cmdDelete = new SqlCommand("DELETE especialidades WHERE id_especialidad = @id", sqlConn);
                cmdDelete.Parameters.Add("@id", SqlDbType.Int).Value = ID;
                cmdDelete.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada = new Exception("Error al eliminar Especialidad", Ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
        }
        protected void Insert(Especialidad especialidad)
        {
            try
            {
                this.OpenConnection();

                SqlCommand cmdInsert = new SqlCommand("INSERT INTO especialidades (desc_especialidad) " +
                                                    "VALUES (@desc_especialidad) SELECT @@identity", sqlConn);

                cmdInsert.Parameters.Add("@id", SqlDbType.Int).Value = especialidad.ID;
                cmdInsert.Parameters.Add("@desc_especialidad", SqlDbType.VarChar, 50).Value = especialidad.Descripcion;
                especialidad.ID = Decimal.ToInt32((decimal)cmdInsert.ExecuteScalar());

            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada = new Exception("Error al crear Especialidad", Ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
        }
        public void Save(Especialidad especialidad)
        {
            if (especialidad.State == BusinessEntity.States.Deleted)
            {
                this.Delete(especialidad.ID);
            }
            else if (especialidad.State == BusinessEntity.States.New)
            {
                this.Insert(especialidad);
            }
            else if (especialidad.State == BusinessEntity.States.Modified)
            {
                this.Update(especialidad);
            }
            especialidad.State = BusinessEntity.States.Unmodified;
        }
    }
}
