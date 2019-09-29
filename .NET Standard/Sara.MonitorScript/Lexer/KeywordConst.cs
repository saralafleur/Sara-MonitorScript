using System.Linq;

namespace Sara.MonitorScript.Lexer
{
    public static class Keyword
    {

        public static readonly string[] PatternKeywords =
        {
            _Pattern, _Repeating, _First, _Source,
            _TotalTime, _IdleTime, _KnownIdle, _UnknownIdle, _HidePattern, _HideFilePath, _Unexpected,
        };
        #region Pattern Keywords
        /// <summary>
        /// Starts a Pattern
        /// </summary>
        public const string _Pattern = "Pattern";
        /// <summary>
        /// When true a Pattern can occur multiple times
        /// </summary>
        public const string _Repeating = "Repeating";
        /// <summary>
        /// Return the first Pattern match
        /// </summary>
        public const string _First = "First";
        public const string _Source = "Source";
        public const string _TotalTime = "TotalTime";
        public const string _IdleTime = "IdleTime";
        public const string _KnownIdle = "KnownIdle";
        public const string _UnknownIdle = "UnknownIdle";
        public const string _HidePattern = "HidePattern";
        public const string _HideFilePath = "HideFilePath";
        public const string _Unexpected = "Unexpected";
        #endregion Pattern Keywords


        public static readonly string[] EventOptionKeywords = {
            _Optional, _Required, _RequiredInOrder, _OneOrMore, _TimeToNext, _TimeToNextKnownIdle, _TimeTo, _TimeToOr, _TimeFrom,
            _Hide, _FirstRepeat, _LastRepeat, _Prior, _Name, _BodyStop, _Value, _HideEvent, _Start, _Body, _Stop, _Reset, _Search,
            _Restart,

            // These are constraints What does that mean, need more defination here. - Sara  
            // Or is a better word conditions?  Think on this - Sara
            _Frequency, _FrequencyPerFile, _Unexpected,
        };


        #region Event Keywords
        public const string _Optional = "Optional";
        public const string _Required = "Required";
        public const string _RequiredInOrder = "RequiredInOrder";
        public const string _OneOrMore = "OneOrMore";
        /// <summary>
        /// Used to record the duration between an event and the next event.
        /// </summary>
        public const string _TimeToNext = "TimeToNext";
        /// <summary>
        /// UnknownIdle is used to measure how IdleTime, time between events that are not measured by a duration, exists within the Pattern.
        /// Time that does not belong to a duration can be flagged as TimeToNextKnownIdle and not included in UnknownIdle
        /// If the un-measured time is too much, then we know we need to revisit the pattern and measure more of it's parts.
        /// </summary>
        public const string _TimeToNextKnownIdle = "TimeToNextKnownIdle";
        public const string _TimeTo = "TimeTo";
        /// <summary>
        /// Same as TimeTo, except allows two different Events to end duration
        /// </summary>
        public const string _TimeToOr = "TimeToOr";
        public const string _TimeFrom = "TimeFrom";
        public const string _Hide = "Hide";
        public const string _FirstRepeat = "FirstRepeat";
        /// <summary>
        /// After the first occurance, continue to search until you reach the last occurance or the next Event in the Pattern
        /// </summary>
        public const string _LastRepeat = "LastRepeat";
        /// <summary>
        /// Prior attribute requires that a Event must occur Prior to this event for this event to be a match
        /// </summary>
        public const string _Prior = "Prior";
        public const string _Name = "Name";
        public const string _BodyStop = "BodyStop";
        /// <summary>
        /// Value will pull any values and add them to the event
        /// Note: Values for the found Event only
        /// </summary>
        public const string _Value = "Value";
        public const string _HideEvent = "HideEvent";
        public const string _Start = "Start";
        public const string _Body = "Body";
        public const string _Stop = "Stop";
        public const string _Reset = "Reset";
        public const string _Search = "Search";
        public const string _Restart = "Restart";
        /// <summary>
        /// Frequency applies to only a single Pattern instance.  It is used to track how often the same event is encountered within the Pattern
        /// Requires OneOrMore option so that a single Event can be found more than once.
        /// Within the Summary, count should be how many Patterns were outside of the Range
        /// </summary>
        public const string _Frequency = "Frequency";
        /// <summary>
        /// FrequencyPerFile applies to all instances of an Event within a Single file regardless if it is within one or more Patterns
        /// </summary>
        public const string _FrequencyPerFile = "FrequencyPerFile";
        #endregion Event Keywords

        static Keyword()
        {
            // Monitor Keywords include Event Option Keywords
            var z = new string[EventOptionKeywords.Length + PatternKeywords.Length];
            EventOptionKeywords.CopyTo(z, 0);
            PatternKeywords.CopyTo(z, EventOptionKeywords.Length);
            PatternKeywords = z;

            // Keywords will include MonitorKeywords
            z = new string[PatternKeywords.Length + Keywords.Length];
            PatternKeywords.CopyTo(z, 0);
            Keywords.CopyTo(z, PatternKeywords.Length);
            Keywords = z;
        }

        #region Methods
        internal static bool IsKeyword(string value)
        {
            return Keywords.Contains(value);
        }

        private static readonly string[] EventKeywords = {
            _Start, _Body, _Stop, _Reset, _Search, _Restart };
        internal static bool IsEventKeyword(string value)
        {
            return EventKeywords.Contains(value);
        }
        #endregion Methods

        #region Ignore
        public const string _class = "class";
        public const string _func = "func";
        public const string _prop = "prop";
        public const string _cotr = "cotr";
        public const string _new = "new";
        public const string _if = "if";
        public const string _else = "else";
        public const string _switch = "switch";
        public const string _case = "case";
        public const string _default = "default";
        public const string _break = "break";
        public const string _return = "return";
        public const string _do = "do";
        public const string _while = "while";
        public const string _for = "for";
        public const string _var = "var";
        public const string _null = "null";
        public const string _true = "true";
        public const string _false = "false";

        public static readonly string[] Keywords = {
            // Example
            _class, _func, _prop, _cotr, _new, _if, _else, _switch, _case, _default,
            _break, _return, _do, _while, _for, _var, _null, _true, _false };
        #endregion Ignore
    }
}
