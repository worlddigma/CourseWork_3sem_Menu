namespace CourseWork_3sem_Menu.Forms
{
    partial class FormRoutes
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
            panelRoutesMenu = new Panel();
            panelRoutesList = new Panel();
            labelNoRoutes = new Label();
            panelRoutesMenuTitle = new Panel();
            buttonRoutesAdd = new Button();
            panelRoutesMenu.SuspendLayout();
            panelRoutesList.SuspendLayout();
            panelRoutesMenuTitle.SuspendLayout();
            SuspendLayout();
            // 
            // panelRoutesMenu
            // 
            panelRoutesMenu.Controls.Add(panelRoutesList);
            panelRoutesMenu.Controls.Add(panelRoutesMenuTitle);
            panelRoutesMenu.Dock = DockStyle.Fill;
            panelRoutesMenu.Location = new Point(0, 0);
            panelRoutesMenu.Name = "panelRoutesMenu";
            panelRoutesMenu.Size = new Size(800, 450);
            panelRoutesMenu.TabIndex = 0;
            // 
            // panelRoutesList
            // 
            panelRoutesList.Controls.Add(labelNoRoutes);
            panelRoutesList.Dock = DockStyle.Fill;
            panelRoutesList.Location = new Point(0, 54);
            panelRoutesList.Name = "panelRoutesList";
            panelRoutesList.Size = new Size(800, 396);
            panelRoutesList.TabIndex = 2;
            // 
            // labelNoRoutes
            // 
            labelNoRoutes.Anchor = AnchorStyles.None;
            labelNoRoutes.AutoSize = true;
            labelNoRoutes.Font = new Font("Segoe UI", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            labelNoRoutes.Location = new Point(288, 171);
            labelNoRoutes.Name = "labelNoRoutes";
            labelNoRoutes.Size = new Size(211, 37);
            labelNoRoutes.TabIndex = 5;
            labelNoRoutes.Text = "Маршрутов нет";
            labelNoRoutes.Visible = false;
            // 
            // panelRoutesMenuTitle
            // 
            panelRoutesMenuTitle.Controls.Add(buttonRoutesAdd);
            panelRoutesMenuTitle.Dock = DockStyle.Top;
            panelRoutesMenuTitle.Location = new Point(0, 0);
            panelRoutesMenuTitle.Name = "panelRoutesMenuTitle";
            panelRoutesMenuTitle.Size = new Size(800, 54);
            panelRoutesMenuTitle.TabIndex = 0;
            // 
            // buttonRoutesAdd
            // 
            buttonRoutesAdd.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonRoutesAdd.FlatStyle = FlatStyle.Flat;
            buttonRoutesAdd.Location = new Point(713, 12);
            buttonRoutesAdd.Name = "buttonRoutesAdd";
            buttonRoutesAdd.Size = new Size(75, 23);
            buttonRoutesAdd.TabIndex = 2;
            buttonRoutesAdd.Text = "Добавить";
            buttonRoutesAdd.UseVisualStyleBackColor = true;
            buttonRoutesAdd.Click += buttonRoutesAdd_Click;
            // 
            // FormRoutes
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(panelRoutesMenu);
            Name = "FormRoutes";
            Text = "Список маршрутов";
            panelRoutesMenu.ResumeLayout(false);
            panelRoutesList.ResumeLayout(false);
            panelRoutesList.PerformLayout();
            panelRoutesMenuTitle.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panelRoutesMenu;
        public Button buttonRoutesAdd;
        private Panel panelRoutesList;
        private Label labelNoRoutes;
        public Panel panelRoutesMenuTitle;
    }
}