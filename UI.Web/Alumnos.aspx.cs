using Business.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Business.Entities;

namespace UI.Web
{
    public partial class Alumnos : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            this.LoadGrid();
            if (!IsPostBack)
            {
                this.Panel1.Visible = false;
                this.LlenarCombo();
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
            if(dt.Columns.Count == 0)
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
            List < Personas > alumnos = pl.GetAllAlumnos();
            foreach(Personas alumno in alumnos)
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
        private Personas Entity
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
            this.SelectedID = (int)Convert.ToInt32(this.GridViewAlumnos.SelectedRow.Cells[0].Text);
            
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            if (SelectedID > 0)
            {
                PersonaLogic perlog = new PersonaLogic();
                perlog.Delete(SelectedID);
                this.LlenarCombo();
                this.CleantxtBox();
                this.Panel1.Visible = false;
                this.LoadGrid();

            }
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            this.Panel1.Visible = true;
            this.formMode = formModes.Alta;
            this.CleantxtBox();
            this.DropDownListPlan.Visible = true;
            this.DropDownListPlan.Enabled = true;
            
        }
        private void CleantxtBox()
        {
            txtApellido.Text = "";
            txtDireccion.Text = "";
            txtEmail.Text = "";
            txtLegajo.Text = "";
            txtNombre.Text = "";
            txtTelefono.Text = "";
        }
        private void LlenarCombo()
        {
            PlanLogic pllogc = new PlanLogic();
            List<Plan> planes = pllogc.GetAll();
            DropDownListPlan.Items.Clear();
            foreach(Plan plan in planes)
            {
                DropDownListPlan.Items.Add(plan.Descripcion);
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if(txtApellido.Text!="" && txtDireccion.Text!="" && txtEmail.Text!="" && txtLegajo.Text!="" && txtNombre.Text!="" && txtTelefono.Text != "")
            {
                if (this.formMode == formModes.Alta)
                {
                    PersonaLogic pl = new PersonaLogic();
                    Personas alumno = new Personas();
                    alumno.Apellido = txtApellido.Text;
                    alumno.Direccion = txtDireccion.Text;
                    alumno.Email = txtEmail.Text;
                    alumno.Legajo = Convert.ToInt32(txtLegajo.Text);
                    alumno.Nombre = txtNombre.Text;
                    alumno.Telefono = txtTelefono.Text;
                    alumno.FechaNacimiento = Calendar1.SelectedDate;
                    alumno.TipoPersona = Personas.tipopersona.Alumno;
                    PlanLogic pllogc = new PlanLogic();
                    List<Plan> planes = pllogc.GetAll();
                    alumno.IDPlan = planes[DropDownListPlan.SelectedIndex].ID;
                    PersonaLogic perlog = new PersonaLogic();
                    alumno.State = BusinessEntity.States.New;
                    perlog.Save(alumno);
                    this.LlenarCombo();
                    this.CleantxtBox();
                    this.Panel1.Visible = false;
                    this.LoadGrid();
                }
                if(this.formMode == formModes.Modificacion)
                {
                    PersonaLogic perlog = new PersonaLogic();
                    Personas alumno = perlog.GetOne(SelectedID); 
                    alumno.Apellido = txtApellido.Text;
                    alumno.Direccion = txtDireccion.Text;
                    alumno.Email = txtEmail.Text;
                    alumno.Legajo = Convert.ToInt32(txtLegajo.Text);
                    alumno.Nombre = txtNombre.Text;
                    alumno.Telefono = txtTelefono.Text;
                    alumno.FechaNacimiento = Calendar1.SelectedDate;
                    alumno.State = BusinessEntity.States.Modified;
                    
                    perlog.Save(alumno);
                    this.LlenarCombo();
                    this.CleantxtBox();
                    this.Panel1.Visible = false;
                    this.LoadGrid();
                    DropDownListPlan.Visible = true;
                    DropDownListPlan.Enabled = true;
                }
            }
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            if (SelectedID > 0)
            {
                formMode = formModes.Modificacion;
                Panel1.Visible = true;
                PersonaLogic perlog = new PersonaLogic();
                Entity = perlog.GetOne(SelectedID);
                txtApellido.Text = Entity.Apellido;
                txtDireccion.Text = Entity.Direccion;
                txtEmail.Text = Entity.Email;
                txtLegajo.Text = Convert.ToString(Entity.Legajo);
                txtNombre.Text = Entity.Nombre;
                txtTelefono.Text = Convert.ToString(Entity.Telefono);
                Calendar1.SelectedDate = Entity.FechaNacimiento;
                DropDownListPlan.Enabled = false;
                DropDownListPlan.Visible = false;
            }
        }
    }
}