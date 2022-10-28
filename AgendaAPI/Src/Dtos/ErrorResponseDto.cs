using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaAPI.Src.Dtos
{
    public class ErrorResponseDto
    {
        public string Error { get; set; }
        public int Status { get; set; }
    }
}
