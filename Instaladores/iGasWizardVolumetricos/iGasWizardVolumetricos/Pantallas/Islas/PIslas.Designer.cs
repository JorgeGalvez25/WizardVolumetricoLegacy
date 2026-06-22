namespace iGasWizardVolumetricos.Pantallas.Islas
{
    partial class PIslas
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
            this.gcIslas = new DevExpress.XtraGrid.GridControl();
            this.gvIslas = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.btnGenerar = new DevExpress.XtraEditors.SimpleButton();
            this.txtIslas = new DevExpress.XtraEditors.SpinEdit();
            this.labelControl16 = new DevExpress.XtraEditors.LabelControl();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcIslas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvIslas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIslas.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.pictureEdit1);
            this.panel1.Controls.Add(this.gcIslas);
            this.panel1.Controls.Add(this.btnGenerar);
            this.panel1.Controls.Add(this.txtIslas);
            this.panel1.Controls.Add(this.labelControl16);
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
            // gcIslas
            // 
            this.gcIslas.EmbeddedNavigator.Name = "";
            this.gcIslas.Location = new System.Drawing.Point(23, 94);
            this.gcIslas.MainView = this.gvIslas;
            this.gcIslas.Name = "gcIslas";
            this.gcIslas.Size = new System.Drawing.Size(196, 283);
            this.gcIslas.TabIndex = 8;
            this.gcIslas.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvIslas});
            // 
            // gvIslas
            // 
            this.gvIslas.GridControl = this.gcIslas;
            this.gvIslas.Name = "gvIslas";
            this.gvIslas.OptionsView.ShowGroupPanel = false;
            // 
            // btnGenerar
            // 
            this.btnGenerar.Location = new System.Drawing.Point(146, 40);
            this.btnGenerar.Name = "btnGenerar";
            this.btnGenerar.Size = new System.Drawing.Size(73, 22);
            this.btnGenerar.TabIndex = 7;
            this.btnGenerar.Text = "&Generar Islas";
            // 
            // txtIslas
            // 
            this.txtIslas.EditValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtIslas.Location = new System.Drawing.Point(23, 42);
            this.txtIslas.Name = "txtIslas";
            this.txtIslas.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtIslas.Properties.Mask.EditMask = "##;##";
            this.txtIslas.Properties.MaxValue = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.txtIslas.Properties.MinValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtIslas.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.txtIslas.Size = new System.Drawing.Size(100, 20);
            this.txtIslas.TabIndex = 6;
            // 
            // labelControl16
            // 
            this.labelControl16.Location = new System.Drawing.Point(23, 23);
            this.labelControl16.Name = "labelControl16";
            this.labelControl16.Size = new System.Drawing.Size(91, 13);
            this.labelControl16.TabIndex = 5;
            this.labelControl16.Text = "Islas en la Estación";
            // 
            // PIslas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "PIslas";
            this.Size = new System.Drawing.Size(632, 380);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcIslas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvIslas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIslas.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.PictureEdit pictureEdit1;
        private DevExpress.XtraGrid.GridControl gcIslas;
        private DevExpress.XtraGrid.Views.Grid.GridView gvIslas;
        private DevExpress.XtraEditors.SimpleButton btnGenerar;
        private DevExpress.XtraEditors.SpinEdit txtIslas;
        private DevExpress.XtraEditors.LabelControl labelControl16;


    }
}
