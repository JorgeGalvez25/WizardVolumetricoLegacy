namespace iGasWizardVolumetricos.Pantallas.Impresora
{
    partial class PImpresora
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
            this.pictureEdit1 = new DevExpress.XtraEditors.PictureEdit();
            this.txtImpresora = new DevExpress.XtraEditors.ComboBoxEdit();
            this.pictureEdit5 = new DevExpress.XtraEditors.PictureEdit();
            this.ckEjecutarNETUSE = new DevExpress.XtraEditors.CheckEdit();
            this.txtIP = new DevExpress.XtraEditors.TextEdit();
            this.ckRemoto = new DevExpress.XtraEditors.CheckEdit();
            this.txtPuerto = new DevExpress.XtraEditors.TextEdit();
            this.labelControl18 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl19 = new DevExpress.XtraEditors.LabelControl();
            this.txtTipoPuerto = new DevExpress.XtraEditors.ComboBoxEdit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtImpresora.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit5.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckEjecutarNETUSE.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIP.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckRemoto.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPuerto.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTipoPuerto.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.txtTipoPuerto);
            this.panel1.Controls.Add(this.pictureEdit1);
            this.panel1.Controls.Add(this.txtImpresora);
            this.panel1.Controls.Add(this.pictureEdit5);
            this.panel1.Controls.Add(this.ckEjecutarNETUSE);
            this.panel1.Controls.Add(this.txtIP);
            this.panel1.Controls.Add(this.ckRemoto);
            this.panel1.Controls.Add(this.txtPuerto);
            this.panel1.Controls.Add(this.labelControl18);
            this.panel1.Controls.Add(this.labelControl19);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(20, 20, 0, 0);
            this.panel1.Size = new System.Drawing.Size(632, 380);
            this.panel1.TabIndex = 0;
            // 
            // pictureEdit1
            // 
            this.pictureEdit1.EditValue = global::iGasWizardVolumetricos.Properties.Resources.icono_igas;
            this.pictureEdit1.Location = new System.Drawing.Point(553, 336);
            this.pictureEdit1.Name = "pictureEdit1";
            this.pictureEdit1.Properties.AllowFocused = false;
            this.pictureEdit1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.pictureEdit1.Properties.Appearance.Options.UseBackColor = true;
            this.pictureEdit1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pictureEdit1.Properties.ShowMenu = false;
            this.pictureEdit1.Size = new System.Drawing.Size(79, 44);
            this.pictureEdit1.TabIndex = 9;
            // 
            // txtImpresora
            // 
            this.txtImpresora.Location = new System.Drawing.Point(25, 93);
            this.txtImpresora.Name = "txtImpresora";
            this.txtImpresora.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtImpresora.Size = new System.Drawing.Size(286, 20);
            this.txtImpresora.TabIndex = 3;
            // 
            // pictureEdit5
            // 
            this.pictureEdit5.EditValue = global::iGasWizardVolumetricos.Properties.Resources.printer2;
            this.pictureEdit5.Location = new System.Drawing.Point(434, 51);
            this.pictureEdit5.Name = "pictureEdit5";
            this.pictureEdit5.Properties.AllowFocused = false;
            this.pictureEdit5.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.pictureEdit5.Properties.Appearance.Options.UseBackColor = true;
            this.pictureEdit5.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pictureEdit5.Properties.ShowMenu = false;
            this.pictureEdit5.Size = new System.Drawing.Size(179, 183);
            this.pictureEdit5.TabIndex = 8;
            // 
            // ckEjecutarNETUSE
            // 
            this.ckEjecutarNETUSE.Location = new System.Drawing.Point(23, 190);
            this.ckEjecutarNETUSE.Name = "ckEjecutarNETUSE";
            this.ckEjecutarNETUSE.Properties.Caption = "Ejecutar NET USE";
            this.ckEjecutarNETUSE.Size = new System.Drawing.Size(109, 19);
            this.ckEjecutarNETUSE.TabIndex = 7;
            this.ckEjecutarNETUSE.ToolTip = "Al habilitar este control permite que de manera automática se registre en el sist" +
                "ema de Windows la redirección de la\nimpresora remota. De lo contrario, solo se g" +
                "uardará la ruta de la impresora.";
            this.ckEjecutarNETUSE.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            this.ckEjecutarNETUSE.ToolTipTitle = "Uso de NET USE";
            // 
            // txtIP
            // 
            this.txtIP.Location = new System.Drawing.Point(25, 48);
            this.txtIP.Name = "txtIP";
            this.txtIP.Properties.Mask.AutoComplete = DevExpress.XtraEditors.Mask.AutoCompleteType.Strong;
            this.txtIP.Properties.Mask.BeepOnError = true;
            this.txtIP.Properties.Mask.EditMask = "(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.(" +
                "25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)";
            this.txtIP.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtIP.Properties.Mask.ShowPlaceHolders = false;
            this.txtIP.Size = new System.Drawing.Size(112, 20);
            this.txtIP.TabIndex = 1;
            // 
            // ckRemoto
            // 
            this.ckRemoto.Location = new System.Drawing.Point(23, 23);
            this.ckRemoto.Name = "ckRemoto";
            this.ckRemoto.Properties.Caption = "Remoto";
            this.ckRemoto.Size = new System.Drawing.Size(75, 19);
            this.ckRemoto.TabIndex = 0;
            // 
            // txtPuerto
            // 
            this.txtPuerto.Location = new System.Drawing.Point(77, 138);
            this.txtPuerto.Name = "txtPuerto";
            this.txtPuerto.Properties.Mask.BeepOnError = true;
            this.txtPuerto.Properties.Mask.EditMask = "[0-9]{1,3}";
            this.txtPuerto.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtPuerto.Properties.Mask.ShowPlaceHolders = false;
            this.txtPuerto.Size = new System.Drawing.Size(60, 20);
            this.txtPuerto.TabIndex = 6;
            // 
            // labelControl18
            // 
            this.labelControl18.Location = new System.Drawing.Point(25, 119);
            this.labelControl18.Name = "labelControl18";
            this.labelControl18.Size = new System.Drawing.Size(32, 13);
            this.labelControl18.TabIndex = 4;
            this.labelControl18.Text = "Puerto";
            // 
            // labelControl19
            // 
            this.labelControl19.Location = new System.Drawing.Point(25, 74);
            this.labelControl19.Name = "labelControl19";
            this.labelControl19.Size = new System.Drawing.Size(49, 13);
            this.labelControl19.TabIndex = 2;
            this.labelControl19.Text = "Impresora";
            // 
            // txtTipoPuerto
            // 
            this.txtTipoPuerto.EditValue = "LPT";
            this.txtTipoPuerto.Location = new System.Drawing.Point(25, 138);
            this.txtTipoPuerto.Name = "txtTipoPuerto";
            this.txtTipoPuerto.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtTipoPuerto.Properties.Items.AddRange(new object[] {
            "LPT",
            "COM"});
            this.txtTipoPuerto.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.txtTipoPuerto.Size = new System.Drawing.Size(46, 20);
            this.txtTipoPuerto.TabIndex = 5;
            // 
            // PImpresora
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "PImpresora";
            this.Size = new System.Drawing.Size(632, 380);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtImpresora.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit5.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckEjecutarNETUSE.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIP.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckRemoto.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPuerto.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTipoPuerto.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.PictureEdit pictureEdit1;
        private DevExpress.XtraEditors.ComboBoxEdit txtImpresora;
        private DevExpress.XtraEditors.PictureEdit pictureEdit5;
        private DevExpress.XtraEditors.CheckEdit ckEjecutarNETUSE;
        private DevExpress.XtraEditors.TextEdit txtIP;
        private DevExpress.XtraEditors.CheckEdit ckRemoto;
        private DevExpress.XtraEditors.TextEdit txtPuerto;
        private DevExpress.XtraEditors.LabelControl labelControl18;
        private DevExpress.XtraEditors.LabelControl labelControl19;
        private DevExpress.XtraEditors.ComboBoxEdit txtTipoPuerto;


    }
}
