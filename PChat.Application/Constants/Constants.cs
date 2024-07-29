namespace PChat.Application.Constants;

public abstract class Constants
{
    public class GroupType
    {
        public const string Single = "single";
        public const string Multi = "multi";
    }

    public class CallStatus
    {
        public const string InComing = "IN_COMMING";
        public const string OutGoing = "OUT_GOING";
        public const string Missed = "MISSED";
    }
    
    public const string AvatarDefault = "PChat.WebAPI/Resources/Static/Avatar/no_image.jpg";
}