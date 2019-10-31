using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

namespace UI.Desktop
{
    public partial class FormReporteCurso : Form
    {
        public FormReporteCurso()
        {
            InitializeComponent();
        }
        DateTime Fecha = DateTime.Now;
        private void FormReporteCurso_Load(object sender, EventArgs e)
        {
            ReportParameter[] parametros = new ReportParameter[1];
            parametros[0] = new ReportParameter("Fecha", (Fecha).ToString());
            reportViewerCurso.LocalReport.SetParameters(parametros);

            // TODO: esta línea de código carga datos en la tabla 'PlanesReporte.CursosReporte' Puede moverla o quitarla según sea necesario.
            this.CursosReporteTableAdapter.Fill(this.PlanesReporte.CursosReporte);
            this.reportViewerCurso.RefreshReport();
        }
    }
}
