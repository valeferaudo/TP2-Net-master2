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
    public partial class Comisiones : Form
    {
        public Comisiones()
        {
            InitializeComponent();
        }

        PlanLogic pllog = new PlanLogic();

        public void Listar()
        {
            ComisionLogic cl = new ComisionLogic();
            this.dgvComision.DataSource = cl.GetAll();

            foreach (DataGridViewRow dr in dgvComision.Rows)
            {
                int idcomi;
                int idpl;
                idcomi = Int32.Parse(dr.Cells["idComision"].Value.ToString());
                Comision comipl = cl.GetOne(idcomi);
                idpl = comipl.IDPlan;
                string planstr;
                planstr = pllog.GetOne(idpl).Descripcion;
                dr.Cells["plan"].Value = planstr;
               
            }
        }

        private void Comisiones_Load(object sender, EventArgs e)
        {
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

        private void tsbNuevo_Click(object sender, EventArgs e)
        {
            ComisionesDesktop FormComision = new ComisionesDesktop(Abm.ModoForm.Alta);
            FormComision.ShowDialog();
            this.Listar();
        }

        private void tsbEditar_Click(object sender, EventArgs e)
        {
            int ID = ((Business.Entities.Comision)this.dgvComision.SelectedRows[0].DataBoundItem).ID;
            ComisionesDesktop FormComision = new ComisionesDesktop(ID, Abm.ModoForm.Modificacion);
            FormComision.ShowDialog();
            this.Listar();
        }

        private void tsbEliminar_Click(object sender, EventArgs e)
        {
            int ID = ((Business.Entities.Comision)this.dgvComision.SelectedRows[0].DataBoundItem).ID;
            ComisionesDesktop FormComision = new ComisionesDesktop(ID, Abm.ModoForm.Baja);
            FormComision.ShowDialog();
            this.Listar();
        }
    }
}
