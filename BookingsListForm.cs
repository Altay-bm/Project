using System;
using System.Data;
using System.Windows.Forms;

namespace Hotel
{
    public partial class BookingsListForm : Form
    {
        public BookingsListForm()
        {
            InitializeComponent();
            LoadBookings();
        }

        private void LoadBookings()
        {
            DataTable dt = BookingService.GetBookings();
            dataGridViewBookings.DataSource = dt;
            dataGridViewBookings.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadBookings();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            MainForm mainForm = new MainForm();
            mainForm.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridViewBookings.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите бронирование для удаления!");
                return;
            }
            int bookingId = Convert.ToInt32(dataGridViewBookings.SelectedRows[0].Cells["ID"].Value);
            string guestName = dataGridViewBookings.SelectedRows[0].Cells["Имя"].Value + " " +
                              dataGridViewBookings.SelectedRows[0].Cells["Фамилия"].Value;

            DialogResult result = MessageBox.Show(
                $"Удалить бронирование для {guestName}?",
                "Подтверждение",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                bool success = BookingService.DeleteBooking(bookingId);
                if (success)
                {
                    MessageBox.Show("Бронирование удалено!");
                    LoadBookings();
                }
            }
       
            

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridViewBookings.SelectedRows.Count > 0)
            {
                string guestName = dataGridViewBookings.SelectedRows[0].Cells["Имя"].Value + " " +
                                  dataGridViewBookings.SelectedRows[0].Cells["Фамилия"].Value;
                MessageBox.Show($"Редактирование: {guestName}\n(в разработке)");
            }
            else
            {
                MessageBox.Show("Выберите бронирование!");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MainForm mainForm = new MainForm();
            mainForm.Show();
            this.Close();
        }
    }
}