namespace iGasWizardVolumetricos.Pantallas.Mangueras
{
    partial class PMangueras
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
            this.btnReplicar = new DevExpress.XtraEditors.SimpleButton();
            this.gcMangueras = new DevExpress.XtraGrid.GridControl();
            this.gvMangueras = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMangueras)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMangueras)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.pictureEdit1);
            this.panel1.Controls.Add(this.btnReplicar);
            this.panel1.Controls.Add(this.gcMangueras);
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
            this.pictureEdit1.TabIndex = 5;
            // 
            // btnReplicar
            // 
            this.btnReplicar.Location = new System.Drawing.Point(563, 23);
            this.btnReplicar.Name = "btnReplicar";
            this.btnReplicar.Size = new System.Drawing.Size(66, 22);
            this.btnReplicar.TabIndex = 3;
            this.btnReplicar.Text = "&Replicar";
            // 
            // gcMangueras
            // 
            this.gcMangueras.EmbeddedNavigator.Name = "";
            this.gcMangueras.Location = new System.Drawing.Point(23, 51);
            this.gcMangueras.MainView = this.gvMangueras;
            this.gcMangueras.Name = "gcMangueras";
            this.gcMangueras.Size = new System.Drawing.Size(606, 278);
            this.gcMangueras.TabIndex = 4;
            this.gcMangueras.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvMangueras});
            // 
            // gvMangueras
            // 
            this.gvMangueras.GridControl = this.gcMangueras;
            this.gvMangueras.Name = "gvMangueras";
            this.gvMangueras.OptionsView.ShowGroupPanel = false;
            // 
            // PMangueras
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "PMangueras";
            this.Size = new System.Drawing.Size(632, 380);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMangueras)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMangueras)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.PictureEdit pictureEdit1;
        private DevExpress.XtraEditors.SimpleButton btnReplicar;
        private DevExpress.XtraGrid.GridControl gcMangueras;
        private DevExpress.XtraGrid.Views.Grid.GridView gvMangueras;


    }
}
