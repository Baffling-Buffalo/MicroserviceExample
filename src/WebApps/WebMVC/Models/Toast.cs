using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.Models
{
    public class Toast
    {
        public Toast()
        {
        }

        public Toast(ToastType type, string message)
        {
            Type = type.ToString();
            Message = message;
        }

        public string Type { get; set; }
        public string Message { get; set; }
    }

    public enum ToastType
    {
        Success,
        Info,
        Error,
        Warning
    }
}
