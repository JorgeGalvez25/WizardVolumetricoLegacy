namespace iGasWizardVolumetricos.Pantallas.Requerimientos
{
    partial class PRequerimientos
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
            this.gcRequerimientos = new DevExpress.XtraGrid.GridControl();
            this.gvRequerimientos = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.picAnimacion = new DevExpress.XtraEditors.PictureEdit();
            this.lbMensaje = new DevExpress.XtraEditors.LabelControl();
            this.btnInstalar = new DevExpress.XtraEditors.SimpleButton();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcRequerimientos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvRequerimientos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picAnimacion.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.btnInstalar);
            this.panel1.Controls.Add(this.pictureEdit1);
            this.panel1.Controls.Add(this.gcRequerimientos);
            this.panel1.Controls.Add(this.picAnimacion);
            this.panel1.Controls.Add(this.lbMensaje);
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
            this.pictureEdit1.TabIndex = 3;
            // 
            // gcRequerimientos
            // 
            this.gcRequerimientos.EmbeddedNavigator.Name = "";
            this.gcRequerimientos.Location = new System.Drawing.Point(23, 110);
            this.gcRequerimientos.MainView = this.gvRequerimientos;
            this.gcRequerimientos.Name = "gcRequerimientos";
            this.gcRequerimientos.Size = new System.Drawing.Size(400, 108);
            this.gcRequerimientos.TabIndex = 2;
            this.gcRequerimientos.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvRequerimientos});
            // 
            // gvRequerimientos
            // 
            this.gvRequerimientos.GridControl = this.gcRequerimientos;
            this.gvRequerimientos.Name = "gvRequerimientos";
            // 
            // picAnimacion
            // 
            this.picAnimacion.EditValue = global::iGasWizardVolumetricos.Properties.Resources.loading;
            this.picAnimacion.Location = new System.Drawing.Point(549, 23);
            this.picAnimacion.Name = "picAnimacion";
            this.picAnimacion.Properties.AllowFocused = false;
            this.picAnimacion.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.picAnimacion.Properties.Appearance.Options.UseBackColor = true;
            this.picAnimacion.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.picAnimacion.Properties.ShowMenu = false;
            this.picAnimacion.Size = new System.Drawing.Size(80, 80);
            this.picAnimacion.TabIndex = 1;
            // 
            // lbMensaje
            // 
            this.lbMensaje.Location = new System.Drawing.Point(23, 23);
            this.lbMensaje.Name = "lbMensaje";
            this.lbMensaje.Size = new System.Drawing.Size(40, 39);
            this.lbMensaje.TabIndex = 0;
            this.lbMensaje.Text = "Mensaje\r\n\r\nMensaje";
            // 
            // btnInstalar
            // 
            this.btnInstalar.Location = new System.Drawing.Point(348, 233);
            this.btnInstalar.Name = "btnInstalar";
            this.btnInstalar.Size = new System.Drawing.Size(75, 23);
            this.btnInstalar.TabIndex = 4;
            this.btnInstalar.Text = "&Instalar";
            // 
            // PRequerimientos
            // 
            this.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "PRequerimientos";
            this.Size = new System.Drawing.Size(632, 380);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcRequerimientos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvRequerimientos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picAnimacion.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.PictureEdit pictureEdit1;
        private DevExpress.XtraGrid.GridControl gcRequerimientos;
        private DevExpress.XtraGrid.Views.Grid.GridView gvRequerimientos;
        private DevExpress.XtraEditors.PictureEdit picAnimacion;
        private DevExpress.XtraEditors.LabelControl lbMensaje;
        private DevExpress.XtraEditors.SimpleButton btnInstalar;


    }
}
