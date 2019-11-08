namespace UI.Desktop
{
    partial class Docentes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Docentes));
            this.tcDocentes = new System.Windows.Forms.ToolStripContainer();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsInscripcion = new System.Windows.Forms.ToolStripButton();
            this.tlDocente = new System.Windows.Forms.TableLayoutPanel();
            this.dgvDocente = new System.Windows.Forms.DataGridView();
            this.btnSalir = new System.Windows.Forms.Button();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.apellido = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.direccion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.email = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.telefono = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fecha_nac = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.legajo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.plan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tcDocentes.ContentPanel.SuspendLayout();
            this.tcDocentes.TopToolStripPanel.SuspendLayout();
            this.tcDocentes.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.tlDocente.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDocente)).BeginInit();
            this.SuspendLayout();
            // 
            // tcDocentes
            // 
            // 
            // tcDocentes.ContentPanel
            // 
            this.tcDocentes.ContentPanel.Controls.Add(this.tlDocente);
            this.tcDocentes.ContentPanel.Size = new System.Drawing.Size(915, 242);
            this.tcDocentes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcDocentes.Location = new System.Drawing.Point(0, 0);
            this.tcDocentes.Name = "tcDocentes";
            this.tcDocentes.Size = new System.Drawing.Size(915, 267);
            this.tcDocentes.TabIndex = 0;
            this.tcDocentes.Text = "toolStripContainer1";
            // 
            // tcDocentes.TopToolStripPanel
            // 
            this.tcDocentes.TopToolStripPanel.Controls.Add(this.toolStrip1);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsInscripcion});
            this.toolStrip1.Location = new System.Drawing.Point(3, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(123, 25);
            this.toolStrip1.TabIndex = 0;
            // 
            // tsInscripcion
            // 
            this.tsInscripcion.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsInscripcion.Image = ((System.Drawing.Image)(resources.GetObject("tsInscripcion.Image")));
            this.tsInscripcion.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsInscripcion.Name = "tsInscripcion";
            this.tsInscripcion.Size = new System.Drawing.Size(80, 22);
            this.tsInscripcion.Text = "Inscripciones";
            this.tsInscripcion.Click += new System.EventHandler(this.tsInscripcion_Click);
            // 
            // tlDocente
            // 
            this.tlDocente.ColumnCount = 2;
            this.tlDocente.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlDocente.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlDocente.Controls.Add(this.dgvDocente, 0, 0);
            this.tlDocente.Controls.Add(this.btnSalir, 1, 1);
            this.tlDocente.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlDocente.Location = new System.Drawing.Point(0, 0);
            this.tlDocente.Name = "tlDocente";
            this.tlDocente.RowCount = 2;
            this.tlDocente.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlDocente.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlDocente.Size = new System.Drawing.Size(915, 242);
            this.tlDocente.TabIndex = 0;
            // 
            // dgvDocente
            // 
            this.dgvDocente.AllowUserToAddRows = false;
            this.dgvDocente.AllowUserToDeleteRows = false;
            this.dgvDocente.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDocente.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.nombre,
            this.apellido,
            this.direccion,
            this.email,
            this.telefono,
            this.fecha_nac,
            this.legajo,
            this.plan});
            this.tlDocente.SetColumnSpan(this.dgvDocente, 2);
            this.dgvDocente.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDocente.Location = new System.Drawing.Point(3, 3);
            this.dgvDocente.MultiSelect = false;
            this.dgvDocente.Name = "dgvDocente";
            this.dgvDocente.ReadOnly = true;
            this.dgvDocente.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDocente.Size = new System.Drawing.Size(909, 207);
            this.dgvDocente.TabIndex = 0;
            // 
            // btnSalir
            // 
            this.btnSalir.Location = new System.Drawing.Point(837, 216);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(75, 23);
            this.btnSalir.TabIndex = 1;
            this.btnSalir.Text = "Salir";
            this.btnSalir.UseVisualStyleBackColor = true;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // ID
            // 
            this.ID.DataPropertyName = "ID";
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            // 
            // nombre
            // 
            this.nombre.DataPropertyName = "nombre";
            this.nombre.HeaderText = "Nombre";
            this.nombre.Name = "nombre";
            this.nombre.ReadOnly = true;
            // 
            // apellido
            // 
            this.apellido.DataPropertyName = "apellido";
            this.apellido.HeaderText = "Apellido";
            this.apellido.Name = "apellido";
            this.apellido.ReadOnly = true;
            // 
            // direccion
            // 
            this.direccion.DataPropertyName = "direccion";
            this.direccion.HeaderText = "Direccion";
            this.direccion.Name = "direccion";
            this.direccion.ReadOnly = true;
            // 
            // email
            // 
            this.email.DataPropertyName = "email";
            this.email.HeaderText = "Email";
            this.email.Name = "email";
            this.email.ReadOnly = true;
            // 
            // telefono
            // 
            this.telefono.DataPropertyName = "telefono";
            this.telefono.HeaderText = "Telefono";
            this.telefono.Name = "telefono";
            this.telefono.ReadOnly = true;
            // 
            // fecha_nac
            // 
            this.fecha_nac.DataPropertyName = "fecha_nac";
            this.fecha_nac.HeaderText = "Fecha Nacimiento";
            this.fecha_nac.Name = "fecha_nac";
            this.fecha_nac.ReadOnly = true;
            // 
            // legajo
            // 
            this.legajo.DataPropertyName = "legajo";
            this.legajo.HeaderText = "Legajo";
            this.legajo.Name = "legajo";
            this.legajo.ReadOnly = true;
            // 
            // plan
            // 
            this.plan.DataPropertyName = "plan";
            this.plan.HeaderText = "Plan";
            this.plan.Name = "plan";
            this.plan.ReadOnly = true;
            // 
            // Docentes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(915, 267);
            this.Controls.Add(this.tcDocentes);
            this.Name = "Docentes";
            this.Text = "Docentes";
            this.Load += new System.EventHandler(this.Docentes_Load);
            this.tcDocentes.ContentPanel.ResumeLayout(false);
            this.tcDocentes.TopToolStripPanel.ResumeLayout(false);
            this.tcDocentes.TopToolStripPanel.PerformLayout();
            this.tcDocentes.ResumeLayout(false);
            this.tcDocentes.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tlDocente.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDocente)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripContainer tcDocentes;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.TableLayoutPanel tlDocente;
        private System.Windows.Forms.DataGridView dgvDocente;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.ToolStripButton tsInscripcion;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn nombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn apellido;
        private System.Windows.Forms.DataGridViewTextBoxColumn direccion;
        private System.Windows.Forms.DataGridViewTextBoxColumn email;
        private System.Windows.Forms.DataGridViewTextBoxColumn telefono;
        private System.Windows.Forms.DataGridViewTextBoxColumn fecha_nac;
        private System.Windows.Forms.DataGridViewTextBoxColumn legajo;
        private System.Windows.Forms.DataGridViewTextBoxColumn plan;
    }
}