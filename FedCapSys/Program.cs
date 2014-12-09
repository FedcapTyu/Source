using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using DevExpress.Skins;
using DevExpress.Skins.Info;
using DevExpress.Utils;
using DevExpress.XtraBars.Docking2010.Customization;
using DevExpress.XtraSplashScreen;

namespace FedCapSys {
    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            SkinManager.EnableFormSkins();
            DevExpress.UserSkins.BonusSkins.Register();
            AppearanceObject.DefaultFont = new Font("Segoe UI", 8.25f);
            SkinBlobXmlCreator skinCreator = new SkinBlobXmlCreator("MetroBlack",
                "FedCapSys.SkinData.", typeof(Program).Assembly, null);
            SkinManager.Default.RegisterSkin(skinCreator);
            AsyncAdornerBootStrapper.RegisterLookAndFeel(
                "MetroBlack", "FedCapSys.SkinData.", typeof(Program).Assembly);
            DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle("Metropolis");
            CultureInfo demoCI = (CultureInfo)Application.CurrentCulture.Clone();
            demoCI.NumberFormat.CurrencySymbol = "$";
            SplashScreenManager.RegisterUserSkin(skinCreator);
            Application.CurrentCulture = demoCI;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmMain());
        }
    }
   
}
