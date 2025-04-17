using SD.Mini.ZooManagement.Domain.Exceptions.Animal;
using SD.Mini.ZooManagement.Domain.Models.Animal.Value.Enums;

namespace SD.Mini.ZooManagement.Domain.Models.Animal;

public record AnimalModel(
    AnimalType Type,
    DateTime Birthday,
    string Nickname,
    AnimalGender Gender,
    string FavouriteFood
)
{
    public AnimalHealthCondition HealthCondition { get; private set; }

    public AnimalModel(AnimalType type, DateTime birthday, string nickname, AnimalGender gender, string favouriteFood,
        AnimalHealthCondition healthCondition) : this(type, birthday, nickname, gender, favouriteFood)
    {
        HealthCondition = healthCondition;
    }

    public void Feed()
    {
        //Покормил, проверяй АХХАХАХАХАХ, идиотия блять
    }

    public void Heal()
    {
        if (HealthCondition == AnimalHealthCondition.Healthy)
        {
            throw new AnimalAlreadyHealthyException("Animal already healthy.");
        }

        HealthCondition = AnimalHealthCondition.Healthy;
    }
};