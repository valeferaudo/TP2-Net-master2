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
    public partial class Comisiones : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Page.IsPostBack)
            {
                this.LoadGrid();
            }
            else
            {
                this.LoadGrid();
            }
            this.LlenarDropPlanes();
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
            this.gridViewComisiones.DataSource = this.Comision.GetAll();
            this.gridViewComisiones.DataBind();
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
            this.SelectedID = (int)this.gridViewComisiones.SelectedValue;
        }
        private void LoadForm(int ID)
        {
            this.Entity = this.Comision.GetOne(ID);
            this.descripcionTextBox.Text = this.Entity.Descripcion;
            this.anioEspecialidadTextBox.Text = (this.Entity.AnioEspecialidad).ToString();
            this.DropDownList1.SelectedValue = (this.Entity.IDPlan).ToString();
         
        }

        protected void editarLinkButton_Click(object sender, EventArgs e)
        {
            if (this.IsEntitySelected)
            {
                this.formPanel.Visible = true;
                this.formMode = formModes.Modificacion;
                this.EnableForm(true);
                this.LoadForm(this.SelectedID);
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
            this.Comision.Save(comision);
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
                        this.DeleteEntity(this.SelectedID);
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
            this.anioEspecialidadTextBox.Enabled = enable;
            this.DropDownList1.Enabled = enable;
            this.descripcionLabel.Visible = enable;
            this.anioEspecialidadLabel.Enabled = enable;
            this.idPlanLabel.Visible = enable;
            
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
            this.Comision.Delete(ID);
        }

        protected void nuevoLinkButton_Click(object sender, EventArgs e)
        {
            this.formPanel.Visible = true;
            this.formMode = formModes.Alta;
            this.ClearForm();
            this.EnableForm(true);
        }
        private void ClearForm()
        {
            this.descripcionTextBox.Text = string.Empty;
            this.anioEspecialidadTextBox.Text = string.Empty;
            
            
        }

        protected void cancelarLinkButton_Click(object sender, EventArgs e)
        {
            this.formPanel.Visible = false;
            this.ClearForm();
        }
        private void LlenarDropPlanes()
        {
            PlanLogic pl = new PlanLogic();
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
}