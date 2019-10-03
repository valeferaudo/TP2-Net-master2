using System;
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
    public partial class PlanesDesktop : UI.Desktop.Abm
    {
        public PlanesDesktop()
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
        private Plan _PlanActual;

        public Plan PlanActual
        {
            get { return _PlanActual; }
            set { _PlanActual = value; }
        }
        public PlanesDesktop(ModoForm modo) : this()
        {
            this.Modo = modo;
            this.SetCBPlan();
        }
        public PlanesDesktop(int ID, ModoForm modo) : this()
        {
            this.Modo = modo;
            PlanLogic pl = new PlanLogic();
            PlanActual = pl.GetOne(ID);
            MapearDeDatos();
            this.SetCBPlan();
        }
        public override void MapearDeDatos()

        {
            this.txtID.Text = this.PlanActual.ID.ToString();
            this.txtDescripcion.Text = this.PlanActual.Descripcion;
            
           

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
                        Plan pla = new Plan();
                        this.PlanActual = pla;
                        this.PlanActual.Descripcion = this.txtDescripcion.Text;
                        this.PlanActual.State = Business.Entities.Plan.States.New;
                        ComboboxItem cbi = (ComboboxItem)this.cbPlan.SelectedItem;
                        this.PlanActual.IDEspecialidad = cbi.Value;
                    }
                    break;
                case ModoForm.Modificacion:
                    {
                        this.PlanActual.Descripcion = this.txtDescripcion.Text;
                        this.PlanActual.State = Business.Entities.Plan.States.Modified;
                        ComboboxItem cbi = (ComboboxItem)this.cbPlan.SelectedItem;
                        this.PlanActual.IDEspecialidad = cbi.Value;
                    }
                    break;
                case ModoForm.Baja:
                    {
                        this.PlanActual.State = Business.Entities.Plan.States.Deleted;

                    }
                    break;
                case ModoForm.Consulta:
                    {
                        this.PlanActual.State = Business.Entities.Plan.States.Unmodified;
                    }
                    break;
                default:
                    break;
            }
        }
        public override void GuardarCambios()
        {
            this.MapearADatos();
            PlanLogic pl = new PlanLogic();
            pl.Save(PlanActual);
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
           
            if (!(this.txtDescripcion.Text != "" && cbPlan.SelectedIndex != -1))
            {
                Notificar("Error en llenado de campos", "Alguno de los campos se encuentra vacio", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        public void SetCBPlan()
        {
            EspecialidadLogic el = new EspecialidadLogic();
            List<Especialidad> especialidades = new List<Especialidad>();
            especialidades = el.GetAll();

            foreach (Especialidad espe in especialidades)
            {
                ComboboxItem item = new ComboboxItem();
                item.Text = espe.Descripcion;
                item.Value = espe.ID;

                cbPlan.Items.Add(item);
            }
            if (PlanActual != null)
            {
                string plstr = el.GetOne(PlanActual.IDEspecialidad).Descripcion;
                cbPlan.SelectedIndex = cbPlan.FindStringExact(plstr);
            }
        }
    }
}
