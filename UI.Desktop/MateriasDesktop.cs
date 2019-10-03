﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Business.Logic;
using Business.Entities;

namespace UI.Desktop
{
    public partial class MateriasDesktop : UI.Desktop.Abm
    {
        public MateriasDesktop()
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
        private Materia _MateriaActual;

        public Materia MateriaActual
        {
            get { return _MateriaActual; }
            set { _MateriaActual = value; }
        }
        public MateriasDesktop(ModoForm modo) : this()
        {
            this.Modo = modo;
            this.SetCBMateria();
        }
        public MateriasDesktop(int ID, ModoForm modo) : this()
        {
            this.SetCBMateria();
            this.Modo = modo;
            MateriaLogic ml = new MateriaLogic();
            MateriaActual = ml.GetOne(ID);
            MapearDeDatos();
        }
        public override void MapearDeDatos()

        {
            this.txtID.Text = this.MateriaActual.ID.ToString();
            this.txtDescripcion.Text = this.MateriaActual.Descripcion;
            this.txtHsSemanales.Text = this.MateriaActual.HSSemanales.ToString();
            this.txtHsTotales.Text = this.MateriaActual.HSTotales.ToString();


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
                        Materia mat = new Materia();
                        this.MateriaActual = mat;
                        this.MateriaActual.Descripcion = this.txtDescripcion.Text;
                        this.MateriaActual.HSSemanales = int.Parse(this.txtHsSemanales.Text);
                        this.MateriaActual.HSTotales = int.Parse(this.txtHsTotales.Text);
                        ComboboxItem cbi = (ComboboxItem)this.cbDescPlan.SelectedItem;
                        this.MateriaActual.IDPlan = cbi.Value;
                        this.MateriaActual.State = Business.Entities.Materia.States.New;
                    }
                    break;
                case ModoForm.Modificacion:
                    {
                        this.MateriaActual.Descripcion = this.txtDescripcion.Text;
                        this.MateriaActual.HSSemanales = int.Parse(this.txtHsSemanales.Text);
                        this.MateriaActual.HSTotales = int.Parse(this.txtHsTotales.Text);
                        ComboboxItem cbi = (ComboboxItem)this.cbDescPlan.SelectedItem;
                        this.MateriaActual.IDPlan = cbi.Value;
                        this.MateriaActual.State = Business.Entities.Materia.States.Modified;
                    }
                    break;
                case ModoForm.Baja:
                    {
                        this.MateriaActual.State = Business.Entities.Materia.States.Deleted;

                    }
                    break;
                case ModoForm.Consulta:
                    {
                        this.MateriaActual.State = Business.Entities.Materia.States.Unmodified;
                    }
                    break;
                default:
                    break;
            }
        }
        public override void GuardarCambios()
        {
            this.MapearADatos();
            MateriaLogic ml = new MateriaLogic();
            ml.Save(MateriaActual);
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

        public override bool Validar()
        {
            
            if (!(this.txtDescripcion.Text != "" || this.txtHsSemanales.Text != "" || this.txtHsTotales.Text != "" || cbDescPlan.SelectedIndex != -1))
            {
                Notificar("Error en llenado de campos", "Alguno de los campos se encuentra vacio", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        public void SetCBMateria()
        {
            PlanLogic pl = new PlanLogic();
            List<Plan> planes = new List<Plan>();
            planes = pl.GetAll();

            foreach (Plan espe in planes)
            {
                ComboboxItem item = new ComboboxItem();
                item.Text = espe.Descripcion;
                item.Value = espe.ID;

                cbDescPlan.Items.Add(item);
            }
        }
    }
}