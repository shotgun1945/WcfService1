using System;

namespace ShareEntity.Entity
{
    public class LifeBasic : EntityBase
    {
        public DataType DataType { get; set; }
        public UserDevice UserDevice { get; set; }
        public string Subject { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string Comment { get; set; }
        public long Latitude { get; set; }
        public long Longitude { get; set; }
        public string ConnectProcess { get; set; }
    }
}
