using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business.Logic;
using Business.Entities;
using System.Data;

namespace UI.Web
{
    public partial class Planes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
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
                //LLenarDropEspecialidades();
            }
            PersonaLogic pl = new PersonaLogic();
            Usuario usuario = (Usuario)Session["UsuarioSesion"];
            if (!(pl.GetOne(usuario.IDPersona).TipoPersona == Personas.tipopersona.Admin) && !(pl.GetOne(usuario.IDPersona).TipoPersona == Personas.tipopersona.Docente))
            {
                Response.Redirect("~/Default.aspx");
            }
        }
        PlanLogic _plan;
        private PlanLogic Plan
        {
            get
            {
                if (_plan == null)
                {
                    _plan = new PlanLogic();
                }
                return _plan;
            }
        }
        private void LoadGrid()
        {
            //this.gridViewPlan.DataSource = this.Plan.GetAll();
            //this.gridViewPlan.DataBind();
            PlanLogic pl = new PlanLogic();
            EspecialidadLogic el = new EspecialidadLogic();
            DataTable dt = new DataTable();

            if (dt.Columns.Count == 0)
            {
                dt.Columns.Add("ID", typeof(string));
                dt.Columns.Add("Descripcion", typeof(string));
                dt.Columns.Add("IDEspecialidad", typeof(string));
            }
            List<Plan> planes = pl.GetAll();
            foreach (Plan plan in planes)
            {
                DataRow NewRow = dt.NewRow();
                NewRow[0] = Convert.ToString(plan.ID);
                NewRow[1] = plan.Descripcion;
                NewRow[2] = el.GetOne(plan.IDEspecialidad).Descripcion;
                dt.Rows.Add(NewRow);
            }
            this.gridViewPlan.DataSource = dt;
            this.gridViewPlan.DataBind();


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
        private Plan Entity
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
            this.SelectedID = (int)Convert.ToInt32(this.gridViewPlan.SelectedRow.Cells[0].Text);
            this.ddlEspecialidades.Items.Clear();
            this.formPanel.Visible = false;
            this.ClearForm();
        }
        private void LoadForm(int ID)
        {
            this.Entity = this.Plan.GetOne(ID);
            this.descripcionTextBox.Text = this.Entity.Descripcion;
            this.ddlEspecialidades.SelectedValue = (this.Entity.IDEspecialidad).ToString();
        }

        protected void editarLinkButton_Click(object sender, EventArgs e)
        {
            if (this.IsEntitySelected)
            {
                LLenarDropEspecialidades();
                this.formPanel.Visible = true;
                this.formMode = formModes.Modificacion;
                this.EnableForm(true);
                this.LoadForm(this.SelectedID);
            }
        }
        private void LoadEntity(Plan plan)
        {
            plan.Descripcion = this.descripcionTextBox.Text;
            plan.IDEspecialidad = Int32.Parse(this.ddlEspecialidades.SelectedValue);

        }
        private void SaveEntity(Plan plan)
        {
            this.Plan.Save(plan);
        }

        protected void aceptarLinkButton_Click(object sender, EventArgs e)
        {
            this.ddlEspecialidades.Items.Clear();
            switch (this.formMode)
            {
                case formModes.Alta:
                    {
                        this.Entity = new Plan();
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
                        this.DeleteEntity(this.SelectedID);
                        this.LoadGrid();
                    }
                    break;
                case formModes.Modificacion:
                    {
                        this.Entity = new Plan();
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

            this.formPanel.Visible = false;
        }
        private void EnableForm(bool enable)
        {
            this.descripcionTextBox.Enabled = enable;
            this.descripcionLabel.Visible = enable;
            this.ddlEspecialidades.Enabled = enable;
            this.idEspecialidadLabel.Visible = enable;

        }

        protected void eliminarLinkButton_Click(object sender, EventArgs e)
        {
            if (this.IsEntitySelected)
            {
                this.formPanel.Visible = true;
                this.formMode = formModes.Baja;
                this.EnableForm(false);
                this.LoadForm(this.SelectedID);
            }
        }
        private void DeleteEntity(int ID)
        {
            this.Plan.Delete(ID);
        }

        protected void nuevoLinkButton_Click(object sender, EventArgs e)
        {
            LLenarDropEspecialidades();
            this.formPanel.Visible = true;
            this.formMode = formModes.Alta;
            this.ClearForm();
            this.EnableForm(true);
        }
        private void ClearForm()
        {
            this.descripcionTextBox.Text = string.Empty;

        }

        protected void cancelarLinkButton_Click(object sender, EventArgs e)
        {
            this.ddlEspecialidades.Items.Clear();
            this.formPanel.Visible = false;
            this.ClearForm();
        }

        public void LLenarDropEspecialidades()
        {
            EspecialidadLogic el = new EspecialidadLogic();
            List<Especialidad> especialidades = el.GetAll();
            foreach (Especialidad espe in especialidades)
            {
                ListItem item = new ListItem();
                item.Text = espe.Descripcion;
                item.Value = Convert.ToString(espe.ID);

                ddlEspecialidades.Items.Add(item);
            }
        }

    }
}