namespace iGasWizardVolumetricos
{
    partial class Wizard
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Wizard));
            this.wcVolumetricos = new DevExpress.XtraWizard.WizardControl();
            this.welcomeWizardPage1 = new DevExpress.XtraWizard.WelcomeWizardPage();
            this.pictureEdit1 = new DevExpress.XtraEditors.PictureEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.completionWizardPage1 = new DevExpress.XtraWizard.CompletionWizardPage();
            this.pictureEdit4 = new DevExpress.XtraEditors.PictureEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.pictureEdit5 = new DevExpress.XtraEditors.PictureEdit();
            this.toolTip = new DevExpress.Utils.DefaultToolTipController(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.wcVolumetricos)).BeginInit();
            this.wcVolumetricos.SuspendLayout();
            this.welcomeWizardPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).BeginInit();
            this.completionWizardPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit4.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit5.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // wcVolumetricos
            // 
            this.wcVolumetricos.CancelText = "Cancelar";
            this.wcVolumetricos.Controls.Add(this.welcomeWizardPage1);
            this.wcVolumetricos.Controls.Add(this.completionWizardPage1);
            this.wcVolumetricos.FinishText = "Finalizar";
            this.wcVolumetricos.HelpText = "&Ayuda";
            this.wcVolumetricos.ImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.wcVolumetricos.Name = "wcVolumetricos";
            this.wcVolumetricos.NextText = "&Siguiente >";
            this.wcVolumetricos.Pages.AddRange(new DevExpress.XtraWizard.BaseWizardPage[] {
            this.welcomeWizardPage1,
            this.completionWizardPage1});
            this.wcVolumetricos.PreviousText = "< &Anterior";
            this.wcVolumetricos.Text = "Asistente de Configuración";
            this.wcVolumetricos.UseAcceptButton = false;
            this.wcVolumetricos.UseCancelButton = false;
            this.wcVolumetricos.WizardStyle = DevExpress.XtraWizard.WizardStyle.WizardAero;
            // 
            // welcomeWizardPage1
            // 
            this.welcomeWizardPage1.Controls.Add(this.pictureEdit1);
            this.welcomeWizardPage1.Controls.Add(this.labelControl1);
            this.welcomeWizardPage1.IntroductionText = "";
            this.welcomeWizardPage1.Name = "welcomeWizardPage1";
            this.welcomeWizardPage1.Padding = new System.Windows.Forms.Padding(20, 20, 0, 0);
            this.welcomeWizardPage1.ProceedText = "";
            this.welcomeWizardPage1.Size = new System.Drawing.Size(632, 379);
            this.welcomeWizardPage1.Text = "Bienvenido";
            // 
            // pictureEdit1
            // 
            this.pictureEdit1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pictureEdit1.EditValue = global::iGasWizardVolumetricos.Properties.Resources.logo;
            this.pictureEdit1.Location = new System.Drawing.Point(23, 106);
            this.pictureEdit1.Name = "pictureEdit1";
            this.pictureEdit1.Properties.AllowFocused = false;
            this.pictureEdit1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pictureEdit1.Properties.ReadOnly = true;
            this.pictureEdit1.Properties.ShowMenu = false;
            this.pictureEdit1.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            this.pictureEdit1.Size = new System.Drawing.Size(330, 269);
            this.pictureEdit1.TabIndex = 3;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Options.UseTextOptions = true;
            this.labelControl1.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
            this.labelControl1.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.labelControl1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl1.Location = new System.Drawing.Point(23, 23);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(606, 58);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = resources.GetString("labelControl1.Text");
            // 
            // completionWizardPage1
            // 
            this.completionWizardPage1.Controls.Add(this.pictureEdit4);
            this.completionWizardPage1.Controls.Add(this.labelControl3);
            this.completionWizardPage1.Controls.Add(this.pictureEdit5);
            this.completionWizardPage1.FinishText = "";
            this.completionWizardPage1.Name = "completionWizardPage1";
            this.completionWizardPage1.Padding = new System.Windows.Forms.Padding(20, 20, 0, 0);
            this.completionWizardPage1.ProceedText = "";
            this.completionWizardPage1.Size = new System.Drawing.Size(632, 379);
            this.completionWizardPage1.Text = "Proceso Terminado";
            // 
            // pictureEdit4
            // 
            this.pictureEdit4.EditValue = global::iGasWizardVolumetricos.Properties.Resources.gear_ok;
            this.pictureEdit4.Location = new System.Drawing.Point(23, 23);
            this.pictureEdit4.Name = "pictureEdit4";
            this.pictureEdit4.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pictureEdit4.Properties.ReadOnly = true;
            this.pictureEdit4.Properties.ShowMenu = false;
            this.pictureEdit4.Size = new System.Drawing.Size(59, 58);
            this.pictureEdit4.TabIndex = 7;
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Options.UseTextOptions = true;
            this.labelControl3.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
            this.labelControl3.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.labelControl3.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl3.Location = new System.Drawing.Point(88, 23);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(524, 81);
            this.labelControl3.TabIndex = 6;
            this.labelControl3.Text = resources.GetString("labelControl3.Text");
            // 
            // pictureEdit5
            // 
            this.pictureEdit5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureEdit5.EditValue = global::iGasWizardVolumetricos.Properties.Resources.logo;
            this.pictureEdit5.Location = new System.Drawing.Point(302, 110);
            this.pictureEdit5.Name = "pictureEdit5";
            this.pictureEdit5.Properties.AllowFocused = false;
            this.pictureEdit5.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pictureEdit5.Properties.ReadOnly = true;
            this.pictureEdit5.Properties.ShowMenu = false;
            this.pictureEdit5.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            this.pictureEdit5.Size = new System.Drawing.Size(330, 269);
            this.pictureEdit5.TabIndex = 5;
            // 
            // toolTip
            // 
            // 
            // 
            // 
            this.toolTip.DefaultController.AppearanceTitle.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolTip.DefaultController.AppearanceTitle.Options.UseFont = true;
            this.toolTip.DefaultController.AutoPopDelay = 30000;
            this.toolTip.DefaultController.Rounded = true;
            this.toolTip.DefaultController.RoundRadius = 15;
            this.toolTip.DefaultController.ShowBeak = true;
            // 
            // Wizard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(692, 541);
            this.ControlBox = false;
            this.Controls.Add(this.wcVolumetricos);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Wizard";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Instalación de iGas Volumétricos versión 4.3.2.0";
            ((System.ComponentModel.ISupportInitialize)(this.wcVolumetricos)).EndInit();
            this.wcVolumetricos.ResumeLayout(false);
            this.welcomeWizardPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).EndInit();
            this.completionWizardPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit4.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit5.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraWizard.WizardControl wcVolumetricos;
        private DevExpress.XtraWizard.WelcomeWizardPage welcomeWizardPage1;
        private DevExpress.XtraWizard.CompletionWizardPage completionWizardPage1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.PictureEdit pictureEdit1;
        private DevExpress.XtraEditors.PictureEdit pictureEdit4;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.PictureEdit pictureEdit5;
        private DevExpress.Utils.DefaultToolTipController toolTip;
    }
}