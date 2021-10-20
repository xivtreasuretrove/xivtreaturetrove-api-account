namespace XIVTreasureTrove.Account.Service.ResponseObjects
{
    /// <summary>
    /// 
    /// </summary>
    public class MessageObject
    {
        /// <summary>
        /// 
        /// </summary>
        public static readonly MessageObject Success = new MessageObject("Success");

        /// <summary>
        /// 
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public MessageObject(string message)
        {
            Message = message;
        }
    }
}
