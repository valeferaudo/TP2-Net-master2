using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business.Entities;
using Business.Logic;
using System.Data;

namespace UI.Web
{
    public partial class InscripcionesDocente : System.Web.UI.Page
    {
        Usuario usuario;
        Business.Entities.Personas perso;
        
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
            
            if ((Business.Entities.Personas)Session["DocenteSelec"] != null)
            {
                perso = (Business.Entities.Personas)Session["DocenteSelec"];
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
            
                DocenteCursoLogic dcl = new DocenteCursoLogic();

            //this.gridViewInscripciones.DataSource = dcl.GetInscripcionesDocente(perso.ID);
            //this.gridViewInscripciones.DataBind();
            //Intento hacerlo con un datatable
            DataTable dt = new DataTable();
            if (dt.Columns.Count == 0)
            {
                dt.Columns.Add("ID", typeof(string));
                dt.Columns.Add("descripcionCurso", typeof(string));
                dt.Columns.Add("Cargo", typeof(string));
            }
            List<DocenteCurso> dclist = dcl.GetInscripcionesDocente(perso.ID);
            foreach (DocenteCurso docur in dclist)
            {
                CursoLogic cl = new CursoLogic();
                DataRow dr = dt.NewRow();
                string desc_cargo="";
                if(docur.Cargo == DocenteCurso.TiposCargos.Auxiliar)
                {
                    desc_cargo = "Auxiliar";
                }
                if (docur.Cargo == DocenteCurso.TiposCargos.JefeCatedra)
                {
                    desc_cargo = "Jefe de catedra";
                }
                dr[0] = Convert.ToString(docur.ID);
                dr[1] = cl.GetOne(docur.IDCurso).Descripcion;
                dr[2] = desc_cargo;
                dt.Rows.Add(dr);
            }
            this.gridViewInscripciones.DataSource = dt;
            this.gridViewInscripciones.DataBind();
        }
    
        protected void btnNuevaInscripcion_Click(object sender, EventArgs e)
        {
            PanelInscribir.Visible = true;
            LlenarCargos();
            LlenarCursos();
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            DocenteCursoLogic dcl = new DocenteCursoLogic();
            DocenteCurso docenteCurso = new DocenteCurso();
            docenteCurso.IDDocente = perso.ID;
            docenteCurso.IDCurso = Convert.ToInt32(ddlCurso.SelectedValue);
            docenteCurso.Cargo = (DocenteCurso.TiposCargos)(ddlCargo.SelectedIndex+1);
            docenteCurso.State = BusinessEntity.States.New;
            dcl.Save(docenteCurso);
            PanelInscribir.Visible = false;
            this.Listar();
        }
        public void LlenarCursos()
        {
            ddlCurso.Items.Clear();
            
                CursoLogic cl = new CursoLogic();
                
                ddlCurso.DataSource = cl.GetCursosDisponiblesDocente(perso.ID);
            ddlCurso.DataTextField = "Descripcion";
            ddlCurso.DataValueField = "ID";
                ddlCurso.DataBind();
            
        }
        public void LlenarCargos()
        {
            ddlCargo.Items.Clear();
            ddlCargo.DataSource = Enum.GetValues(typeof(Business.Entities.DocenteCurso.TiposCargos));
            ddlCargo.DataBind();
        }
    }
}