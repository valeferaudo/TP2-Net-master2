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

namespace UI.Desktop
{
    public partial class UsuarioDesktop : UI.Desktop.Abm
   
    {
        UsuarioLogic ul = new UsuarioLogic();
        String modostr;
        public UsuarioDesktop()
        {
            InitializeComponent();
            
            
        }
        public UsuarioDesktop(ModoForm modo) : this()
        {
            ModoForm value = modo;
            modostr = modo.ToString();
            Settextobtnaceptar();
            LlenarListaPersonas();
        }
        public UsuarioDesktop(int idus, ModoForm modo) : this()
        {
            ModoForm value = modo;
            modostr = modo.ToString();
            
            UsuarioActual = ul.GetOne(idus);
            MapearDeDatos();
            Settextobtnaceptar();
            LlenarListaPersonas();
            

        }
        private Usuario _UsuarioActual;
        public Usuario UsuarioActual
        {
            get { return _UsuarioActual; }
            set { _UsuarioActual = value; }
        }

        public override void MapearDeDatos() {
            this.txtID.Text = this.UsuarioActual.ID.ToString();
            this.chkHabilitado.Checked = this.UsuarioActual.Habilitado;
            this.txtNombre.Text = this.UsuarioActual.Nombre;
            this.txtApellido.Text = this.UsuarioActual.Apellido;
            this.txtClave.Text = this.UsuarioActual.Clave;
            this.txtConfirmarClave.Text = this.UsuarioActual.Clave;
            this.txtEmail.Text = this.UsuarioActual.EMail;
            this.txtUsuario.Text = this.UsuarioActual.NombreUsuario;
            




        }
        public override void MapearADatos() {
            if (modostr == "Alta")
            {
                UsuarioActual = new Usuario();
                UsuarioActual.State = Usuario.States.New;
            }
            if ((modostr == "Modificacion" ) || (modostr == "Alta"))
            {
                PersonaLogic pl = new PersonaLogic();
                List<Personas> personas = pl.GetAll();
                UsuarioActual.Habilitado = chkHabilitado.Checked;
                UsuarioActual.Nombre = txtNombre.Text;
                UsuarioActual.Apellido = txtApellido.Text;
                UsuarioActual.EMail = txtEmail.Text;
                UsuarioActual.NombreUsuario = txtUsuario.Text;
                UsuarioActual.Clave = txtClave.Text;
                UsuarioActual.IDPersona = personas[cmbPersonas.SelectedIndex].ID;

                

            }
            if (modostr == "Modificacion")
            {
                UsuarioActual.State = Usuario.States.Modified;
            }
           if (modostr == "Baja")
            {
               
                
                UsuarioActual.State = Usuario.States.Deleted;
                //ul.Delete(UsuarioActual.ID);
            }
            if (modostr == "Consulta")
            {

                UsuarioActual.State = Usuario.States.Unmodified;
            }
            
        }
        public virtual void GuardarCambios() {
            MapearADatos();
            
            
                ul.Save(UsuarioActual);
            

        }
        public virtual bool Validar() {
            bool ok=false;
            if (txtApellido.Text != "" && txtNombre.Text != "" && txtUsuario.Text != "" && txtEmail.Text != "" && txtClave.Text != "" && cmbPersonas.SelectedIndex != -1)
            {
                if(txtClave.TextLength > 7)
                {
                    if(txtClave.Text == txtConfirmarClave.Text)
                    {
                        
                        if ((txtEmail.Text.Contains("@")) && (txtEmail.Text.Contains(".")))
                        {
                            ok = true;
                        
                        }
                        else
                        {
                            Notificar("Error", "Mail invalido", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        Notificar("Error", "Las claves no coinciden", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    Notificar("Clave insegura", "La clave debe tener al menos 8 caracteres", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                txtClave.ReadOnly = true;
                txtConfirmarClave.ReadOnly = true;
                txtEmail.ReadOnly = true;
                txtUsuario.ReadOnly = true;
                cmbPersonas.Enabled = false;
                
            }
            if (string.Equals(modostr, "Consulta"))
            {
                btnAceptar.Text = "Aceptar";
            }
        }

        private void LlenarListaPersonas()
        {
            PersonaLogic pl = new PersonaLogic();
            List<Personas> personas = pl.GetAll();
            foreach(Personas value in personas)
            {
                cmbPersonas.Items.Add(value.Nombre + " " + value.Apellido + " " + Convert.ToString(value.Legajo) );
            }
            if (UsuarioActual != null)
            {
                string perstr = pl.GetOne(UsuarioActual.IDPersona).Nombre + " " + pl.GetOne(UsuarioActual.IDPersona).Apellido + " " + Convert.ToString(pl.GetOne(UsuarioActual.IDPersona).Legajo);
                cmbPersonas.SelectedIndex = cmbPersonas.FindStringExact(perstr);
            }
        }

        private void cmbPersonas_SelectedIndexChanged(object sender, EventArgs e)
        {
            PersonaLogic pl = new PersonaLogic();
            List<Personas> personas = pl.GetAll();
            txtApellido.Text = personas[cmbPersonas.SelectedIndex].Apellido;
            txtNombre.Text = personas[cmbPersonas.SelectedIndex].Nombre;
            txtEmail.Text = personas[cmbPersonas.SelectedIndex].Email;
        }
    }
}
