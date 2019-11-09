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
    public partial class InscribirDocenteCurso : Form
    {
        Personas per = new Personas();
        public InscribirDocenteCurso()
        {
            InitializeComponent();
        }
        public InscribirDocenteCurso(Personas docente): this()
        {
            per = docente;
            LlenarComboCurso();
            LlenarComboCargo();
        }

        private void btnInscribir_Click(object sender, EventArgs e)
        {
            if (Validar())
            {
                DocenteCursoLogic dcl = new DocenteCursoLogic();
                DocenteCurso docenteCurso = new DocenteCurso();
                docenteCurso.IDDocente = per.ID;
                docenteCurso.IDCurso = Convert.ToInt32(cmbCurso.SelectedValue);
                docenteCurso.Cargo = (DocenteCurso.TiposCargos)(cmbCargo.SelectedValue);
                docenteCurso.State = BusinessEntity.States.New;
                dcl.Save(docenteCurso);
                this.Close();

            }
        }
        private void LlenarComboCurso()
        {
            CursoLogic cl = new CursoLogic();
            cmbCurso.DataSource = cl.GetCursosDisponiblesDocente(per.ID);
            cmbCurso.DisplayMember = "Descripcion";
            cmbCurso.ValueMember = "ID";
            cmbCurso.SelectedIndex = -1;
        }
        private void LlenarComboCargo()
        {
            cmbCargo.DataSource = Enum.GetValues(typeof(DocenteCurso.TiposCargos));
            cmbCargo.SelectedIndex = -1;
        }
        private bool Validar()
        {
            if(cmbCurso.SelectedIndex != -1 && cmbCargo.SelectedIndex != -1)
            {
                return true;
            }
            else
            {
                MessageBox.Show("Hay campos sin completar");
                return false;
            }
        }
        
    }
}
