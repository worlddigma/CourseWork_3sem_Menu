namespace CourseWork_3sem_Menu.Forms.EditForms
{
    partial class DriverEdit
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
            dateTimePickerDateOfBirth = new DateTimePicker();
            labelTransportationTime = new Label();
            labelDepartureTime = new Label();
            labelInermediatePoints = new Label();
            textBoxName = new TextBox();
            labelEndingPoint = new Label();
            buttonSaveChanges = new Button();
            textBoxId = new TextBox();
            labelStartingPoint = new Label();
            buttonDisChanges = new Button();
            textBoxWorkExperience = new TextBox();
            labelCode = new Label();
            checkedListBoxCategory = new CheckedListBox();
            buttonChooseClass = new Button();
            labelChosenClass = new Label();
            SuspendLayout();
            // 
            // dateTimePickerDateOfBirth
            // 
            dateTimePickerDateOfBirth.CustomFormat = "dd.MM.yyyy";
            dateTimePickerDateOfBirth.Format = DateTimePickerFormat.Custom;
            dateTimePickerDateOfBirth.Location = new Point(226, 124);
            dateTimePickerDateOfBirth.MaxDate = new DateTime(2500, 12, 31, 0, 0, 0, 0);
            dateTimePickerDateOfBirth.MinDate = new DateTime(1900, 1, 1, 0, 0, 0, 0);
            dateTimePickerDateOfBirth.Name = "dateTimePickerDateOfBirth";
            dateTimePickerDateOfBirth.Size = new Size(91, 23);
            dateTimePickerDateOfBirth.TabIndex = 62;
            dateTimePickerDateOfBirth.Value = new DateTime(2000, 12, 1, 0, 0, 0, 0);
            // 
            // labelTransportationTime
            // 
            labelTransportationTime.AutoSize = true;
            labelTransportationTime.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            labelTransportationTime.Location = new Point(24, 213);
            labelTransportationTime.Name = "labelTransportationTime";
            labelTransportationTime.Size = new Size(42, 17);
            labelTransportationTime.TabIndex = 60;
            labelTransportationTime.Text = "Класс";
            // 
            // labelDepartureTime
            // 
            labelDepartureTime.AutoSize = true;
            labelDepartureTime.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            labelDepartureTime.Location = new Point(24, 183);
            labelDepartureTime.Name = "labelDepartureTime";
            labelDepartureTime.Size = new Size(70, 17);
            labelDepartureTime.TabIndex = 58;
            labelDepartureTime.Text = "Категория";
            // 
            // labelInermediatePoints
            // 
            labelInermediatePoints.AutoSize = true;
            labelInermediatePoints.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            labelInermediatePoints.Location = new Point(24, 154);
            labelInermediatePoints.Name = "labelInermediatePoints";
            labelInermediatePoints.Size = new Size(88, 17);
            labelInermediatePoints.TabIndex = 56;
            labelInermediatePoints.Text = "Опыт работы";
            // 
            // textBoxName
            // 
            textBoxName.ForeColor = Color.Black;
            textBoxName.ImeMode = ImeMode.NoControl;
            textBoxName.Location = new Point(226, 58);
            textBoxName.MaxLength = 50;
            textBoxName.Name = "textBoxName";
            textBoxName.PlaceholderText = "Иванов Иван Иванович";
            textBoxName.RightToLeft = RightToLeft.No;
            textBoxName.Size = new Size(91, 23);
            textBoxName.TabIndex = 49;
            textBoxName.KeyPress += textName_KeyPress;
            // 
            // labelEndingPoint
            // 
            labelEndingPoint.AutoSize = true;
            labelEndingPoint.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            labelEndingPoint.Location = new Point(24, 121);
            labelEndingPoint.Name = "labelEndingPoint";
            labelEndingPoint.Size = new Size(100, 17);
            labelEndingPoint.TabIndex = 54;
            labelEndingPoint.Text = "Дата рождения";
            // 
            // buttonSaveChanges
            // 
            buttonSaveChanges.FlatStyle = FlatStyle.Flat;
            buttonSaveChanges.Location = new Point(377, 289);
            buttonSaveChanges.Name = "buttonSaveChanges";
            buttonSaveChanges.Size = new Size(91, 45);
            buttonSaveChanges.TabIndex = 52;
            buttonSaveChanges.Text = "Сохранить изменения";
            buttonSaveChanges.UseVisualStyleBackColor = true;
            buttonSaveChanges.Click += buttonSaveChanges_Click;
            // 
            // textBoxId
            // 
            textBoxId.ForeColor = Color.Black;
            textBoxId.Location = new Point(226, 91);
            textBoxId.Name = "textBoxId";
            textBoxId.PlaceholderText = "10";
            textBoxId.Size = new Size(91, 23);
            textBoxId.TabIndex = 51;
            textBoxId.KeyPress += textBoxId_KeyPress;
            // 
            // labelStartingPoint
            // 
            labelStartingPoint.AutoSize = true;
            labelStartingPoint.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            labelStartingPoint.Location = new Point(24, 92);
            labelStartingPoint.Name = "labelStartingPoint";
            labelStartingPoint.Size = new Size(117, 17);
            labelStartingPoint.TabIndex = 50;
            labelStartingPoint.Text = "Табельный номер";
            // 
            // buttonDisChanges
            // 
            buttonDisChanges.FlatStyle = FlatStyle.Flat;
            buttonDisChanges.Location = new Point(497, 289);
            buttonDisChanges.Name = "buttonDisChanges";
            buttonDisChanges.Size = new Size(91, 45);
            buttonDisChanges.TabIndex = 53;
            buttonDisChanges.Text = "Отменить изменения";
            buttonDisChanges.UseVisualStyleBackColor = true;
            buttonDisChanges.Click += buttonDisChanges_Click;
            // 
            // textBoxWorkExperience
            // 
            textBoxWorkExperience.Location = new Point(226, 153);
            textBoxWorkExperience.Name = "textBoxWorkExperience";
            textBoxWorkExperience.PlaceholderText = "5";
            textBoxWorkExperience.Size = new Size(91, 23);
            textBoxWorkExperience.TabIndex = 57;
            textBoxWorkExperience.KeyPress += textBoxWorkExperience_KeyPress;
            // 
            // labelCode
            // 
            labelCode.AutoSize = true;
            labelCode.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            labelCode.Location = new Point(24, 59);
            labelCode.Name = "labelCode";
            labelCode.Size = new Size(37, 17);
            labelCode.TabIndex = 48;
            labelCode.Text = "ФИО";
            // 
            // checkedListBoxCategory
            // 
            checkedListBoxCategory.FormattingEnabled = true;
            checkedListBoxCategory.Items.AddRange(new object[] { "D", "E" });
            checkedListBoxCategory.Location = new Point(226, 182);
            checkedListBoxCategory.Name = "checkedListBoxCategory";
            checkedListBoxCategory.Size = new Size(91, 22);
            checkedListBoxCategory.TabIndex = 65;
            checkedListBoxCategory.ItemCheck += checkedListBoxCategory_ItemCheck;
            // 
            // buttonChooseClass
            // 
            buttonChooseClass.FlatStyle = FlatStyle.Flat;
            buttonChooseClass.Location = new Point(226, 210);
            buttonChooseClass.Name = "buttonChooseClass";
            buttonChooseClass.Size = new Size(91, 29);
            buttonChooseClass.TabIndex = 66;
            buttonChooseClass.Text = "Выбрать";
            buttonChooseClass.UseVisualStyleBackColor = true;
            buttonChooseClass.Click += buttonChooseClass_Click;
            // 
            // labelChosenClass
            // 
            labelChosenClass.AutoSize = true;
            labelChosenClass.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            labelChosenClass.Location = new Point(328, 216);
            labelChosenClass.Name = "labelChosenClass";
            labelChosenClass.Size = new Size(45, 17);
            labelChosenClass.TabIndex = 67;
            labelChosenClass.Text = "Класс:";
            // 
            // DriverEdit
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(labelChosenClass);
            Controls.Add(buttonChooseClass);
            Controls.Add(checkedListBoxCategory);
            Controls.Add(dateTimePickerDateOfBirth);
            Controls.Add(labelTransportationTime);
            Controls.Add(labelDepartureTime);
            Controls.Add(labelInermediatePoints);
            Controls.Add(textBoxName);
            Controls.Add(labelEndingPoint);
            Controls.Add(buttonSaveChanges);
            Controls.Add(textBoxId);
            Controls.Add(labelStartingPoint);
            Controls.Add(buttonDisChanges);
            Controls.Add(textBoxWorkExperience);
            Controls.Add(labelCode);
            Name = "DriverEdit";
            Text = "DriverEdit";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private DateTimePicker dateTimePickerDateOfBirth;
        private Label labelTransportationTime;
        private Label labelDepartureTime;
        private Label labelInermediatePoints;
        private TextBox textBoxName;
        private Label labelEndingPoint;
        private Button buttonSaveChanges;
        private TextBox textBoxId;
        private Label labelStartingPoint;
        private Button buttonDisChanges;
        private TextBox textBoxWorkExperience;
        private Label labelCode;
        private CheckedListBox checkedListBoxCategory;
        private Button buttonChooseClass;
        private Label labelChosenClass;
    }
}