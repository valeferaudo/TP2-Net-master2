using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Data.Database;
using System.Data;

namespace UI.Web
{
    public partial class ReportePlanes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DateTime fecha = DateTime.Now;
                LocalReport localReport = ReportViewer1.LocalReport;

                localReport.ReportPath = "ReportePlan.rdlc";




                ReportParameter[] parametros = new ReportParameter[1];
                parametros[0] = new ReportParameter("Fecha", (fecha).ToString());
                ReportViewer1.LocalReport.SetParameters(parametros);
                ReportViewer1.DataBind();
                PlanAdapter pa = new PlanAdapter();


                this.ReportViewer1.LocalReport.DataSources.Clear();
                DataTable dt = new DataTable();
                dt = pa.ReportePlan();

                ReportDataSource rprtDTSource = new ReportDataSource("Dataset2", dt);

                this.ReportViewer1.LocalReport.DataSources.Add(rprtDTSource);

                this.ReportViewer1.LocalReport.Refresh();
            }
        }
    }
}