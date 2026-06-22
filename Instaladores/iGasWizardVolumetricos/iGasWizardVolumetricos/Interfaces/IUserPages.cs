using System.Windows.Forms;
using DevExpress.XtraWizard;

namespace iGasWizardVolumetricos.Interfaces
{
    public interface IUserPages
    {
        void DoInit(BaseWizardPage parent);
        void NextClick(object sender, WizardCommandButtonClickEventArgs e);
        void PrevClick(object sender, WizardCommandButtonClickEventArgs e);
        void CancelClick(FormClosingEventArgs e);
    }
}
