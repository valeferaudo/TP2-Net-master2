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
	public partial class Especialidades : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
            Session["AlumnoInscSel"] = null;
            PersonaLogic pl = new PersonaLogic();
            Usuario usuario = (Usuario)Session["UsuarioSesion"];
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
            }
            if (!(pl.GetOne(usuario.IDPersona).TipoPersona == Personas.tipopersona.Admin) && !(pl.GetOne(usuario.IDPersona).TipoPersona == Personas.tipopersona.Docente))
            {
                Response.Redirect("~/Default.aspx");
            }
            
        }
    EspecialidadLogic _especialidad;
    private EspecialidadLogic Especialidad
        {
            get
            {
                if(_especialidad == null)
                {
                    _especialidad = new EspecialidadLogic();
                }
                return _especialidad;
            }
        }
        private void LoadGrid()
        {
            this.gridViewEspecialidad.DataSource = this.Especialidad.GetAll();
            this.gridViewEspecialidad.DataBind();
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
        private Especialidad Entity
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
            this.SelectedID = (int)this.gridViewEspecialidad.SelectedValue;
            this.formPanel.Visible = false;
            this.ClearForm();
            MostrarBotones();
        }
        private void LoadForm(int ID)
        {
            this.Entity = this.Especialidad.GetOne(ID);
            this.descripcionTextBox.Text = this.Entity.Descripcion;
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
        private void LoadEntity(Especialidad espe)
        {
            espe.Descripcion = this.descripcionTextBox.Text;
            
        }

        private void SaveEntity(Especialidad espe)
        {
            this.Especialidad.Save(espe);
        }

        protected void aceptarLinkButton_Click(object sender, EventArgs e)
        {
            switch (this.formMode)
            {
                case formModes.Alta:
                    {
                        this.Entity = new Especialidad();
                        this.Entity.State = BusinessEntity.States.New;
                        this.LoadEntity(this.Entity);
                        this.SaveEntity(this.Entity);
                        this.LoadGrid();


                    }
                    break;
                case formModes.Baja:
                    {
                        //this.Entity.State = BusinessEntity.States.Deleted;
                        //TIRA ERROR
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
                        this.Entity = new Business.Entities.Especialidad();
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
            MostrarBotones();
            this.formPanel.Visible = false;
        }
        private void EnableForm(bool enable)
        {
            this.descripcionTextBox.Enabled = enable;
            this.descripcionLabel.Visible = enable;
          
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
            }
        }
        private void DeleteEntity(int ID)
        {
            this.Especialidad.Delete(ID);
        }

        protected void nuevoLinkButton_Click(object sender, EventArgs e)
        {
            this.gridViewEspecialidad.SelectedIndex = -1 ;
            this.formPanel.Visible = true;
            this.formMode = formModes.Alta;
            this.ClearForm();
            this.EnableForm(true);
            OcultarBotones();
        }
        private void ClearForm()
        {
            this.descripcionTextBox.Text = string.Empty;
            
        }

        protected void cancelarLinkButton_Click(object sender, EventArgs e)
        {
            this.formPanel.Visible = false;
            this.ClearForm();
            MostrarBotones();
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