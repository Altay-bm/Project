using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace Hotel
{
    public static class BookingService
    {
        private static List<Booking> bookings = new List<Booking>();
        private static int nextId = 1;

        public class Booking
        {
            public int ID { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Phone { get; set; }
            public string Email { get; set; }
            public string RoomType { get; set; }
            public DateTime CheckInDate { get; set; }
            public DateTime CheckOutDate { get; set; }
            public DateTime BookingDate { get; set; }
            public string Notes { get; set; }
            public int TotalPrice { get; set; }
            public int Nights { get; set; }
        }

        public static bool SaveBooking(string firstName, string lastName, string phone,
                                     string email, string roomType, DateTime checkIn,
                                     DateTime checkOut, int totalPrice, int nights)
        {
            try
            {
                Booking newBooking = new Booking
                {
                    ID = nextId++,
                    FirstName = firstName,
                    LastName = lastName,
                    Phone = phone,
                    Email = email,
                    RoomType = roomType,
                    CheckInDate = checkIn,
                    CheckOutDate = checkOut,
                    BookingDate = DateTime.Now,
                    TotalPrice = totalPrice,
                    Nights = nights
                };

                bookings.Add(newBooking);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения: {ex.Message}");
                return false;
            }
        }
        public static bool DeleteBooking(int bookingId)
        {
            try
            {
                // Ищем бронирование по ID
                Booking bookingToRemove = bookings.Find(b => b.ID == bookingId);

                if (bookingToRemove != null)
                {
                    bookings.Remove(bookingToRemove);
                    return true;
                }
                else
                {
                    MessageBox.Show("Бронирование не найдено!", "Ошибка");
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка удаления: {ex.Message}", "Ошибка");
                return false;
            }
        }
        public static DataTable GetBookings()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("Имя", typeof(string));
            dt.Columns.Add("Фамилия", typeof(string));
            dt.Columns.Add("Телефон", typeof(string));
            dt.Columns.Add("Тип номера", typeof(string));
            dt.Columns.Add("Заезд", typeof(DateTime));
            dt.Columns.Add("Выезд", typeof(DateTime));
            dt.Columns.Add("Ночей", typeof(int));
            dt.Columns.Add("Стоимость", typeof(int));

            foreach (var booking in bookings)
            {
                dt.Rows.Add(
                    booking.ID,
                    booking.FirstName,
                    booking.LastName,
                    booking.Phone,
                    booking.RoomType,
                    booking.CheckInDate,
                    booking.CheckOutDate,
                    booking.Nights,
                    booking.TotalPrice
                );
            }

            return dt;
        }
    }
}