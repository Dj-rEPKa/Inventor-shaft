namespace InvAddIn
{
    partial class addInForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(addInForm));
            this.right_edge_camf = new System.Windows.Forms.ComboBox();
            this.section_type = new System.Windows.Forms.ComboBox();
            this.left_edge_chamf = new System.Windows.Forms.ComboBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.button6 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // right_edge_camf
            // 
            this.right_edge_camf.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.right_edge_camf.FormattingEnabled = true;
            this.right_edge_camf.Location = new System.Drawing.Point(144, 12);
            this.right_edge_camf.Name = "right_edge_camf";
            this.right_edge_camf.Size = new System.Drawing.Size(49, 21);
            this.right_edge_camf.TabIndex = 0;
            this.right_edge_camf.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.right_edge_camf_DrawItem);
            this.right_edge_camf.DropDownClosed += new System.EventHandler(this.right_edge_camf_DropDownClosed);
            // 
            // section_type
            // 
            this.section_type.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.section_type.FormattingEnabled = true;
            this.section_type.Location = new System.Drawing.Point(80, 12);
            this.section_type.Name = "section_type";
            this.section_type.Size = new System.Drawing.Size(49, 21);
            this.section_type.TabIndex = 1;
            this.section_type.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.section_type_DrawItem);
            this.section_type.DropDownClosed += new System.EventHandler(this.section_type_DropDownClosed);
            // 
            // left_edge_chamf
            // 
            this.left_edge_chamf.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.left_edge_chamf.FormattingEnabled = true;
            this.left_edge_chamf.Location = new System.Drawing.Point(15, 12);
            this.left_edge_chamf.Name = "left_edge_chamf";
            this.left_edge_chamf.Size = new System.Drawing.Size(49, 21);
            this.left_edge_chamf.TabIndex = 2;
            this.left_edge_chamf.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.left_edge_chamf_DrawItem);
            this.left_edge_chamf.DropDownClosed += new System.EventHandler(this.left_edge_chamf_DropDownClosed);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(231, 132);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "Add_Pol";
            this.toolTip1.SetToolTip(this.button1, "не нажмай а то зависну");
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(150, 217);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 4;
            this.button2.Text = "OK";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(231, 217);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 5;
            this.button3.Text = "Cancel";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(231, 103);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 6;
            this.button4.Text = "Add_Con";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(231, 74);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 8;
            this.button5.Text = "Add_Cyl";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // listView1
            // 
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(15, 53);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Scrollable = false;
            this.listView1.ShowGroups = false;
            this.listView1.ShowItemToolTips = true;
            this.listView1.Size = new System.Drawing.Size(210, 140);
            this.listView1.TabIndex = 13;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.List;
            this.listView1.ColumnWidthChanging += new System.Windows.Forms.ColumnWidthChangingEventHandler(this.listView1_ColumnWidthChanging);
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(199, 12);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(24, 24);
            this.button6.TabIndex = 14;
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // addInForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(319, 256);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.left_edge_chamf);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.section_type);
            this.Controls.Add(this.right_edge_camf);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "addInForm";
            this.Text = "Shaft";
            this.Load += new System.EventHandler(this.addInForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox right_edge_camf;
        private System.Windows.Forms.ComboBox section_type;
        private System.Windows.Forms.ComboBox left_edge_chamf;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Button button6;
    }
}