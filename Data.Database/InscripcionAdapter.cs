 using System;
using System.Collections.Generic;
using System.Text;
using Business.Entities;
using System.Data;
using System.Data.SqlClient;

namespace Data.Database
{
    public class InscripcionAdapter:Adapter
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

        public List<AlumnoInscripcion> GetAll()
        {
            List<AlumnoInscripcion> alumnoInscripciones = new List<AlumnoInscripcion>();

            try { 
                this.OpenConnection();
                
                SqlCommand cmdInsc = new SqlCommand("SELECT * FROM alumnos_inscripciones", sqlConn);
                SqlDataReader drInsc = cmdInsc.ExecuteReader();
                while (drInsc.Read())
                {
                    AlumnoInscripcion ins= new AlumnoInscripcion();
                    ins.ID = (int)drInsc["id_inscripcion"];
                    ins.IDAlumno = (int)drInsc["id_alumno"];
                    ins.IDCurso = (int)drInsc["id_curso"];
                    ins.Nota = (int)drInsc["nota"];
                    ins.Condicion = (string)drInsc["condicion"];
                    alumnoInscripciones.Add(ins);

                }
                drInsc.Close();
            }
            catch (Exception Ex) {
                Exception ExcepcionManejada = new Exception("Error al recuperar lista de inscripciones", Ex);
                throw ExcepcionManejada;
            }
            this.CloseConnection();
            return alumnoInscripciones;
        }
        public DataTable GetInscAlumno(Personas alumno)
        {
           
            DataTable dt = new DataTable();

            SqlDataReader drInsc;
            try
            {
                this.OpenConnection();

                SqlCommand cmdInsc = new SqlCommand("SELECT alumnos_inscripciones.id_inscripcion id "+ ", materias.desc_materia materia " + ", comisiones.desc_comision comision " +
                                                    ", cursos.anio_calendario aniocal " +
                                                    ", cursos.descripcion curso " +
                                                    ", alumnos_inscripciones.condicion " +
                                                    ", alumnos_inscripciones.nota FROM " +
                                                    "alumnos_inscripciones, materias, cursos, comisiones WHERE " +
"alumnos_inscripciones.id_curso = cursos.id_curso AND " +
"cursos.id_materia = materias.id_materia AND " + 
"cursos.id_comision = comisiones.id_comision AND " +
"alumnos_inscripciones.id_alumno = @idalu", sqlConn);
                cmdInsc.Parameters.Add("@idalu", SqlDbType.Int).Value = alumno.ID;
               
                SqlDataAdapter sqlDataAdap = new SqlDataAdapter(cmdInsc);

                sqlDataAdap.Fill(dt);
                /* while (drInsc.Read())
                 {
                     AlumnoInscripcion ins = new AlumnoInscripcion();
                     ins.ID = (int)drInsc["id_inscripcion"];
                     ins.IDAlumno = (int)drInsc["id_alumno"];
                     ins.IDCurso = (int)drInsc["id_curso"];
                     ins.Nota = (int)drInsc["nota"];
                     ins.Condicion = (string)drInsc["condicion"];


                 }
                 drInsc.Close();*/
                
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada = new Exception("Error al recuperar lista de inscripciones", Ex);
                throw ExcepcionManejada;
            }
            this.CloseConnection();
            return dt;
        }
        public Business.Entities.AlumnoInscripcion GetOne(int ID)
        {
            AlumnoInscripcion ins = new AlumnoInscripcion();
            try
            {
                this.OpenConnection();
                SqlCommand cmdUsuarios = new SqlCommand("SELECT * FROM alumnos_inscripciones WHERE id_inscripcion = @id", sqlConn);
                cmdUsuarios.Parameters.Add("@id", SqlDbType.Int).Value = ID;
                SqlDataReader drInsc = cmdUsuarios.ExecuteReader();
                if (drInsc.Read())
                {

                    ins.ID = (int)drInsc["id_inscripcion"];
                    ins.IDAlumno = (int)drInsc["id_alumno"];
            
                    ins.IDCurso = (int)drInsc["id_curso"];
                    
                    if (!DBNull.Value.Equals(drInsc["nota"]))
                    {
                        ins.Nota = (int)drInsc["nota"];
                        
                    
                    
                  
                    }

                    ins.Condicion = (string)drInsc["condicion"];




                }
                drInsc.Close();
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada = new Exception("Error al recuperar datos de la inscripcion", Ex);
                throw ExcepcionManejada;
            }
            this.CloseConnection();
            return ins;
        }

        public void Delete(int ID)
        {
            try
            {
                this.OpenConnection();
                SqlCommand cmdDelete = new SqlCommand("DELETE alumnos_inscripciones where id_inscripcion=@id", sqlConn);
                cmdDelete.Parameters.Add("@id", SqlDbType.Int).Value = ID;
                cmdDelete.ExecuteNonQuery();

            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada = new Exception("Error al eliminar inscripcion", Ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
        }
        public void LogicDelete(int ID)
        {
            try
            {
                this.OpenConnection();
                SqlCommand cmdDelete = new SqlCommand("UPDATE alumnos_inscripciones  SET logic_del = 1 where id_inscripcion=@id", sqlConn);
                cmdDelete.Parameters.Add("@id", SqlDbType.Int).Value = ID;
                cmdDelete.ExecuteNonQuery();

            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada = new Exception("Error al eliminar inscripcion", Ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
        }
        protected void Update(AlumnoInscripcion ins)
        {
            try
            {
                this.OpenConnection();
                SqlCommand cmdSave = new SqlCommand(
                    "UPDATE alumnos_inscripciones SET id_alumno = @id_alumno, id_curso = @id_curso, " + "condicion = @condicion, nota = @nota " + "WHERE id_inscripcion=@id_inscripcion", sqlConn);
                cmdSave.Parameters.Add("@id_inscripcion", SqlDbType.Int).Value = ins.ID;
                cmdSave.Parameters.Add("@id_alumno", SqlDbType.Int).Value = ins.IDAlumno;
                cmdSave.Parameters.Add("@id_curso", SqlDbType.Int).Value = ins.IDCurso;
                cmdSave.Parameters.Add("@condicion", SqlDbType.VarChar, 50).Value = ins.Condicion;
                cmdSave.Parameters.Add("@nota", SqlDbType.Int).Value = ins.Nota;
                
                
                cmdSave.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada = new Exception("Error al modificar datos de la inscripcion", Ex);
                throw ExcepcionManejada;

            }
            finally
            {
                this.CloseConnection();
            }
        }
        protected void Insert(AlumnoInscripcion ins)
        {


            try { 
                this.OpenConnection();
            
           
                SqlCommand cmdSave = new SqlCommand(
                    "INSERT INTO alumnos_inscripciones(id_alumno,id_curso,condicion,nota) " + 
                    "values(@id_alumno, @id_curso, @condicion, @nota) " + 
                    "select @@identity", sqlConn);

               
                cmdSave.Parameters.Add("@id_alumno", SqlDbType.Int).Value = ins.IDAlumno;
                cmdSave.Parameters.Add("@id_curso", SqlDbType.Int).Value = ins.IDCurso;
                cmdSave.Parameters.Add("@condicion", SqlDbType.VarChar, 50).Value = ins.Condicion;
                cmdSave.Parameters.Add("@nota", SqlDbType.Int).Value = ins.Nota;
                ins.ID = Decimal.ToInt32((decimal)cmdSave.ExecuteScalar());

                
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada = new Exception("Error al crear inscripcion", Ex);
                throw ExcepcionManejada;

            }
            finally
            {
                this.CloseConnection();
            }
        }

        public void Save(AlumnoInscripcion ins)
        {
            if (ins.State == BusinessEntity.States.New)
            {
                this.Insert(ins);
            }
            else if (ins.State == BusinessEntity.States.Deleted)
            {
                this.Delete(ins.ID);
            }
            else if (ins.State == BusinessEntity.States.Modified)
            {
                this.Update(ins);
            }
            ins.State = BusinessEntity.States.Unmodified;            
        }
        
    }
}
