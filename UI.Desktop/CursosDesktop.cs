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
using Util;

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
            this.SetCBMateria();
            cbComision.Enabled = false;
            
        }
        public CursosDesktop(int ID, ModoForm modo) : this()
        {
            this.Modo = modo;
            CursoLogic cl = new CursoLogic();
            CursoActual = cl.GetOne(ID);
            MapearDeDatos();
            SetCBMateria();
            if (modo == ModoForm.Alta || modo == ModoForm.Modificacion)
            {
                cbComision.Enabled = true;
            }
                MateriaLogic ml = new MateriaLogic();
            SetCBComision(CursoActual.IDMateria);
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
                        txtAnio.ReadOnly = true;
                        txtCupo.ReadOnly = true;
                        txtID.ReadOnly = true;
                        cbComision.Enabled = false;
                        cbMateria.Enabled = false;
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
                        this.CursoActual.IDComision = Convert.ToInt32(cbComision.SelectedValue);
                        this.CursoActual.IDMateria = Convert.ToInt32(cbMateria.SelectedValue);
                        this.CursoActual.State = Business.Entities.Curso.States.New;
                    }
                    break;
                case ModoForm.Modificacion:
                    {
                        this.CursoActual.AnioCalendario = int.Parse(this.txtAnio.Text);
                        this.CursoActual.Cupo = int.Parse(this.txtCupo.Text);
                        this.CursoActual.IDComision = Convert.ToInt32(cbComision.SelectedValue);
                        this.CursoActual.IDMateria = Convert.ToInt32(cbMateria.SelectedValue);
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
            try
            {
                cl.Save(CursoActual);
            }
            catch(Exception e)
            {
                MessageBox.Show("No se puede eliminar el Curso porque ya hay Alumnos y/o Docentes inscriptos");
            }
        }
        public void SetCBComision(int id)
        {
            ComisionLogic cl = new ComisionLogic();
            List<Comision> comisiones = new List<Comision>();
            MateriaLogic ml = new MateriaLogic();
            cbComision.DataSource = cl.TraerPorPlan(ml.GetOne(id).IDPlan);
            cbComision.DisplayMember = "Descripcion";
            cbComision.ValueMember = "ID";
            cbComision.SelectedIndex = -1;
            if (CursoActual != null)
            {
                
             string plstr = cl.GetOne(CursoActual.IDComision).Descripcion;
              cbComision.SelectedIndex = cbComision.FindStringExact(plstr);
            }               
        }
        public void SetCBMateria()
        {
            MateriaLogic ml = new MateriaLogic();
            List<Materia> materias = new List<Materia>();
            if (CursoActual != null)
            {
                cbMateria.DataSource = ml.TraerPorPlan((ml.GetOne(CursoActual.IDMateria)).IDPlan);
                cbMateria.DisplayMember = "Descripcion";
                cbMateria.ValueMember = "ID";
                string plstr = ml.GetOne(CursoActual.IDMateria).Descripcion;
                cbMateria.SelectedIndex = cbMateria.FindStringExact(plstr);
            }
                       
            else
            {
                cbMateria.DataSource = ml.GetAll();
                cbMateria.DisplayMember = "Descripcion";
                cbMateria.ValueMember = "ID";
                cbMateria.SelectedIndex = -1;
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
            if (!(Modo == ModoForm.Baja))
            {
                if ((this.txtCupo.Text != "" && this.txtAnio.Text != "" && cbComision.SelectedIndex != -1 && cbMateria.SelectedIndex != -1))
                {
                    if (ValidacionIngresoDatos.EsNumero(txtAnio.Text))
                    {
                        if (ValidacionIngresoDatos.EsNumero(txtCupo.Text))
                        {
                            return true;
                        }
                        else
                        {
                            Notificar("Error en llenado de campos", "Ingrese un numero en Cupo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }
                    }
                    else
                    {
                        Notificar("Error en llenado de campos", "Ingrese un numero en Año Especialidad", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
                else
                {
                    Notificar("Error en llenado de campos", "Alguno de los campos se encuentra vacio", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            else
            {
                if ((this.txtCupo.Text != "" && this.txtAnio.Text != "" ))
                {
                    if (ValidacionIngresoDatos.EsNumero(txtAnio.Text))
                    {
                        if (ValidacionIngresoDatos.EsNumero(txtCupo.Text))
                        {
                            return true;
                        }
                        else
                        {
                            Notificar("Error en llenado de campos", "Ingrese un numero en Cupo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }
                    }
                    else
                    {
                        Notificar("Error en llenado de campos", "Ingrese un numero en Año Especialidad", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
                else
                {
                    Notificar("Error en llenado de campos", "Alguno de los campos se encuentra vacio", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }
        private void Cancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        

        private void cbMateria_SelectionChangeCommitted(object sender, EventArgs e)
        {
            int mat = Convert.ToInt32(cbMateria.SelectedValue);
            SetCBComision(mat);
            cbComision.Enabled = true;
        }

        
    }
}
