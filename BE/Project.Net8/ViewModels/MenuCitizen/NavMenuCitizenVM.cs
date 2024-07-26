using Project.Net8.Models.Major;

namespace Project.Net8.ViewModels.MenuCitizen
{
    public class NavMenuCitizenVM
    {
        public NavMenuCitizenVM(MenuCitizenModel model)
        {
            this.Id = model.Id  ?? "";
            this.Name = model.Name  ?? "";
            this.Icon = model.Icon  ?? "";
            this.ParentId = model.ParentId ?? "";
            this.Level = model.Level ; 
            this.Link = string.IsNullOrEmpty(model.Path)? "/" : model.Path;
        }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public string ParentId { get; set; }
        public string Link { get; set; }
        public bool IsTitle { get; set; }
        public int Level { get; set; }

        public List<NavMenuCitizenVM> Children { get; set; } = new List<NavMenuCitizenVM>();
    }
}