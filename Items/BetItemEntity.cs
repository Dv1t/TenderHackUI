using TenderHackUI.Entities;

namespace TenderHackUI.Items
{
    public class BetItemEntity
    {
        private BetEntity _entity;
        public BetItemEntity(BetEntity entity)
        {
            _entity = entity;
            BetTime = entity.time.ToString("dd.MM.yyyy HH:mm:ss.FF");
        }
        public string Price => $"{_entity.new_price} руб.";
        public bool IsBot => _entity.bot;
        public string Number => $"№{_entity.bet_number}";
        public string BetTime { get; set; }
    }
}
