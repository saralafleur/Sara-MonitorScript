using System.Collections.Generic;
using Sara.NETStandard.MonitorScript.Syntax.Declarations;

namespace Sara.NETStandard.MonitorScript.Syntax.Statements
{
    public class EventStatement : Statement
    {
        public override SyntaxKind Kind => SyntaxKind.EventStatement;

        /// <summary>
        /// Type of the Event: Start, Stop, Reset, etc.
        /// </summary>
        public string EventType;
        
        /// <summary>
        /// Name of the Event, used to recover the Regular Expression 
        /// </summary>
        public string Event;
        private SourceSpan sourceSpan;
        public List<OptionDeclaration> Options;

        public EventStatement(SourceSpan sourceSpan, string eventType, string @event, List<OptionDeclaration> options) : base (sourceSpan)
        {
            this.sourceSpan = sourceSpan;
            EventType = eventType;
            Event = @event;
            Options = options;
        }
    }
}
