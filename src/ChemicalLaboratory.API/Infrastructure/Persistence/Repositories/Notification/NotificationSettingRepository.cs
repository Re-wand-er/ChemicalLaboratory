using ChemicalLaboratory.Domain.Entities;
using ChemicalLaboratory.Domain.Interfaces;

namespace ChemicalLaboratory.Infrastructure.Persistence.Repositories
{
    public class NotificationSettingRepository : BaseRepository<NotificationSetting>, INotificationSettingRepository
    {
        public NotificationSettingRepository(DataBaseContext dataBaseContext) : base(dataBaseContext) { }
    }
}
