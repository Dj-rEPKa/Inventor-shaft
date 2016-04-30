using System;
using Inventor;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace InvAddIn
{
    class addInButtonDefinition 
    {

        private ButtonDefinition Button;
        public ButtonDefinition _Button
        {
            get { return Button; }
            set { Button = value; }
        }
    #region Button constructors

        public addInButtonDefinition(string Name, string ToolTip)
        {

            assemble(var_es._ClientId, Name, "id_" + Name, ToolTip, "", null, null, CommandTypesEnum.kEditMaskCmdType, ButtonDisplayEnum.kDisplayTextInLearningMode);

        }

        public addInButtonDefinition (string Name, string ToolTip, CommandTypesEnum cmd_Type)
        {

            assemble(var_es._ClientId, Name, "id_"+Name,ToolTip,"",null,null, cmd_Type,ButtonDisplayEnum.kDisplayTextInLearningMode);

        }

        public addInButtonDefinition(string Name, string ToolTip, string internal_Name, CommandTypesEnum cmd_Type)
        {

            assemble(var_es._ClientId, Name, internal_Name, ToolTip, "", null, null, cmd_Type, ButtonDisplayEnum.kDisplayTextInLearningMode);

        }

        public addInButtonDefinition(string Name, string ToolTip, string internal_Name, string Description, CommandTypesEnum cmd_Type)
        {

            assemble(var_es._ClientId, Name, internal_Name, ToolTip, Description, null, null, cmd_Type, ButtonDisplayEnum.kDisplayTextInLearningMode);

        }

        public addInButtonDefinition(string Name, string ToolTip, string internal_Name, string Description, Icon small_Ico, CommandTypesEnum cmd_Type)
        {

            assemble(var_es._ClientId, Name, internal_Name, ToolTip, Description, small_Ico, null, cmd_Type, ButtonDisplayEnum.kDisplayTextInLearningMode);

        }

        public addInButtonDefinition(string Name, string ToolTip, string internal_Name, string Description, Icon small_Ico, Icon large_Ico, CommandTypesEnum cmd_Type)
        {

            assemble(var_es._ClientId, Name, internal_Name, ToolTip, Description, small_Ico, large_Ico, cmd_Type, ButtonDisplayEnum.kDisplayTextInLearningMode);

        }

        public addInButtonDefinition(string Name, string ToolTip, string internal_Name, string Description, Icon small_Ico, Icon large_Ico, CommandTypesEnum cmd_Type, ButtonDisplayEnum btn_Type)
        {

            assemble(var_es._ClientId, Name, internal_Name, ToolTip, Description, small_Ico, large_Ico, cmd_Type, btn_Type);

        }

        public addInButtonDefinition(string Name, string ToolTip, Icon small_Ico, Icon large_Ico, CommandTypesEnum cmd_Type)
        {

            assemble(var_es._ClientId, Name, "id_" + Name, ToolTip, "", small_Ico, large_Ico, cmd_Type, ButtonDisplayEnum.kDisplayTextInLearningMode);

        }

        public addInButtonDefinition(string Name, string ToolTip, Icon small_Ico, CommandTypesEnum cmd_Type)
        {

            assemble(var_es._ClientId, Name, "id_" + Name, ToolTip, "", small_Ico, null, cmd_Type, ButtonDisplayEnum.kDisplayTextInLearningMode);

        }
        /*
        public addInButtonDefinition(string Name, string ToolTip, Icon large_Ico, CommandTypesEnum cmd_Type)
        {

            assemble(var_es._ClientId, Name, "id_" + Name, ToolTip, "", null, large_Ico, cmd_Type, ButtonDisplayEnum.kDisplayTextInLearningMode);

        }

        */

    #endregion

        private void assemble(string ClientId, string Name, string internalName, string Tooltip, string Description, Icon small_Ico, Icon large_Ico , CommandTypesEnum cmd_type, ButtonDisplayEnum btn_type)
        {
            
            if(String.IsNullOrEmpty(ClientId))
                ClientId = var_es._ClientId;

            stdole.IPictureDisp common = null;

            if (small_Ico != null)
                common = ImgTypeConv.Ico_to_Picture(small_Ico);
            
            stdole.IPictureDisp large = null;

            if (large_Ico != null)
                large = ImgTypeConv.Ico_to_Picture(large_Ico);

            Button = var_es.InventorApp.CommandManager.ControlDefinitions.AddButtonDefinition(Name, internalName, cmd_type, ClientId, Description, Tooltip, common, large, btn_type);

            Button.Enabled = true;

            Button.OnExecute += BTN_OnExecute;
        }

        private void BTN_OnExecute(NameValueMap e)
        {
            try
            {
                var_es.attach_active_doc();
                Form_Create();
            }
            catch
            {
                MessageBox.Show("Before creating shaft You must save assembly");
                var_es.InventorApp.ActiveDocument.Save();
            }
        }

        public void Form_Create()
        {
            addInForm frm = new addInForm();
            frm.Show();
        }
    }
}
