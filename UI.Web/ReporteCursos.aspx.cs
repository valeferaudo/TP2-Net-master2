using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Data.Database;
using Business.Logic;
using Business.Entities;

namespace UI.Web
{
	public partial class ReporteCursos : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{

            if (Session["UsuarioSesion"] == null)
            {
                //Redirigir a login
                Response.Redirect("~/Login.aspx");
            }
            
            PersonaLogic pl = new PersonaLogic();
            Usuario usuario = (Usuario)Session["UsuarioSesion"];
            if (!(pl.GetOne(usuario.IDPersona).TipoPersona == Personas.tipopersona.Admin))
            {
                Response.Redirect("~/Default.aspx");
            }
            this.reportar();
        }
        private void reportar()
        {
            if (!IsPostBack)
            {
                DateTime fecha = DateTime.Now;
                LocalReport localReport = ReportViewer1.LocalReport;

                localReport.ReportPath = "ReporteCurso.rdlc";




                ReportParameter[] parametros = new ReportParameter[1];
                parametros[0] = new ReportParameter("Fecha", (fecha).ToString());
                ReportViewer1.LocalReport.SetParameters(parametros);
                ReportViewer1.DataBind();
                CursoAdapter ca = new CursoAdapter();


                this.ReportViewer1.LocalReport.DataSources.Clear();
                DataTable dt = new DataTable();
                dt = ca.ReporteCurso();

                ReportDataSource rprtDTSource = new ReportDataSource("DataSet1", dt);

                this.ReportViewer1.LocalReport.DataSources.Add(rprtDTSource);

                this.ReportViewer1.LocalReport.Refresh();
            }
        }
	}
}