﻿namespace CasgemMicroservice.Basket.Dto
{
    public class BasketItemDto
    {
        public int Quantity { get; set; }
        public string ProductID { get; set; }
        public string ProductName { get; set; }
        public Decimal Price { get; set; }
    }
}
