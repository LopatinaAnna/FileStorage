namespace FileStorage.BLL.Infrastructure
{
    /// <summary>
    /// Operation details class, store information about the success of the operation
    /// </summary>
    public class OperationDetails
    {
        public bool Succedeed { get; private set; }

        public string Message { get; private set; }

        public string Property { get; private set; }

        public OperationDetails(bool succedeed, string message, string prop)
        {
            Succedeed = succedeed;
            Message = message;
            Property = prop;
        }
    }
}