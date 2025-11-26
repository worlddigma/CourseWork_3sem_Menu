namespace CourseWork_3sem_Menu.Forms
{
    partial class FormBuses
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
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            panelBusesMenu = new Panel();
            panelBusesList = new Panel();
            labelNoBuses = new Label();
            panelBusesMenuTitle = new Panel();
            buttonBusAdd = new Button();
            panelBusesMenu.SuspendLayout();
            panelBusesList.SuspendLayout();
            panelBusesMenuTitle.SuspendLayout();
            SuspendLayout();
            // 
            // panelBusesMenu
            // 
            panelBusesMenu.Controls.Add(panelBusesList);
            panelBusesMenu.Controls.Add(panelBusesMenuTitle);
            panelBusesMenu.Dock = DockStyle.Fill;
            panelBusesMenu.Location = new Point(0, 0);
            panelBusesMenu.Name = "panelBusesMenu";
            panelBusesMenu.Size = new Size(800, 450);
            panelBusesMenu.TabIndex = 0;
            // 
            // panelBusesList
            // 
            panelBusesList.Controls.Add(labelNoBuses);
            panelBusesList.Dock = DockStyle.Fill;
            panelBusesList.Location = new Point(0, 58);
            panelBusesList.Name = "panelBusesList";
            panelBusesList.Size = new Size(800, 392);
            panelBusesList.TabIndex = 1;
            // 
            // labelNoBuses
            // 
            labelNoBuses.Anchor = AnchorStyles.None;
            labelNoBuses.AutoSize = true;
            labelNoBuses.Font = new Font("Segoe UI", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            labelNoBuses.Location = new Point(290, 154);
            labelNoBuses.Name = "labelNoBuses";
            labelNoBuses.Size = new Size(194, 37);
            labelNoBuses.TabIndex = 5;
            labelNoBuses.Text = "Автобусов нет";
            labelNoBuses.Visible = false;
            // 
            // panelBusesMenuTitle
            // 
            panelBusesMenuTitle.Controls.Add(buttonBusAdd);
            panelBusesMenuTitle.Dock = DockStyle.Top;
            panelBusesMenuTitle.Location = new Point(0, 0);
            panelBusesMenuTitle.Name = "panelBusesMenuTitle";
            panelBusesMenuTitle.Size = new Size(800, 58);
            panelBusesMenuTitle.TabIndex = 0;
            // 
            // buttonBusAdd
            // 
            buttonBusAdd.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonBusAdd.FlatStyle = FlatStyle.Flat;
            buttonBusAdd.Location = new Point(712, 18);
            buttonBusAdd.Name = "buttonBusAdd";
            buttonBusAdd.Size = new Size(75, 23);
            buttonBusAdd.TabIndex = 1;
            buttonBusAdd.Text = "Добавить";
            buttonBusAdd.UseVisualStyleBackColor = true;
            buttonBusAdd.Click += buttonBusAdd_Click;
            // 
            // FormBuses
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(panelBusesMenu);
            Name = "FormBuses";
            Text = "Парк автобусов";
            panelBusesMenu.ResumeLayout(false);
            panelBusesList.ResumeLayout(false);
            panelBusesList.PerformLayout();
            panelBusesMenuTitle.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private Panel panelBusesMenu;
        private Panel panelBusesList;
        private Label labelNoBuses;
        public Panel panelBusesMenuTitle;
        public Button buttonBusAdd;
    }
}