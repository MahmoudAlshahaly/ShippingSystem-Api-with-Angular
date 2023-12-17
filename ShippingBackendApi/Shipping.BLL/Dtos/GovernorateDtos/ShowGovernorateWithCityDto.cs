namespace Shipping.BLL.Dtos
{
    public class ShowGovernorateWithCityDto

    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<ShowCityForDropdownDto> Cities { get; set; }
    }
}
