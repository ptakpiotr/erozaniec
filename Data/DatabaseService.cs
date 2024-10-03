using ERozaniec.Models;
using SQLite;

namespace ERozaniec.Data
{
    public class DatabaseService(string path) : IDatabaseService
    {
        private SQLiteAsyncConnection _conn;
        private readonly string _dbPath = path;

        private async ValueTask InitAsync()
        {
            if (_conn is not null)
                return;

            _conn = new SQLiteAsyncConnection(_dbPath);

            await _conn.CreateTableAsync<PartModel>()
                .ConfigureAwait(false);
        }

        public async Task<IList<PartModel>?> GetPartsAsync(RosaryPart part)
        {
            await InitAsync().ConfigureAwait(false);

            return await _conn.Table<PartModel>().Where(p => p.Part == part)
                .ToListAsync().ConfigureAwait(false);
        }

        public async Task AddPartAsync(PartModel part)
        {
            await InitAsync().ConfigureAwait(false);

            await _conn.InsertAsync(part).ConfigureAwait(false);
        }

        public async Task AddManyPartsAsync(IList<PartModel> parts)
        {
            await InitAsync().ConfigureAwait(false);

            await _conn.InsertAllAsync(parts, true)
                .ConfigureAwait(false);
        }

        public async Task DeletePartAsync(int id)
        {
            await InitAsync().ConfigureAwait(false);

            await _conn.Table<PartModel>().DeleteAsync(p => p.Id == id)
                .ConfigureAwait(false);
        }

        public async Task EditPartAsync(PartModel part)
        {
            await InitAsync().ConfigureAwait(false);

            await _conn.InsertOrReplaceAsync(part)
                .ConfigureAwait(false);
        }
    }
}