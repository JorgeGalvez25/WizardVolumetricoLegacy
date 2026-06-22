namespace iGasWizardVolumetricos.Pantallas.Tanques
{
    partial class PTanques
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
            this.btnGenerar = new DevExpress.XtraEditors.SimpleButton();
            this.gcTanques = new DevExpress.XtraGrid.GridControl();
            this.gvTanques = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.txtTanques = new DevExpress.XtraEditors.SpinEdit();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcTanques)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvTanques)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTanques.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.pictureEdit1);
            this.panel1.Controls.Add(this.btnGenerar);
            this.panel1.Controls.Add(this.gcTanques);
            this.panel1.Controls.Add(this.txtTanques);
            this.panel1.Controls.Add(this.labelControl8);
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
            // btnGenerar
            // 
            this.btnGenerar.Location = new System.Drawing.Point(527, 40);
            this.btnGenerar.Name = "btnGenerar";
            this.btnGenerar.Size = new System.Drawing.Size(102, 22);
            this.btnGenerar.TabIndex = 7;
            this.btnGenerar.Text = "&Generar Tanques";
            // 
            // gcTanques
            // 
            this.gcTanques.EmbeddedNavigator.Name = "";
            this.gcTanques.Location = new System.Drawing.Point(23, 94);
            this.gcTanques.MainView = this.gvTanques;
            this.gcTanques.Name = "gcTanques";
            this.gcTanques.Size = new System.Drawing.Size(606, 236);
            this.gcTanques.TabIndex = 8;
            this.gcTanques.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvTanques});
            // 
            // gvTanques
            // 
            this.gvTanques.GridControl = this.gcTanques;
            this.gvTanques.Name = "gvTanques";
            this.gvTanques.OptionsView.ShowGroupPanel = false;
            // 
            // txtTanques
            // 
            this.txtTanques.EditValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtTanques.Location = new System.Drawing.Point(23, 42);
            this.txtTanques.Name = "txtTanques";
            this.txtTanques.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtTanques.Properties.Mask.EditMask = "##;##";
            this.txtTanques.Properties.MaxValue = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.txtTanques.Properties.MinValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtTanques.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.txtTanques.Size = new System.Drawing.Size(100, 20);
            this.txtTanques.TabIndex = 6;
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(23, 23);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(110, 13);
            this.labelControl8.TabIndex = 5;
            this.labelControl8.Text = "Tanques en la Estación";
            // 
            // PTanques
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "PTanques";
            this.Size = new System.Drawing.Size(632, 380);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcTanques)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvTanques)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTanques.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.PictureEdit pictureEdit1;
        private DevExpress.XtraEditors.SimpleButton btnGenerar;
        private DevExpress.XtraGrid.GridControl gcTanques;
        private DevExpress.XtraGrid.Views.Grid.GridView gvTanques;
        private DevExpress.XtraEditors.SpinEdit txtTanques;
        private DevExpress.XtraEditors.LabelControl labelControl8;



    }
}
