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
    public partial class EspecialidadesDesktop : UI.Desktop.Abm
    {
        public EspecialidadesDesktop()
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
        private Especialidad _EspecialidadActual;

        public Especialidad EspecialidadActual
        {
            get { return _EspecialidadActual; }
            set { _EspecialidadActual = value; }
        }
        public EspecialidadesDesktop(ModoForm modo) : this()
        {
            this.Modo = modo;
        }
        public EspecialidadesDesktop(int ID, ModoForm modo) : this()
        {
            this.Modo = modo;
            EspecialidadLogic el = new EspecialidadLogic();
            EspecialidadActual = el.GetOne(ID);
            MapearDeDatos();
        }
        public override void MapearDeDatos()

        {
            this.txtID.Text = this.EspecialidadActual.ID.ToString();
            this.txtDescripcion.Text = this.EspecialidadActual.Descripcion;

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
                        Especialidad esp = new Especialidad();
                        this.EspecialidadActual = esp;
                        this.EspecialidadActual.Descripcion = this.txtDescripcion.Text;
                        this.EspecialidadActual.State = Business.Entities.Especialidad.States.New;
                    }
                    break;
                case ModoForm.Modificacion:
                    {
                        this.EspecialidadActual.Descripcion = this.txtDescripcion.Text;
                        this.EspecialidadActual.State = Business.Entities.Especialidad.States.Modified;
                    }
                    break;
                case ModoForm.Baja:
                    {
                        this.EspecialidadActual.State = Business.Entities.Especialidad.States.Deleted;
                    }
                    break;
                case ModoForm.Consulta:
                    {
                        this.EspecialidadActual.State = Business.Entities.Especialidad.States.Unmodified;
                    }
                    break;
                default:
                    break;
            }
        }
        public override void GuardarCambios()
        {
            this.MapearADatos();
            EspecialidadLogic el = new EspecialidadLogic();
            try
            {
                el.Save(EspecialidadActual);
            }
            catch(Exception e)
            {
                MessageBox.Show("No se puede eliminar la especialidad porque ya existen registros que dependen de esta");
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
        public override bool Validar()
        {
            if (!(this.txtDescripcion.Text != ""))
            {
                Notificar("Error en llenado de campos", "Alguno de los campos se encuentra vacio", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
    }
}
