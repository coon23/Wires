using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Wires.Resources;


namespace Wires
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            SetCulture();
        }

        /// <summary>
        /// Set culture to avoid possible issue with double parsing because different decimal separators.
        /// </summary>
        private void SetCulture()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(TextResources.DefaultCulture);
        }
    }
}
