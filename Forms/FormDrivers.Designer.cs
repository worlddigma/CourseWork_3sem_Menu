namespace CourseWork_3sem_Menu.Forms
{
    partial class FormDrivers
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
            panelDriversMenu = new Panel();
            panelDriversList = new Panel();
            labelNoDrivers = new Label();
            panelDriversMenuTitle = new Panel();
            buttonAddDriver = new Button();
            panelDriversMenu.SuspendLayout();
            panelDriversList.SuspendLayout();
            panelDriversMenuTitle.SuspendLayout();
            SuspendLayout();
            // 
            // panelDriversMenu
            // 
            panelDriversMenu.Controls.Add(panelDriversList);
            panelDriversMenu.Controls.Add(panelDriversMenuTitle);
            panelDriversMenu.Dock = DockStyle.Fill;
            panelDriversMenu.Location = new Point(0, 0);
            panelDriversMenu.Name = "panelDriversMenu";
            panelDriversMenu.Size = new Size(800, 450);
            panelDriversMenu.TabIndex = 0;
            // 
            // panelDriversList
            // 
            panelDriversList.Controls.Add(labelNoDrivers);
            panelDriversList.Dock = DockStyle.Fill;
            panelDriversList.Location = new Point(0, 44);
            panelDriversList.Name = "panelDriversList";
            panelDriversList.Size = new Size(800, 406);
            panelDriversList.TabIndex = 1;
            // 
            // labelNoDrivers
            // 
            labelNoDrivers.Anchor = AnchorStyles.None;
            labelNoDrivers.AutoSize = true;
            labelNoDrivers.Font = new Font("Segoe UI", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            labelNoDrivers.Location = new Point(295, 185);
            labelNoDrivers.Name = "labelNoDrivers";
            labelNoDrivers.Size = new Size(196, 37);
            labelNoDrivers.TabIndex = 6;
            labelNoDrivers.Text = "Водителей нет";
            labelNoDrivers.Visible = false;
            // 
            // panelDriversMenuTitle
            // 
            panelDriversMenuTitle.Controls.Add(buttonAddDriver);
            panelDriversMenuTitle.Dock = DockStyle.Top;
            panelDriversMenuTitle.Location = new Point(0, 0);
            panelDriversMenuTitle.Name = "panelDriversMenuTitle";
            panelDriversMenuTitle.Size = new Size(800, 44);
            panelDriversMenuTitle.TabIndex = 0;
            // 
            // buttonAddDriver
            // 
            buttonAddDriver.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonAddDriver.FlatStyle = FlatStyle.Flat;
            buttonAddDriver.Location = new Point(713, 12);
            buttonAddDriver.Name = "buttonAddDriver";
            buttonAddDriver.Size = new Size(75, 24);
            buttonAddDriver.TabIndex = 0;
            buttonAddDriver.Text = "Добавить";
            buttonAddDriver.UseVisualStyleBackColor = true;
            buttonAddDriver.Click += buttonAddDriver_Click;
            // 
            // FormDrivers
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(panelDriversMenu);
            Name = "FormDrivers";
            Text = "Список водителей";
            panelDriversMenu.ResumeLayout(false);
            panelDriversList.ResumeLayout(false);
            panelDriversList.PerformLayout();
            panelDriversMenuTitle.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panelDriversMenu;
        private Panel panelDriversList;
        private Button buttonAddDriver;
        private Label labelNoDrivers;
        public Panel panelDriversMenuTitle;
    }
}