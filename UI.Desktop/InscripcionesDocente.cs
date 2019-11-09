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

namespace UI.Desktop
{
    public partial class InscripcionesDocente : Form
    {
        Personas persona = new Personas();
        public InscripcionesDocente()
        {
            InitializeComponent();
            
        }

        private void InscripcionesDocente_Load(object sender, EventArgs e)
        {
            this.dgvInsDocente.AutoGenerateColumns = false;
            this.Listar();
        }

        public InscripcionesDocente(Personas per) : this()
        {
            
            persona = per;
            //Listar();
        }
        public void Listar()
        {
            DocenteCursoLogic dcl = new DocenteCursoLogic();
            
            this.dgvInsDocente.DataSource = dcl.GetInscripcionesDocente(persona.ID);
            
        }

        private void tsbNuevo_Click(object sender, EventArgs e)
        {
            
            InscribirDocenteCurso inscr = new InscribirDocenteCurso(persona);
            inscr.ShowDialog();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            this.Listar();
        }
    }
}
