using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TenderHackUI.Entities;

namespace TenderHackUI
{
    public class RestService
    {
        private static readonly string apiPath = "http://51.250.97.70/api/v1";
        public static HttpClient Client;

        public RestService()
        {
            Client = new HttpClient();
            Client.DefaultRequestHeaders.Add("Connection", "Keep-Alive");
            Client.DefaultRequestHeaders.Add("Keep-Alive", "timeout=3600");
        }
        public async Task<IEnumerable<SessionSimpleEntity>> GetAllSessions()
        {
            HttpResponseMessage response = await Client.GetAsync($"{apiPath}/sessions");
            if (response.IsSuccessStatusCode)
            {
                IEnumerable<SessionSimpleEntity> result = await response.Content.ReadFromJsonAsync<IEnumerable<SessionSimpleEntity>>();
                return result;
            }
            return new List<SessionSimpleEntity>();
        }

        public async Task<SessionEntity> GetSession(int sessionId)
        {
            HttpResponseMessage response = await Client.GetAsync($"{apiPath}/sessions/{sessionId}");
            if (response.IsSuccessStatusCode)
            {
                SessionEntity result = await response.Content.ReadFromJsonAsync<SessionEntity>();
                return result;
            }
            return null;
        }

        public async Task<IEnumerable<BetEntity>> GetBets(int sessionId)
        {
            HttpResponseMessage response = await Client.GetAsync($"{apiPath}/bets/{sessionId}");
            if (response.IsSuccessStatusCode)
            {
                IEnumerable<BetEntity> result = await response.Content.ReadFromJsonAsync<IEnumerable<BetEntity>>();
                return result;
            }
            return new List<BetEntity>();
        }

        public async Task<bool> MakeBet(int userId, int sessionId)
        {
            var entity = new
            {
                bot = false,
                provider_id = userId,
                quotation_session_id = sessionId
            };
            HttpResponseMessage response = await Client.PostAsJsonAsync(
                $"{apiPath}/bets", entity);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                return true;
            else
                return false;
        }

        public async Task<bool> StartStrategy(int userId, int sessionId, float acceptablePrice, float minimalPrice, float desirablePrice, string _strategy)
        {
            var entity = new
            {
                acceptable_price = acceptablePrice,
                minimal_price = minimalPrice,
                preferable_price = desirablePrice,
                quotation_session_id = sessionId,
                strategy = _strategy,
                user_id = userId
            };
            HttpResponseMessage response = await Client.PostAsJsonAsync(
                $"http://51.250.101.22/api/v1/strategies/run", entity);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                return true;
            else
                return false;
        }

        public async Task<bool> StopStrategy(int userId, int sessionId)
        {
            var entity = new
            {
                quotation_session_id = sessionId,
                user_id = userId
            };
            HttpResponseMessage response = await Client.PostAsJsonAsync(
                $"http://51.250.101.22/api/v1/strategies/{sessionId}/{userId}", entity);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                return true;
            else
                return false;
        }

    }
}
