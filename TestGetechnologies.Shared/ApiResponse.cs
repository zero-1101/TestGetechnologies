﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestGetechnologies.Shared
{
    internal class ApiResponse<T>
    {
        public bool IsSuccess { get; set; } = false;
        public string Message { get; set; }
        public T? Value { get; set; }
    }
}
