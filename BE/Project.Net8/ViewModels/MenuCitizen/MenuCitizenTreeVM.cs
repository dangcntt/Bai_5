using Project.Net8.Models.Major;

namespace Project.Net8.ViewModels.MenuCitizen
{
    public class MenuCitizenTreeVM
    {
     
        public MenuCitizenTreeVM(MenuCitizenModel model)
        {
            this.Id = model.Id;
            this.Name = model.Name;
            this.Label = model.Name;
            this.Link = model.Path ?? "";
            this.Icon = model.Icon ?? "";
            this.Level = model.Level ; 
            this.ParentId = model.ParentId;
        }
        public string Id { get; set; }
        public string Name { get; set; }
        
        public string Label { get; set; }

        public List<MenuCitizenTreeVM> Children { get; set; } 

        public int Level { get; set; }
        public string ParentId { get; set; }= "";
        public string Link { get; set; } = "";
        public string Icon { get; set; } = "";

    }
    public class MenuCongDanTreeVMGetAll
    {
        public MenuCongDanTreeVMGetAll(MenuCitizenModel model)
        {
            this.Id = model.Id;
            this.Name = model.Name;
            this.CapDV = model.Level;
        }
        public string Id { get; set; }
        public string Name { get; set; }
        
        
        public int CapDV { get; set; }
        
    }
    
    
    

    
}