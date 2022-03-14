using System.Net.Http;
using System.Threading.Tasks;

namespace HotelManagement_User.Services
{
    public interface IHttpService
    {
        Task<T> GetAsync<T>(string url);
        Task<T> PostAsync<T>(string url, StringContent content);
    }
}
