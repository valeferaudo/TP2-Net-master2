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
    public partial class Cursos : Form
    {
        public Cursos()
        {
            InitializeComponent();
        }

        MateriaLogic mllog = new MateriaLogic();
        ComisionLogic colog = new ComisionLogic();
        public void Listar()
        {
            CursoLogic cl = new CursoLogic();
            this.dgvCursos.DataSource = cl.GetAll();

            foreach (DataGridViewRow dr in dgvCursos.Rows)
            {
                int idmat;
                int idcur;
                int idcomi;
                idcur = Int32.Parse(dr.Cells["id"].Value.ToString());
                Curso prpl = cl.GetOne(idcur);
                idmat = prpl.IDMateria;
                string matstr;
                matstr = mllog.GetOne(idmat).Descripcion;
                dr.Cells["materia"].Value = matstr;

                string comisionstr;
                idcomi = prpl.IDComision;
                comisionstr = colog.GetOne(idcomi).Descripcion;
                dr.Cells["comision"].Value = comisionstr;

            }
        }

        private void Cursos_Load(object sender, EventArgs e)
        {
            this.Listar();
        }

        private void tsbNuevo_Click(object sender, EventArgs e)
        {
            CursosDesktop FormCurso = new CursosDesktop(Abm.ModoForm.Alta);
            FormCurso.ShowDialog();
            this.Listar();
        }

        private void tsbEditar_Click(object sender, EventArgs e)
        {
            int ID = ((Business.Entities.Curso)this.dgvCursos.SelectedRows[0].DataBoundItem).ID;
            CursosDesktop FormCurso = new CursosDesktop(ID, Abm.ModoForm.Modificacion);
            FormCurso.ShowDialog();
            this.Listar();
        }

        private void tsbEliminar_Click(object sender, EventArgs e)
        {
            int ID = ((Business.Entities.Curso)this.dgvCursos.SelectedRows[0].DataBoundItem).ID;
            CursosDesktop FormCurso = new CursosDesktop(ID, Abm.ModoForm.Baja);
            FormCurso.ShowDialog();
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
