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
    public partial class PersonaDesktop : UI.Desktop.Abm
   
    {
        PersonaLogic pl = new PersonaLogic();
        String modostr;
        
        public PersonaDesktop()
        {
            InitializeComponent();
            
            
        }
        public PersonaDesktop(ModoForm modo) : this()
        {
            ModoForm value = modo;
            modostr = modo.ToString();
            Settextobtnaceptar();
            listplanesfill();
            CargarComboTipo();
        }
        public PersonaDesktop(int idper, ModoForm modo) : this()
        {
            ModoForm value = modo;
            modostr = modo.ToString();
            
            PersonaActual = pl.GetOne(idper);
            MapearDeDatos();
            Settextobtnaceptar();
            listplanesfill();
            CargarComboTipo();


        }


      
        private Personas _PersonaActual;
        public Personas PersonaActual
        {
            get { return _PersonaActual; }
            set { _PersonaActual = value; }
        }

        public void MapearDeDatos()
        {                    
            this.txtID.Text = this.PersonaActual.ID.ToString();
            this.cmbTipoPersona.SelectedValue = this.PersonaActual.TipoPersona;
            this.txtNombre.Text = this.PersonaActual.Nombre;
            this.txtApellido.Text = this.PersonaActual.Apellido;
            this.txtDireccion.Text = this.PersonaActual.Direccion;
            this.txtLegajo.Text = this.PersonaActual.Legajo.ToString();
            this.txtTel.Text = this.PersonaActual.Telefono;
            this.txtEmail.Text = this.PersonaActual.Email;
            this.dtpFecha.Value = this.PersonaActual.FechaNacimiento;
               
        }
        public virtual void MapearADatos() {
            

            if (modostr == "Alta")
            {
                PersonaActual = new Personas();
                
                PersonaActual.State = Personas.States.New;
            }
            if ((modostr == "Modificacion" ) || (modostr == "Alta"))
            {
                int nroplan;
                PlanLogic pllgc = new PlanLogic();
                List<Plan> planes = pllgc.GetAll();
                nroplan = planes[cmbPlan.SelectedIndex].ID;
                PersonaActual.Nombre = txtNombre.Text;
                PersonaActual.Apellido = txtApellido.Text;
                PersonaActual.Email = txtEmail.Text;
                PersonaActual.Telefono = txtTel.Text;
                PersonaActual.Legajo = Int32.Parse(txtLegajo.Text);
                PersonaActual.IDPlan = nroplan;
                PersonaActual.FechaNacimiento = dtpFecha.Value;
                PersonaActual.Direccion = txtDireccion.Text;

                if (cmbTipoPersona.SelectedValue.ToString() == "Alumno")
                { PersonaActual.TipoPersona = Personas.tipopersona.Alumno; }
                if (cmbTipoPersona.SelectedValue.ToString() == "Docente")
                { PersonaActual.TipoPersona = Personas.tipopersona.Docente; }
                if (cmbTipoPersona.SelectedValue.ToString() == "Admin")
                { PersonaActual.TipoPersona = Personas.tipopersona.Admin; }
            }
            if (modostr == "Modificacion")
            {
                PersonaActual.State = Usuario.States.Modified;
            }
           if (modostr == "Baja")
            {            
                PersonaActual.State = Usuario.States.Deleted;
                //ul.Delete(PersonaActual.ID);
            }
            if (modostr == "Consulta")
            {

                PersonaActual.State = Usuario.States.Unmodified;
            }
            
        }
        public virtual void GuardarCambios() {
            MapearADatos();

            try
            {
                pl.Save(PersonaActual);
            }
            catch(Exception e)
            {
                MessageBox.Show("No se puede eliminar esta persona porque otros registros dependen de esta");
            }

        }
        public override bool Validar()
        {
            
                if (txtApellido.Text != "" && txtNombre.Text != "" && txtEmail.Text != "" && txtLegajo.Text != "" 
                    && txtTel.Text != "" && txtDireccion.Text != "" 
                   && cmbPlan.SelectedIndex != -1 && cmbTipoPersona.SelectedIndex != -1)
                {
                    if (ValidacionIngresoDatos.EsMail(txtEmail.Text))

                    {
                        if (ValidacionIngresoDatos.EsNumero(txtTel.Text))
                        { return true; }
                        else
                        {
                            Notificar("Error", "El telefono es inválido", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            return false;
                        }
                    }
                    else
                    {
                        Notificar("Error", "El mail es inválido", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return false;
                    }
                }

                else
                {
                    Notificar("Faltan datos", "Alguno de los campos obligatorios estan vacíos", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return false;
                }
                     
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
            if (PersonaActual != null)
            {
                string plstr = pllgc.GetOne(PersonaActual.IDPlan).Descripcion;
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
          public void CargarComboTipo()
        {
            cmbTipoPersona.DataSource = Enum.GetValues(typeof(Personas.tipopersona));
            cmbTipoPersona.SelectedIndex = -1;
            
        }    
    }
}
