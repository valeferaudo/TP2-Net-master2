﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business.Entities;
using Business.Logic;

namespace UI.Web
{
    public partial class Usuarios : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
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
                if (!IsPostBack)
                {
                    LlenarDropPersonas();
                }
            }
            PersonaLogic pl = new PersonaLogic();
            Usuario usuariolog = (Usuario)Session["UsuarioSesion"];
            if (!(pl.GetOne(usuariolog.IDPersona).TipoPersona == Business.Entities.Personas.tipopersona.Admin) && !(pl.GetOne(usuariolog.IDPersona).TipoPersona == Business.Entities.Personas.tipopersona.Docente))
            {
                Response.Redirect("~/Default.aspx");
            }
        }
        private UsuarioLogic _Logic;

        public UsuarioLogic Logic
        {
            get
            {
                if (_Logic == null)
                {
                    _Logic = new UsuarioLogic();
                }
                return _Logic;
            }
            set { _Logic = value; }
        }
        private void LoadGrid()
        {
            this.gridView.DataSource = this.Logic.GetAll();
            this.gridView.DataBind();
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
        private Usuario Entity
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
            this.SelectedID = (int)this.gridView.SelectedValue;
            MostrarBotones();
            this.formPanel.Visible = false;
            this.ClearForm();
        }
        private void LoadForm(int ID)
        {
            this.Entity = this.Logic.GetOne(ID);
            this.nombreTextBox.Text = this.Entity.Nombre;
            this.apellidoTextBox.Text = this.Entity.Apellido;
            this.emailTextBox.Text = this.Entity.EMail;
            this.habilitadoCheckBox.Checked = this.Entity.Habilitado;
            this.nombreUsuarioTextBox.Text = this.Entity.NombreUsuario;
            PersonaLogic pl = new PersonaLogic();
            List<Business.Entities.Personas> personas = pl.GetAll();
            Business.Entities.Personas persona = pl.GetOne(Entity.IDPersona);
            bool contiene = false;//Verificar que no este borrado logico, si esta borrado, no setear dropdown
            foreach (Business.Entities.Personas perso in personas)
            {
                if (perso.ID == persona.ID)
                {
                    contiene = true;
                }
            }
            if (contiene)
            {
                this.ddlPersona.SelectedValue = (this.Entity.IDPersona).ToString();
            }
            
        }

        protected void editarLinkButton_Click(object sender, EventArgs e)
        {
            if (this.IsEntitySelected)
            {
                OcultarBotones();
                this.formPanel.Visible = true;
                this.formMode = formModes.Modificacion;
                this.EnableForm(true);
                this.LoadForm(this.SelectedID);
            }
        }
        private void LoadEntity(Usuario usuario)
        {
            usuario.Nombre = this.nombreTextBox.Text;
            usuario.Apellido = this.apellidoTextBox.Text;
            usuario.EMail = this.emailTextBox.Text;
            usuario.NombreUsuario = this.nombreUsuarioTextBox.Text;
            usuario.Clave = this.claveTextBox.Text;
            usuario.Habilitado = this.habilitadoCheckBox.Checked;
            usuario.IDPersona = Int32.Parse(this.ddlPersona.SelectedValue);
        }

        private void SaveEntity(Usuario usuario)
        {
            this.Logic.Save(usuario);
        }

        protected void aceptarLinkButton_Click(object sender, EventArgs e)
        {
            
            switch (this.formMode)
            {
                case formModes.Alta:
                    {
                        this.Entity = new Usuario();
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
                        this.Entity = new Usuario();
                        this.Entity.ID = this.SelectedID;
                        this.Entity.State = BusinessEntity.States.Modified;
                        this.LoadEntity(this.Entity);
                        this.SaveEntity(this.Entity);
                        this.LoadGrid();
                    }
                    break;
                default:
                    break;
            }
            this.LoadGrid();
            this.ClearForm();
            this.formPanel.Visible = false;
            MostrarBotones();
        }
        private void EnableForm(bool enable)
        {
            this.nombreTextBox.Enabled = enable;
            this.apellidoTextBox.Enabled = enable;
            this.emailTextBox.Enabled = enable;
            this.nombreUsuarioTextBox.Enabled = enable;
            this.claveTextBox.Enabled = enable;
            this.claveLabel.Visible = enable;
            this.repetirClaveTextBox.Enabled = enable;
            this.repetirClaveLabel.Visible = enable;
            this.idPersonaLabel.Visible = enable;
            this.ddlPersona.Enabled = enable;
        }

        protected void eliminarLinkButton_Click(object sender, EventArgs e)
        {
            if (this.IsEntitySelected)
            {
                OcultarBotones();
                this.formPanel.Visible = true;
                this.formMode = formModes.Baja;
                this.EnableForm(false);
                this.LoadForm(this.SelectedID);
                this.claveTextBox.Attributes["value"] = "********";
                this.repetirClaveTextBox.Attributes["value"] = "********";
            }
        }
        private void DeleteEntity(int ID)
        {
            this.Logic.Delete(ID);
        }

        protected void nuevoLinkButton_Click(object sender, EventArgs e)
        {
            OcultarBotones();
            this.formPanel.Visible = true;
            this.formMode = formModes.Alta;
            this.ClearForm();
            this.EnableForm(true);
            this.gridView.SelectedIndex = -1;
        }
        private void ClearForm()
        {
            this.nombreTextBox.Text = string.Empty;
            this.apellidoTextBox.Text = string.Empty;
            this.emailTextBox.Text = string.Empty;
            this.habilitadoCheckBox.Checked = false;
            this.nombreUsuarioTextBox.Text = string.Empty;
            this.claveTextBox.Attributes["value"] = "";
            this.repetirClaveTextBox.Attributes["value"] = "";

        }

        protected void cancelarLinkButton_Click(object sender, EventArgs e)
        {
            MostrarBotones();
            this.formPanel.Visible = false;
            this.ClearForm();
        }

        public void LlenarDropPersonas()
        {
            ddlPersona.Items.Clear();
            PersonaLogic pl = new PersonaLogic();
            List<Business.Entities.Personas> personas = pl.GetAll();
            foreach (Business.Entities.Personas persona in personas)
            {
                ListItem item = new ListItem();
                item.Text = persona.Nombre + " " + persona.Apellido;
                item.Value = Convert.ToString(persona.ID);

                ddlPersona.Items.Add(item);
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