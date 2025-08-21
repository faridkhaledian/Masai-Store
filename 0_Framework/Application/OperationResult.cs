namespace _0_Framework.Application
{
    public class OperationResult
    {
        public bool IsSucceddd { get; set; }
        public string Message { get; set; }
        public OperationResult()
        {
            IsSucceddd = false;
        }
        #region Succedded
        public OperationResult Succedded(string message = "عملیات با موفقیت انجام شد")
        {
            IsSucceddd = true;
            Message = message;
            return this;
        }

        #endregion
        #region Failed
        public OperationResult Failed(string message)
        {
            IsSucceddd = false;
            Message = message;
            return this;
        }

        #endregion
    }
}
