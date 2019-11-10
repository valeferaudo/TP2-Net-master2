using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Business.Entities;
using Business.Logic;

namespace UI.Web
{
    public partial class Docentes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["AlumnoInscSel"] = null;
            }

            PersonaLogic pl = new PersonaLogic();
            Usuario usuario = new Usuario();
            if (Session["UsuarioSesion"] != null)
            {
                usuario = (Usuario)Session["UsuarioSesion"];
            }
            if (!(pl.GetOne(usuario.IDPersona).TipoPersona == Business.Entities.Personas.tipopersona.Admin) && !(pl.GetOne(usuario.IDPersona).TipoPersona == Business.Entities.Personas.tipopersona.Docente))
            {
                Response.Redirect("~/Default.aspx");
            }
            this.LoadGrid();
            if (!IsPostBack)
            {

            }
            if (IsPostBack)
            {

            }
        }
        private void LoadGrid()
        {
            PersonaLogic pl = new PersonaLogic();
            PlanLogic pllog = new PlanLogic();
            DataTable dt = new DataTable();
            if (dt.Columns.Count == 0)
            {
                dt.Columns.Add("ID", typeof(string));
                dt.Columns.Add("Nombre", typeof(string));
                dt.Columns.Add("Apellido", typeof(string));
                dt.Columns.Add("Direccion", typeof(string));
                dt.Columns.Add("Email", typeof(string));
                dt.Columns.Add("Telefono", typeof(string));
                dt.Columns.Add("FechaNac", typeof(string));
                dt.Columns.Add("Legajo", typeof(string));
                dt.Columns.Add("Plan", typeof(string));
            }
            List<Business.Entities.Personas> alumnos = pl.TraerPorTipoPersona(2);
            foreach (Business.Entities.Personas alumno in alumnos)
            {
                int lenfecha = alumno.FechaNacimiento.ToString().Length;
                DataRow dr = dt.NewRow();
                dr[0] = Convert.ToString(alumno.ID);
                dr[1] = alumno.Nombre;
                dr[2] = alumno.Apellido;
                dr[3] = alumno.Direccion;
                dr[4] = alumno.Email;
                dr[5] = Convert.ToString(alumno.Telefono);
                dr[6] = alumno.FechaNacimiento.ToString().Substring(0, lenfecha - 9);
                dr[7] = Convert.ToString(alumno.Legajo);
                dr[8] = pllog.GetOne(alumno.IDPlan).Descripcion;
                dt.Rows.Add(dr);
            }

            this.GridViewAlumnos.DataSource = dt;
            this.GridViewAlumnos.DataBind();
        }
        int id;
        protected void gridView_SelectedIndexChanged(object sender, EventArgs e)
        {
            id = (int)Convert.ToInt32(this.GridViewAlumnos.SelectedRow.Cells[0].Text);

        }
            

        protected void btnInscripciones_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/InscripcionesDocente.aspx?valor=" + id);
        }
    }
}
    