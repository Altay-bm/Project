using HotelApp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hotel
{
    public partial class BookingForm : Form
    {
        private int currentTotalPrice = 0;
        private int currentNights = 0;

        public BookingForm()
        {
            InitializeComponent();

            cmbRoomType.Items.AddRange(new string[] {
                "Эконом", "Стандарт", "Люкс", "Президентский"
            });

            cmbGuests.Items.AddRange(new string[] { "1", "2", "3", "4" });
            cmbGuests.SelectedIndex = 1;
        }

        private void button1_Click(object sender, EventArgs e) // Расчет стоимости
        {
            if (cmbRoomType.SelectedItem == null)
            {
                MessageBox.Show("Сначала выберите тип номера!");
                return;
            }

            currentNights = (int)(dtpCheckOut.Value - dtpCheckIn.Value).TotalDays;
            if (currentNights <= 0)
            {
                MessageBox.Show("Дата выезда должна быть позже даты заезда!");
                return;
            }

            int price = GetPricePerNight(cmbRoomType.SelectedItem.ToString());
            currentTotalPrice = currentNights * price;

            lblPrice.Text = $"Стоимость: {currentTotalPrice} руб. ({currentNights} ночей)";
            panelPrice.BackColor = System.Drawing.Color.LightGreen;
        }

        private void button3_Click(object sender, EventArgs e) // Бронировать
        {
            // Проверка заполнения полей
            if (string.IsNullOrWhiteSpace(textBoxFirstName.Text) ||
                string.IsNullOrWhiteSpace(textBoxLastName.Text) ||
                string.IsNullOrWhiteSpace(textBoxPhone.Text))
            {
                MessageBox.Show("Заполните обязательные поля!");
                return;
            }

            if (currentTotalPrice == 0)
            {
                MessageBox.Show("Сначала рассчитайте стоимость!");
                return;
            }

            // СОХРАНЯЕМ В BookingService
            bool success = BookingService.SaveBooking(
                textBoxFirstName.Text.Trim(),
                textBoxLastName.Text.Trim(),
                textBoxPhone.Text.Trim(),
                textBoxEmail.Text.Trim(),
                cmbRoomType.SelectedItem.ToString(),
                dtpCheckIn.Value,
                dtpCheckOut.Value,
                currentTotalPrice,
                currentNights
            );

            if (success)
            {
                MessageBox.Show("Бронирование сохранено!\nТеперь вы можете посмотреть его в списке бронирований.", "Успех");
                ClearForm();
            }
        }

        private void button2_Click(object sender, EventArgs e) // Назад
        {
            MainForm mainForm = new MainForm();
            mainForm.Show();
            this.Close();
        }

        private void ClearForm()
        {
            textBoxFirstName.Clear();
            textBoxLastName.Clear();
            textBoxPhone.Clear();
            textBoxEmail.Clear();
            cmbRoomType.SelectedIndex = -1;
            cmbGuests.SelectedIndex = 1;
            dtpCheckIn.Value = DateTime.Today;
            dtpCheckOut.Value = DateTime.Today.AddDays(1);
            lblPrice.Text = "Стоимость: -";
            panelPrice.BackColor = System.Drawing.Color.LightGray;
            currentTotalPrice = 0;
            currentNights = 0;
        }

        private int GetPricePerNight(string roomType)
        {
            switch (roomType)
            {
                case "Эконом": return 2000;
                case "Стандарт": return 3500;
                case "Люкс": return 6000;
                case "Президентский": return 10000;
                default: return 0;
            }
        }
    }
}