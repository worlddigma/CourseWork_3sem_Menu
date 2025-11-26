namespace CourseWork_3sem_Menu.Forms.EditForms
{
    partial class TransportationEdit
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
            textBoxCode = new TextBox();
            labelCode = new Label();
            textBoxId = new TextBox();
            labelStartingPoint = new Label();
            textBoxStateNumber = new TextBox();
            labelState = new Label();
            dateTimePickerDateOfTransportation = new DateTimePicker();
            labelEndingPoint = new Label();
            labelYearOfMajor = new Label();
            labelYear = new Label();
            textBoxSoldTickets = new TextBox();
            textBoxTicketCost = new TextBox();
            textBoxTotalRevenue = new TextBox();
            label1 = new Label();
            buttonSaveChanges = new Button();
            buttonDisChanges = new Button();
            buttonChooseRoute = new Button();
            buttonChooseDriver = new Button();
            buttonChooseBus = new Button();
            panelTransportationEdit = new Panel();
            panelTransportationEdit.SuspendLayout();
            SuspendLayout();
            // 
            // textBoxCode
            // 
            textBoxCode.Anchor = AnchorStyles.Right;
            textBoxCode.CharacterCasing = CharacterCasing.Upper;
            textBoxCode.ForeColor = Color.Black;
            textBoxCode.Location = new Point(573, 17);
            textBoxCode.MaxLength = 9;
            textBoxCode.Name = "textBoxCode";
            textBoxCode.PlaceholderText = "A123CV";
            textBoxCode.ReadOnly = true;
            textBoxCode.RightToLeft = RightToLeft.No;
            textBoxCode.Size = new Size(91, 23);
            textBoxCode.TabIndex = 31;
            // 
            // labelCode
            // 
            labelCode.Anchor = AnchorStyles.Left;
            labelCode.AutoSize = true;
            labelCode.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            labelCode.Location = new Point(12, 18);
            labelCode.Name = "labelCode";
            labelCode.Size = new Size(110, 17);
            labelCode.TabIndex = 30;
            labelCode.Text = "Шифр маршрута";
            // 
            // textBoxId
            // 
            textBoxId.Anchor = AnchorStyles.Right;
            textBoxId.ForeColor = Color.Black;
            textBoxId.Location = new Point(573, 53);
            textBoxId.Name = "textBoxId";
            textBoxId.PlaceholderText = "10";
            textBoxId.ReadOnly = true;
            textBoxId.Size = new Size(91, 23);
            textBoxId.TabIndex = 53;
            // 
            // labelStartingPoint
            // 
            labelStartingPoint.Anchor = AnchorStyles.Left;
            labelStartingPoint.AutoSize = true;
            labelStartingPoint.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            labelStartingPoint.Location = new Point(12, 54);
            labelStartingPoint.Name = "labelStartingPoint";
            labelStartingPoint.Size = new Size(176, 17);
            labelStartingPoint.TabIndex = 52;
            labelStartingPoint.Text = "Табельный номер водителя";
            // 
            // textBoxStateNumber
            // 
            textBoxStateNumber.Anchor = AnchorStyles.Right;
            textBoxStateNumber.CharacterCasing = CharacterCasing.Upper;
            textBoxStateNumber.ForeColor = Color.Black;
            textBoxStateNumber.Location = new Point(573, 89);
            textBoxStateNumber.MaxLength = 9;
            textBoxStateNumber.Name = "textBoxStateNumber";
            textBoxStateNumber.PlaceholderText = "A111A111";
            textBoxStateNumber.ReadOnly = true;
            textBoxStateNumber.RightToLeft = RightToLeft.No;
            textBoxStateNumber.Size = new Size(91, 23);
            textBoxStateNumber.TabIndex = 55;
            // 
            // labelState
            // 
            labelState.Anchor = AnchorStyles.Left;
            labelState.AutoSize = true;
            labelState.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            labelState.Location = new Point(12, 90);
            labelState.Name = "labelState";
            labelState.Size = new Size(212, 17);
            labelState.TabIndex = 54;
            labelState.Text = "Государственный номер автобуса";
            // 
            // dateTimePickerDateOfTransportation
            // 
            dateTimePickerDateOfTransportation.Anchor = AnchorStyles.Right;
            dateTimePickerDateOfTransportation.CustomFormat = "dd.MM.yyyy";
            dateTimePickerDateOfTransportation.Format = DateTimePickerFormat.Custom;
            dateTimePickerDateOfTransportation.Location = new Point(573, 127);
            dateTimePickerDateOfTransportation.MaxDate = new DateTime(2500, 12, 31, 0, 0, 0, 0);
            dateTimePickerDateOfTransportation.MinDate = new DateTime(1960, 1, 1, 0, 0, 0, 0);
            dateTimePickerDateOfTransportation.Name = "dateTimePickerDateOfTransportation";
            dateTimePickerDateOfTransportation.Size = new Size(91, 23);
            dateTimePickerDateOfTransportation.TabIndex = 64;
            dateTimePickerDateOfTransportation.Value = new DateTime(2000, 12, 1, 0, 0, 0, 0);
            // 
            // labelEndingPoint
            // 
            labelEndingPoint.Anchor = AnchorStyles.Left;
            labelEndingPoint.AutoSize = true;
            labelEndingPoint.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            labelEndingPoint.Location = new Point(12, 124);
            labelEndingPoint.Name = "labelEndingPoint";
            labelEndingPoint.Size = new Size(75, 17);
            labelEndingPoint.TabIndex = 63;
            labelEndingPoint.Text = "Дата рейса";
            // 
            // labelYearOfMajor
            // 
            labelYearOfMajor.Anchor = AnchorStyles.Left;
            labelYearOfMajor.AutoSize = true;
            labelYearOfMajor.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            labelYearOfMajor.Location = new Point(12, 202);
            labelYearOfMajor.Name = "labelYearOfMajor";
            labelYearOfMajor.Size = new Size(84, 17);
            labelYearOfMajor.TabIndex = 67;
            labelYearOfMajor.Text = "Цена билета";
            // 
            // labelYear
            // 
            labelYear.Anchor = AnchorStyles.Left;
            labelYear.AutoSize = true;
            labelYear.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            labelYear.Location = new Point(12, 169);
            labelYear.Name = "labelYear";
            labelYear.Size = new Size(115, 17);
            labelYear.TabIndex = 65;
            labelYear.Text = "Продано билетов";
            // 
            // textBoxSoldTickets
            // 
            textBoxSoldTickets.Anchor = AnchorStyles.Right;
            textBoxSoldTickets.Location = new Point(573, 168);
            textBoxSoldTickets.MaxLength = 10;
            textBoxSoldTickets.Name = "textBoxSoldTickets";
            textBoxSoldTickets.PlaceholderText = "20";
            textBoxSoldTickets.Size = new Size(91, 23);
            textBoxSoldTickets.TabIndex = 66;
            // 
            // textBoxTicketCost
            // 
            textBoxTicketCost.Anchor = AnchorStyles.Right;
            textBoxTicketCost.Location = new Point(573, 201);
            textBoxTicketCost.MaxLength = 20;
            textBoxTicketCost.Name = "textBoxTicketCost";
            textBoxTicketCost.PlaceholderText = "2000";
            textBoxTicketCost.Size = new Size(91, 23);
            textBoxTicketCost.TabIndex = 68;
            textBoxTicketCost.KeyPress += textBoxTicketCost_KeyPress;
            // 
            // textBoxTotalRevenue
            // 
            textBoxTotalRevenue.Anchor = AnchorStyles.Right;
            textBoxTotalRevenue.CharacterCasing = CharacterCasing.Upper;
            textBoxTotalRevenue.ForeColor = Color.Black;
            textBoxTotalRevenue.Location = new Point(573, 241);
            textBoxTotalRevenue.MaxLength = 9;
            textBoxTotalRevenue.Name = "textBoxTotalRevenue";
            textBoxTotalRevenue.PlaceholderText = "40000";
            textBoxTotalRevenue.ReadOnly = true;
            textBoxTotalRevenue.RightToLeft = RightToLeft.No;
            textBoxTotalRevenue.Size = new Size(91, 23);
            textBoxTotalRevenue.TabIndex = 70;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Left;
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label1.Location = new Point(12, 242);
            label1.Name = "label1";
            label1.Size = new Size(105, 17);
            label1.TabIndex = 69;
            label1.Text = "Общая выручка";
            // 
            // buttonSaveChanges
            // 
            buttonSaveChanges.Anchor = AnchorStyles.Right;
            buttonSaveChanges.FlatStyle = FlatStyle.Flat;
            buttonSaveChanges.Location = new Point(573, 299);
            buttonSaveChanges.Name = "buttonSaveChanges";
            buttonSaveChanges.Size = new Size(91, 45);
            buttonSaveChanges.TabIndex = 71;
            buttonSaveChanges.Text = "Сохранить изменения";
            buttonSaveChanges.UseVisualStyleBackColor = true;
            buttonSaveChanges.Click += buttonSaveChanges_Click;
            // 
            // buttonDisChanges
            // 
            buttonDisChanges.Anchor = AnchorStyles.Right;
            buttonDisChanges.FlatStyle = FlatStyle.Flat;
            buttonDisChanges.Location = new Point(697, 299);
            buttonDisChanges.Name = "buttonDisChanges";
            buttonDisChanges.Size = new Size(91, 45);
            buttonDisChanges.TabIndex = 72;
            buttonDisChanges.Text = "Отменить изменения";
            buttonDisChanges.UseVisualStyleBackColor = true;
            buttonDisChanges.Click += buttonDisChanges_Click;
            // 
            // buttonChooseRoute
            // 
            buttonChooseRoute.Anchor = AnchorStyles.Right;
            buttonChooseRoute.FlatStyle = FlatStyle.Flat;
            buttonChooseRoute.Location = new Point(697, 12);
            buttonChooseRoute.Name = "buttonChooseRoute";
            buttonChooseRoute.Size = new Size(91, 31);
            buttonChooseRoute.TabIndex = 73;
            buttonChooseRoute.Text = "Выбрать";
            buttonChooseRoute.UseVisualStyleBackColor = true;
            buttonChooseRoute.Click += buttonChooseRoute_Click;
            // 
            // buttonChooseDriver
            // 
            buttonChooseDriver.Anchor = AnchorStyles.Right;
            buttonChooseDriver.FlatStyle = FlatStyle.Flat;
            buttonChooseDriver.Location = new Point(697, 49);
            buttonChooseDriver.Name = "buttonChooseDriver";
            buttonChooseDriver.Size = new Size(91, 31);
            buttonChooseDriver.TabIndex = 74;
            buttonChooseDriver.Text = "Выбрать";
            buttonChooseDriver.UseVisualStyleBackColor = true;
            buttonChooseDriver.Click += buttonChooseDriver_Click;
            // 
            // buttonChooseBus
            // 
            buttonChooseBus.Anchor = AnchorStyles.Right;
            buttonChooseBus.FlatStyle = FlatStyle.Flat;
            buttonChooseBus.Location = new Point(697, 86);
            buttonChooseBus.Name = "buttonChooseBus";
            buttonChooseBus.Size = new Size(91, 31);
            buttonChooseBus.TabIndex = 75;
            buttonChooseBus.Text = "Выбрать";
            buttonChooseBus.UseVisualStyleBackColor = true;
            buttonChooseBus.Click += buttonChooseBus_Click;
            // 
            // panelTransportationEdit
            // 
            panelTransportationEdit.Controls.Add(buttonChooseBus);
            panelTransportationEdit.Controls.Add(buttonChooseDriver);
            panelTransportationEdit.Controls.Add(buttonChooseRoute);
            panelTransportationEdit.Controls.Add(buttonSaveChanges);
            panelTransportationEdit.Controls.Add(buttonDisChanges);
            panelTransportationEdit.Controls.Add(textBoxTotalRevenue);
            panelTransportationEdit.Controls.Add(label1);
            panelTransportationEdit.Controls.Add(labelYearOfMajor);
            panelTransportationEdit.Controls.Add(labelYear);
            panelTransportationEdit.Controls.Add(textBoxSoldTickets);
            panelTransportationEdit.Controls.Add(textBoxTicketCost);
            panelTransportationEdit.Controls.Add(dateTimePickerDateOfTransportation);
            panelTransportationEdit.Controls.Add(labelEndingPoint);
            panelTransportationEdit.Controls.Add(textBoxStateNumber);
            panelTransportationEdit.Controls.Add(labelState);
            panelTransportationEdit.Controls.Add(textBoxId);
            panelTransportationEdit.Controls.Add(labelStartingPoint);
            panelTransportationEdit.Controls.Add(textBoxCode);
            panelTransportationEdit.Controls.Add(labelCode);
            panelTransportationEdit.Dock = DockStyle.Fill;
            panelTransportationEdit.Location = new Point(0, 0);
            panelTransportationEdit.Name = "panelTransportationEdit";
            panelTransportationEdit.Size = new Size(800, 450);
            panelTransportationEdit.TabIndex = 76;
            // 
            // TransportationEdit
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(panelTransportationEdit);
            Name = "TransportationEdit";
            Text = "TransportationEdit";
            panelTransportationEdit.ResumeLayout(false);
            panelTransportationEdit.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TextBox textBoxCode;
        private Label labelCode;
        private TextBox textBoxId;
        private Label labelStartingPoint;
        private TextBox textBoxStateNumber;
        private Label labelState;
        private DateTimePicker dateTimePickerDateOfTransportation;
        private Label labelEndingPoint;
        private Label labelYearOfMajor;
        private Label labelYear;
        private TextBox textBoxSoldTickets;
        private TextBox textBoxTicketCost;
        private TextBox textBoxTotalRevenue;
        private Label label1;
        private Button buttonSaveChanges;
        private Button buttonDisChanges;
        private Button buttonChooseRoute;
        private Button buttonChooseDriver;
        private Button buttonChooseBus;
        private Panel panelTransportationEdit;
    }
}