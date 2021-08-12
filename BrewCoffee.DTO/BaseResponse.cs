using System;

namespace BrewCoffee.DTO
{
    public class BaseResponse <T>
    {
        public string Status { get; set; }
        public T Data { get; set; }
    }
}

