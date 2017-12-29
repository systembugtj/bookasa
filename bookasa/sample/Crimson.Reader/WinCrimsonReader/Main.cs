using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Threading;
using System.Web.ClientServices;
using System.Web.ClientServices.Providers;
using System.Web.Security;
using Crimson.Reader.ClientServices;

namespace WinCrimsonReader
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
        }

        private void loginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Crimson.Reader.ClientServices.FormsAuthenticatedUser formsUser = new FormsAuthenticatedUser();
            if (!formsUser.IsFormsAuthenticated)
                formsUser.Login(string.Empty, string.Empty);

        }
    }
}
