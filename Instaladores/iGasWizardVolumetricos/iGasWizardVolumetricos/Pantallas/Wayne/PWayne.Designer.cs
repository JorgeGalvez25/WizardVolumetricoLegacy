namespace iGasWizardVolumetricos.Pantallas.Wayne
{
    partial class PWayne
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
            this.gcCombustibles = new DevExpress.XtraGrid.GridControl();
            this.gvCombustibles = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.txtConDigitoAjuste = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.txtDecimalesPresetLitros = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtDecimalesPreset = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcCombustibles)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvCombustibles)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtConDigitoAjuste.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDecimalesPresetLitros.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDecimalesPreset.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.groupControl1);
            this.panel1.Controls.Add(this.pictureEdit1);
            this.panel1.Controls.Add(this.gcCombustibles);
            this.panel1.Controls.Add(this.txtConDigitoAjuste);
            this.panel1.Controls.Add(this.labelControl4);
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
            this.pictureEdit1.TabIndex = 4;
            // 
            // gcCombustibles
            // 
            this.gcCombustibles.EmbeddedNavigator.Name = "";
            this.gcCombustibles.Location = new System.Drawing.Point(23, 160);
            this.gcCombustibles.MainView = this.gvCombustibles;
            this.gcCombustibles.Name = "gcCombustibles";
            this.gcCombustibles.Size = new System.Drawing.Size(241, 115);
            this.gcCombustibles.TabIndex = 3;
            this.gcCombustibles.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvCombustibles});
            // 
            // gvCombustibles
            // 
            this.gvCombustibles.GridControl = this.gcCombustibles;
            this.gvCombustibles.Name = "gvCombustibles";
            this.gvCombustibles.OptionsView.ShowGroupPanel = false;
            // 
            // txtConDigitoAjuste
            // 
            this.txtConDigitoAjuste.EditValue = "000000.00";
            this.txtConDigitoAjuste.Location = new System.Drawing.Point(23, 125);
            this.txtConDigitoAjuste.Name = "txtConDigitoAjuste";
            this.txtConDigitoAjuste.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtConDigitoAjuste.Properties.Items.AddRange(new object[] {
            "00000.000",
            "000000.00",
            "0000000.0"});
            this.txtConDigitoAjuste.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.txtConDigitoAjuste.Size = new System.Drawing.Size(128, 20);
            this.txtConDigitoAjuste.TabIndex = 2;
            this.txtConDigitoAjuste.ToolTip = "Posición del punto decimal en la lectura del importe de la venta.";
            this.txtConDigitoAjuste.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            this.txtConDigitoAjuste.ToolTipTitle = "Con_DigitoAjuste";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(23, 106);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(215, 13);
            this.labelControl4.TabIndex = 1;
            this.labelControl4.Text = "Formato de la lectura del importe de la venta";
            // 
            // txtDecimalesPresetLitros
            // 
            this.txtDecimalesPresetLitros.EditValue = "000000.00";
            this.txtDecimalesPresetLitros.Location = new System.Drawing.Point(139, 42);
            this.txtDecimalesPresetLitros.Name = "txtDecimalesPresetLitros";
            this.txtDecimalesPresetLitros.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtDecimalesPresetLitros.Properties.Items.AddRange(new object[] {
            "0000000.0",
            "000000.00",
            "00000.000",
            "0000.0000"});
            this.txtDecimalesPresetLitros.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.txtDecimalesPresetLitros.Size = new System.Drawing.Size(118, 20);
            this.txtDecimalesPresetLitros.TabIndex = 3;
            this.txtDecimalesPresetLitros.ToolTip = "Posición del punto decimal al enviar una venta prefijada en litros.";
            this.txtDecimalesPresetLitros.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            this.txtDecimalesPresetLitros.ToolTipTitle = "DecimalesPresetWayneLitros";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(139, 23);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(26, 13);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "Litros";
            // 
            // txtDecimalesPreset
            // 
            this.txtDecimalesPreset.EditValue = "000000.00";
            this.txtDecimalesPreset.Location = new System.Drawing.Point(5, 42);
            this.txtDecimalesPreset.Name = "txtDecimalesPreset";
            this.txtDecimalesPreset.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtDecimalesPreset.Properties.Items.AddRange(new object[] {
            "0000000.0",
            "000000.00",
            "00000.000"});
            this.txtDecimalesPreset.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.txtDecimalesPreset.Size = new System.Drawing.Size(123, 20);
            this.txtDecimalesPreset.TabIndex = 1;
            this.txtDecimalesPreset.ToolTip = "Posición del punto decimal al enviar una venta prefijada en pesos.";
            this.txtDecimalesPreset.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            this.txtDecimalesPreset.ToolTipTitle = "DecimalesPresetWayne";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(5, 23);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(38, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Importe";
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Controls.Add(this.txtDecimalesPreset);
            this.groupControl1.Controls.Add(this.txtDecimalesPresetLitros);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Location = new System.Drawing.Point(23, 23);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(262, 67);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "Formatos para prefijar una venta";
            // 
            // PWayne
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "PWayne";
            this.Size = new System.Drawing.Size(632, 380);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcCombustibles)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvCombustibles)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtConDigitoAjuste.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDecimalesPresetLitros.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDecimalesPreset.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.PictureEdit pictureEdit1;
        private DevExpress.XtraGrid.GridControl gcCombustibles;
        private DevExpress.XtraGrid.Views.Grid.GridView gvCombustibles;
        private DevExpress.XtraEditors.ComboBoxEdit txtConDigitoAjuste;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.ComboBoxEdit txtDecimalesPresetLitros;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.ComboBoxEdit txtDecimalesPreset;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.GroupControl groupControl1;


    }
}
