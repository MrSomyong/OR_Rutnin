using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    /// <summary>
    /// A Class for return value of function of method.
    /// </summary>
    [Serializable]
    public class ReturnValue
    {
        /// <summary>
        /// Initializing for default ReturnValue.
        /// </summary>
        public ReturnValue() { }
        /// <summary>
        /// Initializing for ReturnValue with Parameter
        /// </summary>
        /// <param name="value">Value to return [true:false]</param>
        /// <param name="exception">Exception to return</param>
        public ReturnValue(bool value, Exception exception) { this._value = value; this._exception = exception; }
        /// <summary>
        /// Return true or false.
        /// </summary>
        public bool Value
        {
            get { return _value; }
            set { _value = value; }
        }
        /// <summary>
        /// Return Exception if false.
        /// </summary>
        public Exception Exception
        {
            get { return _exception; }
            set { _exception = value; }
        }
        private Exception _exception;
        private bool _value;
        private StringBuilder _Info;
        public StringBuilder DebugInfo
        {
            get { return _Info; }
            set { _Info = value; }
        }
    }
}