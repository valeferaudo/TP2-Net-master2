using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Entities;
using System.Data.SqlClient;
using System.Data;

namespace Data.Database
{
   public class DocenteCursoAdapter : Adapter
    {
        public List<DocenteCurso> GetInscripcionesDocente(int id)
        {
            List<DocenteCurso> inscripciones = new List<DocenteCurso>();
            try
            {
                
                OpenConnection();

                string query = "SELECT dc.*, per.nombre + ' ' + per.apellido as descripcion_docente, cur.descripcion as descripcion_curso " +
                         "from docentes_cursos as dc inner join personas as per on dc.id_docente = per.id_persona " +
                         "inner join cursos as cur on dc.id_curso = cur.id_curso" +
                         " where dc.id_docente = @id  ";

                SqlCommand cmd = new SqlCommand(query, sqlConn);
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    DocenteCurso inscr = new DocenteCurso();
                    inscr.ID = (int)dr["id_dictado"];
                    inscr.IDDocente = (int)dr["id_docente"];
                    inscr.IDCurso = (int)dr["id_curso"];
                    inscr.DescripcionDocente = dr["descripcion_docente"].ToString();
                    inscr.DescripcionCurso = dr["descripcion_curso"].ToString();
                    if ((int)dr["cargo"] == 1)
                    {
                        inscr.Cargo = DocenteCurso.TiposCargos.JefeCatedra;

                    }
                    if ((int)dr["cargo"] == 2)
                    {
                        inscr.Cargo = DocenteCurso.TiposCargos.Auxiliar;

                    }
                    inscripciones.Add(inscr);
                }
                dr.Close();
                return inscripciones;
                
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada = new Exception("Error al recuperar inscripciones del docente", Ex);
                throw ExcepcionManejada;
            }
            finally
            {
                CloseConnection();
            }
        }
        public void Save(DocenteCurso ins)
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
        public void Insert(DocenteCurso ins)
        {
            try
            {
                OpenConnection();
                SqlCommand cmd = new SqlCommand("INSERT INTO docentes_cursos (id_curso,id_docente,cargo)" +
                                                "VALUES (@curso,@docente,@cargo) select @@identity", sqlConn);
                cmd.Parameters.Add("@curso", SqlDbType.Int).Value = ins.IDCurso;
                cmd.Parameters.Add("@docente", SqlDbType.Int).Value = ins.IDDocente;
                cmd.Parameters.Add("@cargo", SqlDbType.Int).Value = ins.Cargo;
                cmd.ExecuteNonQuery();
            }
            catch(Exception Ex)
            {
                Exception ExcepcionManejada = new Exception("Error al insertar un Docente_Curso", Ex);
                throw ExcepcionManejada;
            }
            finally
            {

            }
        }
        public void Delete(int id)
        {

        }
        public void Update(DocenteCurso ins)
        {

        }
    }
}
