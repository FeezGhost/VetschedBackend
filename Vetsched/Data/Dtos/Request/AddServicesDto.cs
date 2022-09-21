namespace Vetsched.Data.Dtos.Request
{
    public class AddServicesDto
    {
        public Guid UserId { get; set; }
        public List<Guid> ServiceIds { get; set; }
    }
}
