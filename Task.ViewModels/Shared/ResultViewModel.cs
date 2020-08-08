using System;
using System.Collections.Generic;
using System.Text;

namespace Task.ViewModels.Shared
{
    public  class ResultViewModel
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public  object Data { get; set; }        
        public IEnumerable<string> Errors { get; set; }
    }
}
