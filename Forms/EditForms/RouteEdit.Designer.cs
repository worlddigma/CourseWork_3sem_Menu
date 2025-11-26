namespace CourseWork_3sem_Menu.Forms.EditForms
{
    partial class RouteEdit
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
            labelTransportationTime = new Label();
            labelDepartureDays = new Label();
            labelDepartureTime = new Label();
            labelInermediatePoints = new Label();
            textBoxCode = new TextBox();
            labelEndingPoint = new Label();
            buttonSaveChanges = new Button();
            textBoxStartingPoint = new TextBox();
            labelStartingPoint = new Label();
            buttonDisChanges = new Button();
            textBoxEndingPoint = new TextBox();
            textBoxIntermediatePoints = new TextBox();
            labelCode = new Label();
            checkedListBoxDepartureDays = new CheckedListBox();
            dateTimePickerDepartureTime = new DateTimePicker();
            dateTimePickerTransportationTimeHours = new DateTimePicker();
            textBoxTransportationTimeDay = new TextBox();
            SuspendLayout();
            // 
            // labelTransportationTime
            // 
            labelTransportationTime.AutoSize = true;
            labelTransportationTime.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            labelTransportationTime.Location = new Point(16, 167);
            labelTransportationTime.Name = "labelTransportationTime";
            labelTransportationTime.Size = new Size(86, 17);
            labelTransportationTime.TabIndex = 42;
            labelTransportationTime.Text = "Время в пути";
            // 
            // labelDepartureDays
            // 
            labelDepartureDays.AutoSize = true;
            labelDepartureDays.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            labelDepartureDays.Location = new Point(348, 13);
            labelDepartureDays.Name = "labelDepartureDays";
            labelDepartureDays.Size = new Size(112, 17);
            labelDepartureDays.TabIndex = 40;
            labelDepartureDays.Text = "Дни отправления";
            // 
            // labelDepartureTime
            // 
            labelDepartureTime.AutoSize = true;
            labelDepartureTime.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            labelDepartureTime.Location = new Point(16, 137);
            labelDepartureTime.Name = "labelDepartureTime";
            labelDepartureTime.Size = new Size(127, 17);
            labelDepartureTime.TabIndex = 38;
            labelDepartureTime.Text = "Время отправления";
            // 
            // labelInermediatePoints
            // 
            labelInermediatePoints.AutoSize = true;
            labelInermediatePoints.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            labelInermediatePoints.Location = new Point(16, 108);
            labelInermediatePoints.Name = "labelInermediatePoints";
            labelInermediatePoints.Size = new Size(151, 17);
            labelInermediatePoints.TabIndex = 36;
            labelInermediatePoints.Text = "Промежуточные пункты";
            // 
            // textBoxCode
            // 
            textBoxCode.CharacterCasing = CharacterCasing.Upper;
            textBoxCode.ForeColor = Color.Black;
            textBoxCode.Location = new Point(218, 12);
            textBoxCode.MaxLength = 9;
            textBoxCode.Name = "textBoxCode";
            textBoxCode.PlaceholderText = "A123CV";
            textBoxCode.RightToLeft = RightToLeft.No;
            textBoxCode.Size = new Size(91, 23);
            textBoxCode.TabIndex = 29;
            textBoxCode.KeyPress += textBoxCode_KeyPress;
            // 
            // labelEndingPoint
            // 
            labelEndingPoint.AutoSize = true;
            labelEndingPoint.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            labelEndingPoint.Location = new Point(16, 75);
            labelEndingPoint.Name = "labelEndingPoint";
            labelEndingPoint.Size = new Size(103, 17);
            labelEndingPoint.TabIndex = 34;
            labelEndingPoint.Text = "Конечный пункт";
            // 
            // buttonSaveChanges
            // 
            buttonSaveChanges.FlatStyle = FlatStyle.Flat;
            buttonSaveChanges.Location = new Point(369, 243);
            buttonSaveChanges.Name = "buttonSaveChanges";
            buttonSaveChanges.Size = new Size(91, 45);
            buttonSaveChanges.TabIndex = 32;
            buttonSaveChanges.Text = "Сохранить изменения";
            buttonSaveChanges.UseVisualStyleBackColor = true;
            buttonSaveChanges.Click += buttonSaveChanges_Click;
            // 
            // textBoxStartingPoint
            // 
            textBoxStartingPoint.ForeColor = Color.Black;
            textBoxStartingPoint.Location = new Point(218, 45);
            textBoxStartingPoint.Name = "textBoxStartingPoint";
            textBoxStartingPoint.PlaceholderText = "Moskow";
            textBoxStartingPoint.Size = new Size(91, 23);
            textBoxStartingPoint.TabIndex = 31;
            textBoxStartingPoint.KeyPress += textBoxStartingPoint_KeyPress;
            // 
            // labelStartingPoint
            // 
            labelStartingPoint.AutoSize = true;
            labelStartingPoint.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            labelStartingPoint.Location = new Point(16, 46);
            labelStartingPoint.Name = "labelStartingPoint";
            labelStartingPoint.Size = new Size(110, 17);
            labelStartingPoint.TabIndex = 30;
            labelStartingPoint.Text = "Начальный пункт";
            // 
            // buttonDisChanges
            // 
            buttonDisChanges.FlatStyle = FlatStyle.Flat;
            buttonDisChanges.Location = new Point(489, 243);
            buttonDisChanges.Name = "buttonDisChanges";
            buttonDisChanges.Size = new Size(91, 45);
            buttonDisChanges.TabIndex = 33;
            buttonDisChanges.Text = "Отменить изменения";
            buttonDisChanges.UseVisualStyleBackColor = true;
            buttonDisChanges.Click += buttonDisChanges_Click;
            // 
            // textBoxEndingPoint
            // 
            textBoxEndingPoint.Location = new Point(218, 74);
            textBoxEndingPoint.Name = "textBoxEndingPoint";
            textBoxEndingPoint.PlaceholderText = "Omsk";
            textBoxEndingPoint.Size = new Size(91, 23);
            textBoxEndingPoint.TabIndex = 35;
            textBoxEndingPoint.KeyPress += textBoxEndingPoint_KeyPress;
            // 
            // textBoxIntermediatePoints
            // 
            textBoxIntermediatePoints.Location = new Point(218, 107);
            textBoxIntermediatePoints.Name = "textBoxIntermediatePoints";
            textBoxIntermediatePoints.PlaceholderText = "Балачово, Адыгеевка, Сараево, Кисловодск";
            textBoxIntermediatePoints.Size = new Size(91, 23);
            textBoxIntermediatePoints.TabIndex = 37;
            textBoxIntermediatePoints.KeyPress += textBoxIntermediatePoints_KeyPress;
            // 
            // labelCode
            // 
            labelCode.AutoSize = true;
            labelCode.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            labelCode.Location = new Point(16, 13);
            labelCode.Name = "labelCode";
            labelCode.Size = new Size(110, 17);
            labelCode.TabIndex = 28;
            labelCode.Text = "Шифр маршрута";
            // 
            // checkedListBoxDepartureDays
            // 
            checkedListBoxDepartureDays.FormattingEnabled = true;
            checkedListBoxDepartureDays.Items.AddRange(new object[] { "Понедельник ", "Вторник", "Среда", "Четверг", "Пятница", "Суббота", "Воскресенье" });
            checkedListBoxDepartureDays.Location = new Point(475, 12);
            checkedListBoxDepartureDays.Name = "checkedListBoxDepartureDays";
            checkedListBoxDepartureDays.Size = new Size(105, 130);
            checkedListBoxDepartureDays.TabIndex = 44;
            checkedListBoxDepartureDays.Tag = "";
            checkedListBoxDepartureDays.SelectedIndexChanged += checkedListBoxDepartureDays_SelectedIndexChanged;
            // 
            // dateTimePickerDepartureTime
            // 
            dateTimePickerDepartureTime.CustomFormat = "HH:mm";
            dateTimePickerDepartureTime.Format = DateTimePickerFormat.Custom;
            dateTimePickerDepartureTime.Location = new Point(218, 137);
            dateTimePickerDepartureTime.Name = "dateTimePickerDepartureTime";
            dateTimePickerDepartureTime.ShowUpDown = true;
            dateTimePickerDepartureTime.Size = new Size(91, 23);
            dateTimePickerDepartureTime.TabIndex = 45;
            dateTimePickerDepartureTime.Value = new DateTime(2025, 10, 1, 0, 0, 0, 0);
            // 
            // dateTimePickerTransportationTimeHours
            // 
            dateTimePickerTransportationTimeHours.CustomFormat = "HH:mm";
            dateTimePickerTransportationTimeHours.Format = DateTimePickerFormat.Custom;
            dateTimePickerTransportationTimeHours.Location = new Point(236, 167);
            dateTimePickerTransportationTimeHours.Name = "dateTimePickerTransportationTimeHours";
            dateTimePickerTransportationTimeHours.Size = new Size(73, 23);
            dateTimePickerTransportationTimeHours.TabIndex = 46;
            dateTimePickerTransportationTimeHours.Value = new DateTime(2025, 11, 1, 0, 0, 0, 0);
            dateTimePickerTransportationTimeHours.ValueChanged += dateTimePicker2_ValueChanged;
            // 
            // textBoxTransportationTimeDay
            // 
            textBoxTransportationTimeDay.Location = new Point(218, 167);
            textBoxTransportationTimeDay.Name = "textBoxTransportationTimeDay";
            textBoxTransportationTimeDay.Size = new Size(20, 23);
            textBoxTransportationTimeDay.TabIndex = 47;
            textBoxTransportationTimeDay.Text = "0";
            textBoxTransportationTimeDay.KeyPress += textBoxTransportationTimeDay_KeyPress;
            // 
            // RouteEdit
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(textBoxTransportationTimeDay);
            Controls.Add(dateTimePickerTransportationTimeHours);
            Controls.Add(dateTimePickerDepartureTime);
            Controls.Add(checkedListBoxDepartureDays);
            Controls.Add(labelTransportationTime);
            Controls.Add(labelDepartureDays);
            Controls.Add(labelDepartureTime);
            Controls.Add(labelInermediatePoints);
            Controls.Add(textBoxCode);
            Controls.Add(labelEndingPoint);
            Controls.Add(buttonSaveChanges);
            Controls.Add(textBoxStartingPoint);
            Controls.Add(labelStartingPoint);
            Controls.Add(buttonDisChanges);
            Controls.Add(textBoxEndingPoint);
            Controls.Add(textBoxIntermediatePoints);
            Controls.Add(labelCode);
            Name = "RouteEdit";
            Text = "RouteEdit";
            Load += RouteEdit_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelTransportationTime;
        private Label labelDepartureDays;
        private Label labelDepartureTime;
        private Label labelInermediatePoints;
        private TextBox textBoxCode;
        private Label labelEndingPoint;
        private Button buttonSaveChanges;
        private TextBox textBoxStartingPoint;
        private Label labelStartingPoint;
        private Button buttonDisChanges;
        private TextBox textBoxEndingPoint;
        private TextBox textBoxIntermediatePoints;
        private Label labelCode;
        private CheckedListBox checkedListBoxDepartureDays;
        private DateTimePicker dateTimePickerDepartureTime;
        private DateTimePicker dateTimePickerTransportationTimeHours;
        private TextBox textBoxTransportationTimeDay;
    }
}