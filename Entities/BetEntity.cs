using System;

namespace TenderHackUI.Entities
{
    public class BetEntity
    {
        public int bet_number { get; set; }
        public bool bot { get; set; }
        public int id { get; set; }
        public float new_price { get; set; }
        public int provider_id { get; set; }
        public int quotation_session_id { get; set; }
        public DateTime time { get; set; }
    }
}
