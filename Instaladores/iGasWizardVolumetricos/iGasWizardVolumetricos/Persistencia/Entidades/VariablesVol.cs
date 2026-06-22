namespace iGasWizardVolumetricos.Persistencia.Entidades
{
    public class VariablesVol
    {
        public VariablesVol()
        {
            this.DecimalesPresetWayne = 0;
            this.DecimalesPresetWayneLitros = 0;
            this.AjusteWayne = false;
            this.NivelPrecioWayne = 0;
            this.ConDigitoAjuste = 0;
            this.ConProductoPrecio = new ListaCombustibleVol();
            this.SoportaSeleccionProducto = true;
            this.SetUpPam1000 = null;
            this.DecimalesPresetPAM = 0;
            this.DecimalesPresetPAMLitros = 0;
            this.MaximoPresetPAM = 0;
            this.AjustePam = false;
            this.ModoAutorizaPam = 0;
            this.DigitoAjusteVol = 0;
            this.DigitosGilbarco = 0;
        }

        public int DecimalesPresetWayne { get; set; }
        public int DecimalesPresetWayneLitros { get; set; }
        public bool AjusteWayne { get; set; }
        public int NivelPrecioWayne { get; set; }
        public int ConDigitoAjuste { get; set; }
        public ListaCombustibleVol ConProductoPrecio { get; set; }
        public bool SoportaSeleccionProducto { get; set; }
        public int? SetUpPam1000 { get; set; }
        public int DecimalesPresetPAM { get; set; }
        public int DecimalesPresetPAMLitros { get; set; }
        public int MaximoPresetPAM { get; set; }
        public bool AjustePam { get; set; }
        public int ModoAutorizaPam { get; set; }
        public int DigitoAjusteVol { get; set; }
        public int DigitosGilbarco { get; set; }

    }
}
