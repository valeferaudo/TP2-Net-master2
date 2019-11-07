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
    public partial class Persona : Form
    {
        public Persona()
        {
            InitializeComponent();
            
        }
        PlanLogic pllog = new PlanLogic();

        public void Listar()
        {                                 
            PersonaLogic pl = new PersonaLogic();
            this.dgvPersona.DataSource = pl.GetAll();

            foreach (DataGridViewRow dr in dgvPersona.Rows)
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
                dr.Cells["fecha_nac"].Value = prpl.FechaNacimiento.ToString().Substring(0, lenfecha - 9);
                dr.Cells["tipo_persona"].Value = prpl.TipoPersona;

            }
        }

        private void Persona_Load(object sender, EventArgs e)
        {
            this.dgvPersona.AutoGenerateColumns = false;
            this.Listar();
        }

        private void tsbNuevo_Click(object sender, EventArgs e)
        {
            PersonaDesktop ud = new PersonaDesktop(Abm.ModoForm.Alta);
            ud.ShowDialog();
            this.Listar();

        }

        private void tsbEditar_Click(object sender, EventArgs e)
        {
            int ID = ((Business.Entities.Personas)this.dgvPersona.SelectedRows[0].DataBoundItem).ID;
            PersonaDesktop ud = new PersonaDesktop(ID, Abm.ModoForm.Modificacion);
            ud.ShowDialog();
            this.Listar();
        }

        private void tsbEliminar_Click(object sender, EventArgs e)
        {
            int ID = ((Business.Entities.Personas)this.dgvPersona.SelectedRows[0].DataBoundItem).ID;
            PersonaDesktop ud = new PersonaDesktop(ID, Abm.ModoForm.Baja);
            ud.ShowDialog();
            this.Listar();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            this.Listar();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
