namespace CourseWork_3sem_Menu.Forms.EditForms
{
    partial class BusEdit
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
            labelState = new Label();
            pictureBoxBusImage = new PictureBox();
            buttonAddImage = new Button();
            buttonDeleteImage = new Button();
            textBoxStateNumber = new TextBox();
            textBoxBrand = new TextBox();
            labelBrand = new Label();
            buttonSaveChanges = new Button();
            buttonDisChanges = new Button();
            textBoxCapacity = new TextBox();
            labelCapacity = new Label();
            textBoxModel = new TextBox();
            labelModel = new Label();
            textBoxMilleage = new TextBox();
            labelMiliage = new Label();
            textBoxYearOfRepair = new TextBox();
            labelYearOfMajor = new Label();
            textBoxYear = new TextBox();
            labelYear = new Label();
            panel1 = new Panel();
            ((System.ComponentModel.ISupportInitialize)pictureBoxBusImage).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // labelState
            // 
            labelState.Anchor = AnchorStyles.Right;
            labelState.AutoSize = true;
            labelState.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            labelState.Location = new Point(479, 12);
            labelState.Name = "labelState";
            labelState.Size = new Size(154, 17);
            labelState.TabIndex = 7;
            labelState.Text = "Государственный номер";;
            // 
            // pictureBoxBusImage
            // 
            pictureBoxBusImage.BorderStyle = BorderStyle.FixedSingle;
            pictureBoxBusImage.InitialImage = null;
            pictureBoxBusImage.Location = new Point(12, 12);
            pictureBoxBusImage.Name = "pictureBoxBusImage";
            pictureBoxBusImage.Size = new Size(350, 212);
            pictureBoxBusImage.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxBusImage.TabIndex = 6;
            pictureBoxBusImage.TabStop = false;
            // 
            // buttonAddImage
            // 
            buttonAddImage.FlatStyle = FlatStyle.Flat;
            buttonAddImage.Location = new Point(12, 230);
            buttonAddImage.Name = "buttonAddImage";
            buttonAddImage.Size = new Size(91, 45);
            buttonAddImage.TabIndex = 11;
            buttonAddImage.Text = "Добавить изображение";
            buttonAddImage.UseVisualStyleBackColor = true;
            buttonAddImage.Click += buttonAddImage_Click;
            // 
            // buttonDeleteImage
            // 
            buttonDeleteImage.FlatStyle = FlatStyle.Flat;
            buttonDeleteImage.Location = new Point(109, 230);
            buttonDeleteImage.Name = "buttonDeleteImage";
            buttonDeleteImage.Size = new Size(91, 45);
            buttonDeleteImage.TabIndex = 12;
            buttonDeleteImage.Text = "Удалить изображение";
            buttonDeleteImage.UseVisualStyleBackColor = true;
            buttonDeleteImage.Click += buttonDelImage_Click;
            // 
            // textBoxStateNumber
            // 
            textBoxStateNumber.Anchor = AnchorStyles.Right;
            textBoxStateNumber.CharacterCasing = CharacterCasing.Upper;
            textBoxStateNumber.ForeColor = Color.Black;
            textBoxStateNumber.Location = new Point(681, 11);
            textBoxStateNumber.MaxLength = 9;
            textBoxStateNumber.Name = "textBoxStateNumber";
            textBoxStateNumber.PlaceholderText = "A111A111";
            textBoxStateNumber.RightToLeft = RightToLeft.No;
            textBoxStateNumber.Size = new Size(91, 23);
            textBoxStateNumber.TabIndex = 13;
            textBoxStateNumber.KeyPress += textBoxStateNumber_KeyPress;
            // 
            // textBoxBrand
            // 
            textBoxBrand.Anchor = AnchorStyles.Right;
            textBoxBrand.ForeColor = Color.Black;
            textBoxBrand.Location = new Point(681, 44);
            textBoxBrand.Name = "textBoxBrand";
            textBoxBrand.PlaceholderText = "BMW";
            textBoxBrand.Size = new Size(91, 23);
            textBoxBrand.TabIndex = 15;
            textBoxBrand.KeyPress += textBoxBrand_KeyPress;
            // 
            // labelBrand
            // 
            labelBrand.Anchor = AnchorStyles.Right;
            labelBrand.AutoSize = true;
            labelBrand.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            labelBrand.Location = new Point(479, 45);
            labelBrand.Name = "labelBrand";
            labelBrand.Size = new Size(44, 17);
            labelBrand.TabIndex = 14;
            labelBrand.Text = "Бренд";
            // 
            // buttonSaveChanges
            // 
            buttonSaveChanges.Anchor = AnchorStyles.Right;
            buttonSaveChanges.FlatStyle = FlatStyle.Flat;
            buttonSaveChanges.Location = new Point(557, 230);
            buttonSaveChanges.Name = "buttonSaveChanges";
            buttonSaveChanges.Size = new Size(91, 45);
            buttonSaveChanges.TabIndex = 16;
            buttonSaveChanges.Text = "Сохранить изменения";
            buttonSaveChanges.UseVisualStyleBackColor = true;
            buttonSaveChanges.Click += buttonSaveChanges_Click;
            // 
            // buttonDisChanges
            // 
            buttonDisChanges.Anchor = AnchorStyles.Right;
            buttonDisChanges.FlatStyle = FlatStyle.Flat;
            buttonDisChanges.Location = new Point(681, 230);
            buttonDisChanges.Name = "buttonDisChanges";
            buttonDisChanges.Size = new Size(91, 45);
            buttonDisChanges.TabIndex = 17;
            buttonDisChanges.Text = "Отменить изменения";
            buttonDisChanges.UseVisualStyleBackColor = true;
            buttonDisChanges.Click += buttonDisChanges_Click;
            // 
            // textBoxCapacity
            // 
            textBoxCapacity.Anchor = AnchorStyles.Right;
            textBoxCapacity.Location = new Point(681, 106);
            textBoxCapacity.Name = "textBoxCapacity";
            textBoxCapacity.PlaceholderText = "50";
            textBoxCapacity.Size = new Size(91, 23);
            textBoxCapacity.TabIndex = 21;
            textBoxCapacity.KeyPress += textBoxCapacity_KeyPress;
            // 
            // labelCapacity
            // 
            labelCapacity.Anchor = AnchorStyles.Right;
            labelCapacity.AutoSize = true;
            labelCapacity.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            labelCapacity.Location = new Point(479, 107);
            labelCapacity.Name = "labelCapacity";
            labelCapacity.Size = new Size(84, 17);
            labelCapacity.TabIndex = 20;
            labelCapacity.Text = "Вместимость";
            // 
            // textBoxModel
            // 
            textBoxModel.Anchor = AnchorStyles.Right;
            textBoxModel.Location = new Point(681, 73);
            textBoxModel.Name = "textBoxModel";
            textBoxModel.PlaceholderText = "S4";
            textBoxModel.Size = new Size(91, 23);
            textBoxModel.TabIndex = 19;
            textBoxModel.KeyPress += textBoxModel_KeyPress;
            // 
            // labelModel
            // 
            labelModel.Anchor = AnchorStyles.Right;
            labelModel.AutoSize = true;
            labelModel.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            labelModel.Location = new Point(479, 74);
            labelModel.Name = "labelModel";
            labelModel.Size = new Size(56, 17);
            labelModel.TabIndex = 18;
            labelModel.Text = "Модель";
            // 
            // textBoxMilleage
            // 
            textBoxMilleage.Anchor = AnchorStyles.Right;
            textBoxMilleage.Location = new Point(681, 197);
            textBoxMilleage.Name = "textBoxMilleage";
            textBoxMilleage.PlaceholderText = "10000";
            textBoxMilleage.Size = new Size(91, 23);
            textBoxMilleage.TabIndex = 27;
            textBoxMilleage.KeyPress += textBoxMilleage_KeyPress;
            // 
            // labelMiliage
            // 
            labelMiliage.Anchor = AnchorStyles.Right;
            labelMiliage.AutoSize = true;
            labelMiliage.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            labelMiliage.Location = new Point(479, 198);
            labelMiliage.Name = "labelMiliage";
            labelMiliage.Size = new Size(53, 17);
            labelMiliage.TabIndex = 26;
            labelMiliage.Text = "Пробег";
            // 
            // textBoxYearOfRepair
            // 
            textBoxYearOfRepair.Anchor = AnchorStyles.Right;
            textBoxYearOfRepair.Location = new Point(681, 168);
            textBoxYearOfRepair.Name = "textBoxYearOfRepair";
            textBoxYearOfRepair.PlaceholderText = "2005";
            textBoxYearOfRepair.Size = new Size(91, 23);
            textBoxYearOfRepair.TabIndex = 25;
            textBoxYearOfRepair.KeyPress += textBoxYearOfRepair_KeyPress;
            // 
            // labelYearOfMajor
            // 
            labelYearOfMajor.Anchor = AnchorStyles.Right;
            labelYearOfMajor.AutoSize = true;
            labelYearOfMajor.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            labelYearOfMajor.Location = new Point(479, 169);
            labelYearOfMajor.Name = "labelYearOfMajor";
            labelYearOfMajor.Size = new Size(169, 17);
            labelYearOfMajor.TabIndex = 24;
            labelYearOfMajor.Text = "Год капитального ремонта";
            // 
            // textBoxYear
            // 
            textBoxYear.Anchor = AnchorStyles.Right;
            textBoxYear.Location = new Point(681, 135);
            textBoxYear.Name = "textBoxYear";
            textBoxYear.PlaceholderText = "2000";
            textBoxYear.Size = new Size(91, 23);
            textBoxYear.TabIndex = 23;
            textBoxYear.KeyPress += textBoxYear_KeyPress;
            // 
            // labelYear
            // 
            labelYear.Anchor = AnchorStyles.Right;
            labelYear.AutoSize = true;
            labelYear.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            labelYear.Location = new Point(479, 136);
            labelYear.Name = "labelYear";
            labelYear.Size = new Size(81, 17);
            labelYear.TabIndex = 22;
            labelYear.Text = "Год выпуска";
            // 
            // panel1
            // 
            panel1.Controls.Add(labelMiliage);
            panel1.Controls.Add(textBoxMilleage);
            panel1.Controls.Add(labelYearOfMajor);
            panel1.Controls.Add(labelYear);
            panel1.Controls.Add(textBoxYear);
            panel1.Controls.Add(labelCapacity);
            panel1.Controls.Add(textBoxStateNumber);
            panel1.Controls.Add(labelModel);
            panel1.Controls.Add(textBoxYearOfRepair);
            panel1.Controls.Add(buttonSaveChanges);
            panel1.Controls.Add(textBoxBrand);
            panel1.Controls.Add(labelBrand);
            panel1.Controls.Add(buttonDisChanges);
            panel1.Controls.Add(textBoxModel);
            panel1.Controls.Add(textBoxCapacity);
            panel1.Controls.Add(labelState);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(784, 450);
            panel1.TabIndex = 28;
            // 
            // BusEdit
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(784, 450);
            Controls.Add(buttonDeleteImage);
            Controls.Add(buttonAddImage);
            Controls.Add(pictureBoxBusImage);
            Controls.Add(panel1);
            Name = "BusEdit";
            Text = "FormEdit";
            ((System.ComponentModel.ISupportInitialize)pictureBoxBusImage).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Button buttonBusDelete;
        private Button buttonBusEdit;
        private Label labelState;
        private PictureBox pictureBoxBusImage;
        private Button buttonAddImage;
        private Button buttonDeleteImage;
        private TextBox textBoxStateNumber;
        private TextBox textBoxBrand;
        private Label labelBrand;
        private Button buttonSaveChanges;
        private Button buttonDisChanges;
        private TextBox textBoxCapacity;
        private Label labelCapacity;
        private TextBox textBoxModel;
        private Label labelModel;
        private TextBox textBoxMilleage;
        private Label labelMiliage;
        private TextBox textBoxYearOfRepair;
        private Label labelYearOfMajor;
        private TextBox textBoxYear;
        private Label labelYear;
        private Panel panel1;
    }
}