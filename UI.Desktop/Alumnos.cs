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
    public partial class Alumnos : Form
    {
        public Alumnos()
        {
            InitializeComponent();
        }
        PlanLogic pllog = new PlanLogic();

        public void Listar()
        {
            int tipoper = 1;
            PersonaLogic pl = new PersonaLogic();
            this.dgvAlumnos.DataSource = pl.TraerPorTipoPersona(tipoper);
            
            foreach (DataGridViewRow dr in dgvAlumnos.Rows)
            {
                int idpl;
                int idper;
                int lenfecha;
                idper = Int32.Parse(dr.Cells["ID"].Value.ToString());
                Personas prpl = pl.GetOne(idper);
                idpl = prpl.IDPlan;
                string planstr;
                planstr = pllog.GetOne(idpl).Descripcion;
                dr.Cells["Plan"].Value = planstr;
                lenfecha = prpl.FechaNacimiento.ToString().Length;
                dr.Cells["fechanac"].Value = prpl.FechaNacimiento.ToString().Substring(0,lenfecha-9);

            }
        }

        private void Usuarios_Load(object sender, EventArgs e)
        {
            dgvAlumnos.AutoGenerateColumns = false;
            Listar();
            
        }
              

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (dgvAlumnos.SelectedRows != null)
            {
                int ID = ((Business.Entities.Personas)this.dgvAlumnos.SelectedRows[0].DataBoundItem).ID;
                PersonaLogic psl = new PersonaLogic();
                InscripcionesAlumno inscr = new InscripcionesAlumno(psl.GetOne(ID),2);
                inscr.ShowDialog();
                this.Listar();
            }
        }
    }
   
}
