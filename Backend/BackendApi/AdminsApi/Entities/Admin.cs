﻿using ServicesCommon;

namespace AdminsApi.Entities
{
    public class Admin : IEntity
    {
        public Guid Id { get; set; }
        public required string UserName { get; set; }
        public required string PassWord { get; set; }
        public string? Email { get; set; }

        public string? Name { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Image {  get; set; }


        public bool isDeleted { get; set; }
        public DateTimeOffset? CreatedDate { get; set; }
        public DateTimeOffset LatestUpdatedDate { get; set; }
    }
}