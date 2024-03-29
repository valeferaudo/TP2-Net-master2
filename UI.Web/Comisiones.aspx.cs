﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business.Entities;
using Business.Logic;

namespace UI.Web
{
    public partial class Comisiones : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Usuario usuario = new Usuario();
            if (Session["UsuarioSesion"] != null)
            {
                usuario = (Usuario)Session["UsuarioSesion"];
            }
            Session["AlumnoInscSel"] = null;

            if (Session["UsuarioSesion"] == null)
            {
                //Redirigir a login
                Response.Redirect("~/Login.aspx");
            }
            else
            {
                if (this.Page.IsPostBack)
                {
                    this.LoadGrid();
                }
                else
                {
                    this.LoadGrid();
                }
                PersonaLogic pl = new PersonaLogic();
                usuario = (Usuario)Session["UsuarioSesion"];
                if (!(pl.GetOne(usuario.IDPersona).TipoPersona == Business.Entities.Personas.tipopersona.Admin) && !(pl.GetOne(usuario.IDPersona).TipoPersona == Business.Entities.Personas.tipopersona.Docente))
                {
                    Response.Redirect("~/Default.aspx");
                }
                if (!IsPostBack)
                {
                  //this.LlenarDropPlanes();
                }
            }
        }
        private ComisionLogic _Comision;

        public ComisionLogic Comision
        {
            get
            {
                if (_Comision == null)
                {
                    _Comision = new ComisionLogic();
                }
                return _Comision;
            }
            set { _Comision = value; }
        }
        private void LoadGrid()
        {
            //this.gridViewComisiones.DataSource = this.Comision.GetAll();
            //this.gridViewComisiones.DataBind();
            PlanLogic pl = new PlanLogic();
            DataTable dt = new DataTable();

            if (dt.Columns.Count == 0)
            {
                dt.Columns.Add("ID", typeof(string));
                dt.Columns.Add("Descripcion", typeof(string));
                dt.Columns.Add("AnioEspecialidad", typeof(string));
                dt.Columns.Add("IDPlan", typeof(string));
            }
            List<Comision> comisiones = this.Comision.GetAll();
            foreach (Comision comision in comisiones)
            {
                DataRow NewRow = dt.NewRow();
                NewRow[0] = Convert.ToString(comision.ID);
                NewRow[1] = comision.Descripcion;
                NewRow[2] = Convert.ToString(comision.AnioEspecialidad);
                NewRow[3] = pl.GetOne(comision.IDPlan).Descripcion;
                dt.Rows.Add(NewRow);
            }
            this.gridViewComisiones.DataSource = dt;
            this.gridViewComisiones.DataBind();
            this.gridViewComisiones.SelectedIndex = -1;
        }
        public enum formModes
        {
            Alta,
            Baja,
            Modificacion
        }

        public formModes formMode
        {
            get { return (formModes)this.ViewState["formMode"]; }
            set { this.ViewState["formMode"] = value; }
        }
        private Comision Entity
        {
            get;
            set;
        }

        private int SelectedID
        {
            get
            {
                if (this.ViewState["SelectedID"] != null)
                {
                    return (int)this.ViewState["SelectedID"];
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                this.ViewState["SelectedID"] = value;
            }
        }

        private bool IsEntitySelected
        {
            get
            {
                return (this.SelectedID != 0);
            }
        }

        protected void gridView_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SelectedID = (int)Convert.ToInt32(this.gridViewComisiones.SelectedRow.Cells[0].Text);
            MostrarBotones();
            this.formPanel.Visible = false;
            this.ClearForm();
        }
        private void LoadForm(int ID)
        {
            PlanLogic pl = new PlanLogic();
            this.Entity = this.Comision.GetOne(ID);
            this.descripcionTextBox.Text = this.Entity.Descripcion;
            this.anioEspecialidadTextBox.Text = (this.Entity.AnioEspecialidad).ToString();
            this.LlenarDropPlanes();
            List<Plan> planes = pl.GetAll();
            Plan plan = pl.GetOne(Entity.IDPlan);
            foreach(Plan pla in planes)
            {
                if (pla.ID == plan.ID)
                {
                    this.DropDownList1.SelectedValue = (this.Entity.IDPlan).ToString();
                }
            }
            
        }

        protected void editarLinkButton_Click(object sender, EventArgs e)
        {
            this.LlenarDropPlanes();
            if (this.IsEntitySelected)
            {
                OcultarBotones();
                this.formPanel.Visible = true;
                this.formMode = formModes.Modificacion;
                this.EnableForm(true);
                this.LoadForm(this.SelectedID);

                CursoLogic cl = new CursoLogic();
                ComisionLogic comlog = new ComisionLogic();
                List<Curso> todosloscursos = cl.GetAll();
                int cursodemateria = 0;
                foreach(Curso curso in todosloscursos)
                {
                    if(curso.IDComision == comlog.GetOne(SelectedID).ID)
                    {
                        cursodemateria++;
                    }
                }
                if (cursodemateria > 0)
                {
                    this.DropDownList1.Enabled = false;
                }
            }
        }
        private void LoadEntity(Comision comision)
        {
            comision.Descripcion = this.descripcionTextBox.Text;
            comision.AnioEspecialidad = Int32.Parse(this.anioEspecialidadTextBox.Text);
            comision.IDPlan = Int32.Parse(this.DropDownList1.SelectedValue);
        }

        private void SaveEntity(Comision comision)
        {
            try
            {
                this.Comision.Save(comision);
            }
            catch (Exception)
            {
                Response.Write("<script>alert('No se puede eliminar, otros registros referencian a este')</script>");
            }
        }

        protected void aceptarLinkButton_Click(object sender, EventArgs e)
        {
            
            switch (this.formMode)
            {
                case formModes.Alta:
                    {
                        this.Entity = new Comision();
                        this.Entity.State = BusinessEntity.States.New;
                        this.LoadEntity(this.Entity);
                        this.SaveEntity(this.Entity);
                        this.LoadGrid();


                    }
                    break;
                case formModes.Baja:
                    {
                        //this.Entity.State = BusinessEntity.States.Deleted;
                        //tira error
                        //Agarrar la excepcion y mostrar alerta
                        try
                        {
                            this.DeleteEntity(this.SelectedID);
                        }
                        catch (Exception)
                        {
                            Response.Write("<script>alert('No se puede eliminar, otros registros referencian a este')</script>");
                        }
                            this.LoadGrid();
                        
                        
                    }
                    break;
                case formModes.Modificacion:
                    {
                        this.Entity = new Comision();
                        this.Entity.ID = this.SelectedID;
                        this.Entity.State = BusinessEntity.States.Modified;
                        this.LoadEntity(this.Entity);
                        this.SaveEntity(this.Entity);
                        this.LoadGrid();
                        this.DropDownList1.Enabled = true;
                    }
                    break;
                default:
                    break;
            }
            this.LoadGrid();
            MostrarBotones();
            this.formPanel.Visible = false;
        }
        private void EnableForm(bool enable)
        {
            this.descripcionTextBox.Enabled = enable;
            this.anioEspecialidadTextBox.Enabled = enable;
            this.DropDownList1.Enabled = enable;
            this.descripcionLabel.Visible = enable;
            this.anioEspecialidadLabel.Enabled = enable;
            this.idPlanLabel.Visible = enable;
            
        }

        protected void eliminarLinkButton_Click(object sender, EventArgs e)
        {
            this.LlenarDropPlanes();
            if (this.IsEntitySelected)
            {
                OcultarBotones();
                this.formMode = formModes.Baja;
                this.EnableForm(false);
                try
                {
                    this.DeleteEntity(this.SelectedID);
                }
                catch (Exception)
                {
                    Response.Write("<script>alert('No se puede eliminar, otros registros referencian a este')</script>");
                }
                this.LoadGrid();
            }
        }
        private void DeleteEntity(int ID)
        {
            this.Comision.Delete(ID);
        }

        protected void nuevoLinkButton_Click(object sender, EventArgs e)
        {
            this.DropDownList1.Enabled = true;
            this.LlenarDropPlanes();
            OcultarBotones();
            this.formPanel.Visible = true;
            this.formMode = formModes.Alta;
            this.ClearForm();
            this.EnableForm(true);
            this.gridViewComisiones.SelectedIndex = -1;
        }
        private void ClearForm()
        {
            this.descripcionTextBox.Text = string.Empty;
            this.anioEspecialidadTextBox.Text = string.Empty;
                       
            
        }

        protected void cancelarLinkButton_Click(object sender, EventArgs e)
        {
            MostrarBotones();   
            this.formPanel.Visible = false;
            this.ClearForm();
            this.DropDownList1.Enabled = true;
        }
        private void LlenarDropPlanes()
        {
            DropDownList1.Items.Clear();
            PlanLogic pl = new PlanLogic();
            if(this.Entity != null)
            {
                List<Plan> planes = pl.TraerPorEspecialidad(pl.GetOne(Entity.IDPlan).IDEspecialidad);
                foreach (Plan plan in planes)
                {
                    ListItem item = new ListItem();
                    item.Text = plan.Descripcion;
                    item.Value = Convert.ToString(plan.ID);

                    DropDownList1.Items.Add(item);
                }
            }
            else
            {
                List<Plan> planes = pl.GetAll();
                foreach (Plan plan in planes)
                {
                    ListItem item = new ListItem();
                    item.Text = plan.Descripcion;
                    item.Value = Convert.ToString(plan.ID);

                    DropDownList1.Items.Add(item);
                }
            }
            
        }
        private void OcultarBotones()
        {
            nuevoLinkButton.Visible = false;
            eliminarLinkButton.Visible = false;
            editarLinkButton.Visible = false;
        }
        private void MostrarBotones()
        {
            nuevoLinkButton.Visible = true;
            eliminarLinkButton.Visible = true;
            editarLinkButton.Visible = true;
        }
    }
}