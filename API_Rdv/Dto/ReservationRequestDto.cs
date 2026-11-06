namespace API_Rdv.Dto
{
    public class ReservationRequestDto
    {
        public int SalleId { get; set; }
        public DateTime Debut { get; set; }
        public DateTime Fin { get; set; }
        public int UserId { get; set; }
    }
}
