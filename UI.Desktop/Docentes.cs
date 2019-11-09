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
    public partial class Docentes : Form
    {
        public Docentes()
        {
            InitializeComponent();
        }

        private void Docentes_Load(object sender, EventArgs e)
        {
            this.dgvDocente.AutoGenerateColumns = false;
            this.Listar();
        }
        PlanLogic pllog = new PlanLogic();
        public void Listar()
        {
            int tipoper = 2;
            PersonaLogic pl = new PersonaLogic();
            this.dgvDocente.DataSource = pl.TraerPorTipoPersona(tipoper);

            foreach (DataGridViewRow dr in dgvDocente.Rows)
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

            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tsInscripcion_Click(object sender, EventArgs e)
        {
            if (dgvDocente.SelectedRows != null)
            {
                int ID = ((Business.Entities.Personas)this.dgvDocente.SelectedRows[0].DataBoundItem).ID;
                PersonaLogic psl = new PersonaLogic();
                InscripcionesDocente inscr = new InscripcionesDocente (psl.GetOne(ID));
                inscr.ShowDialog();
                this.Listar();
            }
        }
    }
}
