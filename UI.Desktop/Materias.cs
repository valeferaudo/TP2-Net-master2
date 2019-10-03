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
    public partial class Materias : Form
    {
        public Materias()
        {
            InitializeComponent();
        }
        PlanLogic pllog = new PlanLogic();
        public void Listar()
        {
            MateriaLogic ml = new MateriaLogic();
            this.dgvMaterias.DataSource = ml.GetAll();

            foreach (DataGridViewRow dr in dgvMaterias.Rows)
            {
                int idpl;
                int idmateria;
                idmateria = Int32.Parse(dr.Cells["idMateria"].Value.ToString());
                Materia prpl = ml.GetOne(idmateria);
                idpl = prpl.IDPlan;
                string planstr;
                planstr = pllog.GetOne(idpl).Descripcion;
                dr.Cells["plan"].Value = planstr;                

            }
        }

        private void Materias_Load(object sender, EventArgs e)
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
            MateriasDesktop FormMateria = new MateriasDesktop(Abm.ModoForm.Alta);
            FormMateria.ShowDialog();
            this.Listar();
        }

        private void tsbEditar_Click(object sender, EventArgs e)
        {
            int ID = ((Business.Entities.Materia)this.dgvMaterias.SelectedRows[0].DataBoundItem).ID;
            MateriasDesktop FormMateria = new MateriasDesktop(ID, Abm.ModoForm.Modificacion);
            FormMateria.ShowDialog();
            this.Listar();
        }

        private void tsbEliminar_Click(object sender, EventArgs e)
        {
            int ID = ((Business.Entities.Materia)this.dgvMaterias.SelectedRows[0].DataBoundItem).ID;
            MateriasDesktop FormMateria = new MateriasDesktop(ID, Abm.ModoForm.Baja);
            FormMateria.ShowDialog();
            this.Listar();
        }
    }
}
