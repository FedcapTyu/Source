using System;
using DevExpress.Utils;
using DevExpress.XtraBars.Docking2010.Views;
using DevExpress.XtraBars.Docking2010.Views.WindowsUI;
using DevExpress.XtraEditors;
using DevExpress.Utils.About;

namespace FedCapSys {
    public partial class frmMain : XtraForm {
        public frmMain() {
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Location = System.Windows.Forms.Screen.GetBounds(MousePosition).Location;
            InitializeComponent();
            InitTitleImages();
           
                      
            windowsUIView.ContentContainerActions.Add(new SetSkinAction("Metropolis", "White Theme"));
            //windowsUIView.ContentContainerActions.Add(new SetSkinAction("Seven Classic", "Gray Theme")); 
        }
        void InitTitleImages() {
            ucSettingsTile.Elements[0].Image = global::FedCapSys.Properties.Resources.System;
            ucResearchTile.Elements[0].Image = global::FedCapSys.Properties.Resources.Research;
            ucStatsTile.Elements[0].Image = global::FedCapSys.Properties.Resources.Statistics;
        }
        object current;
      
        void windowsUIView_QueryControl(object sender, QueryControlEventArgs e) {
            BaseModule module = e.Document.Tag is BaseModule ? (BaseModule)e.Document.Tag :
                Activator.CreateInstance(typeof(frmMain).Assembly.GetType(e.Document.ControlTypeName)) as BaseModule;
            module.InitModule(barManager1, windowsUIView);
            BaseTile tile = null;
            if(windowsUIView.Tiles.TryGetValue(e.Document, out tile)) {
                TileItemFrame frame = tile.CurrentFrame;
                object data = current ?? ((frame != null) ? frame.Tag : null);
                module.ShowModule(data);
            }
            e.Document.Tag = module;
            e.Control = module;
        }
        void windowsUIView_TileClick(object sender, TileClickEventArgs e) {
            Tile tile = e.Tile as Tile;
            if(tile != null && tile.Document != null) {
                BaseModule module = tile.Document.Control as BaseModule;
                if(module != null) {
                    TileItemFrame frame = tile.CurrentFrame;
                    object data = (frame != null) ? frame.Tag : null;
                    module.ShowModule(data);
                }
                if(tile.ActivationTarget == page) {
                    page.Document = tile.Document;
                    page.Caption = tile.Elements[0].Text;
                }
            }
        }
       
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e) {
            base.OnClosing(e);          
            FlyoutAction closeAction = CreateCloseAction();
            flyout.Action = closeAction;
            
            if(windowsUIView.ShowFlyoutDialog(flyout) != System.Windows.Forms.DialogResult.Yes) {
                e.Cancel = true;
                
            }
        }
        FlyoutAction CreateCloseAction() {
            FlyoutAction closeAction = new FlyoutAction();
            closeAction.Caption = Text;
            closeAction.Description = "Are you sure you want to close the application?";
            closeAction.Commands.Add(FlyoutCommand.Yes);
            closeAction.Commands.Add(FlyoutCommand.No);
            return closeAction;
        }
    }
}
