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
    public partial class formMain : Form
    {
        private Usuario usuariologeado;
        public formMain()
        {
            InitializeComponent();
        }

        private void mnuSalir_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void formMain_Shown(object sender, EventArgs e)
        {
            formLogin appLogin = new formLogin();
           
            
          
           
            if (appLogin.ShowDialog() != DialogResult.OK)
            {
                this.Dispose();
                
            }
            this.bloquearopciones(appLogin.uslogeado);
            usuariologeado = appLogin.uslogeado;
        }

        private void mnuArchivo_Click(object sender, EventArgs e)
        {

        }

        private void usuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form u = new Usuarios();
            u.Show();

        }

        private void alumnosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form per = new Persona();
            per.Show();
        }
        
        private void especialidadesToolStripMenuItem_Click(object sender, EventArgs e)
        {
        
        }



        private void inscripcionesToolStripMenuItem_Click_1(object sender, EventArgs e)
        {

            
            PersonaLogic pl = new PersonaLogic();
            Form insc = new Inscripciones(pl.GetOne(usuariologeado.IDPersona),1);
            insc.Show();
        
        }

        private void formMain_Load(object sender, EventArgs e)
        {

        }
        public void bloquearopciones(Usuario usr)
        {
            try
            {
                PersonaLogic pl = new PersonaLogic();
                Personas per = pl.GetOne(usr.IDPersona);
                this.miUsuarioToolStripMenuItem.Visible = false;
                if (per.TipoPersona == Personas.tipopersona.Alumno)
                {
                    this.usuariosToolStripMenuItem.Visible = false;
                    this.alumnosToolStripMenuItem.Visible = false;
                    this.especialidadesToolStripMenuItem.Visible = false;
                    this.planesToolStripMenuItem.Visible = false;
                    this.especialidadesToolStripMenuItem.Visible = false;
                    this.comisionesToolStripMenuItem.Visible = false;
                    this.materiasToolStripMenuItem.Visible = false;
                    this.toolStripMenuItem1.Visible = false;
                    this.reporteCursoToolStripMenuItem.Visible = false;
                    this.reportePlanToolStripMenuItem.Visible = false;
                    
                }
                if (per.TipoPersona == Personas.tipopersona.Docente)
                {
                    this.usuariosToolStripMenuItem.Visible = false;
                    this.inscripcionesToolStripMenuItem.Visible = false;
                    this.usuariosToolStripMenuItem.Visible = false;
                    this.especialidadesToolStripMenuItem.Visible = false;
                    this.planesToolStripMenuItem.Visible = false;
                    this.comisionesToolStripMenuItem.Visible = false;
                    this.materiasToolStripMenuItem.Visible = false;
                    this.toolStripMenuItem1.Visible = false;
                    
                }
                if (per.TipoPersona == Personas.tipopersona.Admin)
                {
                    this.inscripcionesToolStripMenuItem.Visible = false;
                    
                }
            }
            catch (Exception)
            {

            }
            
        }

        private void especialidadesToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Form FormEspecialidades = new Especialidades();
            FormEspecialidades.ShowDialog();
        }

        private void planesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form FormPlan = new Planes();
            FormPlan.ShowDialog();
        }

        private void comisionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form FormComision = new Comisiones();
            FormComision.ShowDialog();
        }

        private void materiasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form FormMateria = new Materias();
            FormMateria.ShowDialog();
        }

             

        private void reportePlanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form reportePlan = new FormReportePlan();
            reportePlan.ShowDialog();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form FormCurso = new Cursos();
            FormCurso.ShowDialog();
        }

        private void reporteCursoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form reporteCurso = new FormReporteCurso();
            reporteCurso.ShowDialog();
        }
    }
}

