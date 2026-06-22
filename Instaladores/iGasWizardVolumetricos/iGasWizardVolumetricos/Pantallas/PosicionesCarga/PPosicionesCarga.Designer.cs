namespace iGasWizardVolumetricos.Pantallas.PosicionesCarga
{
    partial class PPosicionesCarga
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
            this.pictureEdit3 = new DevExpress.XtraEditors.PictureEdit();
            this.btnAplicar = new DevExpress.XtraEditors.SimpleButton();
            this.txtMangueras = new DevExpress.XtraEditors.SpinEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.gcPosicion = new DevExpress.XtraGrid.GridControl();
            this.gvPosicion = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit3.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMangueras.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcPosicion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvPosicion)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.pictureEdit1);
            this.panel1.Controls.Add(this.pictureEdit3);
            this.panel1.Controls.Add(this.btnAplicar);
            this.panel1.Controls.Add(this.txtMangueras);
            this.panel1.Controls.Add(this.labelControl2);
            this.panel1.Controls.Add(this.gcPosicion);
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
            this.pictureEdit1.TabIndex = 11;
            // 
            // pictureEdit3
            // 
            this.pictureEdit3.EditValue = global::iGasWizardVolumetricos.Properties.Resources.mangueras_157x104;
            this.pictureEdit3.Location = new System.Drawing.Point(472, 94);
            this.pictureEdit3.Name = "pictureEdit3";
            this.pictureEdit3.Properties.AllowFocused = false;
            this.pictureEdit3.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.pictureEdit3.Properties.Appearance.Options.UseBackColor = true;
            this.pictureEdit3.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pictureEdit3.Properties.ShowMenu = false;
            this.pictureEdit3.Size = new System.Drawing.Size(157, 104);
            this.pictureEdit3.TabIndex = 10;
            // 
            // btnAplicar
            // 
            this.btnAplicar.Location = new System.Drawing.Point(403, 45);
            this.btnAplicar.Name = "btnAplicar";
            this.btnAplicar.Size = new System.Drawing.Size(66, 22);
            this.btnAplicar.TabIndex = 8;
            this.btnAplicar.Text = "&Aplicar";
            // 
            // txtMangueras
            // 
            this.txtMangueras.EditValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtMangueras.Location = new System.Drawing.Point(23, 42);
            this.txtMangueras.Name = "txtMangueras";
            this.txtMangueras.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtMangueras.Properties.Mask.EditMask = "##;##";
            this.txtMangueras.Properties.MaxValue = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.txtMangueras.Properties.MinValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtMangueras.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.txtMangueras.Size = new System.Drawing.Size(100, 20);
            this.txtMangueras.TabIndex = 7;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(23, 23);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(160, 13);
            this.labelControl2.TabIndex = 6;
            this.labelControl2.Text = "Mangueras por Posición de Carga";
            // 
            // gcPosicion
            // 
            this.gcPosicion.EmbeddedNavigator.Name = "";
            this.gcPosicion.Location = new System.Drawing.Point(23, 94);
            this.gcPosicion.MainView = this.gvPosicion;
            this.gcPosicion.Name = "gcPosicion";
            this.gcPosicion.Size = new System.Drawing.Size(446, 283);
            this.gcPosicion.TabIndex = 9;
            this.gcPosicion.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvPosicion});
            // 
            // gvPosicion
            // 
            this.gvPosicion.GridControl = this.gcPosicion;
            this.gvPosicion.Name = "gvPosicion";
            this.gvPosicion.OptionsView.ShowGroupPanel = false;
            // 
            // PPosicionesCarga
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "PPosicionesCarga";
            this.Size = new System.Drawing.Size(632, 380);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit3.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMangueras.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcPosicion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvPosicion)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.PictureEdit pictureEdit1;
        private DevExpress.XtraEditors.PictureEdit pictureEdit3;
        private DevExpress.XtraEditors.SimpleButton btnAplicar;
        private DevExpress.XtraEditors.SpinEdit txtMangueras;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraGrid.GridControl gcPosicion;
        private DevExpress.XtraGrid.Views.Grid.GridView gvPosicion;


    }
}
