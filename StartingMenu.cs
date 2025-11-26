using CourseWork_3sem;
using CourseWork_3sem_Menu.Forms.EditForms;

namespace CourseWork_3sem_Menu
{
    public partial class StartingMenu : Form
    {
        private Button _CurrentButton;
        private Form _ActiveForm;
        private BusFleet _BusFleet;
        private RouteCollection _RouteCollection;
        private DriverStaff _DriverStaff;
        private VolumeOfTransportation _VolumeOfTransportation;
        public StartingMenu(BusFleet busFleet, RouteCollection routeCollection, DriverStaff driverStaff, VolumeOfTransportation volumeOfTransportation)
        {
            InitializeComponent();
            _BusFleet = busFleet;
            _RouteCollection = routeCollection;
            _DriverStaff = driverStaff;
            _VolumeOfTransportation = volumeOfTransportation;
        }

        private void ActivateButton(object btnSender)
        {
            if (btnSender != null)
            {
                DisableButton();
                if (_CurrentButton != (Button)btnSender)
                {
                    _CurrentButton = (Button)btnSender;
                    _CurrentButton.BackColor = Color.Gray;
                    _CurrentButton.ForeColor = Color.White;
                    _CurrentButton.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 204);
                }
            }
        }
        private void DisableButton()
        {
            foreach (Control previousButton in panelMenu.Controls)
            {
                previousButton.BackColor = Color.DarkGray;
                previousButton.ForeColor = Color.Black;
                previousButton.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 204);
            }
        }
        public void OpenChildForm(Form childForm, object btnSender)
        {
            if (_ActiveForm != null)
            {
                _ActiveForm.Close();
            }
            ActivateButton(btnSender);
            _ActiveForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            this.panelDesktop.Controls.Add(childForm);
            this.panelDesktop.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
            labelTitelText.Text = childForm.Text;

        }


        private void StartingWindow(object sender, EventArgs e)
        {

        }


        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
        }


        private void StartingMenu_MouseDown(object sender, MouseEventArgs e)
        {
        }

        private void buttonBuses_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.FormBuses(_BusFleet), sender);
        }

        private void buttonRoutes_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.FormRoutes(_RouteCollection), sender);
        }

        private void buttonDrivers_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.FormDrivers(_DriverStaff), sender);
        }

        private void buttonTransportation_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.FormTransportation(_RouteCollection, _BusFleet, _DriverStaff, _VolumeOfTransportation), sender);
        }

        private void panelTitleBar_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panelDesktop_Paint(object sender, PaintEventArgs e)
        {

        }

        private void StartingMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                // Автоматически сохраняем данные
                WriteFile.Write(_BusFleet, _DriverStaff, _RouteCollection, _VolumeOfTransportation);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
