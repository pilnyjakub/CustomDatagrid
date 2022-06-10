namespace CustomDatagrid
{
    partial class CustomDatagrid
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Panel = new System.Windows.Forms.Panel();
            this.ComboBox_ItemsPerPage = new System.Windows.Forms.ComboBox();
            this.TextBox_Search = new System.Windows.Forms.TextBox();
            this.Panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // Panel
            // 
            this.Panel.Controls.Add(this.TextBox_Search);
            this.Panel.Controls.Add(this.ComboBox_ItemsPerPage);
            this.Panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel.Location = new System.Drawing.Point(0, 0);
            this.Panel.Name = "Panel";
            this.Panel.Size = new System.Drawing.Size(800, 450);
            this.Panel.TabIndex = 0;
            this.Panel.Paint += new System.Windows.Forms.PaintEventHandler(this.Panel_Paint);
            this.Panel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Panel_MouseDown);
            this.Panel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Panel_MouseMove);
            this.Panel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Panel_MouseUp);
            // 
            // ComboBox_ItemsPerPage
            // 
            this.ComboBox_ItemsPerPage.FormattingEnabled = true;
            this.ComboBox_ItemsPerPage.Items.AddRange(new object[] {
            "5",
            "10",
            "15",
            "20",
            "25"});
            this.ComboBox_ItemsPerPage.Location = new System.Drawing.Point(155, 2);
            this.ComboBox_ItemsPerPage.Name = "ComboBox_ItemsPerPage";
            this.ComboBox_ItemsPerPage.Size = new System.Drawing.Size(121, 23);
            this.ComboBox_ItemsPerPage.TabIndex = 0;
            this.ComboBox_ItemsPerPage.SelectedValueChanged += new System.EventHandler(this.ComboBox_ItemsPerPage_SelectedValueChanged);
            // 
            // TextBox_Search
            // 
            this.TextBox_Search.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBox_Search.Location = new System.Drawing.Point(689, 2);
            this.TextBox_Search.Name = "TextBox_Search";
            this.TextBox_Search.Size = new System.Drawing.Size(100, 23);
            this.TextBox_Search.TabIndex = 1;
            this.TextBox_Search.TextChanged += new System.EventHandler(this.TextBox_Search_TextChanged);
            // 
            // CustomDatagrid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Panel);
            this.DoubleBuffered = true;
            this.Name = "CustomDatagrid";
            this.Text = "Custom Datagrid";
            this.Panel.ResumeLayout(false);
            this.Panel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Panel Panel;
        private TextBox TextBox_Search;
        private ComboBox ComboBox_ItemsPerPage;
    }
}