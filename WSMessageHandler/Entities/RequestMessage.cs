namespace WSMessageHandler.Entities
{
    public class RequestMessage
    {
        public string Message { get; set; }

        public bool IsValid()
        {
            if (string.IsNullOrEmpty(Message))
                return false;

            return true;
        }
    }
}
