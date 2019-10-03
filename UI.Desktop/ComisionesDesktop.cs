﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Business.Entities;
using Business.Logic;

namespace UI.Desktop
{
    public partial class ComisionesDesktop : UI.Desktop.Abm
    {
        public ComisionesDesktop()
        {
            InitializeComponent();
        }
        public void Notificar(string titulo, string mensaje, MessageBoxButtons botones, MessageBoxIcon icono)
        {
            MessageBox.Show(mensaje, titulo, botones, icono);
        }
        public void Notificar(string mensaje, MessageBoxButtons botones, MessageBoxIcon icono)
        {
            this.Notificar(this.Text, mensaje, botones, icono);
        }
        private Comision _ComisionActual;

        public Comision ComisionActual
        {
            get { return _ComisionActual; }
            set { _ComisionActual = value; }
        }
        public ComisionesDesktop(ModoForm modo) : this()
        {
            this.Modo = modo;
            this.SetCBComision();
        }
        public ComisionesDesktop(int ID, ModoForm modo) : this()
        {
            this.SetCBComision();
            this.Modo = modo;
            ComisionLogic cl = new ComisionLogic();
            ComisionActual = cl.GetOne(ID);
            MapearDeDatos();
        }
        public override void MapearDeDatos()

        {
            this.txtIDComision.Text = this.ComisionActual.ID.ToString();
            this.txtDescripcion.Text = this.ComisionActual.Descripcion;
            this.txtAnioEspe.Text = this.ComisionActual.AnioEspecialidad.ToString();
            //this.cbIDPlan.SelectedItem = this.ComisionActual.IDPlan.ToString();

            switch (Modo)
            {
                case ModoForm.Alta:
                    {
                        this.btnAceptar.Text = "Guardar";
                    }
                    break;
                case ModoForm.Baja:
                    {
                        this.btnAceptar.Text = "Eliminar";
                    }
                    break;
                case ModoForm.Modificacion:
                    {
                        this.btnAceptar.Text = "Guardar";
                    }
                    break;
                case ModoForm.Consulta:
                    {
                        this.btnAceptar.Text = "Aceptar";
                    }
                    break;
                default:
                    break;
            }
        }
        public override void MapearADatos()
        {
            switch (Modo)
            {
                case ModoForm.Alta:
                    {
                        Comision comi = new Comision();
                        this.ComisionActual = comi;
                        this.ComisionActual.Descripcion = this.txtDescripcion.Text;
                        this.ComisionActual.State = Business.Entities.Plan.States.New;
                        this.ComisionActual.AnioEspecialidad = Int32.Parse(this.txtAnioEspe.Text);
                        ComboboxItem cbi = (ComboboxItem)this.cbIDPlan.SelectedItem;
                        this.ComisionActual.IDPlan = cbi.Value;
                    }
                    break;
                case ModoForm.Modificacion:
                    {

                        this.ComisionActual.Descripcion = this.txtDescripcion.Text;
                        this.ComisionActual.AnioEspecialidad = Int32.Parse(this.txtAnioEspe.Text);
                        this.ComisionActual.State = Business.Entities.Plan.States.Modified;
                        ComboboxItem cbi = (ComboboxItem)this.cbIDPlan.SelectedItem;
                        this.ComisionActual.IDPlan = cbi.Value;
                    }
                    break;
                case ModoForm.Baja:
                    {
                        this.ComisionActual.State = Business.Entities.Plan.States.Deleted;

                    }
                    break;
                case ModoForm.Consulta:
                    {
                        this.ComisionActual.State = Business.Entities.Plan.States.Unmodified;
                    }
                    break;
                default:
                    break;
            }
        }
        public override void GuardarCambios()
        {
            this.MapearADatos();
            ComisionLogic cl = new ComisionLogic();
            cl.Save(ComisionActual);
        }


        public override bool Validar()
        {
            //COMPROBAR QUE SE LLENE EL CAMPO IDPLAN
            if (!(this.txtDescripcion.Text != "" && this.txtAnioEspe.Text != ""  && cbIDPlan.SelectedIndex != -1))
            {
                Notificar("Error en llenado de campos", "Alguno de los campos se encuentra vacio", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        public void SetCBComision()
        {
            PlanLogic pl = new PlanLogic();
            List<Plan> planes = new List<Plan>();
            planes = pl.GetAll();
            foreach (Plan plan in planes)
            {
                ComboboxItem item = new ComboboxItem();
                item.Text = plan.Descripcion;
                item.Value = plan.ID;

                cbIDPlan.Items.Add(item);
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (Validar())
            {
                this.GuardarCambios();
                this.Close();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}