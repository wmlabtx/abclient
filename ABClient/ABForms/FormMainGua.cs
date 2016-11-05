namespace ABClient.ABForms
{
    using System;

    internal sealed partial class FormMain
    {
        internal void ResetCure()
        {
            switch (AppVars.Autoboi)
            {
                case AutoboiState.Guamod:
                    ChangeAutoboiState(AutoboiState.AutoboiOn);
                    break;
                case AutoboiState.Restoring:
                    AppVars.Profile.Pers.Ready = DateTime.Now.Ticks;
                    break;
            }
        }
    }
}