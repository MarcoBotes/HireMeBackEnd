using System.Threading.Channels;

namespace HireMeBackEnd.Services.ProductConsolidator
{
    public class ChannelHandler
    {
        private readonly Dictionary<Type, object> _channels = new();
        public void RegisterChannel<TEntity>(Channel<TEntity> channel)
        {
            if (!_channels.ContainsKey(typeof(TEntity)))
            {
                _channels.Add(typeof(TEntity), channel);
            }
            else
            {
                throw new InvalidOperationException($"Channel for type {typeof(TEntity)} is already registered.");
            }
        }

        public Channel<TEntity> GetChannel<TEntity>()
        {
            if (_channels.TryGetValue(typeof(TEntity), out var channel))
            {
                return (Channel<TEntity>)channel;
            }
            else
            {
                throw new KeyNotFoundException($"No channel registered for type {typeof(TEntity)}.");
            }
        }
    }
}
