namespace UI.Desktop
{
    partial class FormReportePlan
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.PlanesReporteBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.PlanesReporte = new UI.Desktop.PlanesReporte();
            this.reportViewerPlan = new Microsoft.Reporting.WinForms.ReportViewer();
            this.PlanesReporteTableAdapter = new UI.Desktop.PlanesReporteTableAdapters.PlanesReporteTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.PlanesReporteBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PlanesReporte)).BeginInit();
            this.SuspendLayout();
            // 
            // PlanesReporteBindingSource
            // 
            this.PlanesReporteBindingSource.DataMember = "PlanesReporte";
            this.PlanesReporteBindingSource.DataSource = this.PlanesReporte;
            // 
            // PlanesReporte
            // 
            this.PlanesReporte.DataSetName = "PlanesReporte";
            this.PlanesReporte.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // reportViewerPlan
            // 
            this.reportViewerPlan.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "Reporte1";
            reportDataSource1.Value = this.PlanesReporteBindingSource;
            this.reportViewerPlan.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewerPlan.LocalReport.ReportEmbeddedResource = "UI.Desktop.ReportePlan.rdlc";
            this.reportViewerPlan.Location = new System.Drawing.Point(0, 0);
            this.reportViewerPlan.Name = "reportViewerPlan";
            this.reportViewerPlan.ServerReport.BearerToken = null;
            this.reportViewerPlan.Size = new System.Drawing.Size(800, 450);
            this.reportViewerPlan.TabIndex = 0;
            // 
            // PlanesReporteTableAdapter
            // 
            this.PlanesReporteTableAdapter.ClearBeforeFill = true;
            // 
            // FormReportePlan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.reportViewerPlan);
            this.Name = "FormReportePlan";
            this.Text = "FormPlanReporte";
            this.Load += new System.EventHandler(this.FormPlanReporte_Load);
            ((System.ComponentModel.ISupportInitialize)(this.PlanesReporteBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PlanesReporte)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewerPlan;
        private System.Windows.Forms.BindingSource PlanesReporteBindingSource;
        private PlanesReporte PlanesReporte;
        private PlanesReporteTableAdapters.PlanesReporteTableAdapter PlanesReporteTableAdapter;
    }
}