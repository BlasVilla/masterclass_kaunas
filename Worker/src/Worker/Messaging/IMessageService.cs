using System.Threading.Tasks;

namespace Worker.Messaging
{
    public interface IMessageService
    {
        void Run();
    }
}