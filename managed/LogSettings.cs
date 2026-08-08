using System;

namespace vaudionativewrapper.managed
{
    /// <summary>Global logging callbacks used by this managed wrapper.</summary>
    public static class LogSettings
    {
    /// <summary>Global info log callback</summary>
        public static Action<string> Log = Console.WriteLine;
        
    /// <summary>Global warning log callback</summary>
        public static Action<string> Warn = Console.WriteLine;
        
    /// <summary>Global error log callback</summary>
        public static Action<string> Error = Console.Error.WriteLine;
    }
}
