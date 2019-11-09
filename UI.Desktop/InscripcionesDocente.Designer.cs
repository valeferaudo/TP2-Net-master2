namespace UI.Desktop
{
    partial class InscripcionesDocente
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InscripcionesDocente));
            this.tsInsDocente = new System.Windows.Forms.ToolStripContainer();
            this.tlInsDocente = new System.Windows.Forms.TableLayoutPanel();
            this.dgvInsDocente = new System.Windows.Forms.DataGridView();
            this.btnActualizar = new System.Windows.Forms.Button();
            this.btnSalir = new System.Windows.Forms.Button();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbNuevo = new System.Windows.Forms.ToolStripButton();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.curso = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cargo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tsInsDocente.ContentPanel.SuspendLayout();
            this.tsInsDocente.TopToolStripPanel.SuspendLayout();
            this.tsInsDocente.SuspendLayout();
            this.tlInsDocente.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInsDocente)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tsInsDocente
            // 
            // 
            // tsInsDocente.ContentPanel
            // 
            this.tsInsDocente.ContentPanel.Controls.Add(this.tlInsDocente);
            this.tsInsDocente.ContentPanel.Size = new System.Drawing.Size(715, 236);
            this.tsInsDocente.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tsInsDocente.Location = new System.Drawing.Point(0, 0);
            this.tsInsDocente.Name = "tsInsDocente";
            this.tsInsDocente.Size = new System.Drawing.Size(715, 261);
            this.tsInsDocente.TabIndex = 0;
            this.tsInsDocente.Text = "toolStripContainer1";
            // 
            // tsInsDocente.TopToolStripPanel
            // 
            this.tsInsDocente.TopToolStripPanel.Controls.Add(this.toolStrip1);
            // 
            // tlInsDocente
            // 
            this.tlInsDocente.ColumnCount = 2;
            this.tlInsDocente.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlInsDocente.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlInsDocente.Controls.Add(this.dgvInsDocente, 0, 0);
            this.tlInsDocente.Controls.Add(this.btnActualizar, 0, 1);
            this.tlInsDocente.Controls.Add(this.btnSalir, 1, 1);
            this.tlInsDocente.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlInsDocente.Location = new System.Drawing.Point(0, 0);
            this.tlInsDocente.Name = "tlInsDocente";
            this.tlInsDocente.RowCount = 2;
            this.tlInsDocente.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlInsDocente.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlInsDocente.Size = new System.Drawing.Size(715, 236);
            this.tlInsDocente.TabIndex = 0;
            // 
            // dgvInsDocente
            // 
            this.dgvInsDocente.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvInsDocente.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.curso,
            this.cargo});
            this.tlInsDocente.SetColumnSpan(this.dgvInsDocente, 2);
            this.dgvInsDocente.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvInsDocente.Location = new System.Drawing.Point(3, 3);
            this.dgvInsDocente.MultiSelect = false;
            this.dgvInsDocente.Name = "dgvInsDocente";
            this.dgvInsDocente.ReadOnly = true;
            this.dgvInsDocente.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvInsDocente.Size = new System.Drawing.Size(709, 201);
            this.dgvInsDocente.TabIndex = 0;
            // 
            // btnActualizar
            // 
            this.btnActualizar.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnActualizar.Location = new System.Drawing.Point(556, 210);
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new System.Drawing.Size(75, 23);
            this.btnActualizar.TabIndex = 1;
            this.btnActualizar.Text = "Actualizar";
            this.btnActualizar.UseVisualStyleBackColor = true;
            this.btnActualizar.Click += new System.EventHandler(this.btnActualizar_Click);
            // 
            // btnSalir
            // 
            this.btnSalir.Location = new System.Drawing.Point(637, 210);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(75, 23);
            this.btnSalir.TabIndex = 2;
            this.btnSalir.Text = "Salir";
            this.btnSalir.UseVisualStyleBackColor = true;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbNuevo});
            this.toolStrip1.Location = new System.Drawing.Point(3, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(65, 25);
            this.toolStrip1.TabIndex = 0;
            // 
            // tsbNuevo
            // 
            this.tsbNuevo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbNuevo.Image = ((System.Drawing.Image)(resources.GetObject("tsbNuevo.Image")));
            this.tsbNuevo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbNuevo.Name = "tsbNuevo";
            this.tsbNuevo.Size = new System.Drawing.Size(53, 22);
            this.tsbNuevo.Text = "Inscribir";
            this.tsbNuevo.ToolTipText = "Nuevo";
            this.tsbNuevo.Click += new System.EventHandler(this.tsbNuevo_Click);
            // 
            // id
            // 
            this.id.DataPropertyName = "ID";
            this.id.HeaderText = "ID";
            this.id.Name = "id";
            this.id.ReadOnly = true;
            this.id.Width = 50;
            // 
            // curso
            // 
            this.curso.DataPropertyName = "DescripcionCurso";
            this.curso.HeaderText = "Curso";
            this.curso.Name = "curso";
            this.curso.ReadOnly = true;
            this.curso.Width = 170;
            // 
            // cargo
            // 
            this.cargo.DataPropertyName = "Cargo";
            this.cargo.HeaderText = "Cargo";
            this.cargo.Name = "cargo";
            this.cargo.ReadOnly = true;
            this.cargo.Width = 120;
            // 
            // InscripcionesDocente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(715, 261);
            this.Controls.Add(this.tsInsDocente);
            this.Name = "InscripcionesDocente";
            this.Text = "InscripcionesDocente";
            this.Load += new System.EventHandler(this.InscripcionesDocente_Load);
            this.tsInsDocente.ContentPanel.ResumeLayout(false);
            this.tsInsDocente.TopToolStripPanel.ResumeLayout(false);
            this.tsInsDocente.TopToolStripPanel.PerformLayout();
            this.tsInsDocente.ResumeLayout(false);
            this.tsInsDocente.PerformLayout();
            this.tlInsDocente.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvInsDocente)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripContainer tsInsDocente;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbNuevo;
        private System.Windows.Forms.TableLayoutPanel tlInsDocente;
        private System.Windows.Forms.DataGridView dgvInsDocente;
        private System.Windows.Forms.Button btnActualizar;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn curso;
        private System.Windows.Forms.DataGridViewTextBoxColumn cargo;
    }
}