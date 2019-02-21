using System;
using System.Collections.Generic;
using System.Text;

namespace Codeping.Utils
{
    public class OperatResult
    {
        public bool IsError { get; set; }

        public string ErrorMessage { get; set; }

        public Exception Exception { get; set; }

        public virtual OperatResult Ok()
        {
            this.IsError = false;

            return this;
        }

        public virtual OperatResult Fail(string message)
        {
            this.IsError = true;

            this.ErrorMessage = message;

            return this;
        }

        public virtual OperatResult Fail(Exception exception)
        {
            this.IsError = true;

            this.Exception = exception;

            this.ErrorMessage = exception.Message;

            return this;
        }
    }

    public class OperatResult<TValue> : OperatResult
    {
        public TValue Value { get; set; }

        public OperatResult<TValue> Ok(TValue value)
        {
            base.Ok();

            return this.SetValue(value);
        }

        public new OperatResult<TValue> Fail(string message)
        {
            base.Fail(message);

            return this;
        }

        public OperatResult<TValue> Fail(OperatResult result)
        {
            base.Fail(result.ErrorMessage);

            return this;
        }

        public new OperatResult<TValue> Fail(Exception exception)
        {
            base.Fail(exception);

            return this;
        }

        public OperatResult<TValue> SetValue(TValue value)
        {
            this.Value = value;

            return this;
        }
    }
}
