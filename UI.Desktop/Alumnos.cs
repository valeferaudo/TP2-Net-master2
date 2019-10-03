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
            
            


            PersonaLogic pl = new PersonaLogic();
            this.dgvAlumnos.DataSource = pl.GetAllAlumnos();
            
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

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            Listar();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tsbNuevo_Click(object sender, EventArgs e)
        {
            AlumnoDesktop ud = new AlumnoDesktop(Abm.ModoForm.Alta);
            ud.ShowDialog();
            this.Listar();
        }

        private void tsbEditar_Click(object sender, EventArgs e)
        {
            if (dgvAlumnos.SelectedRows != null)
            {
                int ID = ((Business.Entities.Personas)this.dgvAlumnos.SelectedRows[0].DataBoundItem).ID;
                AlumnoDesktop ud = new AlumnoDesktop(ID, Abm.ModoForm.Modificacion);
                ud.ShowDialog();
                this.Listar();
            }
            
        }

        private void tsbEliminar_Click(object sender, EventArgs e)
        {
            if (dgvAlumnos.SelectedRows != null)
            {
                int ID = ((Business.Entities.Personas)this.dgvAlumnos.SelectedRows[0].DataBoundItem).ID;
                AlumnoDesktop ud = new AlumnoDesktop(ID, Abm.ModoForm.Baja);
                ud.ShowDialog();
                this.Listar();
            }
        }

        private void dgvUsuarios_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (dgvAlumnos.SelectedRows != null)
            {
                int ID = ((Business.Entities.Personas)this.dgvAlumnos.SelectedRows[0].DataBoundItem).ID;
                PersonaLogic psl = new PersonaLogic();
                Inscripciones inscr = new Inscripciones(psl.GetOne(ID));
                inscr.ShowDialog();
                this.Listar();
            }
        }
    }
   
}
