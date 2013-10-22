namespace ShareEntity.Entity
{
    public class UserDevice :EntityBase
    {
        public DeviceType DeviceType { get; set; }
        public string LoginId { get; set; }
        public string Name { get; set;}
    }

    public enum DeviceType
    {
        PC,Mobile,Web
    };
}
