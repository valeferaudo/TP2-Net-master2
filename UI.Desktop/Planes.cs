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
    public partial class Planes : Form
    {
        public Planes()
        {
            InitializeComponent();
        }
        EspecialidadLogic espelog = new EspecialidadLogic();
        public void Listar()
        {
            PlanLogic pl = new PlanLogic();
            this.dgvPlan.DataSource = pl.GetAll();

            foreach (DataGridViewRow dr in dgvPlan.Rows)
            {
                int idplan;
                int idespe;
                idplan = Int32.Parse(dr.Cells["idPlan"].Value.ToString());
                Plan planpl = pl.GetOne(idplan);
                idespe = planpl.IDEspecialidad;
                string planstr;
                planstr = espelog.GetOne(idespe).Descripcion;
                dr.Cells["especialidad"].Value = planstr;

            }
        }

        private void Planes_Load(object sender, EventArgs e)
        {
            this.Listar();
        }

        private void tsbNuevo_Click(object sender, EventArgs e)
        {
            PlanesDesktop FormPlan = new PlanesDesktop(Abm.ModoForm.Alta);
            FormPlan.ShowDialog();
            this.Listar();
        }

        private void tsbEditar_Click(object sender, EventArgs e)
        {
            int ID = ((Business.Entities.Plan)this.dgvPlan.SelectedRows[0].DataBoundItem).ID;
            PlanesDesktop FormPlan = new PlanesDesktop(ID, Abm.ModoForm.Modificacion);
            FormPlan.ShowDialog();
            this.Listar();
        }

        private void tsbEliminar_Click(object sender, EventArgs e)
        {
            int ID = ((Business.Entities.Plan)this.dgvPlan.SelectedRows[0].DataBoundItem).ID;
            PlanesDesktop FormPlan = new PlanesDesktop(ID, Abm.ModoForm.Baja);
            FormPlan.ShowDialog();
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
