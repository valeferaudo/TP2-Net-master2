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
    public partial class CursosDesktop : UI.Desktop.Abm
    {
        public CursosDesktop()
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
        private Curso _CursoActual;

        public Curso CursoActual
        {
            get { return _CursoActual; }
            set { _CursoActual = value; }
        }
        public CursosDesktop(ModoForm modo) : this()
        {
            this.Modo = modo;
            this.SetCBComision();
            this.SetCBMateria();
        }
        public CursosDesktop(int ID, ModoForm modo) : this()
        {
            this.SetCBComision();
            this.SetCBMateria();
            this.Modo = modo;
            CursoLogic cl = new CursoLogic();
            CursoActual = cl.GetOne(ID);
            MapearDeDatos();
        }
        public override void MapearDeDatos()

        {
            this.txtID.Text = this.CursoActual.ID.ToString();
            this.txtAnio.Text = this.CursoActual.AnioCalendario.ToString();
            this.txtCupo.Text = this.CursoActual.Cupo.ToString();

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
                        Curso cur = new Curso();
                        this.CursoActual = cur;
                        this.CursoActual.AnioCalendario = int.Parse(this.txtAnio.Text);
                        this.CursoActual.Cupo = int.Parse(this.txtCupo.Text);
                        ComboboxItem cbc = (ComboboxItem)this.cbComision.SelectedItem;
                        this.CursoActual.IDComision = cbc.Value;
                        ComboboxItem cbm = (ComboboxItem)this.cbMateria.SelectedItem;
                        this.CursoActual.IDMateria = cbm.Value;
                        this.CursoActual.State = Business.Entities.Curso.States.New;
                    }
                    break;
                case ModoForm.Modificacion:
                    {
                        this.CursoActual.AnioCalendario = int.Parse(this.txtAnio.Text);
                        this.CursoActual.Cupo = int.Parse(this.txtCupo.Text);
                        ComboboxItem cbc = (ComboboxItem)this.cbComision.SelectedItem;
                        this.CursoActual.IDComision = cbc.Value;
                        ComboboxItem cbm = (ComboboxItem)this.cbMateria.SelectedItem;
                        this.CursoActual.IDMateria = cbm.Value;
                        this.CursoActual.State = Business.Entities.Curso.States.Modified;

                    }
                    break;
                case ModoForm.Baja:
                    {
                        this.CursoActual.State = Business.Entities.Curso.States.Deleted;

                    }
                    break;
                case ModoForm.Consulta:
                    {
                        this.CursoActual.State = Business.Entities.Curso.States.Unmodified;
                    }
                    break;
                default:
                    break;
            }
        }
        public override void GuardarCambios()
        {
            this.MapearADatos();
            CursoLogic cl = new CursoLogic();
            cl.Save(CursoActual);
        }
        public void SetCBComision()
        {
            ComisionLogic cl = new ComisionLogic();
            List<Comision> comisiones = new List<Comision>();
            comisiones = cl.GetAll();

            foreach (Comision comi in comisiones)
            {
                ComboboxItem item = new ComboboxItem();
                item.Text = comi.Descripcion;
                item.Value = comi.ID;

                cbComision.Items.Add(item);
            }
        }
        public void SetCBMateria()
        {
            MateriaLogic ml = new MateriaLogic();
            List<Materia> materias = new List<Materia>();
            materias = ml.GetAll();

            foreach (Materia mat in materias)
            {
                ComboboxItem item = new ComboboxItem();
                item.Text = mat.Descripcion;
                item.Value = mat.ID;

                cbMateria.Items.Add(item);
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
        public override bool Validar()
        {
           
            if (!(this.txtCupo.Text != "" && this.txtAnio.Text != "" && cbComision.SelectedIndex != -1 && cbMateria.SelectedIndex != -1))
            {
                Notificar("Error en llenado de campos", "Alguno de los campos se encuentra vacio", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        private void Cancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
