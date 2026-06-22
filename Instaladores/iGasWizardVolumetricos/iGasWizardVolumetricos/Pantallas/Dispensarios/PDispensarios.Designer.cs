namespace iGasWizardVolumetricos.Pantallas.Dispensarios
{
    partial class PDispensarios
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
            this.btnAplicar = new DevExpress.XtraEditors.SimpleButton();
            this.gcDispensarios = new DevExpress.XtraGrid.GridControl();
            this.gvDispensarios = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.txtPosiciones = new DevExpress.XtraEditors.SpinEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcDispensarios)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDispensarios)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPosiciones.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.pictureEdit1);
            this.panel1.Controls.Add(this.btnAplicar);
            this.panel1.Controls.Add(this.gcDispensarios);
            this.panel1.Controls.Add(this.txtPosiciones);
            this.panel1.Controls.Add(this.labelControl1);
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
            // btnAplicar
            // 
            this.btnAplicar.Location = new System.Drawing.Point(273, 40);
            this.btnAplicar.Name = "btnAplicar";
            this.btnAplicar.Size = new System.Drawing.Size(66, 22);
            this.btnAplicar.TabIndex = 7;
            this.btnAplicar.Text = "&Aplicar";
            // 
            // gcDispensarios
            // 
            this.gcDispensarios.EmbeddedNavigator.Name = "";
            this.gcDispensarios.Location = new System.Drawing.Point(23, 93);
            this.gcDispensarios.MainView = this.gvDispensarios;
            this.gcDispensarios.Name = "gcDispensarios";
            this.gcDispensarios.Size = new System.Drawing.Size(316, 283);
            this.gcDispensarios.TabIndex = 8;
            this.gcDispensarios.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvDispensarios});
            // 
            // gvDispensarios
            // 
            this.gvDispensarios.GridControl = this.gcDispensarios;
            this.gvDispensarios.Name = "gvDispensarios";
            this.gvDispensarios.OptionsView.ShowGroupPanel = false;
            // 
            // txtPosiciones
            // 
            this.txtPosiciones.EditValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtPosiciones.Location = new System.Drawing.Point(23, 42);
            this.txtPosiciones.Name = "txtPosiciones";
            this.txtPosiciones.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtPosiciones.Properties.Mask.EditMask = "##;##";
            this.txtPosiciones.Properties.MaxValue = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.txtPosiciones.Properties.MinValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtPosiciones.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.txtPosiciones.Size = new System.Drawing.Size(100, 20);
            this.txtPosiciones.TabIndex = 6;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(23, 23);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(173, 13);
            this.labelControl1.TabIndex = 5;
            this.labelControl1.Text = "Posiciones de Carga por Dispensario";
            // 
            // PDispensarios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "PDispensarios";
            this.Size = new System.Drawing.Size(632, 380);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcDispensarios)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDispensarios)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPosiciones.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.PictureEdit pictureEdit1;
        private DevExpress.XtraEditors.SimpleButton btnAplicar;
        private DevExpress.XtraGrid.GridControl gcDispensarios;
        private DevExpress.XtraGrid.Views.Grid.GridView gvDispensarios;
        private DevExpress.XtraEditors.SpinEdit txtPosiciones;
        private DevExpress.XtraEditors.LabelControl labelControl1;


    }
}
