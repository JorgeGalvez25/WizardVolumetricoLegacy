namespace iGasWizardVolumetricos.Pantallas.Procesando
{
    partial class PProcesando
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnReintentar = new System.Windows.Forms.Button();
            this.pictureEdit1 = new DevExpress.XtraEditors.PictureEdit();
            this.lblEstatus = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.pnlProgress = new DevExpress.XtraEditors.ProgressBarControl();
            this.lblMensaje = new System.Windows.Forms.Label();
            this.pictureEdit2 = new DevExpress.XtraEditors.PictureEdit();
            this.pnlNotificacion = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlProgress.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit2.Properties)).BeginInit();
            this.pnlNotificacion.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.pnlNotificacion);
            this.panel1.Controls.Add(this.pictureEdit1);
            this.panel1.Controls.Add(this.lblEstatus);
            this.panel1.Controls.Add(this.labelControl1);
            this.panel1.Controls.Add(this.pnlProgress);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(20, 20, 0, 0);
            this.panel1.Size = new System.Drawing.Size(632, 380);
            this.panel1.TabIndex = 0;
            // 
            // btnReintentar
            // 
            this.btnReintentar.Location = new System.Drawing.Point(125, 3);
            this.btnReintentar.Name = "btnReintentar";
            this.btnReintentar.Size = new System.Drawing.Size(75, 23);
            this.btnReintentar.TabIndex = 15;
            this.btnReintentar.Text = "Reintentar";
            this.btnReintentar.UseVisualStyleBackColor = true;
            // 
            // pictureEdit1
            // 
            this.pictureEdit1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureEdit1.EditValue = global::iGasWizardVolumetricos.Properties.Resources.icono_igas;
            this.pictureEdit1.Location = new System.Drawing.Point(553, 336);
            this.pictureEdit1.Name = "pictureEdit1";
            this.pictureEdit1.Properties.AllowFocused = false;
            this.pictureEdit1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.pictureEdit1.Properties.Appearance.Options.UseBackColor = true;
            this.pictureEdit1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pictureEdit1.Properties.ShowMenu = false;
            this.pictureEdit1.Size = new System.Drawing.Size(79, 44);
            this.pictureEdit1.TabIndex = 14;
            // 
            // lblEstatus
            // 
            this.lblEstatus.Appearance.Options.UseTextOptions = true;
            this.lblEstatus.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.EllipsisCharacter;
            this.lblEstatus.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
            this.lblEstatus.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.lblEstatus.AutoEllipsis = true;
            this.lblEstatus.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblEstatus.Location = new System.Drawing.Point(206, 126);
            this.lblEstatus.MaximumSize = new System.Drawing.Size(250, 13);
            this.lblEstatus.MinimumSize = new System.Drawing.Size(250, 13);
            this.lblEstatus.Name = "lblEstatus";
            this.lblEstatus.Size = new System.Drawing.Size(250, 13);
            this.lblEstatus.TabIndex = 2;
            this.lblEstatus.Text = "...";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(146, 124);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(54, 16);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "Estatus:";
            // 
            // pnlProgress
            // 
            this.pnlProgress.EditValue = "0";
            this.pnlProgress.Location = new System.Drawing.Point(146, 146);
            this.pnlProgress.Name = "pnlProgress";
            this.pnlProgress.Properties.LookAndFeel.SkinName = "Blue";
            this.pnlProgress.Properties.LookAndFeel.UseDefaultLookAndFeel = false;
            this.pnlProgress.Properties.LookAndFeel.UseWindowsXPTheme = true;
            this.pnlProgress.Properties.ShowTitle = true;
            this.pnlProgress.Size = new System.Drawing.Size(327, 59);
            this.pnlProgress.TabIndex = 0;
            // 
            // lblMensaje
            // 
            this.lblMensaje.Location = new System.Drawing.Point(98, 29);
            this.lblMensaje.Name = "lblMensaje";
            this.lblMensaje.Size = new System.Drawing.Size(156, 29);
            this.lblMensaje.TabIndex = 16;
            this.lblMensaje.Text = "Puede dejar abierta esta aplicacion mientras actualiza";
            // 
            // pictureEdit2
            // 
            this.pictureEdit2.EditValue = global::iGasWizardVolumetricos.Properties.Resources.information_16x16;
            this.pictureEdit2.Location = new System.Drawing.Point(71, 29);
            this.pictureEdit2.Name = "pictureEdit2";
            this.pictureEdit2.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pictureEdit2.Properties.PictureAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.pictureEdit2.Properties.ShowMenu = false;
            this.pictureEdit2.Size = new System.Drawing.Size(21, 23);
            this.pictureEdit2.TabIndex = 17;
            // 
            // pnlNotificacion
            // 
            this.pnlNotificacion.Controls.Add(this.btnReintentar);
            this.pnlNotificacion.Controls.Add(this.pictureEdit2);
            this.pnlNotificacion.Controls.Add(this.lblMensaje);
            this.pnlNotificacion.Location = new System.Drawing.Point(146, 211);
            this.pnlNotificacion.Name = "pnlNotificacion";
            this.pnlNotificacion.Size = new System.Drawing.Size(327, 100);
            this.pnlNotificacion.TabIndex = 19;
            this.pnlNotificacion.Visible = false;
            // 
            // PProcesando
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "PProcesando";
            this.Size = new System.Drawing.Size(632, 380);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlProgress.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit2.Properties)).EndInit();
            this.pnlNotificacion.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.ProgressBarControl pnlProgress;
        private DevExpress.XtraEditors.LabelControl lblEstatus;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.PictureEdit pictureEdit1;
        private System.Windows.Forms.Button btnReintentar;
        private System.Windows.Forms.Label lblMensaje;
        private DevExpress.XtraEditors.PictureEdit pictureEdit2;
        private System.Windows.Forms.Panel pnlNotificacion;
    }
}
