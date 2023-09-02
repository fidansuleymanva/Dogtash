using Fundamental.Domain.Entities;

namespace Dogtas.MVC.ViewModels
{
    public class HomeViewModel
    {
        public List<Slider> Sliders { get; set; }
        public List<Category> Categories { get; set; }
        public List<MenuSlider> MenuSliders { get; set; }
    }
}
