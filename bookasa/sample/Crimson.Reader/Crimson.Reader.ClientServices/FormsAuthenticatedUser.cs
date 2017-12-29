using System;
using System.Net;
using System.Threading;
using System.Web.ClientServices;
using System.Web.ClientServices.Providers;
using System.Web.Security;

namespace Crimson.Reader.ClientServices
{
    /// <summary>
    /// Information about the current forms authenticated user.
    /// </summary>
    public sealed class FormsAuthenticatedUser
    {
        /// <summary>True if the current user is authenticated</summary>
        public bool IsFormsAuthenticated { get; internal set; }

        /// <summary>Gets the user name.</summary>
        public string Name { get; internal set; }

        /// <summary>
        /// Login the user using the given username and password.  If the username and password
        /// are the empty string, the login dialog box is displayed.
        /// </summary>
        /// <param name="userName">Username for login</param>
        /// <param name="password">Password for login</param>
        /// <returns>True if the user can be logged in</returns>
        public bool Login(string userName, string password)
        {
            bool isAuthorized = false;
            try
            {
                // Call ValidateUser with empty strings in order to display the 
                // login dialog box configured as a credentials provider.
                isAuthorized = Membership.ValidateUser(userName, password);
                if (isAuthorized)
                {
                    ClientFormsIdentity ci = Thread.CurrentPrincipal.Identity as ClientFormsIdentity;
                    if (ci != null)
                    {
                        IsFormsAuthenticated = ci.IsAuthenticated;
                        Name = ci.Name;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to Login", ex);
            }
            return isAuthorized;
        }

        /// <summary>
        /// Construct the current class, set IsFormsAuthenticated property
        /// </summary>
        public FormsAuthenticatedUser()
        {
            if (Thread.CurrentPrincipal.Identity is ClientFormsIdentity)
                IsFormsAuthenticated = (Thread.CurrentPrincipal.Identity as ClientFormsIdentity).IsAuthenticated;
            else
                IsFormsAuthenticated = false;
        }

        /// <summary>
        /// Log a user off the server.
        /// </summary>
        public void LogOut()
        {
            ClientFormsAuthenticationMembershipProvider authProvider =
                (ClientFormsAuthenticationMembershipProvider)
                System.Web.Security.Membership.Provider;

            try
            {
                authProvider.Logout();
            }
            catch (WebException ex)
            {
                ConnectivityStatus.IsOffline = true;
                authProvider.Logout();
                ConnectivityStatus.IsOffline = false;
            }
        }
    }
}
