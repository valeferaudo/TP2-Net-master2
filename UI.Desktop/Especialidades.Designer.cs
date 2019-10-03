namespace UI.Desktop
{
    partial class Especialidades
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Especialidades));
            this.tcEspecialidad = new System.Windows.Forms.ToolStripContainer();
            this.tlEspecialidad = new System.Windows.Forms.TableLayoutPanel();
            this.dgvEspecialidad = new System.Windows.Forms.DataGridView();
            this.tsUsuarios = new System.Windows.Forms.ToolStrip();
            this.tsbNuevo = new System.Windows.Forms.ToolStripButton();
            this.tsbEditar = new System.Windows.Forms.ToolStripButton();
            this.tsbEliminar = new System.Windows.Forms.ToolStripButton();
            this.btnActualizar = new System.Windows.Forms.Button();
            this.btnSalir = new System.Windows.Forms.Button();
            this.idEspecialidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.descripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tcEspecialidad.ContentPanel.SuspendLayout();
            this.tcEspecialidad.TopToolStripPanel.SuspendLayout();
            this.tcEspecialidad.SuspendLayout();
            this.tlEspecialidad.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEspecialidad)).BeginInit();
            this.tsUsuarios.SuspendLayout();
            this.SuspendLayout();
            // 
            // tcEspecialidad
            // 
            // 
            // tcEspecialidad.ContentPanel
            // 
            this.tcEspecialidad.ContentPanel.Controls.Add(this.tlEspecialidad);
            this.tcEspecialidad.ContentPanel.Size = new System.Drawing.Size(674, 178);
            this.tcEspecialidad.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcEspecialidad.Location = new System.Drawing.Point(0, 0);
            this.tcEspecialidad.Name = "tcEspecialidad";
            this.tcEspecialidad.Size = new System.Drawing.Size(674, 203);
            this.tcEspecialidad.TabIndex = 0;
            this.tcEspecialidad.Text = "toolStripContainer1";
            // 
            // tcEspecialidad.TopToolStripPanel
            // 
            this.tcEspecialidad.TopToolStripPanel.Controls.Add(this.tsUsuarios);
            // 
            // tlEspecialidad
            // 
            this.tlEspecialidad.ColumnCount = 2;
            this.tlEspecialidad.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlEspecialidad.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlEspecialidad.Controls.Add(this.btnActualizar, 0, 1);
            this.tlEspecialidad.Controls.Add(this.btnSalir, 0, 1);
            this.tlEspecialidad.Controls.Add(this.dgvEspecialidad, 0, 0);
            this.tlEspecialidad.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlEspecialidad.Location = new System.Drawing.Point(0, 0);
            this.tlEspecialidad.Name = "tlEspecialidad";
            this.tlEspecialidad.RowCount = 2;
            this.tlEspecialidad.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlEspecialidad.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlEspecialidad.Size = new System.Drawing.Size(674, 178);
            this.tlEspecialidad.TabIndex = 0;
            // 
            // dgvEspecialidad
            // 
            this.dgvEspecialidad.AllowUserToAddRows = false;
            this.dgvEspecialidad.AllowUserToDeleteRows = false;
            this.dgvEspecialidad.AutoGenerateColumns = false;
            this.dgvEspecialidad.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEspecialidad.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idEspecialidad,
            this.descripcion});
            this.tlEspecialidad.SetColumnSpan(this.dgvEspecialidad, 2);
            this.dgvEspecialidad.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvEspecialidad.Location = new System.Drawing.Point(3, 3);
            this.dgvEspecialidad.MultiSelect = false;
            this.dgvEspecialidad.Name = "dgvEspecialidad";
            this.dgvEspecialidad.ReadOnly = true;
            this.dgvEspecialidad.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvEspecialidad.Size = new System.Drawing.Size(668, 143);
            this.dgvEspecialidad.TabIndex = 0;
            // 
            // tsUsuarios
            // 
            this.tsUsuarios.Dock = System.Windows.Forms.DockStyle.None;
            this.tsUsuarios.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbNuevo,
            this.tsbEditar,
            this.tsbEliminar});
            this.tsUsuarios.Location = new System.Drawing.Point(3, 0);
            this.tsUsuarios.Name = "tsUsuarios";
            this.tsUsuarios.Size = new System.Drawing.Size(81, 25);
            this.tsUsuarios.TabIndex = 1;
            // 
            // tsbNuevo
            // 
            this.tsbNuevo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbNuevo.Image = ((System.Drawing.Image)(resources.GetObject("tsbNuevo.Image")));
            this.tsbNuevo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbNuevo.Name = "tsbNuevo";
            this.tsbNuevo.Size = new System.Drawing.Size(23, 22);
            this.tsbNuevo.Text = "toolStripButton1";
            this.tsbNuevo.ToolTipText = "Nuevo";
            this.tsbNuevo.Click += new System.EventHandler(this.tsbNuevo_Click);
            // 
            // tsbEditar
            // 
            this.tsbEditar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbEditar.Image = ((System.Drawing.Image)(resources.GetObject("tsbEditar.Image")));
            this.tsbEditar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbEditar.Name = "tsbEditar";
            this.tsbEditar.Size = new System.Drawing.Size(23, 22);
            this.tsbEditar.Text = "toolStripButton1";
            this.tsbEditar.ToolTipText = "Editar";
            this.tsbEditar.Click += new System.EventHandler(this.tsbEditar_Click);
            // 
            // tsbEliminar
            // 
            this.tsbEliminar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbEliminar.Image = ((System.Drawing.Image)(resources.GetObject("tsbEliminar.Image")));
            this.tsbEliminar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbEliminar.Name = "tsbEliminar";
            this.tsbEliminar.Size = new System.Drawing.Size(23, 22);
            this.tsbEliminar.Text = "toolStripButton1";
            this.tsbEliminar.ToolTipText = "Eliminar";
            this.tsbEliminar.Click += new System.EventHandler(this.tsbEliminar_Click);
            // 
            // btnActualizar
            // 
            this.btnActualizar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnActualizar.Location = new System.Drawing.Point(515, 152);
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new System.Drawing.Size(75, 23);
            this.btnActualizar.TabIndex = 3;
            this.btnActualizar.Text = "Actualizar";
            this.btnActualizar.UseVisualStyleBackColor = true;
            this.btnActualizar.Click += new System.EventHandler(this.btnActualizar_Click);
            // 
            // btnSalir
            // 
            this.btnSalir.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnSalir.Location = new System.Drawing.Point(596, 152);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(75, 23);
            this.btnSalir.TabIndex = 4;
            this.btnSalir.Text = "Salir";
            this.btnSalir.UseVisualStyleBackColor = true;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // idEspecialidad
            // 
            this.idEspecialidad.DataPropertyName = "ID";
            this.idEspecialidad.HeaderText = "ID-Especialidad";
            this.idEspecialidad.Name = "idEspecialidad";
            this.idEspecialidad.ReadOnly = true;
            // 
            // descripcion
            // 
            this.descripcion.DataPropertyName = "descripcion";
            this.descripcion.HeaderText = "Descripcion";
            this.descripcion.Name = "descripcion";
            this.descripcion.ReadOnly = true;
            // 
            // Especialidades
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(674, 203);
            this.Controls.Add(this.tcEspecialidad);
            this.Name = "Especialidades";
            this.Text = "Especialidades";
            this.Load += new System.EventHandler(this.Especialidades_Load);
            this.tcEspecialidad.ContentPanel.ResumeLayout(false);
            this.tcEspecialidad.TopToolStripPanel.ResumeLayout(false);
            this.tcEspecialidad.TopToolStripPanel.PerformLayout();
            this.tcEspecialidad.ResumeLayout(false);
            this.tcEspecialidad.PerformLayout();
            this.tlEspecialidad.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvEspecialidad)).EndInit();
            this.tsUsuarios.ResumeLayout(false);
            this.tsUsuarios.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripContainer tcEspecialidad;
        private System.Windows.Forms.TableLayoutPanel tlEspecialidad;
        private System.Windows.Forms.DataGridView dgvEspecialidad;
        private System.Windows.Forms.ToolStrip tsUsuarios;
        private System.Windows.Forms.ToolStripButton tsbNuevo;
        private System.Windows.Forms.ToolStripButton tsbEditar;
        private System.Windows.Forms.ToolStripButton tsbEliminar;
        private System.Windows.Forms.Button btnActualizar;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.DataGridViewTextBoxColumn idEspecialidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn descripcion;
    }
}