namespace API_Rdv.Models
{
    public class ReservationModel
    {
        public int Id { get; set; }
        public int SalleId { get; set; }
        public int CreneauId { get; set; }
        public int UserId { get; set; }
        public DateTime DateReservation { get; set; }
    }
}
