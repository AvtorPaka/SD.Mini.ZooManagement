using SD.Mini.ZooManagement.Application.Contracts.Dal.Entities;

namespace SD.Mini.ZooManagement.Infrastructure.Dal.Infrastructure;

public class InMemoryStorage
{
    public List<AnimalEntity> AnimalEntities { get; } = [];
    public List<EnclosureEntity> EnclosureEntities { get; } = [];
    public List<FeedingScheduleEntity> FeedingScheduleEntities { get; } = [];
}