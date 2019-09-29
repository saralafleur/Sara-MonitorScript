﻿using System;

namespace Sara.NETStandard.MonitorScript.Parser
{
    [Serializable]
    internal class SyntaxException : Exception
    {
        public SyntaxException()
        {
        }

        public SyntaxException(string message) : base(message)
        {
        }

        public SyntaxException(string message, Exception inner) : base(message, inner)
        {
        }

        protected SyntaxException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context)
        { }
    }
}
