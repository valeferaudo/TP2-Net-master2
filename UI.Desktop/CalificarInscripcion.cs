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
    public partial class CalificarInscripcion : Form
    {
        int id;
        public CalificarInscripcion()
        {
            InitializeComponent();
        }
        public CalificarInscripcion(int idins)
        {
            id = idins;
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (maskedTextBox1.Text != "")
            {
                InscripcionLogic il = new InscripcionLogic();
                AlumnoInscripcion ai = new AlumnoInscripcion();
                ai = il.GetOne(id);
                ai.Nota = Convert.ToInt32(maskedTextBox1.Text);
                ai.Condicion = comboBox1.SelectedItem.ToString();
                ai.State = BusinessEntity.States.Modified;
                il.Save(ai);
                this.Close();
            }
        }
    }
}
