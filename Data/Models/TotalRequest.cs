using System;

namespace Lab23.Data.Models
{
    public class TotalRequest
    {
        public int Id { get; set; }
        public int TotalRequestCount { get; set; }
        public DateTime DateTillUsersBlocked { get; set; }
    }
}