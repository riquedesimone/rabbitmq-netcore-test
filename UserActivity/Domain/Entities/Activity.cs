using System;
namespace UserActivity.Domain.Entities
{
    public class Activity
    {
        public int ActivityId { get; set; }
        public string SessionId { get; set; }
        public string Path { get; set; }
        public string IP { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
