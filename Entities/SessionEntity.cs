using System;
using System.Collections.Generic;

namespace TenderHackUI.Entities
{
    public class SessionEntity
    {
        public class Creator
        {
            public int organizationId { get; set; }
            public string organizationName { get; set; }
            public string organizationType { get; set; }
        }

        public Creator creator { get; set; }
        public int creator_id { get; set; }
        public float current_price { get; set; }
        public int id { get; set; }
        public bool is_in_additional_purchase { get; set; }
        public BetEntity last_bet { get; set; }
        public int last_bet_id { get; set; }
        public string name { get; set; }
        public int session_duration { get; set; }
        public float session_step_percent { get; set; }
        public float start_price { get; set; }
        public DateTime start_time { get; set; }
        public string status { get; set; }

        public class Category
        {
            public string description { get; set; }
            public int id { get; set; }
            public string name { get; set; }
        }

        public class Product2
        {
            public Category category { get; set; }
            public int category_id { get; set; }
            public string description { get; set; }
            public int id { get; set; }
            public string name { get; set; }
        }

        public class Product
        {
            public int count { get; set; }
            public Product2 product { get; set; }
            public int product_id { get; set; }
            public int record_id { get; set; }
            public int session_id { get; set; }
        }
        public List<Product> products { get; set; }

    }
}
