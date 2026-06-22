namespace iGasWizardVolumetricos.Pantallas.Gilbarco
{
    partial class PGilbarco
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
            this.pictureEdit2 = new DevExpress.XtraEditors.PictureEdit();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.txtDecimalesPreset = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtDecimalesPresetLitros = new DevExpress.XtraEditors.ComboBoxEdit();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.txtConDigitoAjuste = new DevExpress.XtraEditors.ComboBoxEdit();
            this.txtDigitoAjusteVol = new DevExpress.XtraEditors.ComboBoxEdit();
            this.pictureEdit1 = new DevExpress.XtraEditors.PictureEdit();
            this.txtDigitoGilbarco = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDecimalesPreset.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDecimalesPresetLitros.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtConDigitoAjuste.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDigitoAjusteVol.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDigitoGilbarco.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.pictureEdit2);
            this.panel1.Controls.Add(this.groupControl2);
            this.panel1.Controls.Add(this.groupControl1);
            this.panel1.Controls.Add(this.pictureEdit1);
            this.panel1.Controls.Add(this.txtDigitoGilbarco);
            this.panel1.Controls.Add(this.labelControl8);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(20, 20, 0, 0);
            this.panel1.Size = new System.Drawing.Size(632, 380);
            this.panel1.TabIndex = 0;
            // 
            // pictureEdit2
            // 
            this.pictureEdit2.EditValue = global::iGasWizardVolumetricos.Properties.Resources.information_16x16;
            this.pictureEdit2.Location = new System.Drawing.Point(157, 206);
            this.pictureEdit2.Name = "pictureEdit2";
            this.pictureEdit2.Properties.AllowFocused = false;
            this.pictureEdit2.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.pictureEdit2.Properties.Appearance.Options.UseBackColor = true;
            this.pictureEdit2.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pictureEdit2.Properties.ShowMenu = false;
            this.pictureEdit2.Size = new System.Drawing.Size(18, 18);
            this.pictureEdit2.TabIndex = 4;
            this.pictureEdit2.ToolTip = "Utilizada en ventas PREAUTORIZADAS (Preset) para definir los dígitos totales de l" +
                "a\nmáscara del importe del dispensario (incluye parte entera y decimales). ";
            this.pictureEdit2.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            this.pictureEdit2.ToolTipTitle = "DigitosGilbarco";
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.labelControl4);
            this.groupControl2.Controls.Add(this.txtDecimalesPreset);
            this.groupControl2.Controls.Add(this.labelControl2);
            this.groupControl2.Controls.Add(this.txtDecimalesPresetLitros);
            this.groupControl2.Location = new System.Drawing.Point(23, 23);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(262, 67);
            this.groupControl2.TabIndex = 0;
            this.groupControl2.Text = "Formatos para prefijar una venta";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(5, 23);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(38, 13);
            this.labelControl4.TabIndex = 0;
            this.labelControl4.Text = "Importe";
            // 
            // txtDecimalesPreset
            // 
            this.txtDecimalesPreset.EditValue = "000000.00";
            this.txtDecimalesPreset.Location = new System.Drawing.Point(5, 42);
            this.txtDecimalesPreset.Name = "txtDecimalesPreset";
            this.txtDecimalesPreset.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtDecimalesPreset.Properties.Items.AddRange(new object[] {
            "00000000",
            "0000000.0",
            "000000.00"});
            this.txtDecimalesPreset.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.txtDecimalesPreset.Size = new System.Drawing.Size(123, 20);
            this.txtDecimalesPreset.TabIndex = 1;
            this.txtDecimalesPreset.ToolTip = "Posición del punto decimal al enviar una venta prefijada en pesos.";
            this.txtDecimalesPreset.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            this.txtDecimalesPreset.ToolTipTitle = "DecimalesPresetPAM";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(139, 23);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(26, 13);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "Litros";
            // 
            // txtDecimalesPresetLitros
            // 
            this.txtDecimalesPresetLitros.EditValue = "000000.00";
            this.txtDecimalesPresetLitros.Location = new System.Drawing.Point(139, 42);
            this.txtDecimalesPresetLitros.Name = "txtDecimalesPresetLitros";
            this.txtDecimalesPresetLitros.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtDecimalesPresetLitros.Properties.Items.AddRange(new object[] {
            "00000000",
            "0000000.0",
            "000000.00",
            "00000.000"});
            this.txtDecimalesPresetLitros.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.txtDecimalesPresetLitros.Size = new System.Drawing.Size(118, 20);
            this.txtDecimalesPresetLitros.TabIndex = 3;
            this.txtDecimalesPresetLitros.ToolTip = "Posición del punto decimal al enviar una venta prefijada en litros.";
            this.txtDecimalesPresetLitros.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            this.txtDecimalesPresetLitros.ToolTipTitle = "DecimalesPresetPAMLitros";
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.labelControl7);
            this.groupControl1.Controls.Add(this.labelControl6);
            this.groupControl1.Controls.Add(this.txtConDigitoAjuste);
            this.groupControl1.Controls.Add(this.txtDigitoAjusteVol);
            this.groupControl1.Location = new System.Drawing.Point(23, 105);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(262, 67);
            this.groupControl1.TabIndex = 1;
            this.groupControl1.Text = "Formatos para lectura de ventas";
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(139, 23);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(26, 13);
            this.labelControl7.TabIndex = 2;
            this.labelControl7.Text = "Litros";
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(5, 23);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(38, 13);
            this.labelControl6.TabIndex = 0;
            this.labelControl6.Text = "Importe";
            // 
            // txtConDigitoAjuste
            // 
            this.txtConDigitoAjuste.EditValue = "00000.000";
            this.txtConDigitoAjuste.Location = new System.Drawing.Point(5, 42);
            this.txtConDigitoAjuste.Name = "txtConDigitoAjuste";
            this.txtConDigitoAjuste.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtConDigitoAjuste.Properties.Items.AddRange(new object[] {
            "00000.000",
            "000000.00",
            "0000000.0"});
            this.txtConDigitoAjuste.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.txtConDigitoAjuste.Size = new System.Drawing.Size(123, 20);
            this.txtConDigitoAjuste.TabIndex = 1;
            this.txtConDigitoAjuste.ToolTip = "Posición del punto decimal en la lectura del importe de venta.";
            this.txtConDigitoAjuste.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            this.txtConDigitoAjuste.ToolTipTitle = "Con_DigitoAjuste";
            // 
            // txtDigitoAjusteVol
            // 
            this.txtDigitoAjusteVol.EditValue = "00000.000";
            this.txtDigitoAjusteVol.Location = new System.Drawing.Point(139, 42);
            this.txtDigitoAjusteVol.Name = "txtDigitoAjusteVol";
            this.txtDigitoAjusteVol.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtDigitoAjusteVol.Properties.Items.AddRange(new object[] {
            "00000.000",
            "000000.00"});
            this.txtDigitoAjusteVol.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.txtDigitoAjusteVol.Size = new System.Drawing.Size(118, 20);
            this.txtDigitoAjusteVol.TabIndex = 3;
            this.txtDigitoAjusteVol.ToolTip = "Posición del punto decimal en la lectura del volumen de venta.";
            this.txtDigitoAjusteVol.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            this.txtDigitoAjusteVol.ToolTipTitle = "DigitoAjusteVol";
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
            this.pictureEdit1.TabIndex = 5;
            // 
            // txtDigitoGilbarco
            // 
            this.txtDigitoGilbarco.EditValue = "000.00";
            this.txtDigitoGilbarco.Location = new System.Drawing.Point(23, 206);
            this.txtDigitoGilbarco.Name = "txtDigitoGilbarco";
            this.txtDigitoGilbarco.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtDigitoGilbarco.Properties.Items.AddRange(new object[] {
            "000.00",
            "0000.00"});
            this.txtDigitoGilbarco.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.txtDigitoGilbarco.Size = new System.Drawing.Size(128, 20);
            this.txtDigitoGilbarco.TabIndex = 3;
            this.txtDigitoGilbarco.ToolTip = "Utilizada en ventas PREAUTORIZADAS (Preset) para definir los dígitos totales de l" +
                "a\nmáscara del importe del dispensario (incluye parte entera y decimales). ";
            this.txtDigitoGilbarco.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            this.txtDigitoGilbarco.ToolTipTitle = "DigitosGilbarco";
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(23, 187);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(174, 13);
            this.labelControl8.TabIndex = 2;
            this.labelControl8.Text = "Dígitos configurados en dispensarios";
            // 
            // PGilbarco
            // 
            this.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "PGilbarco";
            this.Size = new System.Drawing.Size(632, 380);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.groupControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDecimalesPreset.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDecimalesPresetLitros.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtConDigitoAjuste.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDigitoAjusteVol.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDigitoGilbarco.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.ComboBoxEdit txtConDigitoAjuste;
        private DevExpress.XtraEditors.ComboBoxEdit txtDigitoAjusteVol;
        private DevExpress.XtraEditors.PictureEdit pictureEdit1;
        private DevExpress.XtraEditors.ComboBoxEdit txtDigitoGilbarco;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.ComboBoxEdit txtDecimalesPresetLitros;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.ComboBoxEdit txtDecimalesPreset;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.PictureEdit pictureEdit2;


    }
}
