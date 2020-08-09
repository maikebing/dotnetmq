/*
DotNetMQ - A Complete Message Broker For .NET
Copyright (C) 2011 Halil ibrahim KALKAN

This library is free software; you can redistribute it and/or
modify it under the terms of the GNU Lesser General Public
License as published by the Free Software Foundation; either
version 2.1 of the License, or (at your option) any later version.

This library is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
Lesser General Public License for more details.

You should have received a copy of the GNU Lesser General Public
License along with this library; if not, write to the Free Software
Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301  USA
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using DotNetMQ.GUI;
using DotNetMQ.Manager.Properties;
using log4net;

namespace DotNetMQ.Management.GUI
{
    /// <summary>
    /// This form is used to connect a MDS Server.
    /// </summary>
    public partial class ConnectToServerForm : Form
    {
        /// <summary>
        /// Reference to logger.
        /// </summary>
        private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Gets/Sets MDSController that is being connected.
        /// </summary>
        public MDSController MDSController { get; private set; }

     

        /// <summary>
        /// Constructor.
        /// </summary>
        public ConnectToServerForm()
        {
         
            InitializeComponent();
            PrepareForm();
        }

        /// <summary>
        /// Prepares form.
        /// </summary>
        private void PrepareForm()
        {
            Left = (Screen.GetWorkingArea(this).Width - Width) / 2;
            Top = (Screen.GetWorkingArea(this).Height - Height) / 2;
        }


        private void ConnectToServerForm_Load(object sender, EventArgs e)
        {
            try
            {
                txtIPAddress.Text =  Settings.Default.LastConnectedIPAddress;
                txtPort.Text =  Settings.Default.LastConnectedTCPPort.ToString();
            }
            catch (Exception ex)
            {
                Logger.Warn(ex.Message, ex);
            }
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                var port = Convert.ToInt32(txtPort.Text);
                MDSController = new MDSController(txtIPAddress.Text, port);
                Settings.Default.LastConnectedIPAddress= txtIPAddress.Text;
                Settings.Default.LastConnectedTCPPort = port;
                Settings.Default.Save();
                Close();
            }
            catch
            {
                MDSGuiHelper.ShowErrorMessage("Please check IP address and TCP Port. TCP port must be numeric.");
            }
        }
    }
}
