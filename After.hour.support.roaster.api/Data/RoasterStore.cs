using After.hour.support.roaster.api.Model.Dto;

namespace After.hour.support.roaster.api.Data
{
    public static class RoasterStore
    {
        public static List<RoasterCreateDto> roasterList = new List<RoasterCreateDto>
        {
                new RoasterCreateDto{Id=1,firstLine="Thato",secondLine="Theo",supportDueDate=DateTime.Now.Date},
                new RoasterCreateDto{Id=2,firstLine="Sbo",secondLine="Daniel",supportDueDate=DateTime.Now.Date},
                new RoasterCreateDto{Id=3,firstLine="Fried",secondLine="Mwansa",supportDueDate=DateTime.Now.Date}

        };
    }
}
