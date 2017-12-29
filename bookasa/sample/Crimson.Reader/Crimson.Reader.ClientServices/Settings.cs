using System.Windows.Forms;
using System.Web.Security;
using System;
namespace Crimson.Reader.ClientServices.Properties 
{
    // This class allows you to handle specific events on the settings class:
    //  The SettingChanging event is raised before a setting's value is changed.
    //  The PropertyChanged event is raised after a setting's value is changed.
    //  The SettingsLoaded event is raised after the setting values are loaded.
    //  The SettingsSaving event is raised before the setting values are saved.
    public sealed partial class Settings {
        
        public Settings() {
            // // To add event handlers for saving and changing settings, uncomment the lines below:
            //
            // this.SettingChanging += this.SettingChangingEventHandler;
            //
            // this.SettingsSaving += this.SettingsSavingEventHandler;
            //
        }
        
        private void SettingChangingEventHandler(object sender, System.Configuration.SettingChangingEventArgs e) {
            // Add code to handle the SettingChangingEvent event here.
        }
        
        private void SettingsSavingEventHandler(object sender, System.ComponentModel.CancelEventArgs e) {
            // Add code to handle the SettingsSaving event here.
        }

        public void SaveSettings()
        {
            if
              (!System.Threading.Thread.CurrentPrincipal.Identity.AuthenticationType.Equals(
                "ClientForms"))
                return;

            try
            {
                Save();
            }
            catch (System.Net.WebException ex)
            {
                // This means you are logged out
                if (ex.Message.Contains("You must log on to call this method."))
                {
                    MessageBox.Show(
                      "Your session has expired. Please login again to be able to save" +
                      "your settings.", "Saving Web Settings");

                    try
                    {
                        // Show the Login form for the user to enter his/her credentials
                        // to login again
                        if (!Membership.ValidateUser(String.Empty, String.Empty))
                        {
                            MessageBox.Show("Unable to authenticate. " +
                              "Settings were not saved on the remote service.", "Not logged in",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            // Try again saving the settings
                            // after the user has been authenticated
                            Save();
                        }
                    }
                    catch (System.Net.WebException)
                    {
                        MessageBox.Show("Unable to access the authentication service. " +
                          "Settings were not saved on the remote service.", "Not logged in",
                          MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Unable to access the Web settings service. " +
                      "Settings were not saved on the remote service.", "Not logged in",
                      MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
    }
}
