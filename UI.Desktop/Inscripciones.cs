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
    public partial class Inscripciones : Form
    {
        Personas alumno;
        public Inscripciones()
        {
            InitializeComponent();
        }
        public Inscripciones(Personas alu, int ModoUsuario) : this()
        {
            alumno = alu;
            int modo = ModoUsuario;
            this.Listar();
            this.bloquearopciones(modo);
            
        }
        public void Listar()
        {
            InscripcionLogic il = new InscripcionLogic();

            this.dgvUsuarios.DataSource = il.GetInscAlumno(alumno);


        }

        private void Inscripciones_Load(object sender, EventArgs e)
        {
            dgvUsuarios.AutoGenerateColumns = false;
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
            
            InscribirAlumnoMateria iam = new InscribirAlumnoMateria(alumno.ID);

            iam.ShowDialog();
            this.Listar();
        }

        private void tsbEditar_Click(object sender, EventArgs e)
        {
            if (dgvUsuarios.SelectedRows != null)
            {
                int ID = ((Business.Entities.Usuario)this.dgvUsuarios.SelectedRows[0].DataBoundItem).ID;
                UsuarioDesktop ud = new UsuarioDesktop(ID, Abm.ModoForm.Modificacion);
                ud.ShowDialog();
                this.Listar();
            }
            
        }

        private void tsbEliminar_Click(object sender, EventArgs e)
        {
            if (dgvUsuarios.SelectedRows != null)
            {
                int ID = ((Business.Entities.Usuario)this.dgvUsuarios.SelectedRows[0].DataBoundItem).ID;
                UsuarioDesktop ud = new UsuarioDesktop(ID, Abm.ModoForm.Baja);
                ud.ShowDialog();
                this.Listar();
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (dgvUsuarios.SelectedRows != null && dgvUsuarios.RowCount>0)
            {
                string IDstr = this.dgvUsuarios.CurrentRow.Cells["ID"].Value.ToString();
                int idn = Int32.Parse(IDstr);
                CalificarInscripcion ci = new CalificarInscripcion(idn);
                ci.ShowDialog();
                this.Listar();
            }
        }
        private void bloquearopciones(int modo)
        {
            
            if(modo==1)
            {
                this.toolStripButton1.Visible = false;
            }
        }
    }
   
}
