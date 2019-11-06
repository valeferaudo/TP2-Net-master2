using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Data.Database;

namespace UI.Web
{
	public partial class ReporteCursos : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
            DateTime fecha = DateTime.Now;
            LocalReport localReport = ReportViewer1.LocalReport;

            localReport.ReportPath = "ReporteCurso.rdlc";
            

            

            ReportParameter[] parametros = new ReportParameter[1];
            parametros[0] = new ReportParameter("Fecha", (fecha).ToString());
            ReportViewer1.LocalReport.SetParameters(parametros);
            ReportViewer1.DataBind();
            // TODO: esta línea de código carga datos en la tabla 'PlanesReporte.CursosReporte' Puede moverla o quitarla según sea necesario.
            
        }
	}
}