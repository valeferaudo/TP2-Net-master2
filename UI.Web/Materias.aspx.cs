using System;
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
    public partial class Materias : System.Web.UI.Page
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
                
            }
            PersonaLogic pl = new PersonaLogic();
            Usuario usuario = (Usuario)Session["UsuarioSesion"];
            if (!(pl.GetOne(usuario.IDPersona).TipoPersona == Personas.tipopersona.Admin) && !(pl.GetOne(usuario.IDPersona).TipoPersona == Personas.tipopersona.Docente))
            {
                Response.Redirect("~/Default.aspx");
            }
            if (!IsPostBack)
            {
                this.LlenarDropMateria();
            }
        }
        private MateriaLogic _Materia;

        public MateriaLogic Materia

        {
            get
            {
                if (_Materia == null)
                {
                    _Materia = new MateriaLogic();
                }
                return _Materia;
            }
            set { _Materia = value; }
        }
        private void LoadGrid()
        {
            //this.gridViewMaterias.DataSource = this.Materia.GetAll();
            //this.gridViewMaterias.DataBind();
            PlanLogic pl = new PlanLogic();
            DataTable dt = new DataTable();

            if (dt.Columns.Count == 0)
            {
                dt.Columns.Add("ID", typeof(string));
                dt.Columns.Add("Descripcion", typeof(string));
                dt.Columns.Add("HSSemanales", typeof(string));
                dt.Columns.Add("HSTotales", typeof(string));
                dt.Columns.Add("IDPlan", typeof(string));
            }
            List<Materia> materias = this.Materia.GetAll();
            foreach (Materia materia in materias)
            {
                DataRow NewRow = dt.NewRow();
                NewRow[0] = Convert.ToString(materia.ID);
                NewRow[1] = materia.Descripcion;
                NewRow[2] = Convert.ToString(materia.HSSemanales);
                NewRow[3] = Convert.ToString(materia.HSTotales);
                NewRow[4] = pl.GetOne(materia.IDPlan).Descripcion;
                dt.Rows.Add(NewRow);
            }
            this.gridViewMaterias.DataSource = dt;
            this.gridViewMaterias.DataBind();
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
        private Materia Entity
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
            this.SelectedID = (int)Convert.ToInt32(this.gridViewMaterias.SelectedRow.Cells[0].Text);
            MostrarBotones();
            this.formPanel.Visible = false;
            this.ClearForm();
        }
        private void LoadForm(int ID)
        {
            this.Entity = this.Materia.GetOne(ID);
            this.descripcionTextBox.Text = this.Entity.Descripcion;
            this.hsSemanalesTextBox.Text = (this.Entity.HSSemanales).ToString();
            this.hsTotalesTextBox.Text = (this.Entity.HSTotales).ToString();
            PlanLogic pl = new PlanLogic();
            List<Plan> planes = pl.GetAll();
            Plan plan = pl.GetOne(Entity.IDPlan);
            bool contiene = false;//Verificar que no este borrado logico, si esta borrado, no setear dropdown
            foreach (Plan pla in planes)
            {
                if (pla.ID == plan.ID)
                {
                    contiene = true;
                }
            }
            if (contiene)
            {
                this.ddlPlanes.SelectedValue = (this.Entity.IDPlan).ToString();
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
        private void LoadEntity(Materia materia)
        {
            materia.Descripcion = this.descripcionTextBox.Text;
            materia.HSSemanales = Int32.Parse(this.hsSemanalesTextBox.Text);
            materia.HSTotales = Int32.Parse(this.hsTotalesTextBox.Text);
            materia.IDPlan = Int32.Parse(this.ddlPlanes.SelectedValue);

        }

        private void SaveEntity(Materia materia)
        {
            this.Materia.Save(materia);
        }

        protected void aceptarLinkButton_Click(object sender, EventArgs e)
        {
            
            switch (this.formMode)
            {
                case formModes.Alta:
                    {
                        this.Entity = new Materia();
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
                        this.DeleteEntity(this.SelectedID);
                        this.LoadGrid();
                    }
                    break;
                case formModes.Modificacion:
                    {
                        this.Entity = new Materia();
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
            this.hsSemanalesTextBox.Enabled = enable;
            this.hsTotalesTextBox.Enabled = enable;
            this.ddlPlanes.Enabled = enable;
            this.descripcionLabel.Visible = enable;
            this.hsSemanalesLabel.Enabled = enable;
            this.hsTotalesLabel.Visible = enable;
            this.idPlanLabel.Visible = enable;
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
            this.Materia.Delete(ID);
        }

        protected void nuevoLinkButton_Click(object sender, EventArgs e)
        {
            OcultarBotones();
            this.formPanel.Visible = true;
            this.formMode = formModes.Alta;
            this.ClearForm();
            this.EnableForm(true);
            this.gridViewMaterias.SelectedIndex = -1;
        }
        private void ClearForm()
        {
            this.descripcionTextBox.Text = string.Empty;
            this.hsSemanalesTextBox.Text = string.Empty;
            this.hsTotalesTextBox.Text = string.Empty;
        }

        protected void cancelarLinkButton_Click(object sender, EventArgs e)
        {
            MostrarBotones();
            this.formPanel.Visible = false;
            this.ClearForm();
        }

        public void LlenarDropMateria()
        {
            ddlPlanes.Items.Clear();
            PlanLogic pl = new PlanLogic();
            List<Plan> planes = pl.GetAll();
            foreach (Plan plan in planes)
            {
                ListItem item = new ListItem();
                item.Text = plan.Descripcion;
                item.Value = Convert.ToString(plan.ID);

                ddlPlanes.Items.Add(item);
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