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
    public partial class formLogin : Form
    {
        public Usuario uslogeado;
        public formLogin()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            try
            {
                UsuarioLogic ul = new UsuarioLogic();
                Usuario uslog = new Usuario();
                uslog = ul.Logearse(txtUsuario.Text, txtPass.Text);
                if(uslog.ID > 1)
                {
                    uslogeado = uslog;
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    MessageBox.Show("Credenciales invalidas","Error");
                }
            }
            catch(NullReferenceException ex) 
            {
                MessageBox.Show("Credenciales invalidas (EXCEPCION)");
            }
        }

        private void lnkOlvidaPass_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Intente hacer memoria", "Olvidé mi contraseña",
            MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

        }

        private void lnkOlvidaPass_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }
        
    }
}
