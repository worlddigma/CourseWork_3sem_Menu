namespace CourseWork_3sem_Menu
{
    partial class StartingMenu
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
            panelMenu = new Panel();
            buttonTransportation = new Button();
            buttonDrivers = new Button();
            buttonRoutes = new Button();
            buttonBuses = new Button();
            panelLogo = new Panel();
            labelLogo = new Label();
            panelTitleBar = new Panel();
            labelTitelText = new Label();
            panelDesktop = new Panel();
            panelMenu.SuspendLayout();
            panelLogo.SuspendLayout();
            panelTitleBar.SuspendLayout();
            SuspendLayout();
            // 
            // panelMenu
            // 
            panelMenu.BackColor = Color.DarkGray;
            panelMenu.Controls.Add(buttonTransportation);
            panelMenu.Controls.Add(buttonDrivers);
            panelMenu.Controls.Add(buttonRoutes);
            panelMenu.Controls.Add(buttonBuses);
            panelMenu.Controls.Add(panelLogo);
            panelMenu.Dock = DockStyle.Left;
            panelMenu.Location = new Point(0, 0);
            panelMenu.Name = "panelMenu";
            panelMenu.Size = new Size(125, 450);
            panelMenu.TabIndex = 0;
            // 
            // buttonTransportation
            // 
            buttonTransportation.BackColor = Color.DarkGray;
            buttonTransportation.Dock = DockStyle.Top;
            buttonTransportation.FlatStyle = FlatStyle.Flat;
            buttonTransportation.ForeColor = Color.Black;
            buttonTransportation.Location = new Point(0, 229);
            buttonTransportation.Name = "buttonTransportation";
            buttonTransportation.Size = new Size(125, 59);
            buttonTransportation.TabIndex = 3;
            buttonTransportation.Text = "Обьем перевозок";
            buttonTransportation.UseVisualStyleBackColor = false;
            buttonTransportation.Click += buttonTransportation_Click;
            // 
            // buttonDrivers
            // 
            buttonDrivers.BackColor = Color.DarkGray;
            buttonDrivers.Dock = DockStyle.Top;
            buttonDrivers.FlatStyle = FlatStyle.Flat;
            buttonDrivers.ForeColor = Color.Black;
            buttonDrivers.Location = new Point(0, 170);
            buttonDrivers.Name = "buttonDrivers";
            buttonDrivers.Size = new Size(125, 59);
            buttonDrivers.TabIndex = 2;
            buttonDrivers.Text = "Штат водителей";
            buttonDrivers.UseVisualStyleBackColor = false;
            buttonDrivers.Click += buttonDrivers_Click;
            // 
            // buttonRoutes
            // 
            buttonRoutes.BackColor = Color.DarkGray;
            buttonRoutes.Dock = DockStyle.Top;
            buttonRoutes.FlatStyle = FlatStyle.Flat;
            buttonRoutes.ForeColor = Color.Black;
            buttonRoutes.Location = new Point(0, 111);
            buttonRoutes.Name = "buttonRoutes";
            buttonRoutes.Size = new Size(125, 59);
            buttonRoutes.TabIndex = 1;
            buttonRoutes.Text = "Маршруты";
            buttonRoutes.UseVisualStyleBackColor = false;
            buttonRoutes.Click += buttonRoutes_Click;
            // 
            // buttonBuses
            // 
            buttonBuses.BackColor = Color.DarkGray;
            buttonBuses.Dock = DockStyle.Top;
            buttonBuses.FlatStyle = FlatStyle.Flat;
            buttonBuses.Font = new Font("Segoe UI", 9F);
            buttonBuses.ForeColor = Color.Black;
            buttonBuses.ImageAlign = ContentAlignment.TopCenter;
            buttonBuses.Location = new Point(0, 52);
            buttonBuses.Name = "buttonBuses";
            buttonBuses.Size = new Size(125, 59);
            buttonBuses.TabIndex = 0;
            buttonBuses.Text = "Парк автобусов";
            buttonBuses.UseVisualStyleBackColor = false;
            buttonBuses.Click += buttonBuses_Click;
            // 
            // panelLogo
            // 
            panelLogo.BackColor = Color.Gray;
            panelLogo.Controls.Add(labelLogo);
            panelLogo.Dock = DockStyle.Top;
            panelLogo.Location = new Point(0, 0);
            panelLogo.Name = "panelLogo";
            panelLogo.Size = new Size(125, 52);
            panelLogo.TabIndex = 0;
            // 
            // labelLogo
            // 
            labelLogo.AutoSize = true;
            labelLogo.Font = new Font("SimSun-ExtG", 16F);
            labelLogo.ForeColor = SystemColors.ButtonHighlight;
            labelLogo.Location = new Point(26, 13);
            labelLogo.Name = "labelLogo";
            labelLogo.Size = new Size(58, 22);
            labelLogo.TabIndex = 0;
            labelLogo.Text = "СУПА";
            // 
            // panelTitleBar
            // 
            panelTitleBar.BackColor = Color.DarkGray;
            panelTitleBar.Controls.Add(labelTitelText);
            panelTitleBar.Dock = DockStyle.Top;
            panelTitleBar.Location = new Point(125, 0);
            panelTitleBar.Name = "panelTitleBar";
            panelTitleBar.Size = new Size(675, 52);
            panelTitleBar.TabIndex = 4;
            // 
            // labelTitelText
            // 
            labelTitelText.Anchor = AnchorStyles.None;
            labelTitelText.AutoSize = true;
            labelTitelText.Font = new Font("Century", 13.25F);
            labelTitelText.ForeColor = Color.Black;
            labelTitelText.Location = new Point(283, 13);
            labelTitelText.Name = "labelTitelText";
            labelTitelText.Size = new Size(173, 22);
            labelTitelText.TabIndex = 0;
            labelTitelText.Text = "Добро пожаловать!";
            // 
            // panelDesktop
            // 
            panelDesktop.Dock = DockStyle.Fill;
            panelDesktop.Location = new Point(125, 52);
            panelDesktop.Name = "panelDesktop";
            panelDesktop.Size = new Size(675, 398);
            panelDesktop.TabIndex = 5;
            // 
            // StartingMenu
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ButtonFace;
            ClientSize = new Size(800, 450);
            Controls.Add(panelDesktop);
            Controls.Add(panelTitleBar);
            Controls.Add(panelMenu);
            ForeColor = SystemColors.ControlDarkDark;
            KeyPreview = true;
            Name = "StartingMenu";
            Text = "Система учета пассажирских автоперевозок";
            FormClosing += StartingMenu_FormClosing;
            panelMenu.ResumeLayout(false);
            panelLogo.ResumeLayout(false);
            panelLogo.PerformLayout();
            panelTitleBar.ResumeLayout(false);
            panelTitleBar.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panelMenu;
        private Button buttonRoutes;
        private Panel panelLogo;
        private Button buttonTransportation;
        private Button buttonDrivers;
        private Button buttonBuses;
        private Panel panelTitleBar;
        private Label labelTitelText;
        private Label labelLogo;
        private Panel panelDesktop;
    }
}
