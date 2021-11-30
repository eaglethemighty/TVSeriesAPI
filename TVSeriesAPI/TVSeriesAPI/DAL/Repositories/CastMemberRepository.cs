using Microsoft.EntityFrameworkCore;
using TVSeriesAPI.DAL.Repositories.Interfaces;
using TVSeriesAPI.Models.Entities;

namespace TVSeriesAPI.DAL.Repositories
{
    public class CastMemberRepository : ICastMemberRepository
    {
        public CastMemberRepository(TVSeriesDbContext context)
        {
            _Context = context;
        }
        private TVSeriesDbContext _Context { get; init; }

        public async Task AddAsync(CastMember obj)
        {
            await _Context.CastMembers.AddAsync(obj);
        }

        public async Task DeleteAsync(CastMember obj)
        {
            await Task.Run( () => _Context.CastMembers.Remove(obj) );
        }

        public async Task<ICollection<CastMember>> GetAll()
        {
            return await _Context.CastMembers.ToListAsync();
        }

        public async Task<CastMember> GetById(int id)
        {
            return  await _Context.CastMembers.FindAsync(id);
        }

        public async Task<bool> SaveChanges()
        {
            return await _Context.SaveChangesAsync() != 0;
        }

        public async Task UpdateAsync(CastMember obj)
        {
            await Task.Run(() => _Context.CastMembers.Update(obj));
        }
    }
}
