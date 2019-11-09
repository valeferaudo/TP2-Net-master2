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
    public partial class InscribirAlumnoMateria : Form
    {
        List<Curso> inscribibles = new List<Curso>();
        int idalumno;
        PersonaLogic plll = new PersonaLogic();
        public InscribirAlumnoMateria()
        {
            InitializeComponent();
        }
        public InscribirAlumnoMateria(int idalu) : this()
        {
            idalumno = idalu;
            this.Listardisp();
        }
        public void Listardisp()
        {
            CursoLogic cl = new CursoLogic();
            List<Curso> cursos = cl.GetDispAlumno(idalumno);
            
            foreach(Curso value in cursos)
            {

                if (cl.YaAprobado(idalumno,value) == false){
                    
                    comboBox1.Items.Add(value.Descripcion);
                    inscribibles.Add(value);
                }
           
            }

        }
        public void Inscribir()
        {
            int nrocurso;
            CursoLogic cl = new CursoLogic();
            
            nrocurso = inscribibles[comboBox1.SelectedIndex].ID;
            AlumnoInscripcion alinsc = new AlumnoInscripcion();
            alinsc.IDAlumno = idalumno;
            alinsc.IDCurso = nrocurso;
            alinsc.Condicion = "Inscripto";
            alinsc.State = BusinessEntity.States.New;
            InscripcionLogic il = new InscripcionLogic();
            il.Save(alinsc);
        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                this.Inscribir();
                this.Close();
            }
            else
            {
                MessageBox.Show("Seleccione algún Curso");
            }
        }
    }
}
