﻿namespace Vetsched.Data.Dtos.Request
{
    public class AddServicesDto
    {
        public Guid ProfileId { get; set; }
        public List<Guid> ServiceIds { get; set; }
    }
}
