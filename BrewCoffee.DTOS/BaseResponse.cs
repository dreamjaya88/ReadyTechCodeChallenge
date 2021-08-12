using System;

namespace BrewCoffee.DTOS
{
    public class BaseResponse <T>
    {
        public string Status { get; set; }
        public T Data { get; set; }
    }
}

