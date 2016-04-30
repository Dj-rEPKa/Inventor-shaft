using System;
using System.Runtime.InteropServices;
using Inventor;
using System.Windows.Forms;
using System.Drawing;
using InvAddIn;

namespace test
{
    /// <summary>
    /// This is the primary AddIn Server class that implements the ApplicationAddInServer interface
    /// that all Inventor AddIns are required to implement. The communication between Inventor and
    /// the AddIn is via the methods on this interface.
    /// </summary>
    [GuidAttribute("a23ea6c9-0772-4b2b-95e5-a2566e66203f")]
    public class StandardAddInServer : Inventor.ApplicationAddInServer
    {

        // Inventor application object.

        private addInButtonDefinition button;

        public StandardAddInServer()
        {

        }

        #region ApplicationAddInServer Members

        public static Inventor.Application ThisApplication; 

        public void Activate(Inventor.ApplicationAddInSite addInSiteObject, bool firstTime)
        {
            // This method is called by Inventor when it loads the addin.
            // The AddInSiteObject provides access to the Inventor Application object.
            // The FirstTime flag indicates if the addin is loaded for the first time.

            // Initialize AddIn members.

            var_es.InventorApp = addInSiteObject.Application;
            try
            {
                var_es.Get_addInClassId(this.GetType());
                Icon iconSmall = new Icon("logoSmall.ico");
                Icon iconLarge = new Icon("logoLarge.ico");


                //MessageBox.Show("Test addIn say: \"Hello\" to you!!! " +  var_es._ClientId);

                button = new addInButtonDefinition("Shaft", "\"Shaft\" is a simple plug-in to work with shaft`s angles", "ShaftButton_" + Guid.NewGuid().ToString(), "Simple plug-in to work with axel`s angels", iconSmall, iconLarge, CommandTypesEnum.kShapeEditCmdType);

                if (firstTime)
                {
                    UserInterfaceCreate();
                }

            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            // TODO: Add ApplicationAddInServer.Activate implementation.
            // e.g. event initialization, command creation etc.
        }

        private void UserInterfaceCreate()
        {
            //Get a reference to the UserInterfaceManager object.

            UserInterfaceManager UIManager = var_es.InventorApp.UserInterfaceManager;

            //create the UI for ribbon interface
            //Get the ribbon

            Inventor.Ribbons ribbons;
            ribbons = UIManager.Ribbons;

            Inventor.Ribbon newRibbon;
            newRibbon = ribbons["Assembly"];

            //Get the getting started tab

            RibbonTabs ribbonTabs;
            ribbonTabs = newRibbon.RibbonTabs;

            RibbonTab startedTab;
            startedTab = ribbonTabs["id_TabAssemble"]; // startedTab = ribbonTabs["id_GetStarted"];

            //Get the new features panel.
            string ribbonpanel_ID = "{d04e0c45-dec6-4881-bd3f-a7a81b99f307}";
            RibbonPanel panel = startedTab.RibbonPanels.Add("Shaft", "ShaftRibbonPanel_" + Guid.NewGuid().ToString(), ribbonpanel_ID, string.Empty, true);
            //add controls to the slot panel
            CommandControls cmdControls = panel.CommandControls;
            cmdControls.AddButton(button._Button, true, true, "", true);
        }


        public void Deactivate()
        {
            // This method is called by Inventor when the AddIn is unloaded.
            // The AddIn will be unloaded either manually by the user or
            // when the Inventor session is terminated

            // TODO: Add ApplicationAddInServer.Deactivate implementation

            // Release objects.
           // m_inventorApplication = null;

            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        public void ExecuteCommand(int commandID)
        {
            // Note:this method is now obsolete, you should use the 
            // ControlDefinition functionality for implementing commands.
        }

        public object Automation //just for loolz or if u wish for developing addIn as API to other programs.
        {
            // This property is provided to allow the AddIn to expose an API 
            // of its own to other programs. Typically, this  would be done by
            // implementing the AddIn's API interface in a class and returning 
            // that class object through this property.

            get
            {
                // TODO: Add ApplicationAddInServer.Automation getter implementation
                return null;
            }
        }

        #endregion

    }
}