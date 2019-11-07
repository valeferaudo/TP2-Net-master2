using System;
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
using UI.Desktop;
using Util;

namespace UI.Desktop
{
    public partial class AlumnoDesktop : UI.Desktop.Abm
   
    {
        PersonaLogic pl = new PersonaLogic();
        String modostr;
        
        public AlumnoDesktop()
        {
            InitializeComponent();
            
            
        }
        public AlumnoDesktop(ModoForm modo) : this()
        {
            ModoForm value = modo;
            modostr = modo.ToString();
            Settextobtnaceptar();
            listplanesfill();
        }
        public AlumnoDesktop(int idper, ModoForm modo) : this()
        {
            ModoForm value = modo;
            modostr = modo.ToString();
            
            AlumnoActual = pl.GetOne(idper);
            MapearDeDatos();
            Settextobtnaceptar();
            listplanesfill();
            

        }
        
        
        
        private Personas _AlumnoActual;
        public Personas AlumnoActual
        {
            get { return _AlumnoActual; }
            set { _AlumnoActual = value; }
        }

        public void MapearDeDatos() {

            

            this.txtID.Text = this.AlumnoActual.ID.ToString();
            
            this.txtNombre.Text = this.AlumnoActual.Nombre;
            this.txtApellido.Text = this.AlumnoActual.Apellido;
            this.txtDireccion.Text = this.AlumnoActual.Direccion;
            this.txtLegajo.Text = this.AlumnoActual.Legajo.ToString();
            this.txtTel.Text = this.AlumnoActual.Telefono;
            this.txtEmail.Text = this.AlumnoActual.Email;
            this.dtpFecha.Value = this.AlumnoActual.FechaNacimiento;
           
            




        }
        public virtual void MapearADatos() {
            

            if (modostr == "Alta")
            {
                AlumnoActual = new Personas();
                AlumnoActual.TipoPersona = Personas.tipopersona.Alumno;
                AlumnoActual.State = Personas.States.New;
            }
            if ((modostr == "Modificacion" ) || (modostr == "Alta"))
            {
                int nroplan;
                PlanLogic pllgc = new PlanLogic();
                List<Plan> planes = pllgc.GetAll();
                nroplan = planes[cmbPlan.SelectedIndex].ID;
                AlumnoActual.Nombre = txtNombre.Text;
                AlumnoActual.Apellido = txtApellido.Text;
                AlumnoActual.Email = txtEmail.Text;
                AlumnoActual.Telefono = txtTel.Text;
                AlumnoActual.Legajo = Int32.Parse(txtLegajo.Text);
                AlumnoActual.IDPlan = nroplan;
                AlumnoActual.FechaNacimiento = dtpFecha.Value;
                AlumnoActual.Direccion = txtDireccion.Text;


            }
            if (modostr == "Modificacion")
            {
                AlumnoActual.State = Usuario.States.Modified;
            }
           if (modostr == "Baja")
            {
               
                
                AlumnoActual.State = Usuario.States.Deleted;
                //ul.Delete(UsuarioActual.ID);
            }
            if (modostr == "Consulta")
            {

                AlumnoActual.State = Usuario.States.Unmodified;
            }
            
        }
        public virtual void GuardarCambios() {
            MapearADatos();
            
            
                pl.Save(AlumnoActual);
            

        }
        public virtual bool Validar() {
            bool ok=false;
            if (txtApellido.Text != "" && txtNombre.Text != "" && txtEmail.Text != "" && txtLegajo.Text != "" && txtTel.Text != "" && txtDireccion.Text != "" && cmbPlan.SelectedIndex != -1 )
            {
                if (ValidacionIngresoDatos.EsMail(txtEmail.Text))

                {
                    if (ValidacionIngresoDatos.EsNumero(txtTel.Text))
                    { ok = true; }
                    else
                    {
                        Notificar("Error", "El telefono es inválido", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                }
                else
                {
                    Notificar("Error", "El mail es inválido", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
            else
            {
                Notificar("Faltan datos", "Alguno de los campos obligatorios estan vacíos", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            return ok;
        }
        public void Notificar(string titulo, string mensaje, MessageBoxButtons botones, MessageBoxIcon icono)
        {
            MessageBox.Show(mensaje, titulo, botones, icono);
        }
        public void Notificar(string mensaje, MessageBoxButtons botones, MessageBoxIcon icono)
        {
            this.Notificar(this.Text, mensaje, botones, icono);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (Validar()){
                GuardarCambios();
                Close();
            }

        }
        private void listplanesfill()
        {
            PlanLogic pllgc = new PlanLogic();
            List<Plan> planes = pllgc.GetAll();
            foreach (Plan value in planes)
            {
                //PENDIENTE : AGREGAR ESPECIALIDAD DEL PLAN Y CONCATENAR
                cmbPlan.Items.Add(value.Descripcion);
            }
            if (AlumnoActual != null)
            {
                string plstr = pllgc.GetOne(AlumnoActual.IDPlan).Descripcion;
                cmbPlan.SelectedIndex = cmbPlan.FindStringExact(plstr);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void Settextobtnaceptar()
        {
            if (string.Equals(modostr, "Alta" ))
            {
                btnAceptar.Text = "Registrar";
            }
            if (string.Equals(modostr, "Modificacion"))
            {
                btnAceptar.Text = "Guardar";
            }
            if (modostr == "Baja")
            {
                btnAceptar.Text = "Eliminar";
                txtApellido.ReadOnly = true;
                txtNombre.ReadOnly = true;
                txtLegajo.ReadOnly = true;
                txtDireccion.ReadOnly = true;
                txtEmail.ReadOnly = true;
                txtTel.ReadOnly = true;
                cmbPlan.Enabled = false;
                dtpFecha.Enabled = false;
            }
            if (string.Equals(modostr, "Consulta"))
            {
                btnAceptar.Text = "Aceptar";
            }
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtFecha_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
