using System;
using mRemoteNG.Config;

namespace mRemoteNG.UI.Forms.OptionsPages
{
    public partial class ConnectionsPage
    {
        private FrmMain _frmMain = FrmMain.Default;

        public ConnectionsPage()
        {
            InitializeComponent();
        }

        public override string PageName
        {
            get { return Language.strConnections; }
            set { }
        }

        public override void ApplyLanguage()
        {
            base.ApplyLanguage();

            chkSingleClickOnConnectionOpensIt.Text = Language.strSingleClickOnConnectionOpensIt;
            chkSingleClickOnOpenedConnectionSwitchesToIt.Text = Language.strSingleClickOnOpenConnectionSwitchesToIt;
            chkHostnameLikeDisplayName.Text = Language.strSetHostnameLikeDisplayName;

            lblRdpReconnectionCount.Text = Language.strRdpReconnectCount;

            lblRDPConTimeout.Text = Language.strRDPOverallConnectionTimeout;

            lblAutoSave1.Text = Language.strAutoSaveEvery;
            lblAutoSave2.Text = Language.strAutoSaveMins;

            lblClosingConnections.Text = Language.strLabelClosingConnections;
            radCloseWarnAll.Text = Language.strRadioCloseWarnAll;
            radCloseWarnMultiple.Text = Language.strRadioCloseWarnMultiple;
            radCloseWarnExit.Text = Language.strRadioCloseWarnExit;
            radCloseWarnNever.Text = Language.strRadioCloseWarnNever;
        }

        public override void LoadSettings()
        {
            base.SaveSettings();

            chkSingleClickOnConnectionOpensIt.Checked = Settings.Default.SingleClickOnConnectionOpensIt;
            chkSingleClickOnOpenedConnectionSwitchesToIt.Checked = Settings.Default.SingleClickSwitchesToOpenConnection;
            chkHostnameLikeDisplayName.Checked = Settings.Default.SetHostnameLikeDisplayName;

            numRdpReconnectionCount.Value = Convert.ToDecimal(Settings.Default.RdpReconnectionCount);

            numRDPConTimeout.Value = Convert.ToDecimal(Settings.Default.ConRDPOverallConnectionTimeout);

            numAutoSave.Value = Convert.ToDecimal(Settings.Default.AutoSaveEveryMinutes);

            switch (Settings.Default.ConfirmCloseConnection)
            {
                case (int) ConfirmCloseEnum.Never:
                    radCloseWarnNever.Checked = true;
                    break;
                case (int) ConfirmCloseEnum.Exit:
                    radCloseWarnExit.Checked = true;
                    break;
                case (int) ConfirmCloseEnum.Multiple:
                    radCloseWarnMultiple.Checked = true;
                    break;
                default:
                    radCloseWarnAll.Checked = true;
                    break;
            }
        }

        public override void SaveSettings()
        {
            Settings.Default.SingleClickOnConnectionOpensIt = chkSingleClickOnConnectionOpensIt.Checked;
            Settings.Default.SingleClickSwitchesToOpenConnection = chkSingleClickOnOpenedConnectionSwitchesToIt.Checked;
            Settings.Default.SetHostnameLikeDisplayName = chkHostnameLikeDisplayName.Checked;

            Settings.Default.RdpReconnectionCount = (int) numRdpReconnectionCount.Value;

            Settings.Default.ConRDPOverallConnectionTimeout = (int) numRDPConTimeout.Value;

            Settings.Default.AutoSaveEveryMinutes = (int) numAutoSave.Value;
            if (Settings.Default.AutoSaveEveryMinutes > 0)
            {
                _frmMain.tmrAutoSave.Interval = Settings.Default.AutoSaveEveryMinutes*60000;
                _frmMain.tmrAutoSave.Enabled = true;
            }
            else
            {
                _frmMain.tmrAutoSave.Enabled = false;
            }

            if (radCloseWarnAll.Checked)
            {
                Settings.Default.ConfirmCloseConnection = (int) ConfirmCloseEnum.All;
            }
            if (radCloseWarnMultiple.Checked)
            {
                Settings.Default.ConfirmCloseConnection = (int) ConfirmCloseEnum.Multiple;
            }
            if (radCloseWarnExit.Checked)
            {
                Settings.Default.ConfirmCloseConnection = (int) ConfirmCloseEnum.Exit;
            }
            if (radCloseWarnNever.Checked)
            {
                Settings.Default.ConfirmCloseConnection = (int) ConfirmCloseEnum.Never;
            }

            Settings.Default.Save();
        }

        
    }
}