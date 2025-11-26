namespace CourseWork_3sem_Menu.Forms
{
    partial class FormTransportation
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
            panelTransportationMenu = new Panel();
            panelTransportationList = new Panel();
            labelNoTransportation = new Label();
            panelTransportationMenuTitle = new Panel();
            buttonTransportationAdd = new Button();
            panelTransportationMenu.SuspendLayout();
            panelTransportationList.SuspendLayout();
            panelTransportationMenuTitle.SuspendLayout();
            SuspendLayout();
            // 
            // panelTransportationMenu
            // 
            panelTransportationMenu.Controls.Add(panelTransportationList);
            panelTransportationMenu.Controls.Add(panelTransportationMenuTitle);
            panelTransportationMenu.Dock = DockStyle.Fill;
            panelTransportationMenu.Location = new Point(0, 0);
            panelTransportationMenu.Name = "panelTransportationMenu";
            panelTransportationMenu.Size = new Size(816, 471);
            panelTransportationMenu.TabIndex = 0;
            // 
            // panelTransportationList
            // 
            panelTransportationList.Controls.Add(labelNoTransportation);
            panelTransportationList.Dock = DockStyle.Fill;
            panelTransportationList.Location = new Point(0, 39);
            panelTransportationList.Name = "panelTransportationList";
            panelTransportationList.Size = new Size(816, 432);
            panelTransportationList.TabIndex = 1;
            // 
            // labelNoTransportation
            // 
            labelNoTransportation.Anchor = AnchorStyles.None;
            labelNoTransportation.AutoSize = true;
            labelNoTransportation.Font = new Font("Segoe UI", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            labelNoTransportation.Location = new Point(303, 198);
            labelNoTransportation.Name = "labelNoTransportation";
            labelNoTransportation.Size = new Size(199, 37);
            labelNoTransportation.TabIndex = 6;
            labelNoTransportation.Text = "Перевозок нет";
            labelNoTransportation.Visible = false;
            // 
            // panelTransportationMenuTitle
            // 
            panelTransportationMenuTitle.Controls.Add(buttonTransportationAdd);
            panelTransportationMenuTitle.Dock = DockStyle.Top;
            panelTransportationMenuTitle.Location = new Point(0, 0);
            panelTransportationMenuTitle.Name = "panelTransportationMenuTitle";
            panelTransportationMenuTitle.Size = new Size(816, 39);
            panelTransportationMenuTitle.TabIndex = 0;
            // 
            // buttonTransportationAdd
            // 
            buttonTransportationAdd.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonTransportationAdd.FlatStyle = FlatStyle.Flat;
            buttonTransportationAdd.Location = new Point(729, 10);
            buttonTransportationAdd.Name = "buttonTransportationAdd";
            buttonTransportationAdd.Size = new Size(75, 23);
            buttonTransportationAdd.TabIndex = 3;
            buttonTransportationAdd.Text = "Добавить";
            buttonTransportationAdd.UseVisualStyleBackColor = true;
            buttonTransportationAdd.Click += buttonTransportationAdd_Click;
            // 
            // FormTransportation
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(816, 471);
            Controls.Add(panelTransportationMenu);
            Name = "FormTransportation";
            Text = "Перевозки";
            panelTransportationMenu.ResumeLayout(false);
            panelTransportationList.ResumeLayout(false);
            panelTransportationList.PerformLayout();
            panelTransportationMenuTitle.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panelTransportationMenu;
        private Panel panelTransportationList;
        public Button buttonTransportationAdd;
        private Label labelNoTransportation;
        public Panel panelTransportationMenuTitle;
    }
}