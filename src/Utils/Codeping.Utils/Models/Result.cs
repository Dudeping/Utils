using System;

namespace Codeping.Utils
{
    public class Result
    {
        public bool Succeeded { get; set; }

        public string ErrorMessage { get; set; }

        public Exception Exception { get; set; }

        public virtual Result Ok()
        {
            this.Succeeded = true;

            this.Exception = null;

            this.ErrorMessage = null;

            return this;
        }

        public virtual Result Fail(string message)
        {
            this.Succeeded = false;

            this.ErrorMessage = message;

            return this;
        }

        public virtual Result Fail(Exception exception)
        {
            this.Succeeded = false;

            this.Exception = exception;

            this.ErrorMessage = exception.Message;

            return this;
        }

        public Result Merge(Result result)
        {
            this.Succeeded = result.Succeeded;

            this.Exception = result.Exception;

            this.ErrorMessage = result.ErrorMessage;

            return this;
        }
    }

    public class Result<TValue> : Result
    {
        public TValue Value { get; set; }

        public Result<TValue> Ok(TValue value)
        {
            base.Ok();

            return this.SetValue(value);
        }

        public new Result<TValue> Fail(string message)
        {
            base.Fail(message);

            return this.SetValue(default);
        }

        public new Result<TValue> Fail(Exception exception)
        {
            base.Fail(exception);

            return this.SetValue(default);
        }

        public new Result<TValue> Merge(Result result)
        {
            base.Merge(result);

            return this;
        }

        public Result<TValue> Merge(Result<TValue> result)
        {
            base.Merge(result);

            return this.SetValue(result.Value);
        }

        public Result<TValue> SetValue(TValue value)
        {
            this.Value = value;

            return this;
        }
    }
}
