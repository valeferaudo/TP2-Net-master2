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
    public partial class Personas : System.Web.UI.Page
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
                    //this.LlenarDropTipo();
                    //this.LlenarDropPlan();
                }
            }
        }

        private PersonaLogic _Persona;

        public PersonaLogic Persona
        {
            get
            {
                if (_Persona == null)
                {
                    _Persona = new PersonaLogic();
                }
                return _Persona;
            }
            set { _Persona = value; }
        }
        private void LoadGrid()
        {
            
            PlanLogic pl = new PlanLogic();
            DataTable dt = new DataTable();

            if (dt.Columns.Count == 0)
            {
                dt.Columns.Add("ID", typeof(string));
                dt.Columns.Add("Nombre", typeof(string));
                dt.Columns.Add("Apellido", typeof(string));
                dt.Columns.Add("Direccion", typeof(string));
                dt.Columns.Add("Email", typeof(string));
                dt.Columns.Add("Telefono", typeof(string));
                dt.Columns.Add("FechaNacimiento", typeof(string));
                dt.Columns.Add("Legajo", typeof(string));
                dt.Columns.Add("TipoPersona", typeof(string));
                dt.Columns.Add("Plan", typeof(string));
            }
            List<Business.Entities.Personas> personas = this.Persona.GetAll();
            foreach (Business.Entities.Personas persona in personas)
            {
                DataRow NewRow = dt.NewRow();
                NewRow[0] = Convert.ToString(persona.ID);
                NewRow[1] = persona.Nombre;
                NewRow[2] = persona.Apellido;
                NewRow[3] = persona.Direccion;
                NewRow[4] = persona.Email;
                NewRow[5] = persona.Telefono;
                NewRow[6] = Convert.ToString(persona.FechaNacimiento);
                NewRow[7] = Convert.ToString(persona.Legajo);
                NewRow[8] = persona.TipoPersona;
                NewRow[9] = pl.GetOne(persona.IDPlan).Descripcion;
                dt.Rows.Add(NewRow);
            }
            this.gridViewPersonas.DataSource = dt;
            this.gridViewPersonas.DataBind();
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
        private Business.Entities.Personas Entity
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
            this.SelectedID = (int)Convert.ToInt32(this.gridViewPersonas.SelectedRow.Cells[0].Text);
            MostrarBotones();
            this.formPanel.Visible = false;
            this.ClearForm();
        }
        private void LoadForm(int ID)
        {
            PlanLogic pl = new PlanLogic();
            this.Entity = this.Persona.GetOne(ID);
            this.nombreTextBox.Text = this.Entity.Nombre;
            this.apellidoTextBox.Text = this.Entity.Apellido;
            this.direccionTextBox.Text = this.Entity.Direccion;
            this.emailTextBox.Text = this.Entity.Email;
            this.Calendar1.SelectedDate = Entity.FechaNacimiento;
            this.telefonoTextBox.Text = (this.Entity.Telefono).ToString();
            this.legajoTextBox.Text = (this.Entity.Legajo).ToString();

            this.LlenarDropTipo();
            this.LlenarDropPlan();
            ddlTipoPersona.SelectedValue = Convert.ToString(this.Entity.TipoPersona);
            List<Plan> planes = pl.GetAll();
            Plan plan = pl.GetOne(Entity.IDPlan);
            foreach (Plan pla in planes)
            {
                if (pla.ID == plan.ID)
                {
                    this.ddlPlan.SelectedValue = (this.Entity.IDPlan).ToString();
                }
            }

        }
        protected void editarLinkButton_Click(object sender, EventArgs e)
        {
            this.LlenarDropPlan();
            this.LlenarDropTipo();
            if (this.IsEntitySelected)
            {
                OcultarBotones();
                this.formPanel.Visible = true;
                this.formMode = formModes.Modificacion;
                this.EnableForm(true);
                this.LoadForm(this.SelectedID);
            }
        }
        private void LoadEntity(Business.Entities.Personas persona)
        {
            persona.Nombre = this.nombreTextBox.Text;
            persona.Apellido = this.apellidoTextBox.Text;
            persona.Direccion = this.direccionTextBox.Text;
            persona.Email = this.emailTextBox.Text;
            persona.Telefono = this.telefonoTextBox.Text;
            persona.FechaNacimiento = (this.Calendar1.SelectedDate);
            persona.Legajo = Int32.Parse(this.legajoTextBox.Text);
            if (ddlTipoPersona.SelectedValue.ToString() == "Alumno")
            { persona.TipoPersona = Business.Entities.Personas.tipopersona.Alumno; }
            if (ddlTipoPersona.SelectedValue.ToString() == "Docente")
            { persona.TipoPersona = Business.Entities.Personas.tipopersona.Docente; }
            if (ddlTipoPersona.SelectedValue.ToString() == "Admin")
            { persona.TipoPersona = Business.Entities.Personas.tipopersona.Admin; }
            persona.IDPlan = Int32.Parse(this.ddlPlan.SelectedValue);
        }

        private void SaveEntity(Business.Entities.Personas persona)
        {
            try
            {
                this.Persona.Save(persona);
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
                        this.Entity = new Business.Entities.Personas();
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
                        this.Entity = new Business.Entities.Personas();
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
            this.nombreLabel.Visible = enable;
            this.apellidoLabel.Visible = enable;
            this.direccionLabel.Visible = enable;
            this.emailLabel.Visible = enable;
            this.telefonoLabel.Visible = enable;
            this.fechanacLabel.Visible = enable;
            this.legajoLabel.Visible = enable;
            this.tipoLabel.Visible = enable;
            this.idPlanLabel.Visible = enable;
            this.nombreTextBox.Enabled = enable;
            this.apellidoTextBox.Enabled = enable;
            this.direccionTextBox.Enabled = enable;
            this.emailTextBox.Enabled = enable;
            this.telefonoTextBox.Enabled = enable;
            this.Calendar1.Enabled = enable;
            this.legajoTextBox.Enabled = enable;
            this.ddlTipoPersona.Enabled = enable;
            this.ddlPlan.Enabled = enable;
            
        }

        protected void eliminarLinkButton_Click(object sender, EventArgs e)
        {
            this.LlenarDropPlan();
            this.LlenarDropTipo();
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
            this.Persona.Delete(ID);
        }

        protected void nuevoLinkButton_Click(object sender, EventArgs e)
        {
            this.LlenarDropPlan();
            this.LlenarDropTipo();
            OcultarBotones();
            this.formPanel.Visible = true;
            this.formMode = formModes.Alta;
            this.ClearForm();
            this.EnableForm(true);
            this.gridViewPersonas.SelectedIndex = -1;
        }
        private void ClearForm()
        {
            this.nombreTextBox.Text = string.Empty;
            this.apellidoTextBox.Text = string.Empty;
            this.direccionTextBox.Text = string.Empty;
            this.emailTextBox.Text = string.Empty;
            this.telefonoTextBox.Text = string.Empty;
            this.legajoTextBox.Text = string.Empty;
            


        }

        protected void cancelarLinkButton_Click(object sender, EventArgs e)
        {
            MostrarBotones();
            this.formPanel.Visible = false;
            this.ClearForm();
        }
        private void LlenarDropTipo()
        {
            ddlTipoPersona.DataSource = Enum.GetValues(typeof(Business.Entities.Personas.tipopersona));
            ddlTipoPersona.DataBind();
            ddlTipoPersona.SelectedIndex = -1;
        }
        private void LlenarDropPlan()
        {
            ddlPlan.Items.Clear();
            PlanLogic pl = new PlanLogic();
            if (this.Entity != null)
            {
                List<Plan> planes = pl.TraerPorEspecialidad(pl.GetOne(Entity.IDPlan).IDEspecialidad);
                foreach (Plan plan in planes)
                {
                    ListItem item = new ListItem();
                    item.Text = plan.Descripcion;
                    item.Value = Convert.ToString(plan.ID);

                    ddlPlan.Items.Add(item);
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

                    ddlPlan.Items.Add(item);
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
