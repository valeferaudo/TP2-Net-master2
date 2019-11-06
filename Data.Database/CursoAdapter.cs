 using System;
using System.Collections.Generic;
using System.Text;
using Business.Entities;
using System.Data;
using System.Data.SqlClient;

namespace Data.Database
{
    public class CursoAdapter : Adapter
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

        public List<Curso> GetAll()
        {
            List<Curso> cursos = new List<Curso>();

            try
            {
                this.OpenConnection();

                SqlCommand cmdUsuarios = new SqlCommand("SELECT * FROM cursos where deleted is null", sqlConn);
                SqlDataReader drCursos = cmdUsuarios.ExecuteReader();
                while (drCursos.Read())
                {
                    Curso csr = new Curso();
                    csr.ID = (int)drCursos["id_curso"];
                    csr.AnioCalendario = (int)drCursos["anio_calendario"];
                    csr.Cupo = (int)drCursos["cupo"];
                    csr.Descripcion = (string)drCursos["descripcion"];
                    csr.IDComision = (int)drCursos["id_comision"];
                    csr.IDMateria = (int)drCursos["id_materia"];
                    cursos.Add(csr);

                }
                drCursos.Close();
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada = new Exception("Error al recuperar lista de cursos", Ex);
                throw ExcepcionManejada;
            }
            this.CloseConnection();
            return cursos;
        }
        public DataTable ReporteCurso()
        {
            DataSet reporte = new DataSet();
            DataTable dt = new DataTable();
            if (dt.Columns.Count == 0)
            {
                dt.Columns.Add("id_curso", typeof(int));
                dt.Columns.Add("descripcion", typeof(string));
                dt.Columns.Add("cupo", typeof(int));
                dt.Columns.Add("anio_calendario", typeof(int));
                dt.Columns.Add("desc_materia", typeof(string));
                dt.Columns.Add("desc_comision", typeof(string));
            }
            List<Curso> cursos = this.GetAll();
            MateriaAdapter ma = new MateriaAdapter();
            ComisionAdapter ca = new ComisionAdapter();
            foreach(Curso curso in cursos)
            {
                DataRow newrow = dt.NewRow();
                newrow[0] = curso.ID;
                newrow[1] = curso.Descripcion;
                newrow[2] = curso.Cupo;
                newrow[3] = curso.AnioCalendario;
                newrow[4] = ma.GetOne(curso.IDMateria).Descripcion;
                newrow[5] = ca.GetOne(curso.IDComision).Descripcion;
                dt.Rows.Add(newrow);
            }
            
            return dt;
        }

        public List<Curso> GetInscribibleAlumno(int alumnoid)
        {

            List<Curso> cursos = new List<Curso>();

            try
            {
                this.OpenConnection();

                SqlCommand cmdUsuarios = new SqlCommand("select * from cursos, materias where " +
                "cursos.id_materia = materias.id_materia AND materias.id_plan = @idpl and cursos.deleted is null", sqlConn);
                PersonaAdapter pa = new PersonaAdapter();
                cmdUsuarios.Parameters.Add("@idpl", SqlDbType.Int).Value = pa.GetOne(alumnoid).IDPlan;
                SqlDataReader drCursos = cmdUsuarios.ExecuteReader();
                while (drCursos.Read())
                {
                    Curso csr = new Curso();
                    csr.ID = (int)drCursos["id_curso"];
                   csr.AnioCalendario = (int)drCursos["anio_calendario"];
                   csr.Cupo = (int)drCursos["cupo"];
                    csr.Descripcion = (string)drCursos["descripcion"];
                    csr.IDComision = (int)drCursos["id_comision"];
                    csr.IDMateria = (int)drCursos["id_materia"];
                    cursos.Add(csr);

                }
                drCursos.Close();
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada = new Exception("Error al recuperar lista de cursos", Ex);
                throw ExcepcionManejada;
            }
            this.CloseConnection();
            return cursos;
        }

        public bool VerificaAprobado(int idalumno, int idmateria)
        {
            List<Curso> cursos = new List<Curso>();

            try
            {
                this.OpenConnection();

                SqlCommand cmdUsuarios = new SqlCommand("select * from alumnos_inscripciones, cursos WHERE alumnos_inscripciones.id_curso = cursos.id_curso AND " +
"alumnos_inscripciones.id_alumno = @idalumno AND cursos.id_materia = @idmat " +
"AND alumnos_inscripciones.condicion = 'Aprobado'", sqlConn);
                cmdUsuarios.Parameters.Add("@idalumno", SqlDbType.Int).Value = idalumno;
                cmdUsuarios.Parameters.Add("@idmat", SqlDbType.Int).Value = idmateria;
                SqlDataReader drCursos = cmdUsuarios.ExecuteReader();
                while (drCursos.Read())
                {
                    Curso csr = new Curso();
                    csr.ID = (int)drCursos["id_curso"];
                    csr.AnioCalendario = (int)drCursos["anio_calendario"];
                    csr.Cupo = (int)drCursos["cupo"];
                    csr.Descripcion = (string)drCursos["descripcion"];
                    csr.IDComision = (int)drCursos["id_comision"];
                    csr.IDMateria = (int)drCursos["id_materia"];
                    cursos.Add(csr);

                }
                drCursos.Close();
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada = new Exception("Error al validar la aprobacion del curso", Ex);
                throw ExcepcionManejada;
            }
            this.CloseConnection();
            if (cursos.Count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        public Business.Entities.Curso GetOne(int ID)
        {
            Curso csr = new Curso();
            try
            {
                this.OpenConnection();
                SqlCommand cmdUsuarios = new SqlCommand("SELECT * FROM cursos WHERE id_curso = @id", sqlConn);
                cmdUsuarios.Parameters.Add("@id", SqlDbType.Int).Value = ID;
                SqlDataReader drCursos = cmdUsuarios.ExecuteReader();
                if (drCursos.Read())
                {

                    csr.ID = (int)drCursos["id_curso"];
                    csr.AnioCalendario = (int)drCursos["anio_calendario"];
                    csr.Cupo = (int)drCursos["cupo"];
                    csr.Descripcion = (string)drCursos["descripcion"];
                    csr.IDComision = (int)drCursos["id_comision"];
                    csr.IDMateria = (int)drCursos["id_materia"];


                }
                drCursos.Close();
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada = new Exception("Error al recuperar datos del usuario", Ex);
                throw ExcepcionManejada;
            }
            this.CloseConnection();
            return csr;
        }

        public void Delete(int ID)
        {
            try
            {
                this.OpenConnection();
                SqlCommand cmdDelete = new SqlCommand("UPDATE cursos SET deleted = 1 where id_curso=@id", sqlConn);
                cmdDelete.Parameters.Add("@id", SqlDbType.Int).Value = ID;
                cmdDelete.ExecuteNonQuery();

            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada = new Exception("Error al eliminar curso", Ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
        }
        protected void Update(Curso curso)
        {
            try
            {
                this.OpenConnection();
                SqlCommand cmdSave = new SqlCommand(
                    "UPDATE cursos SET id_materia = @id_materia, id_comision = @id_comision, " + "anio_calendario = @anio, cupo = @cupo, descripcion = @descripcion " + "WHERE id_curso=@id", sqlConn);
                cmdSave.Parameters.Add("@id", SqlDbType.Int).Value = curso.ID;
                cmdSave.Parameters.Add("@id_materia", SqlDbType.Int).Value = curso.IDMateria;
                cmdSave.Parameters.Add("@id_comision", SqlDbType.Int).Value = curso.IDComision;
                cmdSave.Parameters.Add("@anio", SqlDbType.Int).Value = curso.AnioCalendario;
                cmdSave.Parameters.Add("@cupo", SqlDbType.Int).Value = curso.Cupo;
                cmdSave.Parameters.Add("@descripcion", SqlDbType.VarChar, 50).Value = curso.Descripcion;

                cmdSave.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada = new Exception("Error al modificar datos del curso", Ex);
                throw ExcepcionManejada;

            }
            finally
            {
                this.CloseConnection();
            }
        }
        protected void Insert(Curso curso)
        {


            try
            {
                this.OpenConnection();


                SqlCommand cmdSave = new SqlCommand(
                    "INSERT INTO cursos(id_materia,id_comision,anio_calendario,cupo,descripcion) " +
                    "values(@id_materia, @id_comision, @anio, @cupo, @descripcion) " +
                    "select @@identity", sqlConn);

                cmdSave.Parameters.Add("@id_materia", SqlDbType.Int).Value = curso.IDMateria;
                cmdSave.Parameters.Add("@id_comision", SqlDbType.Int).Value = curso.IDComision;
                cmdSave.Parameters.Add("@anio", SqlDbType.Int).Value = curso.AnioCalendario;
                cmdSave.Parameters.Add("@cupo", SqlDbType.Int).Value = curso.Cupo;
                cmdSave.Parameters.Add("@descripcion", SqlDbType.VarChar, 50).Value = curso.Descripcion;
                curso.ID = Decimal.ToInt32((decimal)cmdSave.ExecuteScalar());

            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada = new Exception("Error al crear curso", Ex);
                throw ExcepcionManejada;

            }
            finally
            {
                this.CloseConnection();
            }
        }

        public void Save(Curso curso)
        {
            if (curso.State == BusinessEntity.States.New)
            {
                this.Insert(curso);
            }
            else if (curso.State == BusinessEntity.States.Deleted)
            {
                this.Delete(curso.ID);
            }
            else if (curso.State == BusinessEntity.States.Modified)
            {
                this.Update(curso);
            }
            curso.State = BusinessEntity.States.Unmodified;
        }
    }
}
