namespace UI.Desktop
{
    partial class FormReporteCurso
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
            this.CursosReporteBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.PlanesReporte = new UI.Desktop.PlanesReporte();
            this.reportViewerCurso = new Microsoft.Reporting.WinForms.ReportViewer();
            this.PlanesReporteBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.PlanesReporteTableAdapter = new UI.Desktop.PlanesReporteTableAdapters.PlanesReporteTableAdapter();
            this.CursosReporteTableAdapter = new UI.Desktop.PlanesReporteTableAdapters.CursosReporteTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.CursosReporteBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PlanesReporte)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PlanesReporteBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // CursosReporteBindingSource
            // 
            this.CursosReporteBindingSource.DataMember = "CursosReporte";
            this.CursosReporteBindingSource.DataSource = this.PlanesReporte;
            // 
            // PlanesReporte
            // 
            this.PlanesReporte.DataSetName = "PlanesReporte";
            this.PlanesReporte.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // reportViewerCurso
            // 
            this.reportViewerCurso.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.CursosReporteBindingSource;
            this.reportViewerCurso.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewerCurso.LocalReport.ReportEmbeddedResource = "UI.Desktop.ReporteCurso.rdlc";
            this.reportViewerCurso.Location = new System.Drawing.Point(0, 0);
            this.reportViewerCurso.Name = "reportViewerCurso";
            this.reportViewerCurso.ServerReport.BearerToken = null;
            this.reportViewerCurso.Size = new System.Drawing.Size(813, 428);
            this.reportViewerCurso.TabIndex = 0;
            // 
            // PlanesReporteBindingSource
            // 
            this.PlanesReporteBindingSource.DataMember = "PlanesReporte";
            this.PlanesReporteBindingSource.DataSource = this.PlanesReporte;
            // 
            // PlanesReporteTableAdapter
            // 
            this.PlanesReporteTableAdapter.ClearBeforeFill = true;
            // 
            // CursosReporteTableAdapter
            // 
            this.CursosReporteTableAdapter.ClearBeforeFill = true;
            // 
            // FormReporteCurso
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(813, 428);
            this.Controls.Add(this.reportViewerCurso);
            this.Name = "FormReporteCurso";
            this.Text = "FormReporteCurso";
            this.Load += new System.EventHandler(this.FormReporteCurso_Load);
            ((System.ComponentModel.ISupportInitialize)(this.CursosReporteBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PlanesReporte)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PlanesReporteBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewerCurso;
        private PlanesReporte PlanesReporte;
        private System.Windows.Forms.BindingSource PlanesReporteBindingSource;
        private PlanesReporteTableAdapters.PlanesReporteTableAdapter PlanesReporteTableAdapter;
        private System.Windows.Forms.BindingSource CursosReporteBindingSource;
        private PlanesReporteTableAdapters.CursosReporteTableAdapter CursosReporteTableAdapter;
    }
}