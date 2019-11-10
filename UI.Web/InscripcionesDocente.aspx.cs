using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business.Entities;
using Business.Logic;

namespace UI.Web
{
    public partial class InscripcionesDocente : System.Web.UI.Page
    {
        Usuario usuario;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            PanelInscribir.Visible = false;
            if (Session["UsuarioSesion"] != null)
            {
                usuario = (Usuario)Session["UsuarioSesion"];
            }
            else
            {
                Response.Redirect("~/Default.aspx");
            }
            if (!IsPostBack)
            {
                this.Listar();
            }
        }
        private void Listar()
        {
            if (Request.Params["valor"] != null)
            {
                DocenteCursoLogic dcl = new DocenteCursoLogic();
                int id = Convert.ToInt32(Request.Params["valor"]);
                this.gridViewInscripciones.DataSource = dcl.GetInscripcionesDocente(id);
            }
        }

        protected void btnNuevaInscripcion_Click(object sender, EventArgs e)
        {
            PanelInscribir.Visible = true;
            LlenarCargos();
            LlenarCursos();
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            //La parte de guardar
            PanelInscribir.Visible = false;
        }
        public void LlenarCursos()
        {
            ddlCurso.Items.Clear();
            if (Request.Params["valor"] != null)
            {
                CursoLogic cl = new CursoLogic();
                int id = Convert.ToInt32(Request.Params["valor"]);
                ddlCurso.DataSource = cl.GetCursosDisponiblesDocente(id);
                ddlCurso.DataBind();
            }
        }
        public void LlenarCargos()
        {
            ddlCargo.Items.Clear();
            ddlCargo.DataSource = Enum.GetValues(typeof(Business.Entities.DocenteCurso.TiposCargos));
            ddlCargo.DataBind();
        }
    }
}