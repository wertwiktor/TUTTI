using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Framework.ExtensionMethods
{
    public static class StringExtensions
    {
        public static string Here<T>(this T message, [CallerFilePath] string sourceFile = "", [CallerMemberName] string sourceMethod = "", [CallerLineNumber] int sourceLine = 0)
        {
            var fileName = Path.GetFileNameWithoutExtension(sourceFile);
            return $"[{fileName}.{sourceMethod}:{sourceLine}] {message}";
        }
    }
}
