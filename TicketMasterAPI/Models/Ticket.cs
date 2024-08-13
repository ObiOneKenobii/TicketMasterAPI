namespace TicketMasterAPI.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public required string EventName { get; set; }
        public DateTime EventDate { get; set; }
        public decimal Price { get; set; }
        public required string Location { get; set; }
        public required string ArtistName { get; set; }
        public string ? CoverPictureUrl { get; set; }
    }


}
