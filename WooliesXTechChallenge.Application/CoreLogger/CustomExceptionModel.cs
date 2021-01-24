using System;
using System.Collections.Generic;
using System.Text;

namespace WooliesXTechChallenge.Application.CoreLogger
{
    public class CustomExceptionModel
    {
        public string ExceptionName { get; set; }

        public string ModuleName { get; set; }

        public string Message { get; set; }

        public List<DicEntry> Data { get; set; }
    }

    public class DicEntry
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
